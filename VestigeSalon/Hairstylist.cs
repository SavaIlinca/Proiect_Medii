namespace VestigeSalon
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hairstylist")]
    public partial class Hairstylist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hairstylist()
        {
            Programares = new HashSet<Programare>();
        }

        [Key]
        public int IdHairstylist { get; set; }

        [StringLength(50)]
        public string NumeH { get; set; }

        [StringLength(50)]
        public string PrenumeH { get; set; }

        [StringLength(50)]
        public string Experienta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Programare> Programares { get; set; }
    }
}
