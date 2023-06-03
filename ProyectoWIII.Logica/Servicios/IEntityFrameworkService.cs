using Rompecabezas.Logica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rompecabezas.Logica.Servicios
{
    public interface IEntityFrameworkService
    {
        public void AddSala(Sala sala);
        public Sala GetSalaByNumber(int numberSala);
        public Sala? GetSalaById(int id);
        public List<Sala> GetAllSala();
    }
}
