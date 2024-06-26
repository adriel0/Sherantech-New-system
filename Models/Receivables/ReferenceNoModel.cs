﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models.Receivables
{
    public class ReferenceNoModel
    {
        [Key]
        public int Id { get; set; }
        [DisallowNull]
        public int receivablesId { get; set; }
        [AllowNull]
        public int accountNo { get; set; }
        [AllowNull]
        public int drNo { get; set; }
        [AllowNull]
        public int invoiceNo { get; set; }
        [AllowNull]
        public DateTime? date { get; set; }
        [AllowNull]
        public int dealer { get; set; }
        [AllowNull]
        public int drAmount { get; set; }
        [AllowNull]
        public int amount { get; set; }
        [AllowNull]
        public int checkAmount { get; set; }
        [AllowNull]
        public int checkBalance { get; set; }
        [AllowNull]
        public int total { get; set; }
    }
}
