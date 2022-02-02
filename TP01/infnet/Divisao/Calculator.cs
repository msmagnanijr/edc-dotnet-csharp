//TP1 - Calcula a divisão de dois números
using System;
namespace infnet
{
    class Calculator
    {
        static void Main()
        {
            try
            {

                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
                Console.WriteLine("Ambiente configurado para os padrões Brasileiros! Utilize ',' para números com casas decimais ou o resultado será inesperado!\n");

                Console.WriteLine("Essa aplicação calcula a divisão de dois números!\n");

                Console.WriteLine("Digite o primeiro número:");
                var firstValue = Console.ReadLine();

                Console.WriteLine("Digite o segundo número:");
                var secondValue = Console.ReadLine();
                //TODO: Validar se o usuário utilizou corretamente o separador de casa decimais
                if (double.TryParse(firstValue, out double firttNumber) && double.TryParse(secondValue, out double secondNumber))
                {
                    var result = firttNumber / secondNumber;
                    Console.WriteLine($"O resultado da divisão dos números {firttNumber} e {secondNumber} é: {result:0.##} !");
                }
                else
                {
                    throw new ArgumentException("O valor digitado é inválido!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
