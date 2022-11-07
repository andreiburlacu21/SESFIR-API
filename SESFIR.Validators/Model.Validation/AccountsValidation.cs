using FluentValidation;
using SESFIR.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.Validators.Model.Validation
{
    public sealed class AccountsValidation : AbstractValidator<AccountsDTO>
    {
        public AccountsValidation()
        {
            RuleFor(acc => acc.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(4, 99);
        }
    }
}
