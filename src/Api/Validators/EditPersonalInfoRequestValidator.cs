using Api.Models;
using FluentValidation;

namespace Api.Validators
{
    public class EditPersonalInfoRequestValidator : AbstractValidator<EditPersonalInfoRequest>
    {
        public EditPersonalInfoRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(0, 200);

            //RuleFor(x => x.Address).NotNull()
            //    .SetValidator(new AddressValidator());

            RuleFor(a => a.Addresses).NotNull().SetValidator(new AddressesValidator());
        }
    }
}
