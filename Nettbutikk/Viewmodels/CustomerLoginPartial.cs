using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nettbutikk.Viewmodels
{
    public class CustomerLoginPartial
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Epost")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Passord")]
        public string Password { get; set; }
    }
}