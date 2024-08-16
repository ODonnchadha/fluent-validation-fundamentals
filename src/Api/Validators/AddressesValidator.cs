using FluentValidation;

namespace Api.Validators
{
    public class AddressesValidator: AbstractValidator<Models.Address[]>
    {
        public AddressesValidator()
        {
            RuleFor(x => x)
                .Must(x => x?.Length >= 1 && x.Length <= 3)
                .WithMessage("The number of student addresses must between 1 and 3.")
                .ForEach(x =>
                {
                    x.NotNull();
                    x.SetValidator(new AddressValidator());
                });
        }
    }
}
