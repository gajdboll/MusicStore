using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStoreData.Models.Store
{
    public class TechInfo
    {
 
        [Key]
        public int Id { get; set; }

        // Foreign Key to ProductType
        public int ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductType ProductType { get; set; }

        // Technical Information Fields
        public string BodyMaterial { get; set; }
        public string NeckMaterial { get; set; }
        public string FingerboardMaterial { get; set; }
        public string Pickups { get; set; }
        public string BridgeType { get; set; }
        public string Electronics { get; set; }
        public string StringGauge { get; set; }
        public string TuningMachines { get; set; }
        public double FretboardRadius { get; set; }
        public int NumberOfFrets { get; set; }
        public string Finish { get; set; }
        public double Weight { get; set; }
        public string AccessoriesIncluded { get; set; }
    }
}
