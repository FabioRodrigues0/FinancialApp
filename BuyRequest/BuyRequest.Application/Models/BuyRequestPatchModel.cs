using Infrastructure.Shared.Enums;

namespace BuyRequest.Application.Models;

public class BuyRequestPatchModel
{
	public Guid Id { get; set; }
	public Status Status { get; set; }
}
