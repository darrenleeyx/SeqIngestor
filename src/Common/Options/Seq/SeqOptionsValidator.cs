using FluentValidation;

namespace Common.Options.Seq;

public class SeqOptionsValidator : AbstractValidator<SeqOptions>
{
    public SeqOptionsValidator()
    {
        RuleFor(x => x.ServerUrl).NotEmpty();
        RuleFor(x => x.ApiKey).NotEmpty();
    }
}