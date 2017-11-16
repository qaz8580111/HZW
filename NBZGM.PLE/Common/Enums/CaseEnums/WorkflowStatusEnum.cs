using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Common.Enums.CaseEnums
{
	public enum WorkflowStatusEnum : int
	{
		/// <summary>
		/// 已删除
		/// </summary>
		Deleted = -1,

		/// <summary>
		/// 空
		/// </summary>
		None = 0,

		/// <summary>
		/// 初始化
		/// </summary>
		Initiation = 1,

		/// <summary>
		/// 进行中
		/// </summary>
		Progress = 2,

		/// <summary>
		/// 已中止
		/// </summary>
		Broken = 3,

		/// <summary>
		/// 已完成
		/// </summary>
		Completed = 4
	}
}
