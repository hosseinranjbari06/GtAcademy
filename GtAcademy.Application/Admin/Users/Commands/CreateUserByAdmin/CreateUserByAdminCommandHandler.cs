using AutoMapper;
using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Tools.RandomCodeGenerator;
using GtAcademy.Domain.Roles;
using GtAcademy.Domain.Users;
using GtAcademy.Domain.Wallets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Commands.CreateUserByAdmin
{
    public class CreateUserByAdminCommandHandler : IRequestHandler<CreateUserByAdminCommand, ErrorOr<Guid>>
    {
        private readonly IGenericService<User> _genericUserService;

        private readonly IGenericService<Wallet> _genericWalletService;

        private readonly IGenericService<Role> _genericRoleService;

        private readonly IUserService _userService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<CreateUserDto> _validator;

        private readonly IMapper _mapper;

        private readonly ICodeGenerator _codeGenerator;

        public CreateUserByAdminCommandHandler(IUnitOfWork unitOfWork, IGenericService<User> genericUserService, IUserService userService, IValidator<CreateUserDto> validator, IMapper mapper, IGenericService<Role> genericRoleService, ICodeGenerator codeGenerator, IGenericService<Wallet> genericWalletService)
        {
            _unitOfWork = unitOfWork;
            _genericUserService = genericUserService;
            _userService = userService;
            _validator = validator;
            _mapper = mapper;
            _genericRoleService = genericRoleService;
            _codeGenerator = codeGenerator;
            _genericWalletService = genericWalletService;
        }

        public async Task<ErrorOr<Guid>> Handle(CreateUserByAdminCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.UserDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            if (await _userService.ExistByUserName(request.UserDto.UserName))
            {
                return Error.Validation(code: "UserName", description: "نام کاربری وارد شده قبلا استفاده شده است");
            }

            if (!string.IsNullOrEmpty(request.UserDto.EmailAddress) && await _userService.ExistByEmail(request.UserDto.EmailAddress))
            {
                return Error.Validation(code: "EmailAddress", description: "ایمیل وارد شده قبلا استفاده شده است");
            }

            if (!string.IsNullOrEmpty(request.UserDto.PhoneNumber) && await _userService.ExistByPhoneNumber(request.UserDto.PhoneNumber))
            {
                return Error.Validation(code: "PhoneNumber", description: "شماره موبایل وارد شده قبلا استفاده شده است");
            }

            var user = _mapper.Map<User>(request.UserDto);
            user.Roles = new List<Role>();

            foreach (int roleId in request.UserDto.RoleIds)
            {
                if (roleId > 0)
                {
                    var role = await _genericRoleService.GetByIdAsync(roleId);

                    if (role == null)
                    {
                        return Error.Validation(code: "RoleIds", description: "نقش انتخاب شده نامعتبر است");
                    }

                    user.Roles.Add(role);
                }
            }

            user.UserId = Guid.NewGuid();
            user.AvatarName = "default.jpg";
            user.IsActive = true;
            user.RegisterDate = DateTime.Now;
            user.ReferralCode = _codeGenerator.GenerateReferralCode();

            Wallet wallet = new Wallet()
            {
                WalletId = Guid.NewGuid(),
                WalletBalance = 0,
                UserId = user.UserId
            };

            await _genericUserService.AddAsync(user);
            await _genericWalletService.AddAsync(wallet);

            await _unitOfWork.CommitAsync();

            return user.UserId;
        }
    }
}
