using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete.Identity.ViewModels
{
    public class CreateRoleModel
    {
        public string Name { get; set; }
    }

    public class EditRoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Description { get; set; }
    }

}
