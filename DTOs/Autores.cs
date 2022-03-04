using System;

namespace ApiLibros.DTOs
{
    public class Autores
    {
        public string Id { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string CuidadProcedencia { get; set; }
        public string CorreElectronico { get; set; }
    }
}
