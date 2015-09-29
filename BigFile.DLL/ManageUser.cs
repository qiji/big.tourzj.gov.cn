using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigFile.DLL
{
    public class ManageUser
    {
        [Required]
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(200)]
        [Index("idx_ManageUser_UserName", Order = 1, IsUnique = true)]
        public string UserName { get; set; }

        public string Pwd { get; set; }
    }
}
