using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.Validations
{
    public class 商品名稱驗證Attribute : DataTypeAttribute
    {
        public 商品名稱驗證Attribute() : base(DataType.Text)
        {

        }

        public override bool IsValid(object value)
        {
            string Str = Convert.ToString(value);
            if (Str.ToLower().Contains("aaronlivy"))
                return false;
            return true;
        }
    }
}