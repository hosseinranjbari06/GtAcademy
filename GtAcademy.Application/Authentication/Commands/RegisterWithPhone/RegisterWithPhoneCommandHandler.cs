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
        private readonly IValidator<RegisterWithPhoneCommand> _validator;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IGenericService<User> _userGenericService;

        private readonly IUserService _userService;

        private readonly ICodeGenerator _codeGenerator;

        public RegisterWithPhoneCommandHandler(IValidator<RegisterWithPhoneCommand> validator, IUnitOfWork unitOfWork, IGenericService<User> userGenericService, ICodeGenerator codeGenerator, IUserService userService)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _userGenericService = userGenericService;
            _codeGenerator = codeGenerator;
            _userService = userService;
        }

        public async Task<ErrorOr<string>> Handle(RegisterWithPhoneCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            if (_userService.ExistByUserName(request.UserName))
            {
                return Error.Validation(code: "UserName", description: "نام کاربری وارد شده قبلا استفاده شده است");
            }

            if (_userService.ExistByPhoneNumber(request.PhoneNumber))
            {
                return Error.Validation(code: "PhoneNumber", description: "با شماره موبایل وارد شده قبلا در سایت ثبت نام شده است");
            }

            User user = new User()
            {
                UserId = Guid.NewGuid(),
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                RegisterDate = DateTime.Now,
                IsActive = false,
                VerifyToken = _codeGenerator.GenerateFiveDigitCode()
            };

            //Send SMS

            await _userGenericService.AddAsync(user);
            await _unitOfWork.CommitAsync();

            return user.PhoneNumber;
        }
    }
}
