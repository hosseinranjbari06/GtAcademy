using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Episodes.Commands.EditEpisode
{
    public class EditEpisodeCommandValidator : AbstractValidator<EditEpisodeDto>
    {
        public EditEpisodeCommandValidator()
        {
            RuleFor(e => e.Title).NotEmpty().MaximumLength(40);

            RuleFor(e => e.TopicId).NotEmpty();

            RuleFor(e => e.EpisodeId).NotEmpty();

            RuleFor(e => e.Time).NotEmpty();

            RuleFor(e => e.FileName).NotEmpty().MaximumLength(200);
        }
    }
}
