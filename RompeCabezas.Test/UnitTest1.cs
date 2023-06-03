using Moq;
using Rompecabezas.Logica.Servicios;
using Rompecabezas.Web.Controllers;

namespace RompeCabezas.Test
{
    public class UnitTest1
    {
        private readonly IEntityFrameworkService entityFrameworkService;
        [Fact]
        public void MiPrueba()
        {
            var mockAlmacenamiento = new Mock<IEntityFrameworkService>();
            mockAlmacenamiento.Setup(a => a.ObtenerDato()).Returns("Dato de ejemplo");

            _almacenamiento = mockAlmacenamiento.Object;
        }
    }
}