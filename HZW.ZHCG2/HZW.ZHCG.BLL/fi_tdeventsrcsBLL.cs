using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZW.ZHCG.DAL;

namespace HZW.ZHCG.BLL
{
    public class fi_tdeventsrcsBLL
    {
        //实例化dal
        fi_tdeventsrcsDALcs caseDal = new fi_tdeventsrcsDALcs();

        public List<fi_tdeventsrcs> getType()
        {
            return caseDal.getType();
        }
    }
}
