using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nettbutikk.Viewmodels
{
    public class CustomerRegisterPartial
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Epost")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Passord")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Gjenta passord")]
        public string RepeatPassword { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Fornavn")]
        public string Firstname { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Etternavn")]
        public string Lastname { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Adresse")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression("@[0-9]{4}")]
        [DisplayName("Postnummer")]
        public string Zipcode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Poststed")]
        public string City { get; set; }



    }
}