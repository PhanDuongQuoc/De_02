namespace De02
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sanpham")]
    public partial class Sanpham
    {
        [Key]
        [StringLength(6)]
        public string masp { get; set; }

        [StringLength(30)]
        public string tensp { get; set; }

        public DateTime? ngaynhap { get; set; }

        [StringLength(2)]
        public string maloai { get; set; }

        public virtual LoaiSP LoaiSP { get; set; }
    }
}
