using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Referral;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.ReferralOptions.Commands.EditRewardPercent
{
    public class EditRewardPercentCommandHandler : IRequestHandler<EditRewardPercentCommand, ErrorOr<bool>>
    {
        private readonly IGenericService<Domain.Referral.ReferralOptions> _genericOptionsService;

        private readonly IUnitOfWork _unitOfWork;

        public EditRewardPercentCommandHandler(IGenericService<Domain.Referral.ReferralOptions> genericService, IUnitOfWork unitOfWork)
        {
            _genericOptionsService = genericService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(EditRewardPercentCommand request, CancellationToken cancellationToken)
        {
            if (request.RewardPercent == null || request.RewardPercent > 100 || request.RewardPercent <= 0)
            {
                return Error.Validation("All", "مقدار وارد شده نامعتبر است");
            }

            var referralOption = await _genericOptionsService.GetByIdAsync(1);

            referralOption!.RewardPercent = request.RewardPercent;

            _genericOptionsService.Update(referralOption);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
