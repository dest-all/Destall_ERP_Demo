using Business.Tests.Setup;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Protocol.Models.Filters;
using Protocol.Models.PaginationRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Tests;

[TestFixture]
public class Filtering : ActionBase
{
    [Test]
    public async Task FilterEmployees()
    {
        const string inputJson = @"{""PageNumber"":1,""PageSize"":50,""Orderings"":[],""Filter"":{""FirstName"":{},""LastName"":{},""Reference"":{""Representation"":{""Pattern"":""Neva""}},""Mentor"":{""Representation"":{}},""ResponsibleForGoods"":{""Any"":{""Representation"":{}},""All"":{""Representation"":{}}}}}";

        var input = JsonConvert.DeserializeObject<EmployeePaginationRequestModel>(inputJson);

        var result = await Business.Actions.Employee.GetPage.CallAsync(1, 50, input.Filter, Enumerable.Empty<string>());


    }
}
