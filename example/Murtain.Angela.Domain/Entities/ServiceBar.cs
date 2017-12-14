using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Murtain.Domain;

namespace Murtain.Angela.Domain.Entities
{
    [Table("service_bar")]
    public class ServiceBar : Entity
    {
        /// <summary>
        /// 服务台名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [MaxLength(50)]
        [Required]
        [Column("ip")]
        public string IP { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(250)]
        [Column("remark")]
        public string Remark { get; set; }
    }
}
