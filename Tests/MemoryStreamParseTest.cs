using System.IO;
using System.Linq;
using NUnit.Framework;
using OfxSharpLib;

namespace OFXSharp.Tests
{
    [TestFixture]
    public class MemoryStreamParseTest
    {
        [Test]
        public void CanParseMemoryStream()
        {
            var parser = new OfxDocumentParser();
            var bytes = File.ReadAllBytes(@"bb.ofx");
            var stream = new MemoryStream(bytes);

            var ofxDocument = parser.Import(stream);
            Assert.AreEqual(3, ofxDocument.Transactions.Count());
        }
    }
}
