using Business.Actions;
using Business.Selectors;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using Microsoft.EntityFrameworkCore;
using Protocol.Models.Filters;
using Protocol.Models.StatusMovementRegistryEntries;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.RegisterEntries;

[ProcessServerRequest]
public partial class StockEntries : ActionContainer
{
    public async Task<IReadOnlyList<IStockEntryReadOnlyModel>> GetStockInfo(IStockEntryFilterReadOnlyModel filter)
    {
        using var repo = await GetRepositoryAsync();

        var expression = repo.Express(filter);

        var selector = StockEntrySelectors.ModelSelector(repo);

        var entries = await repo.StockEntries.Where(expression).Select(selector).Cast<IStockEntryReadOnlyModel>().ToArrayAsync();

        return entries;
    }

}

