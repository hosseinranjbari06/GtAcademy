using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Wallets.Queries.GetUsersWalletBalance
{
    public class GetUsersWalletBalanceQueryHandler : IRequestHandler<GetUsersWalletBalanceQuery, ErrorOr<int>>
    {
        private readonly IWalletService _walletService;

        public GetUsersWalletBalanceQueryHandler(IWalletService walletService)
        {
            _walletService = walletService;
        }

        public async Task<ErrorOr<int>> Handle(GetUsersWalletBalanceQuery request, CancellationToken cancellationToken)
        {
            var walletId = await _walletService.GetWalletIdByUserId(request.UserId);

            if (walletId == null) return Error.NotFound();

            var walletBalance = await _walletService.GetWalletBalance((Guid)walletId);
            return walletBalance;
        }
    }
}
