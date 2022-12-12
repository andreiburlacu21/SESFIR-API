using FluentValidation;
using SESFIR.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.Validators.Model.Validation
{
    public sealed class LocationsValidation : AbstractValidator<LocationDTO>
    {
        public LocationsValidation()
        {
            RuleFor(loc => loc.LocationName)
                .NotEmpty();
        }
    }
}
