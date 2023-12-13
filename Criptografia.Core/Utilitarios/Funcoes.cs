using System.Globalization;
using System.Text;

namespace Criptografia.Core.Utilitarios
{
    internal static class Funcoes
    {
        internal static string RemoverAcentos(string texto)
        {
            var stringRetorno = new StringBuilder();
            var arrayTexto = texto.Normalize(NormalizationForm.FormD);
            foreach (char caractere in arrayTexto)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(caractere) != UnicodeCategory.NonSpacingMark)
                    stringRetorno.Append(caractere);
            }
            return stringRetorno.ToString();
        }

        internal static int GerarNumeroPrimo()
        {
            var randomico = new Random();
            var inteiroAleatorio = randomico.Next();

            if (inteiroAleatorio % 2 == 0)
                inteiroAleatorio++;

            return inteiroAleatorio;
        }
    }
}
