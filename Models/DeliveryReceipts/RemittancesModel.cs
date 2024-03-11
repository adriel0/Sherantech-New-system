﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models.DeliveryReceipts
{
    public class RemittancesModel
    {
        [Key]
        public int drNo { get; set; }
        [AllowNull]
        public int checkNo { get; set; }
        [AllowNull]
        public int accountNo { get; set; }
        [AllowNull]
        public int amount { get; set; }
        [AllowNull]
        public DateTime? dateIssued { get; set; }
        [AllowNull]
        public DateTime? dateDue { get; set; }
        [AllowNull]
        public string? status { get; set; }
        [AllowNull]
        public string? payToTheOrderOf { get; set; }
        [AllowNull]
        public string? bankName { get; set; }
        [AllowNull]
        public string? accountName { get; set; }
    }
}
