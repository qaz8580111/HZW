using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.GeogSpace
{
     public  class LAYERTYPEBLL
    {
         /// <summary>
         /// 查询元素类型表所有数据
         /// </summary>
         /// <returns></returns>
         public IQueryable<LAYERTYPE> GetLAYERTYPEList() {
             PLEEntities db = new PLEEntities();
             return db.LAYERTYPEs;
         }

         /// <summary>
         /// 查询json
         /// </summary>
         /// <param name="id">主键标识</param>
         /// <returns></returns>
         public string getTypeValue(int id) {

             PLEEntities db = new PLEEntities();
             LAYERTYPE model=db.LAYERTYPEs.FirstOrDefault(a=>a.ID==id);
             if (model == null) { return null; }
             else
             {
                 return model.TYPEVALUE;
             }
         }

         public string getNameByLayerID(decimal? LayerID) {
             
             PLEEntities db = new PLEEntities();
             if (LayerID == 0)
             {
                 return "未选择";
             }
             else {
                 LAYERTYPE model = db.LAYERTYPEs.FirstOrDefault(a => a.ID == LayerID);
                 if (model == null)
                 {
                     return "未选择";
                 }
                 else {
                     return model.NAME;
                 }
                
             }
            
         }
    }
}
