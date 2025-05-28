using EventApi.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventApi.Models;

public class EventsDto
{
    public int Id { get; set; }
    public string EventName { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }
    public string Description { get; set; } = null!;
    public decimal Price { get; set; } = 0;

    public int AddressId { get; set; } // foreign key to class Address. connects class Event to the id-row in the class Address
    public AddressDto Address { get; set; } = null!;// navigations property that allows Event to access the address object
}
