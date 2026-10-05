namespace Viamatica.Domain.Entities;

public class Service
{
    public int ServiceId { get; set; }
    public string ServiceName { get; set; } = null!;
    public string ServiceDescription { get; set; } = null!;
    public decimal Price { get; set; }
    
    public decimal SpeedMbps { get; set; } 

    public ICollection<Device> Devices { get; set; } = new List<Device>();
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}