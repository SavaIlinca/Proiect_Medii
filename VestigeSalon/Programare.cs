namespace VestigeSalon
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Programare")]
    public partial class Programare
    {
        [Key]
        public int IdProgramare { get; set; }

        public int? IdClient { get; set; }

        public int? IdHairstylist { get; set; }

        public DateTime? Data { get; set; }

        public virtual Client Client { get; set; }

        public virtual Hairstylist Hairstylist { get; set; }
    }
}
