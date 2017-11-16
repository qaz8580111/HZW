using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.PhoneModel
{
   public class UserHistoryPositionModel
    {
       public int UserId { get; set; }
       public DateTime POSITIONTIME { get; set; }
       public decimal SPEED { get; set; }
       public string IMEICODE { get; set; }
       public string GEOMETRY { get; set; }

    }
   public class UserLateStpositions
   {
       public int UserId { get; set; }
       public string GEOMETRY { get; set; }
       public DateTime POSITIONTIME { get; set; }
       public DateTime LASTLOGINTIME { get; set; }
       public string IMEICODE { get; set; }
   }
    }
