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
        /// Verifica que una l�nea se a�ade a la lista de l�neas
        /// </summary>
        [Test]
        public void TestAnadirLinea()
        {
            // Arrange
            //Obtiene el n�mero total de l�neas antes de agregar una nueva l�nea
            int totalLineasAntes = logicaBus.GetListaLineas().Count;
            Linea linea = new Linea(100, "Origen", "Destino", DateTime.Now, TimeSpan.FromMinutes(30));

            // Act
            // Agrega una nueva l�nea a la lista
            logicaBus.AnadirLineaALista(linea);
            int totalLineasDespues = logicaBus.GetListaLineas().Count;

            // Assert
            // Verifica que el n�mero total de l�neas despu�s de agregar una nueva l�nea sea igual
            // al n�mero total antes de la adici�n m�s uno (la nueva l�nea a�adida)
            Assert.That((totalLineasAntes + 1), Is.EqualTo(totalLineasDespues));
        }

        /// <summary>
        /// Verifica que una parada se elimina de la lista de paradas
        /// </summary>
        [Test]
        public void TestBorrarParada()
        {
            // Arrange
            // A�ade una parada a la lista de paradas
            Parada paradaBorrar = new Parada(200, "Municipio", TimeSpan.FromMinutes(15));
            logicaBus.AnadirParadaALista(paradaBorrar);

            // Act
            // Elimina la parada de la lista
            logicaBus.BorrarParada(paradaBorrar);
            List<Parada> listaParadas = logicaBus.GetListaParadas();

            // Assert
            // Verifica que la parada borrada no est� presente en la lista de paradas
            //Assert.That(listaParadas, Has.No.Member(paradaBorrar));
            bool paradaEncontrada = listaParadas.Any(p => p.Equals(paradaBorrar));
            Assert.That(paradaEncontrada, Is.False, "La parada borrada todav�a est� en la lista de paradas.");
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
            Parada parada2 = new Parada(300, "Avil�s", TimeSpan.FromMinutes(20));
            Parada parada3 = new Parada(300, "Gij�n", TimeSpan.FromMinutes(25));

            logicaBus.AnadirParadaALista(parada1);
            logicaBus.AnadirParadaALista(parada2);
            logicaBus.AnadirParadaALista(parada3);

            // Act
            // Elimina todas las paradas del itinerario
            logicaBus.BorrarTodasParadas(numeroLinea);
            List<Parada> listaItinerarios = logicaBus.GetListaParadas();

            // Assert
            // Verifica que ninguna de las paradas de la lista de paradas tenga
            // el n�mero de l�nea del itinerario que se borr�
            Assert.That(listaItinerarios, Has.None.Matches<Parada>(parada => parada.NumeroLinea == numeroLinea));
            Assert.That(listaItinerarios, Has.All.Matches<Parada>(parada => parada.NumeroLinea != numeroLinea));
        }

        /// <summary>
        /// Verifica que las l�neas se cargan correctamente desde el archivo CSV
        /// </summary>
        [Test]
        public void TestCargarCSVLineas()
        {
            // Arrange
            // Agrega l�neas de prueba en el archivo CSV
            Linea linea1 = new Linea(500, "Origen1", "Destino1", DateTime.Now, TimeSpan.FromMinutes(20));
            Linea linea2 = new Linea(600, "Origen2", "Destino2", DateTime.Now, TimeSpan.FromMinutes(25));
            logicaBus.AnadirLineaALista(linea1);
            logicaBus.AnadirLineaALista(linea2);

            // Act
            // Carga las l�neas del archivo CSV
            List<Linea> lineas = logicaBus.GetListaLineas();

            // Assert
            // Verifica que la lista de l�neas no sea nula y que tenga al menos un elemento
            Assert.That(lineas, Is.Not.Null);
            Assert.That(lineas.Count, Is.GreaterThan(0));
        }
    }
}