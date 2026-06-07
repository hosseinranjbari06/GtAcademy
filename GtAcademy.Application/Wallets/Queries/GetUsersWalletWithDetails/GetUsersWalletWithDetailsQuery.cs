using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Wallets.Queries.GetUsersWalletWithDetails
{
    public record GetUsersWalletWithDetailsQuery(Guid UserId) : IRequest<ErrorOr<WalletDto>>;
}
