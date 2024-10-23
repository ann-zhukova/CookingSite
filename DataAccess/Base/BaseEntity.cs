using System.ComponentModel.DataAnnotations;

namespace DataAccess.Base;

internal abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
}
