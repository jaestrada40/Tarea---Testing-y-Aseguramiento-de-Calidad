using App.Entidades;

namespace Tests
{
    public class Tests
    {
        private Biblioteca _biblioteca;
        private Libro _libro1;
        private Libro _libro2;

        [SetUp]
        public void Setup()
        {
            _biblioteca = new Biblioteca();
            _libro1 = new Libro("1984", "George Orwell");
            _libro2 = new Libro("El Principito", "Antoine de Saint-Exupéry");
            _biblioteca.AgregarLibro(_libro1);
            _biblioteca.AgregarLibro(_libro2);
        }

        [Test]
        [Author ("Javier Estrada")]
        [Description (" Prueba que un libro disponible se preste correctamente y verifica que la propiedad EstaPrestado se actualice a true.")]
        public void PrestarLibro_LibroDisponible_PrestaLibroCorrectamente()
        {
            // Act
            _biblioteca.PrestarLibro("1984");

            // Assert
            Assert.IsTrue(_libro1.EstaPrestado);
        }

        [Test]
        [Author("Javier Estrada")]
        [Description ("Prueba que al intentar prestar un libro que ya está prestado, se lance una excepción con el mensaje adecuado.")]
        public void PrestarLibro_LibroNoDisponible_LanzaExcepcion()
        {
            // Act
            _biblioteca.PrestarLibro("1984"); 

            // Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _biblioteca.PrestarLibro("1984"));
            Assert.AreEqual("El libro ya está prestado.", exception.Message);
        }

        [Test]
        [Author("Javier Estrada")]
        [Description("Verifica que un libro prestado pueda devolverse correctamente y que la propiedad EstaPrestado se actualice a false.")]
        public void DevolverLibro_LibroPrestado_DevolveLibroCorrectamente()
        {
            // Act
            _biblioteca.PrestarLibro("1984"); 
            _biblioteca.DevolverLibro("1984"); 

            // Assert
            Assert.IsFalse(_libro1.EstaPrestado);
        }

        [Test]
        [Author("Javier Estrada")]
        [Description("Asegura que se lance una excepción al intentar devolver un libro que no ha sido prestado.")]
        public void DevolverLibro_LibroNoPrestado_LanzaExcepcion()
        {
            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _biblioteca.DevolverLibro("1984"));
            Assert.AreEqual("El libro no está prestado.", exception.Message);
        }

        [Test]
        [Author("Javier Estrada")]
        [Description("Prueba que el método ObtenerLibros retorne la lista de libros correctamente, verificando tanto la cantidad como la presencia de los libros específicos.")]
        public void ObtenerLibros_RetornaListaDeLibros()
        {
            // Act
            var libros = _biblioteca.ObtenerLibros();

            // Assert
            Assert.AreEqual(2, libros.Count); 
            Assert.Contains(_libro1, libros); 
            Assert.Contains(_libro2, libros); 
        }
    }
}