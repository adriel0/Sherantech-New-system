using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace WebApplication1.Models.DeliveryReceipts
{
    public class DeliveryReceiptsDisplayDataModel
    {
        [AllowNull]
        public List<DeliveryReceiptsModel>? Data { get; set; }
        [AllowNull]
        public List<DeliveryReceiptsItemsModel>? CurrentItems { get; set; }
        [AllowNull]
        public List<SerialsModel>? CurrentSerial { get; set; }
        [AllowNull]
        public List<RemittancesModel>? CurrentRemittances { get; set; }
        [AllowNull]
        public DeliveryReceiptsDetailsModel? CurrentDetails { get; set; }
    }
}
