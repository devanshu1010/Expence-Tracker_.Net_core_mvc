using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using User_Login.Areas.Identity.Data;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace User_Login.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Icon { get; set; } = "";

        [Column(TypeName = "nvarchar(10)")]
        public string Type { get; set; } = "Expense";

        public string User_Id { get; set; }
        [ForeignKey("User_Id")]

        public virtual Users Users { get; set; }

        [NotMapped]
        public string? TitleWithIcon
        {
            get
            {
                return Icon + " " + Title;
            }
        }
    }
}
