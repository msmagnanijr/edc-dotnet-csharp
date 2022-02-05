using GerenciamentoAniversario.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoAniversario
{

    // TODO: Refatorar os métodos para definir as responsabilidades. 
    // TODO: Implementar Repository Pattern?
    class Aniversario
    {
        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu) 
            {
                showMenu = menu();
            }
        }

        private static bool menu() { 
            
            Console.WriteLine("\n### Gerenciador de Aniversário ###\n");
            Console.WriteLine("Selecione uma das opções abaixo");
            Console.WriteLine("1 - Pesquisar pessoas ");
            Console.WriteLine("2 - Adicionar nova pessoa");
            Console.WriteLine("3 - Sair");

            switch (Console.ReadLine())
            {
                case "1":
                    listarPessoa();
                    return true;
                case "2":
                    adicionarPessoa();
                    return true;
                case "3":
                    return false;
                default:
                    return true;

            }

        }
 
        static void adicionarPessoa()
        {

            Console.WriteLine("Digite o nome da pessoa que deseja adicionar:");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o sobrenome da pessoa que deseja adicionar");
            string sobrenome = Console.ReadLine();

            Console.WriteLine("Digite a data do aniversário no formato dd/MM/yyyy");
            var dataNascimento = DateTime.Parse(Console.ReadLine());

            var pessoa = new Pessoa(nome, sobrenome, dataNascimento);

            // TODO: Validar corretamente a data de nascimento no TP03
            Console.WriteLine("Os dados abaixo estão corretos?");
            Console.WriteLine($"Nome: {nome} {sobrenome}");
            Console.WriteLine($"Data do Aniversário: {dataNascimento}");
            Console.WriteLine("1 - Sim");
            Console.WriteLine("2 - Não");
            var resposta = Console.ReadLine();
            if (resposta == "1")
            {
                using (var db = new EFContext())
                {

                    db.Add(pessoa);
                    db.SaveChanges();
                }
                Console.WriteLine($"O cadastro do(a) aniversariante {nome} {sobrenome} foi realizado com sucesso!");
            }
            else 
            {
                Console.WriteLine("Retornando ao menu inicial!");
                return;
            }
        }

        static void listarPessoa()
        {
            Console.WriteLine("Digite o nome, ou parte do nome da pessoa que deseja encontrar:");
            string nome = Console.ReadLine();

            using (var db = new EFContext())
            {
                var query = from e in db.Pessoas
                            where EF.Functions.Like(e.Nome, nome+"%")
                            select e;

                Console.WriteLine(query.Count());
                List<Pessoa> pessoas = query.ToList();
                Console.WriteLine("Selecione uma das opções abaixo para visualizar os dados de uma das pessoas encontradas:\n");
                foreach (Pessoa p in pessoas)
                {
                    Console.WriteLine("{0} - {1} {2}", p.Id, p.Nome, p.Sobrenome);
                    
                }
                var idPessoa = Console.ReadLine();
                var key = int.Parse(idPessoa);

                Pessoa pessoa = db.Pessoas.Find(key);
                Console.WriteLine("\n--- Dados do(a) Aniversariante ---\n");
                Console.WriteLine($"Nome completo: {pessoa.Nome} {pessoa.Sobrenome}");
                Console.WriteLine($"Data de Aniversário {pessoa.DataNascimento}");
                Console.WriteLine($"Faltam {Utilities.diasAniversario(pessoa.DataNascimento)} para esse aniversário");
                return;
            }
        }

    }
}

