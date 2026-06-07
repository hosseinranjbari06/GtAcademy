using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Wallets.Queries.GetUsersWalletWithDetails
{
    public class GetUsersWalletWithDetailsQueryHandler : IRequestHandler<GetUsersWalletWithDetailsQuery, ErrorOr<WalletDto>>
    {
        private readonly IWalletService _walletService;

        private readonly IMapper _mapper;

        public GetUsersWalletWithDetailsQueryHandler(IWalletService walletService, IMapper mapper)
        {
            _walletService = walletService;
            _mapper = mapper;
        }

        public async Task<ErrorOr<WalletDto>> Handle(GetUsersWalletWithDetailsQuery request, CancellationToken cancellationToken)
        {
            var wallet = await _walletService.GetUsersWalletWithDetails(request.UserId);

            if (wallet == null) return Error.NotFound();

            wallet.WalletIncomes = wallet.WalletIncomes
                .Where(income => income.IsPaid && !income.IsReferralReward)
                .OrderByDescending(income => income.IncomeDate).ToList();

            return _mapper.Map<WalletDto>(wallet);
        }
    }
}
