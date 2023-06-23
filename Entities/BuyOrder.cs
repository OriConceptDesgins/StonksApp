﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BuyOrder
    {
        public Guid BuyOrderID { get; set; }
        [Required]
        public string? StockSymbol{get; set; }
        [Required]
        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000)]
        public uint Quantity{get; set; }
        [Range(1, 10000)]
        public double Price { get; set; }
    }
}
