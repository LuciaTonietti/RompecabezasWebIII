using Rompecabezas.Logica.Models;

namespace Rompecabezas.Logica.Servicios
{
    public class SalaServiceImpl : ISalaService
    {
        private readonly RompeCabezaPw3Context _context;

        public SalaServiceImpl(RompeCabezaPw3Context context )
        {
            _context = context;
        }

        public Sala? AgregarSala(Sala sala)
        { 
                Sala aAgregar = new()
                {
                    NickName = sala.NickName,
                    CantPieces = sala.CantPieces,
                    Pin = sala.Pin == null ? null : sala.Pin,
                    NroSala = GenerateRandomNumberSala()
                };
                _context.Salas.Add(aAgregar);
                _context.SaveChanges();
                return aAgregar;
            
        }

        public bool EstaReptidoElNickName(string? nickName)
        {
            Sala sala = _context.Salas.Where(u => u.NickName == nickName).FirstOrDefault();
            if (sala == null)
            {
                return false;
            }
            return true;
        }

        public Sala? ObtenerSalaPorId(int id) => _context.Salas.Where(u => u.Id == id).First();

        public Sala? ObtenerSalaPorNumero(int nroSala) => _context.Salas.Where(u => u.NroSala == nroSala).FirstOrDefault();

        public Sala ObtenerSala(int nroSala, string? pinIngresado, string? nombreUsuario)
        {
            Sala? sala = ObtenerSalaPorNumero(nroSala);
            if (sala != null)
            {
                if (EsValidoElPin(sala, pinIngresado) && EsValidoElNumeroDeSala(sala,nroSala))
                {
                    return sala;
                }
                else
                {
                    throw new Exception("El pin ingresado no es válido!");
                }
            }
            else 
            {
                throw new Exception("La sala no existe!"); 
            }
        }

        private bool EsValidoElNumeroDeSala(Sala sala, int nroSala)
        {
            return sala.NroSala.Equals(nroSala);
        }

        private bool EsValidoElPin(Sala sala, string? pinIngresado)
        {
            if (sala.Pin == null)
            {
                return true;
            }
            else
            {
                return sala.Pin.Equals(pinIngresado);
            }
        }

        private int GenerateRandomNumberSala()
        {
            int randomSalaNumber = 0;
            bool repetido = true;
            while (repetido == true)
            {
                int random = new Random().Next(100000, 999999);
                if (ObtenerSalaPorNumero(random) == null)
                {
                    repetido = false;
                    randomSalaNumber = random;
                }
            }
            return randomSalaNumber;
        }
    }
}
