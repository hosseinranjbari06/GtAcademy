using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Tools.RandomCodeGenerator;
using GtAcademy.Application.Tools.SmsSender;
using GtAcademy.Domain.Referral;
using GtAcademy.Domain.Users;
using GtAcademy.Domain.Wallets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Authentication.Commands.RegisterWithPhone
{
    public class RegisterWithPhoneCommandHandler : IRequestHandler<RegisterWithPhoneCommand, ErrorOr<string>>
    {
        private readonly IValidator<RegisterWithPhoneDto> _validator;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IGenericService<User> _userGenericService;

        private readonly IGenericService<Referral> _referralGenericService;

        private readonly IGenericService<Wallet> _walletGenericService;

        private readonly IUserService _userService;

        private readonly ICodeGenerator _codeGenerator;

        private readonly ISmsSender _smsSender;

        public RegisterWithPhoneCommandHandler(IValidator<RegisterWithPhoneDto> validator, IUnitOfWork unitOfWork, IGenericService<User> userGenericService, ICodeGenerator codeGenerator, IUserService userService, IGenericService<Wallet> walletGenericService, IGenericService<Referral> referralGenericService, ISmsSender smsSender)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _userGenericService = userGenericService;
            _codeGenerator = codeGenerator;
            _userService = userService;
            _walletGenericService = walletGenericService;
            _referralGenericService = referralGenericService;
            _smsSender = smsSender;
        }

        public async Task<ErrorOr<string>> Handle(RegisterWithPhoneCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.RegisterDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            if (await _userService.ExistByUserName(request.RegisterDto.UserName))
            {
                return Error.Validation(code: "UserName", description: "نام کاربری وارد شده قبلا استفاده شده است");
            }

            if (await _userService.ExistByPhoneNumberIncludeDeletedUsers(request.RegisterDto.PhoneNumber))
            {
                return Error.Validation(code: "PhoneNumber", description: "با شماره موبایل وارد شده قبلا در سایت ثبت شده است");
            }

            User user = new User()
            {
                UserId = Guid.NewGuid(),
                UserName = request.RegisterDto.UserName,
                PhoneNumber = request.RegisterDto.PhoneNumber,
                AvatarName = "default.jpg",
                RegisterDate = DateTime.Now,
                IsActive = false,
                VerifyToken = _codeGenerator.GenerateFiveDigitCode(),
                ReferralCode = _codeGenerator.GenerateReferralCode()
            };

            Wallet wallet = new Wallet()
            {
                WalletId = Guid.NewGuid(),
                WalletBalance = 0,
                UserId = user.UserId,
                User = user
            };

            await _userGenericService.AddAsync(user);
            await _walletGenericService.AddAsync(wallet);

            if (!string.IsNullOrEmpty(request.RegisterDto.ReferrerCode))
            {
                var referrerUser = await _userService.GetUserByReferralCode(request.RegisterDto.ReferrerCode);

                if (referrerUser == null) return Error.Validation("ReferrerCode", "کد معرف استفاده شده نامعتبر است");

                if (!referrerUser.IsActive) return Error.Failure();

                var referral = new Referral()
                {
                    ReferralId = Guid.NewGuid(),
                    CreateDate = DateTime.Now,
                    ReferredId = user.UserId,
                    ReferrerId = referrerUser.UserId,
                    IsVerified = false,
                    Referred = user,
                    Referrer = referrerUser
                };

                user.ReferralId = referral.ReferralId;
                user.ReferralReceived = referral;
                //referrerUser.ReferralsSent.Add(referral);

                await _referralGenericService.AddAsync(referral);
            }

            //Send SMS
            var result = await _smsSender.SendVerificationCode(user.PhoneNumber, user.VerifyToken);

            if (result.IsError || !result.Value) return Error.Failure("PhoneNumber", "سرویس ارسال پیامک با مشکل مواجه شد. لطفا مجددا تلاش کنید.");

            await _unitOfWork.CommitAsync();

            return user.PhoneNumber;
        }
    }
}
