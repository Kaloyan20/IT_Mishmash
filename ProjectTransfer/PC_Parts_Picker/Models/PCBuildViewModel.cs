using Data.Data;

namespace PC_Parts_Picker.Models
{
    public class SystemBuilderViewModel
    {
        // Form inputs
        public string UsageType { get; set; }
        public string Budget { get; set; }
        public string FormFactor { get; set; }
        public string BrandPreference { get; set; }
        public string Aesthetics { get; set; }
        public string ColorScheme { get; set; }

        // Build results
        public bool ShowResults { get; set; }
        public string BuildName { get; set; }
        public double TotalPrice { get; set; }

        // PC Components
        public Cpu Cpu { get; set; }
        public Motherboard Motherboard { get; set; }
        public Gpu Gpu { get; set; }
        public Ram Ram { get; set; }
        public Ssd Ssd { get; set; }
        public Psu Psu { get; set; }
        public Case Case { get; set; }
        public Cooler Cooler { get; set; }
    }
}