using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Tools.RandomCodeGenerator;
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

        private readonly IGenericService<Wallet> _walletGenericService;

        private readonly IUserService _userService;

        private readonly ICodeGenerator _codeGenerator;

        public RegisterWithPhoneCommandHandler(IValidator<RegisterWithPhoneDto> validator, IUnitOfWork unitOfWork, IGenericService<User> userGenericService, ICodeGenerator codeGenerator, IUserService userService, IGenericService<Wallet> walletGenericService)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _userGenericService = userGenericService;
            _codeGenerator = codeGenerator;
            _userService = userService;
            _walletGenericService = walletGenericService;
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

            if (await _userService.ExistByPhoneNumber(request.RegisterDto.PhoneNumber))
            {
                return Error.Validation(code: "PhoneNumber", description: "با شماره موبایل وارد شده قبلا در سایت ثبت شده است");
            }

            User user = new User()
            {
                UserId = Guid.NewGuid(),
                UserName = request.RegisterDto.UserName,
                PhoneNumber = request.RegisterDto.PhoneNumber,
                RegisterDate = DateTime.Now,
                IsActive = false,
                VerifyToken = _codeGenerator.GenerateFiveDigitCode()
            };

            Wallet wallet = new Wallet()
            {
                WalletId = Guid.NewGuid(),
                WalletBalance = 0,
                UserId = user.UserId
            };

            //Send SMS

            await _userGenericService.AddAsync(user);
            await _walletGenericService.AddAsync(wallet);
            await _unitOfWork.CommitAsync();

            return user.PhoneNumber;
        }
    }
}
