using NUnit.Framework;
using RailroadWeb.Entities;
using System;


namespace RailroadWebTest
{
    [TestFixture]
    public class RailTests
    {
        [TestCase("ÏÅÒĞÎÇÀÂÎÄÑÊ", "ÏÅÒĞÎÇÀÂÎÄÑÊ")]
        [TestCase("ïÅÒĞÎÇÀÂÎÄÑÊ", "Ïåòğîçàâîäñê")]
        [TestCase("", "")]
        [TestCase(null, null)]
        public void CreateRailWithInvalidArgumentsExpectArgumentException(string station1, string station2)
        {
            // Act and Assert
            Assert.Throws(typeof(ArgumentException), () => new Rail(station1, station2));
        }

        [TestCase("ÏÅÒĞÎÇÀÂÎÄÑÊ", "ØÓÉÑÊÀß")]
        [TestCase("Ïåòğîçàâîäñê", "Øóéñêàÿ")]
        public void GetHashCodeForTwoRailWithSwapOfStationExpectSuccess(string station1,string station2)
        {
            //Arrange
            var Rail1 = new Rail(station1, station2);
            var Rail2 = new Rail(station2.ToUpper(), station1.ToUpper());

            //Act
            var hash1 = Rail1.GetHashCode();
            var hash2 = Rail2.GetHashCode();

            //Assert
            Assert.IsTrue(hash1.Equals(hash2));
        }

        [TestCase("ÏÅÒĞÎÇÀÂÎÄÑÊ", "ØÓÉÑÊÀß", "ØÓÉÑÊÀß", "ÏÅÒĞÎÇÀÂÎÄÑÊ")]
        [TestCase("ÏÅÒĞÎÇÀÂÎÄÑÊ", "ØÓÉÑÊÀß", "Ïåòğîçàâîäñê", "Øóéñêàÿ")]
        [TestCase("ÏÅÒĞÎÇÀÂÎÄÑÊ", "ØÓÉÑÊÀß", "Øóéñêàÿ", "Ïåòğîçàâîäñê")]
        public void IsEqualsForTwoRailWithSwapOfStationExpectSuccess(string station1, string station2, string station3, string station4)
        {
            //Arrange
            var Rail1 = new Rail(station1, station2);
            var Rail2 = new Rail(station3, station4);

            // Act and Assert
            Assert.IsTrue(Rail1.Equals(Rail2));
        }
    }
}