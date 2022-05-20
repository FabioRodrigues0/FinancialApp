using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FluentValidation.Results;

namespace Infrastructure.Shared;

public abstract class EntityBase<TEntity>
{
	public Guid Id { get; set; } = Guid.NewGuid();

	#region Validation

	[NotMapped]
	[JsonIgnore]
	public ValidationResult ValidationResult { get; protected set; }

	public abstract bool IsValid();

	#endregion Validation
}