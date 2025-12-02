using ErrorOr;
using GtAcademy.Application.Authentication.Common;
using GtAcademy.Application.Common.Interfaces;
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

        private readonly IGenericService<User> _userGenericService;

        private readonly IUnitOfWork _unitOfWork;

        public VerifyPhoneNumberCommandHandler(IUserService userService, IGenericService<User> userGenericService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _userGenericService = userGenericService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(VerifyPhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByPhoneNumber(request.PhoneNumber);

            if (user == null)
            {
                return Error.NotFound(code: "PhoneNumber");
            }

            if (user.VerifyToken != request.Code)
            {
                return Error.Validation(code: "Code", description: "کد وارد شده نامعتبر است");
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
