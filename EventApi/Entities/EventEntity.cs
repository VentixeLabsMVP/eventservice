using EventApi.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventApi.Entities;

public class EventEntity
{
    public int Id { get; set; }
    public string EventName { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime? StartDate { get; set; }
    [Column(TypeName = "date")]
    public DateTime? EndDate { get; set; }
    public string Description { get; set; } = null!;
    public decimal Price { get; set; } = 0;


    public int? AddressId { get; set; } // foreign key
    public Address? Address { get; set; } // acces to address object properties
}
