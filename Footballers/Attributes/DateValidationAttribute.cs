using System.ComponentModel.DataAnnotations;

namespace Footballers.Attributes
{
    public class DateValidationAttribute: ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateOnly BirthDay)
            {
                if (BirthDay < DateOnly.FromDateTime(DateTime.Now) && BirthDay > new DateOnly(1900, 1, 1))   
                    return true;
                else
                    ErrorMessage = "Некорректная Дата Рождения";
            }
            return false;
        }
    }
}
