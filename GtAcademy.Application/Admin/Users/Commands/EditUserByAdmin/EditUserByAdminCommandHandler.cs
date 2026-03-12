using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Roles;
using GtAcademy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Commands.EditUserByAdmin
{
    public class EditUserByAdminCommandHandler : IRequestHandler<EditUserByAdminCommand, ErrorOr<Guid>>
    {
        private readonly IValidator<EditUserDto> _validator;

        private readonly IUserService _userService;

        private readonly IGenericService<User> _genericUserService;

        private readonly IGenericService<Role> _genericRoleService;

        private readonly IUnitOfWork _unitOfWork;

        public EditUserByAdminCommandHandler(IValidator<EditUserDto> validator, IUserService userService, IGenericService<User> genericUserService, IUnitOfWork unitOfWork, IGenericService<Role> genericRoleService)
        {
            _validator = validator;
            _userService = userService;
            _genericUserService = genericUserService;
            _unitOfWork = unitOfWork;
            _genericRoleService = genericRoleService;
        }

        public async Task<ErrorOr<Guid>> Handle(EditUserByAdminCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.UserDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            var user = await _userService.GetUserById(request.UserDto.UserId);

            if (user == null) return Error.NotFound();

            if (request.UserDto.UserName != user.UserName && await _userService.ExistByUserName(request.UserDto.UserName))
            {
                return Error.Validation(code: "UserName", description: "نام کاربری وارد شده قبلا استفاده شده است");
            }

            if (request.UserDto.EmailAddress != user.EmailAddress && await _userService.ExistByEmail(request.UserDto.EmailAddress))
            {
                return Error.Validation(code: "EmailAddress", description: "ایمیل وارد شده قبلا استفاده شده است");
            }

            if (request.UserDto.PhoneNumber != user.PhoneNumber && await _userService.ExistByPhoneNumber(request.UserDto.PhoneNumber!))
            {
                return Error.Validation(code: "PhoneNumber", description: "شماره موبایل وارد شده قبلا استفاده شده است");
            }

            if (request.UserDto.ReferralCode != user.ReferralCode && await _userService.ExistByReferralCode(request.UserDto.ReferralCode))
            {
                return Error.Validation(code: "ReferralCode", description: "کد معرف وارد شده قبلا استفاده شده است");
            }

            user.Roles.Clear();

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

            user.UserName = request.UserDto.UserName;
            user.EmailAddress = request.UserDto.EmailAddress;
            user.PhoneNumber = request.UserDto.PhoneNumber;
            user.HomeAddress = request.UserDto.HomeAddress;
            user.Job = request.UserDto.Job;
            user.Biography = request.UserDto.Biography;
            if (request.UserDto.DeleteAvatar)
            {
                user.AvatarName = "default.jpg";
            }
            if (request.UserDto.BirthDate != null)
            {
                user.BirthDate = request.UserDto.BirthDate;
            }
            user.ReferralCode = request.UserDto.ReferralCode;

            _genericUserService.Update(user);
            await _unitOfWork.CommitAsync();

            return user.UserId;
        }
    }
}
