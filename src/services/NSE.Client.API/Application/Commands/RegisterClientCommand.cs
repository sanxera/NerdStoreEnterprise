using System;
using FluentValidation;
using NSE.Core.Messages;

namespace NSE.Client.API.Application.Commands
{
    public class RegisterClientCommand : Command
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public RegisterClientCommand(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterClientValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RegisterClientValidation : AbstractValidator<RegisterClientCommand>
    {
        public RegisterClientValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("O nome do cliente não foi informado");

            RuleFor(c => c.Cpf)
                .Must(CpfIsValid)
                .WithMessage("O CPF informado não é válido.");

            RuleFor(c => c.Email)
                .Must(EmailIsValid)
                .WithMessage("O e-mail informado não é válido.");
        }

        protected static bool CpfIsValid(string cpf)
        {
            return Core.DomainObjects.Cpf.Validar(cpf);
        }

        protected static bool EmailIsValid(string email)
        {
            return Core.DomainObjects.Email.Validate(email);
        }
    }
}
