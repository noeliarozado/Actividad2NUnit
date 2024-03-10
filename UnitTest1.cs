using Actividad2.dto;
using Actividad2.gui;
using Actividad2.logic;
using System.Windows.Shapes;

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

        /// <summary>
        /// Verifica que una línea se añade a la lista de líneas
        /// </summary>
        [Test]
        public void TestAnadirLinea()
        {
            // Arrange
            //Obtiene el número total de líneas antes de agregar una nueva línea
            int totalLineasAntes = logicaBus.GetListaLineas().Count;
            Linea linea = new Linea(100, "Origen", "Destino", DateTime.Now, TimeSpan.FromMinutes(30));

            // Act
            // Agrega una nueva línea a la lista
            logicaBus.AnadirLineaALista(linea);
            int totalLineasDespues = logicaBus.GetListaLineas().Count;

            // Assert
            // Verifica que el número total de líneas después de agregar una nueva línea sea igual
            // al número total antes de la adición más uno (la nueva línea añadida)
            Assert.That((totalLineasAntes + 1), Is.EqualTo(totalLineasDespues));
        }

        /// <summary>
        /// Verifica que una parada se elimina de la lista de paradas
        /// </summary>
        [Test]
        public void TestBorrarParada()
        {
            // Arrange
            // Añade una parada a la lista de paradas
            Parada paradaBorrar = new Parada(200, "Municipio", TimeSpan.FromMinutes(15));
            logicaBus.AnadirParadaALista(paradaBorrar);

            // Act
            // Elimina la parada de la lista
            logicaBus.BorrarParada(paradaBorrar);
            List<Parada> listaParadas = logicaBus.GetListaParadas();

            // Assert
            // Verifica que la parada borrada no esté presente en la lista de paradas
            //Assert.That(listaParadas, Has.No.Member(paradaBorrar));
            bool paradaEncontrada = listaParadas.Any(p => p.Equals(paradaBorrar));
            Assert.That(paradaEncontrada, Is.False, "La parada borrada todavía está en la lista de paradas.");
        }

        /// <summary>
        /// Verifica que un itinerario se elimina de la lista de paradas
        /// </summary>
        [Test]
        public void TestBorrarItinerario()
        {
            // Arrange
            // Agrega un itinerario
            int numeroLinea = 300;
            Parada parada1 = new Parada(300, "Oviedo", TimeSpan.FromMinutes(15));
            Parada parada2 = new Parada(300, "Avilés", TimeSpan.FromMinutes(20));
            Parada parada3 = new Parada(300, "Gijón", TimeSpan.FromMinutes(25));

            logicaBus.AnadirParadaALista(parada1);
            logicaBus.AnadirParadaALista(parada2);
            logicaBus.AnadirParadaALista(parada3);

            // Act
            // Elimina todas las paradas del itinerario
            logicaBus.BorrarTodasParadas(numeroLinea);
            List<Parada> listaItinerarios = logicaBus.GetListaParadas();

            // Assert
            // Verifica que ninguna de las paradas de la lista de paradas tenga
            // el número de línea del itinerario que se borró
            Assert.That(listaItinerarios, Has.None.Matches<Parada>(parada => parada.NumeroLinea == numeroLinea));
            Assert.That(listaItinerarios, Has.All.Matches<Parada>(parada => parada.NumeroLinea != numeroLinea));
        }

        /// <summary>
        /// Verifica que las líneas se cargan correctamente desde el archivo CSV
        /// </summary>
        [Test]
        public void TestCargarCSVLineas()
        {
            // Arrange
            // Agrega líneas de prueba en el archivo CSV
            Linea linea1 = new Linea(500, "Origen1", "Destino1", DateTime.Now, TimeSpan.FromMinutes(20));
            Linea linea2 = new Linea(600, "Origen2", "Destino2", DateTime.Now, TimeSpan.FromMinutes(25));
            logicaBus.AnadirLineaALista(linea1);
            logicaBus.AnadirLineaALista(linea2);

            // Act
            // Carga las líneas del archivo CSV
            List<Linea> lineas = logicaBus.GetListaLineas();

            // Assert
            // Verifica que la lista de líneas no sea nula y que tenga al menos un elemento
            Assert.That(lineas, Is.Not.Null);
            Assert.That(lineas.Count, Is.GreaterThan(0));
        }
    }
}