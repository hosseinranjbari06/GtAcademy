using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Episodes.Commands.CreateEpisode
{
    public class CreateEpisodeCommandValidator : AbstractValidator<CreateEpisodeDto>
    {
        public CreateEpisodeCommandValidator()
        {
            RuleFor(e => e.Title).NotEmpty().MaximumLength(40);

            RuleFor(e => e.TopicId).NotEmpty();

            RuleFor(e => e.Time).NotEmpty();

            RuleFor(e => e.FileName).NotEmpty().MaximumLength(200);
        }
    }
}
