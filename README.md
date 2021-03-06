# RFC6690.Parser

Basic implementation for parsing Constrained RESTful Environments (CoRE) Link Format (RFC6690).

See here for more details: https://tools.ietf.org/html/rfc6690

## Example

The provided demo unit test is as follows:

```csharp
[TestClass]
public class DemoUnitTest
{
    [TestMethod]
    public void Demo()
    {
        var parser = new RFC6690Parser();

        var data = "</sensors>;ct=40;title=\"Sensor Index\"," +
            "</sensors/temp>;rt=\"temperature-c\";if=\"sensor\"," +
            "</sensors/light>;rt=\"light-lux\";if=\"sensor\"," +
            "<http://www.example.com/sensors/t123>;anchor=\"/sensors/temp\"" +
            ";rel=\"describedby\"," +
            "</t>;anchor=\"/sensors/temp\";rel=\"alternate\"";

        var response = parser.Parse(data);

        Assert.AreEqual(5, response.ConstrainedLinks.Count);
        Assert.AreEqual("http://www.example.com/sensors/t123", response.ConstrainedLinks[3].Link);
        Assert.AreEqual("/sensors/temp", response.ConstrainedLinks[3].Params["anchor"]);
        Assert.AreEqual("describedby", response.ConstrainedLinks[3].Params["rel"]);
    }
}
```