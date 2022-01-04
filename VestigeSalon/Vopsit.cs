namespace VestigeSalon
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vopsit")]
    public partial class Vopsit
    {
        [Key]
        public int IdVopsit { get; set; }

        [Column(TypeName = "money")]
        public decimal? PretV { get; set; }

        [StringLength(50)]
        public string Lungime { get; set; }
    }
}
