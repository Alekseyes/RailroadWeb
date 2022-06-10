using NUnit.Framework;
using RailroadWeb.Computation;
using RailroadWeb.Entities;
using RailroadWeb.Enum;
using System.Collections.Generic;

namespace RailroadWebTest
{
    [TestFixture]
    public class ComputationTests
    {
        private CrashComputationService _service;

        private Web _web;

        [SetUp]
        public void Setup()
        {
            _web = new Web();
            var stations = new HashSet<string>()
            {
            "оерпнгюбндяй",
            "рнлхжш",
            "ьсияйюъ",
            "ясмю",
            "гюдекэе",
            "йнмднонцю",
            "йедпнгепн",
            "мнбши оняекнй",
            "мхцнгепн",
            "йъооеяекэцю",
            "оепцсаю",
            "ледбеф цнпю",
            "бхвйю",
            "яецефю",
            "мюдбнхжш",
            "йнвйнлю",
            "хдекэ",
            "кермхи",
            "янямнбеж",
            "ясляйхи оняюд"
            };

            foreach (var station in stations)
            {
                foreach (var anotherStation in stations)
                {
                    if (station.Equals(anotherStation))
                    {
                        continue;
                    }
                    _web.RailRoads[new Rail(station, anotherStation)] = 2;
                }
            }            
            _service = new CrashComputationService(_web);
        }

        [TestCase("оерпнгюбндяй", "рнлхжш", "ьсияйюъ")]
        public void ComputeForRouteWhereIsNoRailsExpectInvalidInputData(params string[] stations)
        {
            //Arrange
            Route testRoute = new Route(stations);
            _web.RailRoads.Remove(new Rail("рнлхжш", "ьсияйюъ"));

            //Act
            var response = _service.ComputeForRoute(testRoute);

            //Assert
            Assert.IsTrue(response.Equals(ComputationResponse.InvalidInputData));
        }

        [TestCase("оюпхф", "лекэаспм", "ьсияйюъ")]
        [TestCase("")]
        [TestCase("", "ьсияйюъ")]
        [TestCase(null)]
        [TestCase(null,null)]
        [TestCase(null, "ьсияйюъ")]
        public void ComputeForRouteWithWrongStationsExpectInvalidInputData(params string[] stations)
        {
            //Arrange
            Route testRoute = new Route(stations);

            //Act
            var response = _service.ComputeForRoute(testRoute);

            //Assert
            Assert.IsTrue(response.Equals(ComputationResponse.InvalidInputData));
        }

        [Test]
        public void ComputeForOneRouteExpectSuccess()
        {
            //Arrange
            Route testRoute = new Route("оерпнгюбндяй", "рнлхжш", "ьсияйюъ");

            //Act
            var response = _service.ComputeForRoute(testRoute);

            //Assert
            Assert.IsTrue(response.Equals(ComputationResponse.Success));
        }

        [Test]
        public void ComputeForTwoRoutesExpectSuccess()
        {
            //Arrange
            Route testRoute1 = new Route("оерпнгюбндяй", "рнлхжш","ьсияйюъ");
            Route testRoute2 = new Route("рнлхжш", "ьсияйюъ");

            //Act
            _ = _service.ComputeForRoute(testRoute1);
            var response = _service.ComputeForRoute(testRoute2);

            //Assert
            Assert.IsTrue(response.Equals(ComputationResponse.Success));
        }

        [Test]
        public void ComputeForTwoOppositeRoutesExpectCrash()
        {
            //Arrange
            Route testRoute1 = new Route("оерпнгюбндяй", "рнлхжш", "ьсияйюъ");
            Route testRoute2 = new Route("ьсияйюъ", "рнлхжш", "оерпнгюбндяй");

            //Act
            _ = _service.ComputeForRoute(testRoute1);
            var response = _service.ComputeForRoute(testRoute2);

            //Assert
            Assert.IsTrue(response.Equals(ComputationResponse.Crash));
        }

        [Test]
        public void ComputeForTwoRoutesExpectCrashAtTheInitialStation()
        {
            //Arrange
            Route testRoute1 = new Route("оерпнгюбндяй", "рнлхжш");
            Route testRoute2 = new Route("оерпнгюбндяй", "рнлхжш");

            //Act
            _ = _service.ComputeForRoute(testRoute1);
            var response = _service.ComputeForRoute(testRoute2);

            //Assert
            Assert.IsTrue(response.Equals(ComputationResponse.Crash));
        }

        [Test]
        public void ComputeForTwoRoutesExpectCrashAtTheFinalStation()
        {
            //Arrange
            Route testRoute1 = new Route("оерпнгюбндяй", "рнлхжш");
            Route testRoute2 = new Route("ьсияйюъ", "рнлхжш");

            //Act
            _ = _service.ComputeForRoute(testRoute1);
            var response = _service.ComputeForRoute(testRoute2);

            //Assert
            Assert.IsTrue(response.Equals(ComputationResponse.Crash));
        }
    }
}