using System.ComponentModel.DataAnnotations;

namespace ValidationUtils.DataAnnotations
{

    public class DateTimeRange : ValidationAttribute
    {
                
        private readonly string _min, _max;

        /// <summary>
        /// Please see correct Param Format for this attribute, 
        /// do not use a different text convention or it will fail
        /// </summary>
        /// <param name="min"> Min year should be YYYY/MM/DD string format or "min"</param>
        /// <param name="max"> Max year should be YYYY/MM/DD string format or "max"</param>
        public DateTimeRange(string min , string max)
        {
            _max = max;
            _min = min;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {


            if (value == null) return new ValidationResult("Date is null so it isn't in range");
            if (VerifyAttirubteParameters() == false) 
            {
                return new ValidationResult("Attribute Parameters are inapproate format for dates");
            }
            
            DateTime date = (DateTime)value;
            DateTime minDate , maxDate;

            if (_min == "min") 
            {
                minDate = DateTime.MinValue;
            }
            else 
            {
                minDate = DateTime.Parse(_min);
            }

            if (_max == "max")
            {
                maxDate = DateTime.MinValue;
            }
            else
            {
                maxDate = DateTime.Parse(_max);
            }
            
            if ((date > minDate) && (date < maxDate))
            {
                return ValidationResult.Success;
            }
            else
            {
                return null;
            }    
        }

       

        private bool VerifyAttirubteParameters() 
        {
            if ((_min.Count(c => c == '/') != 2) && (_min!= "min"))
            {
                return false;
            }
            if ((_max.Count(c => c == '/') != 2) && (_max!= "max"))
            {
                return false;
            }

            if (_max != "max")
            {
                if (_max.Any(c => char.IsLetter(c))) 
                {
                    return false;
                }
            }

            if (_min != "min")
            {
                if (_min.Any(c => char.IsLetter(c)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}