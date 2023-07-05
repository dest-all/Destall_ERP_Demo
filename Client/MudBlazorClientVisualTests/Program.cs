using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazorClientVisualTests;
using MudBlazor.Services;
using Client.Web.View.Services;
using Protocol.Models.Entities.ReferrableEntities.DataHolders.Persons.Employee;
using Protocol.Models.Entities.ReferrableEntities.DataHolders.Good;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var goods = new string[]
{
    "Pencil",
    "Paper",
    "Stappler",
    "Computer"
}.Select((name, i) => new GoodModel
{
    Name = name,
    Reference = new GoodReference
    {
        ID = (uint)i,
        Representation = name
    }
} as IGoodModel).ToList();


var employees = new ValueTuple<string, string>[]
{
    new("Igor", "Zhurbenko"),
    new("Alexander", "Duzhin"),
    new("Petr", "Maikov"),
    new("Inna", "Petrenko")
}.Select((names, i) => new EmployeeModel
{
    FirstName = names.Item1,
    LastName = names.Item2,
    Reference = new EmployeeReference
    {
        ID = (uint)i+1,
        Representation = $"{names.Item1} {names.Item2}"
    }
} as IEmployeeModel).ToList();

const int pageSize = 5;

builder.Services.AddSingleton<IStandardAccessor<IEmployeeModel>>(sp => new StandardAccessor<IEmployeeModel>(
    async employee => { employees.Add(employee); return employee; },
    async id => 
    {
        employees.RemoveAll(e => e.Reference.ID == id);
    },
    async id => employees.FirstOrDefault(e=>e.Reference.ID == id),
    async (stringFilter, pageNumber) => employees.Where(e=>e.Reference.Representation.ToLower().Contains((stringFilter ?? "").ToLower())).Skip(pageNumber * pageSize).Take(pageSize).ToArray(),
    async employee => { employees.RemoveAll(e => e.Reference.ID == employee.Reference.ID); employees.Add(employee); return employee; })
);


builder.Services.AddSingleton<IReferencesGetter<IEmployeeReference>>(sp => new ReferencesGetter<IEmployeeReference>(async (stringFliter, page) =>
{
    var result = employees
    .Where(e => e.Reference.Representation.ToLower().Contains((stringFliter ?? "").ToLower())).Skip(page * pageSize).Take(pageSize)
    .Select(e => e.Reference).ToArray();
    return result;
}));
builder.Services.AddSingleton<IReferencesGetter<IGoodReference>>(sp => new ReferencesGetter<IGoodReference>(async (stringFliter, page) => goods
    .Where(e => e.Reference.Representation.ToLower().Contains((stringFliter ?? "").ToLower())).Skip(page * pageSize).Take(pageSize)
    .Select(e => e.Reference).ToArray())
);

builder.Services.AddMudServices();

await builder.Build().RunAsync();
