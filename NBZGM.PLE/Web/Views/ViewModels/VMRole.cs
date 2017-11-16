using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VMRole
    {
        public decimal RoleID { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public decimal? SeqNo { get; set; }
    }
}