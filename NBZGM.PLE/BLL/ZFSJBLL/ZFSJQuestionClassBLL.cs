using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;
using Taizhou.PLE.Common.Enums.ZFSJEnums;

namespace Taizhou.PLE.BLL.ZFSJBLL
{
    /// <summary>
    /// 执法事件问题类BLL
    /// </summary>
    public class ZFSJQuestionClassBLL
    {
        /// <summary>
        /// 获取所有的大类
        /// </summary>
        /// <returns></returns>
        public static IQueryable<ZFSJQUESTIONCLASS> GetZFSJQuestionDL()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<ZFSJQUESTIONCLASS> results = db.ZFSJQUESTIONCLASSES
                .Where(t => t.CLASSTYPEID == (decimal)ZFSJQuestionClass.QuestionDL);
            return results;
        }

        /// <summary>
        /// 根据大类标识获取该大类下的小类
        /// </summary>
        /// <param name="questionDLID">问题大类标识</param>
        /// <returns></returns>
        public static IQueryable<ZFSJQUESTIONCLASS> GetZFSHQuestionXL(decimal questionDLID)
        {
            IQueryable<ZFSJQUESTIONCLASS> results = null;
            if (Convert.IsDBNull(questionDLID))
            {
                return results;
            }
            else
            {
                PLEEntities db = new PLEEntities();
                results = db.ZFSJQUESTIONCLASSES
                 .Where(t => t.PARENTID == questionDLID);
                return results;
            }

        }

        public static ZFSJQUESTIONCLASS GetZFSHQuestionByID(decimal ID)
        {
            string questionID = ID.ToString();
            if (questionID != "0")
            {
                PLEEntities db = new PLEEntities();
                ZFSJQUESTIONCLASS result = db.ZFSJQUESTIONCLASSES
                    .FirstOrDefault<ZFSJQUESTIONCLASS>(t => t.CLASSID == questionID);
                return result;
            }
            else
            {
                ZFSJQUESTIONCLASS result = null;
                return result;
            }
        }

    }
}
