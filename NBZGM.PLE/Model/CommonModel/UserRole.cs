using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CommonModel
{
    public class UserRole
    {
        public decimal RoleID { get; set; }

        public string RoleName { get; set; }

        public string Descripion { get; set; }
        
        public bool IsChecked { get; set; }
    }
}
