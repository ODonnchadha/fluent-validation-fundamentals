namespace Api.Validators
{
    using Api.Models;
    using FluentValidation;

    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(0, 200);
            RuleFor(x => x.Email).NotEmpty().Length(0, 150).EmailAddress();

            //RuleFor(x => x.Address).NotNull()
            //    .SetValidator(new AddressValidator());

            //RuleForEach(x => x.Addresses).ChildRules(address =>
            //{
            //    address.RuleFor(x => x.Street).NotEmpty().Length(0, 100);
            //    address.RuleFor(x => x.City).NotEmpty().Length(0, 40);
            //    address.RuleFor(x => x.State).NotEmpty().Length(0, 2);
            //    address.RuleFor(x => x.ZipCode).NotEmpty().Length(0, 100);
            //});

            //RuleFor(x => x.Addresses).NotNull()
            //    .Must(x => x?.Length >= 1 && x.Length <= 3)
            //    .WithMessage("The number of student addresses must between 1 and 3.")
            //    .ForEach(x =>
            //    {
            //        x.NotNull();
            //        x.SetValidator(new AddressValidator());
            //    });

            RuleFor(a => a.Addresses).NotNull().SetValidator(new AddressesValidator());
        }
    }
}
