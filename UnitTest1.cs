using Actividad2.dto;
using Actividad2.gui;
using Actividad2.logic;

//

namespace Actividad2NUnit
{
    public class Tests
    {
        private LogicaBus logicaBus;

        [SetUp]
        public void Setup()
        {
            logicaBus = new LogicaBus();
        }

        //[Test]
        //public void Test1()
        //{
        //    Assert.Pass();
        //}

        [Test]
        public void TestAnadirLinea()
        {
            // Arrange
            int totalLineasAntes = logicaBus.GetListaLineas().Count;
            Linea linea = new Linea(100, "Origen", "Destino", DateTime.Now, TimeSpan.FromMinutes(30));

            // Act
            logicaBus.AnadirLineaALista(linea);
            int totalLineasDespues = logicaBus.GetListaLineas().Count;

            // Assert
            Assert.That((totalLineasAntes + 1), Is.EqualTo(totalLineasDespues));
        }

        [Test]
        public void TestBorrarParada()
        {
            // Arrange
            Parada paradaBorrar = new Parada(200, "Municipio", TimeSpan.FromMinutes(15));
            logicaBus.AnadirParadaALista(paradaBorrar);

            // Act
            logicaBus.BorrarParada(paradaBorrar);
            List<Parada> listaParadas = logicaBus.GetListaParadas();

            // Assert
            //Assert.That(listaParadas, Has.No.Member(paradaBorrar));
            bool paradaEncontrada = listaParadas.Any(p => p.Equals(paradaBorrar));
            Assert.That(paradaEncontrada, Is.False, "La parada borrada todavía está en la lista de paradas.");
        }

        [Test]
        public void TestBorrarItinerario()
        {
            // Arrange
            int numeroLinea = 300;
            Parada parada1 = new Parada(300, "Oviedo", TimeSpan.FromMinutes(15));
            Parada parada2 = new Parada(300, "Avilés", TimeSpan.FromMinutes(20));
            Parada parada3 = new Parada(300, "Gijón", TimeSpan.FromMinutes(25));

            logicaBus.AnadirParadaALista(parada1);
            logicaBus.AnadirParadaALista(parada2);
            logicaBus.AnadirParadaALista(parada3);

            // Act
            logicaBus.BorrarTodasParadas(numeroLinea);
            List<Parada> listaItinerarios = logicaBus.GetListaParadas();

            // Assert
            Assert.That(listaItinerarios, Has.None.Matches<Parada>(parada => parada.NumeroLinea == numeroLinea));
            Assert.That(listaItinerarios, Has.All.Matches<Parada>(parada => parada.NumeroLinea != numeroLinea));
        }

        [Test]
        public void TestCargarCSVLineas()
        {
            // Arrange
            Linea linea1 = new Linea(500, "Origen1", "Destino1", DateTime.Now, TimeSpan.FromMinutes(20));
            Linea linea2 = new Linea(600, "Origen2", "Destino2", DateTime.Now, TimeSpan.FromMinutes(25));
            logicaBus.AnadirLineaALista(linea1);
            logicaBus.AnadirLineaALista(linea2);

            // Act
            List<Linea> lineas = logicaBus.GetListaLineas();

            // Assert
            Assert.That(lineas, Is.Not.Null);
            Assert.That(lineas.Count, Is.GreaterThan(0));
        }
    }
}