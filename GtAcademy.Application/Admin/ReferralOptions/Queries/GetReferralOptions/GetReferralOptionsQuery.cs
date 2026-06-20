using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.ReferralOptions.Queries.GetReferralOptions
{
    public record GetReferralOptionsQuery() : IRequest<float>;
}
