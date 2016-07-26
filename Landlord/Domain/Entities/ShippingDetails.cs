using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Укажите Ваше имя.")]
        [Display(Name = "Имя:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите номер контактного телефона.")]
        [Display(Name = "Номер телефона:")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Введен некорректный номер.")]
        public string Phone { get; set; }
    }
}
