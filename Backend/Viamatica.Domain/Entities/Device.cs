namespace Viamatica.Domain.Entities;

public class Device
{
    public int DeviceId { get; set; }
    public string DeviceName { get; set; } = null!;

    public int ServiceServiceId { get; set; }
    public Service Service { get; set; } = null!;
}