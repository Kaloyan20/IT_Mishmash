using Data.Data;

namespace PC_Parts_Picker.Models
{
    public class AdminViewModel
    {
        public bool IsAuthenticated { get; set; }
        public List<Cpu> Cpus { get; set; } = new List<Cpu>();
        public List<Motherboard> Motherboards { get; set; } = new List<Motherboard>();
        public List<Gpu> Gpus { get; set; } = new List<Gpu>();
        public List<Ram> Rams { get; set; } = new List<Ram>();
        public List<Ssd> Ssds { get; set; } = new List<Ssd>();
        public List<Psu> Psus { get; set; } = new List<Psu>();
        public List<Case> Cases { get; set; } = new List<Case>();
        public List<Cooler> Coolers { get; set; } = new List<Cooler>();
    }
}