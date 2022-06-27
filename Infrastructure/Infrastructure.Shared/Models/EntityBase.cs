using FluentValidation.Results;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Shared.Models
{
	public abstract class EntityBase<TEntity>
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		#region Validation

		[NotMapped]
		[JsonIgnore]
		public ValidationResult ValidationResult { get; protected set; }

		public abstract Task<bool> IsValid();

		#endregion Validation
	}
}
