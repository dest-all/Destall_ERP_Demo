using MemoryPack;
using Protocol.Models;
using Protocol.Models.People;

namespace Protocol.Tests
{
    public class MemoryPackSerialization
    {
        [Test]
        public void SerializeProtocolMessageTuple()
        {
            var employee = new EmployeeModel
            {
                FirstName = "first",
                LastName = "last"
            };

            var protocolMessage = ProtocolMessage.FromMessage(employee);

            var bytes = MemoryPackSerializer.Serialize(protocolMessage.ToTuple());

            var employeeDeserialized = MemoryPackSerializer.Deserialize<ValueTuple<ProtocolMessageAddin, EmployeeModel>>(bytes);

            Assert.AreEqual(employeeDeserialized.Item2.ComputeChecksum(), employee.ComputeChecksum());
        }
    }
}