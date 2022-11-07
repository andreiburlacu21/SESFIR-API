using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.Validators
{
    public static class Validate
    {
            public static async Task FluentValidate<T>(IValidator<T> validator, T entity)
            {
                var result = await validator.ValidateAsync(entity);

                if (result.Errors.Count > 0)
                {
                    throw new ValidationException(result.Errors);
                }
            }

    }
}
