using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Trivia;

namespace CharTests
{
    [TestFixture]
    public class CharacterizationTests
    {
        [Test]
        [TestCaseSource("Seeds")]
        public void Test(int seed)
        {
            string sutOutput;
            using (var stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                GameRunner.Main(new[] { seed.ToString() });
                stringWriter.Flush();
                sutOutput = stringWriter.ToString();
            }

            var referenceOutput = File.ReadAllText(Path.Combine(ReferenceOutputDirectory(), seed.ToString()));

            Assert.That(sutOutput, Is.EqualTo(referenceOutput));
        }

        private static IEnumerable<int> Seeds()
        {
            return Directory.GetFiles(ReferenceOutputDirectory())
                .Select(Path.GetFileName)
                .Select(int.Parse);
        }

        private static string ReferenceOutputDirectory()
        {
            return Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "..", "..", "refout");
        }
    }
}