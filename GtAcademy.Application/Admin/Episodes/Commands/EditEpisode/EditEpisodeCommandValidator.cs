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
            RuleFor(e => e.Title)
            .NotEmpty()
            .WithName("عنوان")
            .WithMessage("لطفا {PropertyName} را وارد کنید")
            .MaximumLength(40)
            .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد");

            RuleFor(e => e.TopicId)
            .NotEmpty();

            RuleFor(e => e.EpisodeId)
            .NotEmpty();

            RuleFor(e => e.Time)
            .NotEmpty()
            .WithName("مدت زمان")
            .WithMessage("لطفا {PropertyName} را وارد کنید");

            RuleFor(e => e.FileName)
            .NotEmpty()
            .WithName("فایل اپیزود")
            .WithMessage("لطفا {PropertyName} را وارد کنید")
            .MaximumLength(200)
            .WithMessage("اسم فایل وارد شده نمی تواند بیشتر از 200 کاراکتر داشته باشد");
        }
    }
}
