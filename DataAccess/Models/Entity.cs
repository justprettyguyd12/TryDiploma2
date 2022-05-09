using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

internal abstract class Entity
{
    [Key]
    public Guid Id { get; set; }
}