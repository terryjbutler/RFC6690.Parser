using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace RFC6690.Parser.Tests
{
    [TestClass]
    public class ArchiveIsMementoExample
    {
        public RFC6690ParserResponse Response { get; set; }

        [TestInitialize]
        public void Init()
        {
            var text = System.IO.File.ReadAllText("./files/archive-is-memento-example.txt");

            var parser = new RFC6690Parser();

            Response = parser.Parse(text);
        }

        [TestMethod]
        public void Contains_403_Records()
        {
            Assert.AreEqual(403, Response.ConstrainedLinks.Count());
        }

        // 
        // First:
        // <https://www.bbc.co.uk/>; rel="original",
        // 

        [TestMethod]
        public void Check_First_Record_Link()
        {
            var test = Response.ConstrainedLinks[0];

            Assert.AreEqual("https://www.bbc.co.uk/", test.Link);
        }

        [TestMethod]
        public void Check_First_Record_Param_Count()
        {
            var test = Response.ConstrainedLinks[0];

            Assert.AreEqual(1, test.Params.Count);
        }

        [TestMethod]
        public void Check_First_Record_Param_Rel()
        {
            Response.ConstrainedLinks[0].Params.TryGetValue("rel", out string value);

            Assert.AreEqual("original", value);
        }

        // 
        // Second (timegate):
        // <http://archive.md/timegate/https://www.bbc.co.uk/>; rel="timegate",
        // 

        [TestMethod]
        public void Check_Second_Record_Link()
        {
            var test = Response.ConstrainedLinks[1];

            Assert.AreEqual("http://archive.md/timegate/https://www.bbc.co.uk/", test.Link);
        }

        [TestMethod]
        public void Check_Second_Record_Param_Count()
        {
            var test = Response.ConstrainedLinks[1];

            Assert.AreEqual(1, test.Params.Count);
        }

        [TestMethod]
        public void Check_Second_Record_Param_Rel()
        {
            Response.ConstrainedLinks[1].Params.TryGetValue("rel", out string value);

            Assert.AreEqual("timegate", value);
        }

        // 
        // Third (first memento):
        // <http://archive.md/19961221203254/http://www.bbc.co.uk/>; rel="first memento"; datetime="Sat, 21 Dec 1996 20:32:54 GMT",
        // 

        [TestMethod]
        public void Check_Third_Record_Link()
        {
            var test = Response.ConstrainedLinks[2];

            Assert.AreEqual("http://archive.md/19961221203254/http://www.bbc.co.uk/", test.Link);
        }

        [TestMethod]
        public void Check_Third_Record_Param_Count()
        {
            var test = Response.ConstrainedLinks[2];

            Assert.AreEqual(2, test.Params.Count);
        }

        [TestMethod]
        public void Check_Third_Record_Param_Rel()
        {
            Response.ConstrainedLinks[2].Params.TryGetValue("rel", out string value);

            Assert.AreEqual("first memento", value);
        }

        [TestMethod]
        public void Check_Third_Record_Param_DateTime()
        {
            Response.ConstrainedLinks[2].Params.TryGetValue("datetime", out string value);

            Assert.AreEqual("Sat, 21 Dec 1996 20:32:54 GMT", value);
        }


        // 
        // 402 (last memento):
        // <http://archive.md/20210429141600/http://www.bbc.co.uk/>; rel="last memento"; datetime="Thu, 29 Apr 2021 14:16:00 GMT",
        // 

        [TestMethod]
        public void Check_402_Record_Link()
        {
            var test = Response.ConstrainedLinks[401];

            Assert.AreEqual("http://archive.md/20210429141600/http://www.bbc.co.uk/", test.Link);
        }

        [TestMethod]
        public void Check_402_Record_Param_Count()
        {
            var test = Response.ConstrainedLinks[401];

            Assert.AreEqual(2, test.Params.Count);
        }

        [TestMethod]
        public void Check_402_Record_Param_Rel()
        {
            Response.ConstrainedLinks[401].Params.TryGetValue("rel", out string value);

            Assert.AreEqual("last memento", value);
        }

        [TestMethod]
        public void Check_402_Record_Param_DateTime()
        {
            Response.ConstrainedLinks[401].Params.TryGetValue("datetime", out string value);

            Assert.AreEqual("Thu, 29 Apr 2021 14:16:00 GMT", value);
        }


        // 
        // 403 (timemap):
        // <http://archive.md/timemap/https://www.bbc.co.uk/>; rel="self"; type="application/link-format"; from="Sat, 21 Dec 1996 20:32:54 GMT"; until="Thu, 29 Apr 2021 14:16:00 GMT"
        // 

        [TestMethod]
        public void Check_403_Record_Link()
        {
            var test = Response.ConstrainedLinks[402];

            Assert.AreEqual("http://archive.md/timemap/https://www.bbc.co.uk/", test.Link);
        }

        [TestMethod]
        public void Check_403_Record_Param_Count()
        {
            var test = Response.ConstrainedLinks[402];

            Assert.AreEqual(4, test.Params.Count);
        }

        [TestMethod]
        public void Check_403_Record_Param_Rel()
        {
            Response.ConstrainedLinks[402].Params.TryGetValue("rel", out string value);

            Assert.AreEqual("self", value);
        }

        [TestMethod]
        public void Check_403_Record_Param_Type()
        {
            Response.ConstrainedLinks[402].Params.TryGetValue("type", out string value);

            Assert.AreEqual("application/link-format", value);
        }

        [TestMethod]
        public void Check_403_Record_Param_From()
        {
            Response.ConstrainedLinks[402].Params.TryGetValue("from", out string value);

            Assert.AreEqual("Sat, 21 Dec 1996 20:32:54 GMT", value);
        }

        [TestMethod]
        public void Check_403_Record_Param_Until()
        {
            Response.ConstrainedLinks[402].Params.TryGetValue("until", out string value);

            Assert.AreEqual("Thu, 29 Apr 2021 14:16:00 GMT", value);
        }

    }
}
