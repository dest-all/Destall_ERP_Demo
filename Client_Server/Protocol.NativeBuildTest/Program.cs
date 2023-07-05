using DestallMaterials.WheelProtection.Extensions.Objects;
using Protocol;
using Protocol.Models;
using Protocol.Models.Documents;
using Protocol.Models.People;
using static MemoryPack.MemoryPackSerializer;

const string sessionKey = "3242386_dsfd-34987324:834534****__===";

//Protocol(sessionKey);

//Primitives(sessionKey);

IncomingOrders();

return 0;

void Primitives<T>(T value)
{
    var keys = new T[]   {
        value,
        value
    };

    var bytes = MemoryPack.MemoryPackSerializer.Serialize(keys);

    var keysArray = MemoryPack.MemoryPackSerializer.Deserialize<List<T>>(bytes);
}

static void IncomingOrders()
{
    var incomingOrder = new IncomingOrderModel
    {
        Accountable = new EmployeeReference
        {
            Id = 10,
            Representation = "32432432"
        },
        Currency = new Protocol.Models.ReferrableEntities.CurrencyReference
        {
            Id = 19,
            Representation = "23432432"
        },
        Customer = new Protocol.Models.Counterparties.CustomerReference
        {
            Id = 500,
            Representation = "Cust"
        },
        GoodsSold = new List<Protocol.Models.GoodTransactionLines.IncomingOrderLineModel>
        {
            new Protocol.Models.GoodTransactionLines.IncomingOrderLineModel
            {
                Good = new Protocol.Models.DataHolders.GoodReference
                {
                    Id = 10003,
                    Representation = "Good"
                }
            }
        },
        Number = "2342",
        Reference = new IncomingOrderReference
        {
            Id = 19923,
            Representation = "Order"
        },
        Status = 13
    };

    var incomingOrders = incomingOrder.ToArrayOfOne();

    var checkSum = incomingOrder.ComputeChecksum();

    var bytes = Serialize(incomingOrders);

    incomingOrders = Deserialize<IncomingOrderModel[]>(bytes);

    Console.WriteLine(checkSum);

    Console.WriteLine(incomingOrders.Single().ComputeChecksum());
}

static void Protocol(string sessionKey)
{
    var employee = new EmployeeModel
    {
        FirstName = "Igor",
        LastName = "Zhurbenko",
        Reference = new()
        {
            Id = 150,
            Representation = "Representation"
        }
    };

    var protocolMessageGeneric = ProtocolMessage.FromMessage(employee);

    var protocolMessage = new EmployeeModelProtocolMessage
    {
        Addin = new ProtocolMessageAddin
        {
            SessionKey = sessionKey
        },
        Message = employee
    };

    var protocolMessageArray = protocolMessage.ToArrayOfOne();

    try
    {
        var bytes = Serialize(protocolMessage);
        var result = Deserialize<EmployeeModelProtocolMessage>(bytes);

        var arrayBytes = Serialize(protocolMessageArray);
        var arrayResult = Deserialize<EmployeeModelProtocolMessage[]>(arrayBytes);

        if (result.Message.ComputeChecksum() != employee.ComputeChecksum() || arrayResult[0].Message.ComputeChecksum() != employee.ComputeChecksum())
        {
            throw new Exception();
        }

        Console.WriteLine("Packing/unpacking non-generic succeeded.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Packing/unpacking non-generic failed. {ex}");
    }

    try
    {
        var bytes = protocolMessageGeneric.Pack();
        var result = ProtocolMessage<EmployeeModel>.Unpack(bytes);
        Console.WriteLine("Packing/unpacking generic succeeded.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Packing/unpacking generic failed.");
    }
}