using Rompecabezas.Logica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rompecabezas.Logica.Servicios
{
    public class EntityFrameworkService : IEntityFrameworkService
    {
        private readonly RompeCabezaPw3Context _context;

        public EntityFrameworkService(RompeCabezaPw3Context context )
        {
            _context = context;
        }

        public void AddSala(Sala sala)
        {
            _context.Salas.Add( sala );
            _context.SaveChanges();
        }

        public Sala? GetSalaById(int id) => _context.Salas.Where(u => u.Id == id).First();

        public Sala GetSalaByNumber(int nroSala) => _context.Salas.Where(u => u.NroSala == nroSala).First();

        public List<Sala> GetAllSala() => _context.Salas.ToList();  
 
    }
}
