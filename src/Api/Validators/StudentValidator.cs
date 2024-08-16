using FluentValidation;

namespace Api.Validators
{
    public class StudentValidator : AbstractValidator<Models.Student>
    {
        public StudentValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(0, 200);
            RuleFor(a => a.Addresses).NotNull().SetValidator(new AddressesValidator());

            RuleSet("Email", () =>
            {
                RuleFor(x => x.Email).NotEmpty().Length(0, 150).EmailAddress();
            });
        }
    }
}
