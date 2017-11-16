using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace CacheHelper
{
    public class CacheHelper
    {
        protected static volatile System.Web.Caching.Cache objCache = System.Web.HttpRuntime.Cache;  
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        public static object GetCache(string CacheKey)
        {           
            return objCache.Get(CacheKey);
        }


        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="CacheKey">缓存的键</param>
        /// <param name="objObject">缓存的值</param>
        /// <param name="absoluteExpiration">过期时间</param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration)
        {
            objCache.Add(CacheKey, objObject, null, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, new CacheItemRemovedCallback(CacheItemOnRemoved));
        }

        public static string Values { get; set; } // 测试数据字段
        protected static void CacheItemOnRemoved(string key, object value, CacheItemRemovedReason reason)
        {
            // 当缓存回收时会执行该方法
            switch (reason)
            {
                case CacheItemRemovedReason.DependencyChanged:
                    Values = "后台回调方法提示：该缓存依赖项已更改";// 设置模拟数据
                    break;
                case CacheItemRemovedReason.Expired:
                    Values = "后台回调方法提示：该缓存已过期";
                    break;
                case CacheItemRemovedReason.Removed:
                    Values = "后台回调方法提示：该缓存手动移除";
                    break;
                case CacheItemRemovedReason.Underused:
                    Values = "后台回调方法提示：该缓存已释放";
                    break;
            }
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public static void RemoveCache(string CacheKey)
        {
            objCache.Remove(CacheKey);
        }
         
        /// <summary>
        /// 加入当前对象到缓存中,并对相关文件建立依赖  
        /// </summary>
        /// <param name="CacheKey">对象的键值</param>
        /// <param name="objObject">缓存的对象</param>
        /// <param name="absoluteExpiration">过期时间</param>
        /// <param name="files">监视的路径文件</param>
        public virtual void AddObjectWithFileChange(string CacheKey, object objObject, DateTime absoluteExpiration, string[] files)
        {
            if (CacheKey == null || CacheKey.Length == 0 || objObject == null)
            {
                return;
            }
            
            CacheDependency dep = new CacheDependency(files, DateTime.Now);

            objCache.Add(CacheKey, objObject, dep, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, new CacheItemRemovedCallback(CacheItemOnRemoved));
        }
 
        /// <summary>
        /// 加入当前对象到缓存中,并使用依赖键  
        /// </summary>
        /// <param name="CacheKey">对象的键值</param>
        /// <param name="objObject">缓存的对象</param>
        /// <param name="absoluteExpiration">过期时间</param>
        /// <param name="dependKey">依赖关联的键值</param>
        public virtual void AddObjectWithDepend(string CacheKey, object objObject, DateTime absoluteExpiration, string[] dependKey)
        {
            if (CacheKey == null || CacheKey.Length == 0 || objObject == null)
            {
                return;
            }

            CacheDependency dep = new CacheDependency(null, dependKey, DateTime.Now);

            objCache.Add(CacheKey, objObject, dep, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, new CacheItemRemovedCallback(CacheItemOnRemoved));
        }  
    }
}