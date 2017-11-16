using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;

namespace Taizhou.PLE.BLL.XZSPBLLs
{
    public class WorkflowFileBLL
    {
        public static List<TreeModel> GetFileTree(List<Attachment> attachments)
        {
            List<TreeModel> treemodels = new List<TreeModel>();
            TreeModel root = new TreeModel() 
            {
                name = "所有文件",
                title = "所有文件",
                open = true,
                type = "root"
            };

            treemodels.Add(root);

            foreach (Attachment am in attachments)
            {
                TreeModel treeChild = new TreeModel() 
                {
                     name=am.AttachName,
                      title=am.AttachName,
                       value=am.Path
                };

                root.children.Add(treeChild);
            }

            return treemodels;
        }
    }
}
