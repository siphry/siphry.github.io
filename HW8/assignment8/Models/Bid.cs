namespace assignment8.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Item { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Buyer { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Price { get; set; }

        [Key]
        [Column(Order = 3)]
        private DateTime date = DateTime.Now;
        [Display(Name = "Submission Time")]
        public DateTime Timestamp
        {
            get { return date; }
            set { date = value; }
        }

        public virtual Buyer Buyer1 { get; set; }

        public virtual Item Item1 { get; set; }
    }
}
