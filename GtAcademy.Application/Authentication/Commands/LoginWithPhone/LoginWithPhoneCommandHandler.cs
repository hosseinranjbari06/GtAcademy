using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Authentication.Common;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Tools.RandomCodeGenerator;
using GtAcademy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Authentication.Commands.LoginWithPhone
{
    public class LoginWithPhoneCommandHandler : IRequestHandler<LoginWithPhoneCommand, ErrorOr<string>>
    {
        private readonly IUserService _userService;

        private readonly IGenericService<User> _genericUserService;

        private readonly IValidator<LoginWithPhoneDto> _validator;

        private readonly ICodeGenerator _codeGenerator;

        private readonly IUnitOfWork _unitOfWork;

        public LoginWithPhoneCommandHandler(IValidator<LoginWithPhoneDto> validator, IUserService userService, ICodeGenerator codeGenerator, IGenericService<User> genericUserService, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _userService = userService;
            _codeGenerator = codeGenerator;
            _genericUserService = genericUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<string>> Handle(LoginWithPhoneCommand request, CancellationToken cancellationToken)
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

            _genericUserService.Update(user);
            await _unitOfWork.CommitAsync();

            return user.UserName;
        }
    }
}
