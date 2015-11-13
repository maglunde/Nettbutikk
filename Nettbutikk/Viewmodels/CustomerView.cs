using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nettbutikk.Viewmodels
{
    public class CustomerView
    {
        public int CustomerId { get; set; }

        public string Email { get; set; }

        [Required]
        [DisplayName("Fornavn")]
        public string Firstname { get; set; }

        [DisplayName("Etternavn")]
        public string Lastname { get; set; }

        [Required]
        [DisplayName("Adresse")]
        public string Address { get; set; }

        [Required]
        [DisplayName("Postnummer")]
        [RegularExpression(@"[0-9]{4}")]
        public string Zipcode { get; set; }

        [Required]
        [DisplayName("Poststed")]
        public string City { get; set; }
    }

    public class CustomerChangePassword
    {
        [Required]
        [DisplayName("Nåværende passord")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set;}

        [Required]
        [DisplayName("Nytt passord")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DisplayName("Gjenta nytt passord")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string RepeatNewPassword { get; set; }
    }
}