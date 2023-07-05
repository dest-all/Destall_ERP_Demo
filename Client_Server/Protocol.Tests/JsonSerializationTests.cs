using Common.Extensions.Object;
using Protocol.Models;
using Protocol.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Protocol.Tests;

public class JsonSerializationTests
{
    [Test]
    public void SerializeAndDeserialize()
    {
        var employee = new EmployeeModel()
        {
            FirstName = "Mark"
            ,
            LastName = "Wahlberg",
            Reference = new EmployeeReference
            {
                Id = 12,
                Representation = "Repr"
            }
        };

        var checkSum0 = employee.ComputeChecksum();

        var json = employee.ToJson();

        employee = json.ParseAsJson<EmployeeModel>();

        var checkSum1 = employee.ComputeChecksum();

        Assert.AreEqual(checkSum0, checkSum1);
    }

}
