using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XUnit.Uranus.EntityFramework.SDK
{
    /// <summary>
    /// 服务台
    /// </summary>
    public class ServiceBar
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get; set; }
        /// <summary>
        /// 服务台名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
    }
}
