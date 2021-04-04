using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.Validations.FluentValidation
{
    public class TissueValidator:AbstractValidator<Tissue>
    {
        public TissueValidator()
        {
            RuleFor(p => p.Name).MinimumLength(2);
            RuleFor(p => p.Name).Must(StartsWithM).WithMessage("Tissue name must starts with C.");
        }

        private bool StartsWithM(string arg)
        {
            return arg.StartsWith("C");
        }
    }
}
