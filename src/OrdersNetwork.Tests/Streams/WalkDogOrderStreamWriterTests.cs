using FluentAssertions;
using NUnit.Framework;
using OrdersNetwork.Core.Streams;
using OrdersNetwork.WalkDogModule.StreamBuilders;
using OrdersNetwork.WalkDogModule.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Tests.Streams
{
    [TestFixture]
    public class WalkDogOrderStreamWriterTests
    {
        [Test]
        public void Write_To_Stream()
        {
            //arrange
            var stream = new StringStream();

            var order = new WalkDogOrder(new Dog("Santa's Little Helper", true, false), "Springfield", new DateTime(2017, 03, 17), 17.89);
            var writer = new WalkDogOrderStreamWriter();

            //act
            writer.Write(stream, order);

            //assert
            var content = stream.GetContent();
            content.Should().Be("Santa's Little Helper (small) in Springfield at 2017-03-17T00:00:00.0000000 for 17.89$");
        }
    }
}
