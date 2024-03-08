using Actividad2.logic;

namespace Actividad2NUnit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            LogicaBus bus = new LogicaBus();

            Console.WriteLine(bus);

            Assert.Pass(bus.ToString());

  
        }
    }
}