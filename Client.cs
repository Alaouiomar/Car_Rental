//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LOCATION_DES_VOITURES
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Client
    {
        public int idc { get; set; }
        public string NomPrenom { get; set; }
        public string Email { get; set; }
        public string Numero { get; set; }
        [Required]
        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> DateNaissance { get; set; }
        public string password { get; set; }
        public string Cin { get; set; }
        public string is_valid { get; set; }
        public string permis { get; set; }
        public string isadmin { get; set; }
    
        public virtual reservation reservation { get; set; }
    }
}