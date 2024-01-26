using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProjectRM.viewmodels
{
    public class VMTblCategory
    {
        public int Id { get; set; }
        public string? NameCategory { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public bool? IsDelete { get; set; }
        public int? CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
