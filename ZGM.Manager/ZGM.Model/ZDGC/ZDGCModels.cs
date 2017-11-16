using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.ZDGC
{
    public class ZDGCModels
    {
        /// <summary>
        /// 0或null 不弹窗  1.添加 2 修改 3查看，所有保存按钮变成编辑按钮
        /// </summary>
        public int GCGK { get; set; }
        public int GCZB { get; set; }
        public int GCSG { get; set; }
        public int GCJG { get; set; }
        public int BZQWH { get; set; }

        private BP_GCGKXX _GKXXModel = new BP_GCGKXX();
        /// <summary>
        /// 工程概况
        /// </summary>
        public BP_GCGKXX GKXXModel
        {
            get { return _GKXXModel; }
            set { _GKXXModel = value; }
        }
        private BP_GCZBXX _ZBXXModel = new BP_GCZBXX();
        /// <summary>
        /// 招标信息
        /// </summary>
        public BP_GCZBXX ZBXXModel
        {
            get { return _ZBXXModel; }
            set { _ZBXXModel = value; }
        }

        private BP_GCSGJDXX _GCSGJDXXModel = new BP_GCSGJDXX();
        /// <summary>
        /// 施工进度
        /// </summary>
        public BP_GCSGJDXX GCSGJDXXModel
        {
            get { return _GCSGJDXXModel; }
            set { _GCSGJDXXModel = value; }
        }
        private BP_GCSGWTXX _GCSGWTXXModel = new BP_GCSGWTXX();
        /// <summary>
        /// 施工问题
        /// </summary>
        public BP_GCSGWTXX GCSGWTXXModel
        {
            get { return _GCSGWTXXModel; }
            set { _GCSGWTXXModel = value; }
        }
        private BP_GCZJSYQKYB _GCZJSYQKYBModel = new BP_GCZJSYQKYB();
        /// <summary>
        /// 施工拨付款
        /// </summary>
        public BP_GCZJSYQKYB GCZJSYQKYBModel
        {
            get { return _GCZJSYQKYBModel; }
            set { _GCZJSYQKYBModel = value; }
        }

        private BP_GCJGXX _GCJGXXModel = new BP_GCJGXX();
        /// <summary>
        /// 工程竣工
        /// </summary>
        public BP_GCJGXX GCJGXXModel
        {
            get { return _GCJGXXModel; }
            set { _GCJGXXModel = value; }
        }

        private BP_GCSJXX _GCSJXXModel = new BP_GCSJXX();
        /// <summary>
        /// 工程审计
        /// </summary>
        public BP_GCSJXX GCSJXXModel
        {
            get { return _GCSJXXModel; }
            set { _GCSJXXModel = value; }
        }

        private BP_GCWHXX _GCWHXXModel = new BP_GCWHXX();
        /// <summary>
        /// 工程维护
        /// </summary>
        public BP_GCWHXX GCWHXXModel
        {
            get { return _GCWHXXModel; }
            set { _GCWHXXModel = value; }
        }

        private BP_GCNRXX _GCNRXXModel = new BP_GCNRXX();

        /// <summary>
        /// 工程内容信息
        /// </summary>
        public BP_GCNRXX GCNRXXModel
        {
            get { return _GCNRXXModel; }
            set { _GCNRXXModel = value; }
        }

        private List<BP_GCXMFJ> _ListGCFJModel = new List<BP_GCXMFJ>();
        /// <summary>
        /// 工程附件集合
        /// </summary>
        public List<BP_GCXMFJ> ListGCFJModel
        {
            get { return _ListGCFJModel; }
            set { _ListGCFJModel = value; }
        }
       
    }

}
