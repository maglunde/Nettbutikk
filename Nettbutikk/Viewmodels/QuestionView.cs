using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nettbutikk.Viewmodels
{
    public class QuestionView
    {
        [Required]
        public string Question { get; set; }
        [Required]
        [EmailAddress(ErrorMessage ="Skriv en gyldig epost-adresse")]
        public string Email { get; set; }
    }
}