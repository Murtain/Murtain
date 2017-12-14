using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Murtain.Domain
{
    public class AuditedEntity : AuditedEntity<long>
    {
    }

    public class AuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, IAuditedEntity
    {
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        public virtual DateTime? CreateTime { get; set; }
        /// <summary>
        /// Creator of this entity.
        /// </summary>
        [MaxLength(50)]
        public virtual string CreateUser { get; set; }
        /// <summary>
        /// The  modify user for this entity.
        /// </summary>
        [MaxLength(50)]
        public virtual string ChangeUser { get; set; }
        /// <summary>
        /// The last modified time for this entity.
        /// </summary>
        public virtual DateTime? ChangeTime { get; set; }

    }
}
