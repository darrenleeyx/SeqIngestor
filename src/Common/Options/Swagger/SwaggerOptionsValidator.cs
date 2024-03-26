using FluentValidation;

namespace Common.Options.Swagger;

public class SwaggerOptionsValidator : AbstractValidator<SwaggerOptions>
{
    public SwaggerOptionsValidator()
    {
        RuleFor(x => x.DevelopmentUrl)
            .NotEmpty()
            .When(x => string.IsNullOrEmpty(x.ProductionUrl))
            .WithMessage("At least one of DevelopmentUrl or ProductionUrl must be provided.");

        RuleFor(x => x.ProductionUrl)
            .NotEmpty()
            .When(x => string.IsNullOrEmpty(x.DevelopmentUrl))
            .WithMessage("At least one of DevelopmentUrl or ProductionUrl must be provided.");
    }
}