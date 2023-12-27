using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vb.Base.Entity;

namespace Vb.Api.Dto;

public class AddressDTO
{
    public int CustomerId { get; set; }
    public string Address1 { get; set; } = null!;
    public string? Address2 { get; set; }
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string? County { get; set; }
    public string PostalCode { get; set; } = null!;
    public bool IsDefault { get; set; }
}
