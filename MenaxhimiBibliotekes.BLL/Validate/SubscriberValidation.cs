﻿using FluentValidation;
using MenaxhimiBibliotekes.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenaxhimiBibliotekes.BLL.Validate
{
    public class SubscriberValidation : AbstractValidator<Subscriber>
    {
        public Subscriber subscriber { get; set; }

        public SubscriberValidation()
        {
            subscriber = new Subscriber();
        }

        public void ValidateSubscriber()
        {
            try
            {
                ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

                RuleFor(m => m.Name)
                    .NotEmpty().WithMessage("{PropertyName} is empty! Please fill it!")
                    .Length(1, 20).WithMessage("Not shorter than 1 and not longer than 20");

                RuleFor(m => m.LastName)
                    .NotEmpty().WithMessage("{PropertyName} is empty! Please fill it!")
                    .Length(1, 20).WithMessage("Not shorter than 1 and not longer than 20");

                RuleFor(m => m.Address)
                    .NotEmpty().WithMessage("{PropertyName} is empty! Please fill it!")
                    .Length(1, 20).WithMessage("Not shorter than 1 and not longer than 20");

                if (subscriber.Birthday != null)
                {
                    RuleFor(m => m.Birthday)
                    .Must(BeAValidDate).WithMessage("{PropertyName} not entered properly!");
                }


                if (subscriber.PersonalNo != null)
                {
                    RuleFor(m => m.PersonalNo)
                       .NotEmpty().WithMessage("{PropertyName} is empty! Please fill it!")
                       .Matches("^\\d{1,20}$").WithMessage("Enter only numbers")
                       .Length(10, 10).WithMessage("No longer and not shorter than 10 charachters");
                }

                RuleFor(m => m.PhoneNo)
               .NotEmpty().WithMessage("{PropertyName} is empty! Please fill it!")
               .Matches("^\\d{1,20}$").WithMessage("Enter only numbers")
               .Length(6, 20).WithMessage("Not shorter than 6 and not longer than 20");

                RuleFor(m => m.Email)
                   .NotEmpty().WithMessage("{PropertyName} is empty! Please fill it!")
                   .EmailAddress().WithMessage("{PropertyName} is not correct!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected bool BeAValidDate(DateTime date)
        {
            DateTime d = DateTime.Now;
            DateTime min = new DateTime(1, 1, 1);

            return date <= d && date >= min;
        }
    }
}
