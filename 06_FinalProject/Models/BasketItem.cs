﻿using System.ComponentModel.DataAnnotations.Schema;

namespace _06_FinalProject.Models
{
    public class BasketItem
    {
       
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }


        public int BasketId { get; set; }
        public Basket Basket { get; set; }

    }
}
