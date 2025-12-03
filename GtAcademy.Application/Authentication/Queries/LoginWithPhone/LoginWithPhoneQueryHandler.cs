using ErrorOr;
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

        private readonly LoginWithPhoneQueryValidation _validator;

        private readonly ICodeGenerator _codeGenerator;

        public LoginWithPhoneQueryHandler(LoginWithPhoneQueryValidation validator, IUserService userService, ICodeGenerator codeGenerator)
        {
            _validator = validator;
            _userService = userService;
            _codeGenerator = codeGenerator;
        }

        public async Task<ErrorOr<string>> Handle(LoginWithPhoneQuery request, CancellationToken cancellationToken)
        {
            if (!await _userService.ExistByPhoneNumber(request.PhoneNumber))
                return Error.NotFound(code: "PhoneNumber", description: "کاربری با شماره موبایل وارد شده یافت نشد");

            var user = await _userService.GetUserByPhoneNumber(request.PhoneNumber);
            user!.VerifyToken = _codeGenerator.GenerateFiveDigitCode();

            return user.UserName;
        }
    }
}
