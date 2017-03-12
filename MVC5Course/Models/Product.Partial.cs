namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Validations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
                
        [Required(ErrorMessage = "請輸入商品名稱:")]
        [DisplayName("商品名稱")]
        [StringLength(80, ErrorMessage = "欄位長度不得大於 80 個字元")]
        [商品名稱驗證]
        public string ProductName { get; set; }

        [Required]
        [Range(minimum: 100, maximum: 10000, ErrorMessage = "商品金額輸入錯誤")]
        [DisplayName("商品價格")]
        [DisplayFormat(DataFormatString = "NT$ {0:N0}")]
        public Nullable<decimal> Price { get; set; }

        [Required]
        [DisplayName("有效性")]
        public Nullable<bool> Active { get; set; }

        [DisplayName("庫存")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        [Required]
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
