using WebATomataoes.ViewModels;

namespace WebATomataoes.Repositories

{
    public static class FilmeRepository
    {
        private static List<FilmesViewModel> filmesList = new List<FilmesViewModel>();

        public static List<FilmesViewModel> GetAll()
        {
            return filmesList;
        }

        public static FilmesViewModel GetById(Guid id)
        {
            FilmesViewModel? retorno = null;
            Console.WriteLine("Meu id: ", id);
            if (!id.Equals(""))
            {
                retorno = filmesList.FirstOrDefault(p => p.Id.Equals(id));
            }

            if (retorno == null) retorno = new FilmesViewModel();

            return retorno;
        }

        public static List<FilmesViewModel> GetBySearch(string searchString)
        {
            List<FilmesViewModel>? retorno = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                retorno = filmesList.Where(p => p.Nome.Contains(searchString)).ToList();
            }

            if (retorno == null) retorno = new List<FilmesViewModel>();

            return retorno;
        }

        public static void Create(FilmesViewModel aluno)
        {
            filmesList.Add(aluno);
        }

        public static void Update(FilmesViewModel filme)
        {
            FilmesViewModel? retorno = null;

            if (!filme.Id.Equals(""))
            {
                retorno = filmesList.FirstOrDefault(p => p.Id.Equals(filme.Id));

                if (retorno != null)
                {
                    retorno.Nome = filme.Nome;
                    retorno.Estudio = filme.Estudio;
                    retorno.DataLancamento = filme.DataLancamento;
                }
            }
        }

        public static void Delete(Guid id)
        {
            FilmesViewModel? retorno = null;

            if (!id.Equals("")) retorno = filmesList.FirstOrDefault(p => p.Id.Equals(id));
            if (retorno != null) filmesList.Remove(retorno);
        }
    }
}
