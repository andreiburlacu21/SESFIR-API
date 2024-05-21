using FluentValidation;
using SESFIR.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.Validators.Model.Validation
{
    public sealed class AccountsValidation : AbstractValidator<AccountDTO>
    {
        public AccountsValidation()
        {
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .Length(4, 25)
                .NotEmpty();

            RuleFor(x => x.Password)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .Length(4, 25)
               .Must(MustBeAValidPassowrd).WithMessage("The selected password does not meet the requirements.");

            RuleFor(x => x.Email)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .Length(4, 100)
               .EmailAddress();
        }
        private bool MustBeAValidPassowrd(string password)
        {
            // char[] special = { '@', '#', '$', '%', '^', '&', '+', '=', '-' };

            // if (!password.Any(char.IsUpper)) return false;

            // if (!password.Any(char.IsLower)) return false;

            // if (!password.Any(char.IsDigit)) return false;

            // if (password.IndexOfAny(special) == -1) return false;

            return true;
        }
    }
}
