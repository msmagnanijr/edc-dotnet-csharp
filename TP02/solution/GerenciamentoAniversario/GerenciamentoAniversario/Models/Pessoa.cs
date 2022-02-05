namespace GerenciamentoAniversario.Models
{
    public class Pessoa
    {

        public Pessoa(string nome, string sobrenome, DateTime dataNascimento)
        { 
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;

        }
        public Pessoa() { 
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }

    }
}
