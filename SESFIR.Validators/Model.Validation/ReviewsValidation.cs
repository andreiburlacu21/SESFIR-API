using FluentValidation;
using SESFIR.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.Validators.Model.Validation
{
    public sealed class ReviewsValidation : AbstractValidator<ReviewDTO>
    {
        public ReviewsValidation()
        {
            RuleFor(rev => rev.Description)
                .NotEmpty();
        }
    }
}
