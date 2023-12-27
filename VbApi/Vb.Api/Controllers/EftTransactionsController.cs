using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Data.Entity;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EftTransactionsController : ControllerBase
{
    private readonly VbDbContext dbContext;

    public EftTransactionsController(VbDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<EftTransaction>> Get()
    {
        return await dbContext.Set<EftTransaction>()
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<EftTransaction> Get(int id)
    {
        var EftTransaction =  await dbContext.Set<EftTransaction>()
            .Where(x => x.Id == id).FirstOrDefaultAsync();
       
        return EftTransaction!;
    }

    [HttpPost]
    public async Task Post([FromBody] EftTransaction EftTransaction)
    {
        await dbContext.Set<EftTransaction>().AddAsync(EftTransaction);
        await dbContext.SaveChangesAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] EftTransaction eftTransaction)
    {
        var fromDb = await dbContext.Set<EftTransaction>().Where(x => x.Id == id).FirstOrDefaultAsync();
        fromDb.Amount = eftTransaction.Amount;

        await dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        var fromDb = await dbContext.Set<EftTransaction>().Where(x => x.Id == id).FirstOrDefaultAsync();
        await dbContext.SaveChangesAsync();
    }
}