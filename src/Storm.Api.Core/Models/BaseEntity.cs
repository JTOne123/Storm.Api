using System;
using ServiceStack.DataAnnotations;

namespace Storm.Api.Core.Models
{
	public interface IEntity
	{
		long Id { get; set; }
		Guid CollationId { get; set; }
		DateTime EntityCreatedDate { get; set; }
		DateTime? EntityUpdatedDate { get; set; }
		bool IsDeleted { get; set; }
		DateTime? EntityDeletedDate { get; set; }
	}

	public abstract class BaseEntity : IEntity
	{
		[PrimaryKey]
		public long Id { get; set; }

		[Index]
		public Guid CollationId { get; set; }

		[Index]
		public DateTime EntityCreatedDate { get; set; }

		public DateTime? EntityUpdatedDate { get; set; }

		[Index]
		public bool IsDeleted { get; set; }

		public DateTime? EntityDeletedDate { get; set; }
	}
}