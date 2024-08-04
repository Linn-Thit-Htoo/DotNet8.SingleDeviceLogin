using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Auth.Domain.Entities
{
    [Table("Tbl_Login")]
    public class Tbl_Login
    {
        [Key]
        public string LoginId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
