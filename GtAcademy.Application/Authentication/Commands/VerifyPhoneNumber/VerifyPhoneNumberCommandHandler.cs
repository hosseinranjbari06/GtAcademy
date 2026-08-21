using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Authentication.Common;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Referral;
using GtAcademy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Authentication.Commands.VerifyPhoneNumber
{
    public class VerifyPhoneNumberCommandHandler : IRequestHandler<VerifyPhoneNumberCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IUserService _userService;

        private readonly IReferralService _referralService;

        private readonly IGenericService<User> _userGenericService;

        private readonly IGenericService<Referral> _referralGenericService;

        private readonly IValidator<VerifyPhoneNumberDto> _validator;

        private readonly IUnitOfWork _unitOfWork;

        public VerifyPhoneNumberCommandHandler(IUserService userService, IGenericService<User> userGenericService, IUnitOfWork unitOfWork, IReferralService referralService, IGenericService<Referral> referralGenericService, IValidator<VerifyPhoneNumberDto> validator)
        {
            _userService = userService;
            _userGenericService = userGenericService;
            _unitOfWork = unitOfWork;
            _referralService = referralService;
            _referralGenericService = referralGenericService;
            _validator = validator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(VerifyPhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.VerifyDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            var user = await _userService.GetUserByPhoneNumber(request.VerifyDto.PhoneNumber);

            if (user == null)
            {
                return Error.NotFound(code: "PhoneNumber");
            }

            if (user.VerifyToken != request.VerifyDto.Code)
            {
                return Error.Validation(code: "Code", description: "کد وارد شده نامعتبر است");
            }

            var referral = await _referralService.GetReferralByReferredId(user.UserId);

            if (referral != null && !referral.IsVerified)
            {
                referral.IsVerified = true;
                _referralGenericService.Update(referral);
            }

            user.IsActive = true;
            user.VerifyToken = string.Empty;

            _userGenericService.Update(user);
            await _unitOfWork.CommitAsync();

            return new AuthenticationResult()
            {
                UserId = user.UserId,
                PhoneNumber = user.PhoneNumber!,
                UserName = user.UserName
            };
        }
    }
}
