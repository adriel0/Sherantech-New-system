﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace WebApplication1.Models.Purchases
{
    public class PurchasesDisplayDataModel
    {
        [AllowNull]
        public List<PurchasesModel>? Details { get; set; }
        [AllowNull]
        public List<PurchasesItemsModel>? CurrentItems { get; set; }
        [AllowNull]
        public PurchasesModel? Current { get; set; }
    }
}
