using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Wallets.Queries.GetUsersWalletBalance
{
    public record GetUsersWalletBalanceQuery(Guid UserId) : IRequest<ErrorOr<int>>;
}
