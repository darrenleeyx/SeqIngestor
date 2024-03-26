using FluentValidation;

namespace Contract.Incoming.Seq.CreateIngestRequest;

public class CreateIngestRequestValidator : AbstractValidator<CreateIngestRequest>
{
    public CreateIngestRequestValidator()
    {
        RuleFor(x => x.File).NotEmpty();
    }
}