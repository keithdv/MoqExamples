namespace EfDal.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MoqExample")]
    public partial class MoqExample
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string Column1 { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Column2 { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal Column3 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string Column4 { get; set; }
    }
}
