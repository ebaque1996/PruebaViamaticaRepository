using Viamatica.Application.DTOs;

namespace Viamatica.Application.Interfaces;

public interface IContractService
{
    Task ChangeOrRenewContractAsync(int oldContractId, int newServiceId);

    Task<bool> CancelContractAsync(int contractId);
}