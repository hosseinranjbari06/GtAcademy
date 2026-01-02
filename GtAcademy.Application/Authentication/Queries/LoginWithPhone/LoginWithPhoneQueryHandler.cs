using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Authentication.Common;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Tools.RandomCodeGenerator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Authentication.Queries.LoginWithPhone
{
    public class LoginWithPhoneQueryHandler : IRequestHandler<LoginWithPhoneQuery, ErrorOr<string>>
    {
        private readonly IUserService _userService;

        private readonly IValidator<LoginWithPhoneDto> _validator;

        private readonly ICodeGenerator _codeGenerator;

        public LoginWithPhoneQueryHandler(IValidator<LoginWithPhoneDto> validator, IUserService userService, ICodeGenerator codeGenerator)
        {
            _validator = validator;
            _userService = userService;
            _codeGenerator = codeGenerator;
        }

        public async Task<ErrorOr<string>> Handle(LoginWithPhoneQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.LoginDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            if (!await _userService.ExistByPhoneNumber(request.LoginDto.PhoneNumber))
                return Error.NotFound(code: "PhoneNumber", description: "کاربری با شماره موبایل وارد شده یافت نشد");

            var user = await _userService.GetUserByPhoneNumber(request.LoginDto.PhoneNumber);
            user!.VerifyToken = _codeGenerator.GenerateFiveDigitCode();

            return user.UserName;
        }
    }
}
