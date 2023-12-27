using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model;
using Microsoft.EntityFrameworkCore;
using Vb.Api.Dto;
using Vb.Data;
using Vb.Data.Entity;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressesController : ControllerBase
{
    private readonly VbDbContext dbContext;

    public AddressesController(VbDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<Address>> Get()
    {
        return await dbContext.Set<Address>()
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<Address> Get(int id)
    {
        var address =  await dbContext.Set<Address>()
            .Include(x => x.Customer)
            .ThenInclude(c => c.Addresses)
            .Where(x => x.Id == id).FirstOrDefaultAsync();
       
        return address!;
    }

    [HttpPost]
    public async Task Post([FromBody] AddressDTO addressDto)
    {
        Customer customer = await dbContext.Set<Customer>()
                                           .Where(c => c.Id == addressDto.CustomerId)
                                           .SingleOrDefaultAsync() 
                                           ?? throw new NullReferenceException($"Customer not found with {addressDto.CustomerId}");
        Address address = new ()
        {
            CustomerId = addressDto.CustomerId,
            Address1 = addressDto.Address1,
            Address2 = addressDto?.Address2,
            Country = addressDto!.Country,
            City = addressDto!.City,
            County = addressDto.County,
            Customer = customer!
        };
        await dbContext.Set<Address>().AddAsync(address);
        await dbContext.SaveChangesAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] Address address)
    {
        var fromDb = await dbContext.Set<Address>()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync()
            ?? throw new NullReferenceException();
        fromDb.Address1 = address!.Address1;
        fromDb.City = address.City;

        await dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        var fromDb = await dbContext.Set<Address>().Where(x => x.Id == id).FirstOrDefaultAsync();
        fromDb.IsActive = false;
        await dbContext.SaveChangesAsync();
    }
}