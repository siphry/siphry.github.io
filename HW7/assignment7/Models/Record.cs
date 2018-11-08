namespace assignment7.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Record
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [StringLength(128)]
        public string Input { get; set; }

        [Required]
        [StringLength(16)]
        public string IP { get; set; }

        [Column("Browser-Agent")]
        [Required]
        [StringLength(255)]
        public string Browser_Agent { get; set; }
    }
}
