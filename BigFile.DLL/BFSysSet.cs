using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigFile.DAL
{
    /// <summary>
    /// 系统设置表
    /// </summary>
    public class BFSysSet
    {
        [Required]
        [Key]
        public int SysID { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [Required]
        public string SysName { get; set; }

        /// <summary>
        /// 系统描述
        /// </summary>
        public string SysDesp { get; set; }

        public virtual ICollection<BFInfo> BFInfos { get; set; }

        /// <summary>
        /// 系统加入时间
        /// </summary>
        public DateTime AddDateTime { get; set; }
    }
}
