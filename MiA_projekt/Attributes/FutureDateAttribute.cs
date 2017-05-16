using System;
using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Attributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Convert.ToDateTime(value) >= DateTime.Today;
        }
    }
}