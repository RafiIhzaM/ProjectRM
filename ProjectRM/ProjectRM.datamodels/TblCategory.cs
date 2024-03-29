﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectRM.datamodels
{
    [Table("TblCategory")]
    public partial class TblCategory
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? NameCategory { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Description { get; set; }
        [Unicode(false)]
        public string? Image { get; set; }
        public bool? IsDelete { get; set; }
        public int? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
    }
}
