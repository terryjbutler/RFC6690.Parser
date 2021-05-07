using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace RFC6690.Parser.Tests
{
    [TestClass]
    public class OfficialSensorExampleSingleLine
    {
        public RFC6690ParserResponse Response { get; set; }

        [TestInitialize]
        public void Init()
        {
            var text = System.IO.File.ReadAllText("./files/official-sensor-example-single-line.txt");

            var parser = new RFC6690Parser();

            Response = parser.Parse(text);
        }

        [TestMethod]
        public void Contains_5_Records()
        {
            Assert.AreEqual(5, Response.ConstrainedLinks.Count());
        }

        // 
        // First:
        // </sensors>;ct=40;title="Sensor Index",
        // 

        [TestMethod]
        public void Check_First_Record_Link()
        {
            var test = Response.ConstrainedLinks[0];

            Assert.AreEqual("/sensors", test.Link);
        }

        [TestMethod]
        public void Check_First_Record_Param_Count()
        {
            var test = Response.ConstrainedLinks[0];

            Assert.AreEqual(2, test.Params.Count);
        }

        [TestMethod]
        public void Check_First_Record_Param_Ct()
        {
            Response.ConstrainedLinks[0].Params.TryGetValue("ct", out string value);

            Assert.AreEqual("40", value);
        }

        [TestMethod]
        public void Check_First_Record_Param_Title()
        {
            Response.ConstrainedLinks[0].Params.TryGetValue("title", out string value);

            Assert.AreEqual("Sensor Index", value);
        }

        // 
        // Second:
        // </sensors/temp>;rt="temperature-c";if="sensor",
        // 

        [TestMethod]
        public void Check_Second_Record_Link()
        {
            var test = Response.ConstrainedLinks[1];

            Assert.AreEqual("/sensors/temp", test.Link);
        }

        [TestMethod]
        public void Check_Second_Record_Param_Count()
        {
            var test = Response.ConstrainedLinks[1];

            Assert.AreEqual(2, test.Params.Count);
        }

        [TestMethod]
        public void Check_Second_Record_Param_Rt()
        {
            Response.ConstrainedLinks[1].Params.TryGetValue("rt", out string value);

            Assert.AreEqual("temperature-c", value);
        }

        [TestMethod]
        public void Check_Second_Record_Param_If()
        {
            Response.ConstrainedLinks[1].Params.TryGetValue("if", out string value);

            Assert.AreEqual("sensor", value);
        }

        //
        // Third:
        // </sensors/light>;rt="light-lux";if="sensor",
        //

        [TestMethod]
        public void Check_Third_Record_Link()
        {
            var test = Response.ConstrainedLinks[2];

            Assert.AreEqual("/sensors/light", test.Link);
        }

        [TestMethod]
        public void Check_Third_Record_Param_Count()
        {
            var test = Response.ConstrainedLinks[2];

            Assert.AreEqual(2, test.Params.Count);
        }

        [TestMethod]
        public void Check_Third_Record_Param_Rt()
        {
            Response.ConstrainedLinks[2].Params.TryGetValue("rt", out string value);

            Assert.AreEqual("light-lux", value);
        }

        [TestMethod]
        public void Check_Third_Record_Param_If()
        {
            Response.ConstrainedLinks[2].Params.TryGetValue("if", out string value);

            Assert.AreEqual("sensor", value);
        }


        //
        // Fourth (over multiple lines):
        // <http://www.example.com/sensors/t123>;anchor="/sensors/temp"
        // ;rel="describedby",
        //

        [TestMethod]
        public void Check_Fourth_Record_Link()
        {
            var constrainedLink = Response.ConstrainedLinks[3];

            Assert.AreEqual("http://www.example.com/sensors/t123", constrainedLink.Link);
        }

        [TestMethod]
        public void Check_Fourth_Record_Param_Count()
        {
            var constrainedLink = Response.ConstrainedLinks[3];

            Assert.AreEqual(2, constrainedLink.Params.Count);
        }

        [TestMethod]
        public void Check_Fourth_Record_Param_Anchor()
        {
            Response.ConstrainedLinks[3].Params.TryGetValue("anchor", out string value);

            Assert.AreEqual("/sensors/temp", value);
        }

        [TestMethod]
        public void Check_Fourth_Record_Param_Rel()
        {
            Response.ConstrainedLinks[3].Params.TryGetValue("rel", out string value);

            Assert.AreEqual("describedby", value);
        }

        //
        // Fifth:
        // </t>;anchor="/sensors/temp";rel="alternate"
        //

        [TestMethod]
        public void Check_Fifth_Record_Link()
        {
            var constrainedLink = Response.ConstrainedLinks[4];

            Assert.AreEqual("/t", constrainedLink.Link);
        }

        [TestMethod]
        public void Check_Fifth_Record_Param_Count()
        {
            var constrainedLink = Response.ConstrainedLinks[4];

            Assert.AreEqual(2, constrainedLink.Params.Count);
        }

        [TestMethod]
        public void Check_Fifth_Record_Param_Anchor()
        {
            Response.ConstrainedLinks[4].Params.TryGetValue("anchor", out string value);

            Assert.AreEqual("/sensors/temp", value);
        }

        [TestMethod]
        public void Check_Fifth_Record_Param_Rel()
        {
            Response.ConstrainedLinks[4].Params.TryGetValue("rel", out string value);

            Assert.AreEqual("alternate", value);
        }
    }
}