namespace ApiLibros.DTOs
{
    public class Libros
    {
		public string Id { get; set; }
		public string Titulo { get; set; }
		public string Año { get; set; }
		public string Genero { get; set; }
		public int NumPaginas { get; set; }
		public Editoriales Editorial { get; set; }
		public Autores Autor { get; set; }

	}
}
