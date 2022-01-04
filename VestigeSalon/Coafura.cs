namespace VestigeSalon
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Coafura")]
    public partial class Coafura
    {
        [Key]
        public int IdCoafura { get; set; }

        [Column(TypeName = "money")]
        public decimal? Pret { get; set; }

        public int? Durata { get; set; }

        [StringLength(50)]
        public string Tip { get; set; }
    }
}
