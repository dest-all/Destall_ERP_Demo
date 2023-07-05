using DestallMaterials.WheelProtection.DataWorks;

namespace Data.Tests;

public class Tests
{

    [Test]
    public void GetAllChunks()
    {
        const string input = "abcdef";

        var result = IndexCreation.GetAllChunks(input);

        foreach (var chunk in result)
        {
            Assert.IsTrue(input.Contains(chunk));
        }
    }
}