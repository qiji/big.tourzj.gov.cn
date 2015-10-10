using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigFile.DAL
{
    public class BFInfo
    {
        [Required]
        [Key]
        public int BFID { get; set; }

        /// <summary>
        /// 那个系统引用的
        /// </summary>
        public int SysID { get; set; }

        [ForeignKey("SysID")]
        public virtual BFSysSet BFSysSet { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string ExtName { get; set; }

        /// <summary>
        /// 原始文件名
        /// </summary>
        public string OrignName { get; set; }

        /// <summary>
        /// 二进制文件
        /// </summary>
        public byte[] BFContent { get; set; }

        /// <summary>
        /// 输出类型
        /// </summary>
        //public string OutType { get; set; }
        public DateTime CrtDateTime { get; set; }
        public DateTime LastViewDateTime { get; set; }
    }
}
