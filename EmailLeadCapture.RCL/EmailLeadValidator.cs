using EmailLeadCapture.Shared;
using FluentValidation;
using FluentValidation.Validators;

namespace EmailLeadCapture.RCL;

internal class EmailLeadValidator : AbstractValidator<EmailLead>
{
    public EmailLeadValidator()
    {
        RuleFor(model => model.Email)
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("Email does not appear valid");
    }
}
