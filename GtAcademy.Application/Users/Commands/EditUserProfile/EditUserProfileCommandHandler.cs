using AutoMapper;
using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Users.Common;
using GtAcademy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Commands.EditUserProfile
{
    public class EditUserProfileCommandHandler : IRequestHandler<EditUserProfileCommand, ErrorOr<bool>>
    {
        private readonly IGenericService<User> _genericUserService;

        private readonly IUserService _userService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<EditUserProfileDto> _validator;

        private readonly IMapper _mapper;

        public EditUserProfileCommandHandler(IGenericService<User> genericUserService, IMapper mapper, IUnitOfWork unitOfWork, IValidator<EditUserProfileDto> validator, IUserService userService)
        {
            _genericUserService = genericUserService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _userService = userService;
        }

        public async Task<ErrorOr<bool>> Handle(EditUserProfileCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.ProfileDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            var user = await _userService.GetUserById(request.ProfileDto.UserId);

            if (user == null) return Error.NotFound();

            if (request.ProfileDto.UserName != user.UserName && await _userService.ExistByUserName(request.ProfileDto.UserName))
            {
                return Error.Validation(code: "UserName", description: "نام کاربری وارد شده قبلا استفاده شده است");
            }

            if (!string.IsNullOrEmpty(request.ProfileDto.AvatarName))
                user.AvatarName = request.ProfileDto.AvatarName;

            if (request.ProfileDto.BirthDate != null)
                user.BirthDate = request.ProfileDto.BirthDate;

            user.UserName = request.ProfileDto.UserName;
            user.Job = request.ProfileDto.Job;
            user.Biography = request.ProfileDto.Biography;
            user.HomeAddress = request.ProfileDto.HomeAddress;

            _genericUserService.Update(user);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
