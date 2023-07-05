using Client.Web.View;
using Common.Extensions.Object;
using Newtonsoft.Json;

namespace Client.LibTesting
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AppConfigSerDes()
        {
            var appConfig = new AppConfiguration();

            const string json = "{\\n  \\"ProtocolOptions\\": {\\n    \\"Compress\\": true,\\n    \\"UseMemoryPack\\": true\\n  }\\n}"

            appConfig = JsonConvert.DeserializeObject<AppConfiguration>(json);
        }
    }
}