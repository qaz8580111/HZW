using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL.Enum
{
    /// <summary>
    /// 0,枪机;1,半球;2,快球;3,带云台枪机
    /// </summary>
    public enum cameraType
    {
        /// <summary>
        ///  枪机
        /// </summary>
        bolt=0,

        /// <summary>
        /// 半球
        /// </summary>
        hemisphere=1,
        
        /// <summary>
        /// 快球
        /// </summary>
        fastball=2,
        
        /// <summary>
        /// 带云台枪机
        /// </summary>
        withheadbolt=3
    }
}
