using FluentAssertions;
using NUnit.Framework;
using OrdersNetwork.Core;
using OrdersNetwork.Core.Security;
using OrdersNetwork.Core.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Tests.Security
{
    [TestFixture]
    public class Sha256HashProviderTests
    {
        [Test]
        public void Generate_Hash()
        {
            //arrange
            var text = "iiaasdsadnnvasd";
            var stream = new StringStream();
            stream.Write(text.Substring(0, 5));
            stream.Write(text.Substring(5));

            var provider = new Sha256HashProvider();

            //act
            var hash = provider.GetHashOfStream(stream);

            //assert
            HashValue expected = "06BFF1A1";
            hash.Should().Be(expected);
        }
    }
}
