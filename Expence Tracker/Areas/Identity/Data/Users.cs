using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace User_Login.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Users class
public class Users : IdentityUser
{
    /*
    [Key]
    public int UserId { get; set; }*/

    [Column(TypeName = "nvarchar(50)")]
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    /*
    [Column(TypeName = "nvarchar(50)")]
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }

    

    [Column(TypeName = "nvarchar(10)")]
    [Required(ErrorMessage = "email is required.")]
    public string email { get; set; }
    */

    [NotMapped]
    [Column(TypeName = "bigint")]
    [Required(ErrorMessage = "Mobile No. is required.")]
    public BigInteger mobileNo { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string Address { get; set; }

    [NotMapped]
    [Column(TypeName = "bigint")]
    [Required(ErrorMessage = "Credit Card No. is required.")]
    public BigInteger creditCardNo { get; set; }

    [Column(TypeName = "nvarchar(10)")]
    [Required(ErrorMessage = "Bank Name is required.")]
    public string Bank_Name { get; set; }
}

