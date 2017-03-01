using NUnit.Framework;
using System;
using System.IO;

namespace TeamContacts.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private static string assemblyPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private static string sampleFilePath = String.Format($"{assemblyPath}\\SampleInput\\SampleMasterContacts.xlsx");

        [Test]
        public void IsPathValidated()
        {
            FileNotFoundException ex = Assert.Throws<FileNotFoundException>(
                    delegate { new TeamDetails("nonexistentfile.xlsx"); });          
        }

        [Test]
        public void AreTeamDetailsPopulated()
        {
            var teamDetails = new TeamDetails(sampleFilePath);
            Assert.AreEqual(2, teamDetails.Players.Count);
        }
    }
}
