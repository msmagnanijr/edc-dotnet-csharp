using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoAniversario
{
    public static class Utilities
    {

        public static int diasAniversario(DateTime dataNascimento)
        {
            var data = dataNascimento;
            var hoje = DateTime.Now;
            var futuro = Convert.ToDateTime($"{data.Day}, {data.Month}, {hoje.Year}");
            DateTime dataFuturo = Convert.ToDateTime($"{data.Day}, {data.Month}, {hoje.Year + 1}");
            
            int resultado = (futuro - hoje).Days;

            if (resultado < 0)
            {
                futuro = Convert.ToDateTime($"{data.Day}, {data.Month}, {dataFuturo.Year}");
            }

            resultado = (futuro - hoje).Days;
            
            return resultado;
        }
    }
}
