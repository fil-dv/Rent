using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AreaType
    {
        [Display(Name = "Тип площади:")]
        public int AreaTypeID { get; set; }
        public string AreaTypeName { get; set; }
    }
}
