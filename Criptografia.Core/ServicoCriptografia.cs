using Criptografia.Core.Excecao;
using Criptografia.Core.Utilitarios;
using System.Text;

namespace Criptografia.Core
{
    public class ServicoCriptografia
    {
        private static readonly char[] alfabeto = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '.', ',', '@', '$', '%', '!', '?' };

        private readonly string _chavePrivada;
        private readonly long _chavePublica;

        public ServicoCriptografia()
        {
            _chavePrivada = GerarChavePrivada();
            _chavePublica = GerarChavePublica(_chavePrivada);
        }

        private static string GerarChavePrivada()
        {
            var primeiroNumeroPrimo = Funcoes.GerarNumeroPrimo();
            var segundoNumeroPrimo = Funcoes.GerarNumeroPrimo();
            var terceiroNumeroPrimo = Funcoes.GerarNumeroPrimo();
            var quartoNumeroPrimo = Funcoes.GerarNumeroPrimo();

            return $"{primeiroNumeroPrimo}@@{segundoNumeroPrimo}@@{terceiroNumeroPrimo}@@{quartoNumeroPrimo}";
        }

        private static long GerarChavePublica(string chavePrivada)
        {
            if (string.IsNullOrEmpty(chavePrivada))
                throw new CriptografiaExcecao("A chave privada não foi informada para a geração da chave pública");

            var chavePublica = CalcularOperacao(chavePrivada);
            return chavePublica;
        }

        private static long CalcularOperacao(string chaveParaOperacao)
        {
            long resultadoOperacao = 1;
            var numerosParaOperacao = chaveParaOperacao.Split("@@");
            for (var i = 0; i <= numerosParaOperacao.Length - 1; i++)
                resultadoOperacao *= resultadoOperacao * int.Parse(numerosParaOperacao[i]);

            return Math.Abs(resultadoOperacao);
        }

        public string RecuperarChave()
        {
            return _chavePrivada;
        }

        public string? Criptografar(string? texto)
        {
            if (string.IsNullOrEmpty(texto))
                return null;

            if (_chavePublica <= 0)
                throw new CriptografiaExcecao("A chave pública de criptografia precisa ser informada");

            var textoCriptografado = new StringBuilder();
            var textoSemAcento = Funcoes.RemoverAcentos(texto).ToLower();

            int indexCaractereChave = 0;
            var chaveEmString = _chavePublica.ToString();
            foreach (var caractere in textoSemAcento)
            {
                if (!alfabeto.Contains(caractere))
                {
                    textoCriptografado.Append(caractere);
                    continue;
                }

                var acrescimosParaIndex = int.Parse(chaveEmString[indexCaractereChave].ToString());

                var indexCaractereAlfabeto = Array.IndexOf(alfabeto, caractere);
                var index = indexCaractereAlfabeto + acrescimosParaIndex;
                if (index > alfabeto.Length - 1)
                    index -= alfabeto.Length;

                textoCriptografado.Append(alfabeto[index]);

                if (++indexCaractereChave > chaveEmString.Length - 1)
                    indexCaractereChave = 0;
            }

            return textoCriptografado.ToString();
        }

        public static string? Descriptograr(string chavePrivada, string conteudo)
        {
            var textoDescriptografado = new StringBuilder();

            var chaveParaDescriptografia = CalcularOperacao(chavePrivada).ToString();

            int indexCaractereChave = 0;
            foreach (var caractere in conteudo)
            {
                if (!alfabeto.Contains(caractere))
                {
                    textoDescriptografado.Append(caractere);
                    continue;
                }

                var decrescimosParaIndex = int.Parse(chaveParaDescriptografia[indexCaractereChave].ToString());

                var indexCaractereAlfabeto = Array.IndexOf(alfabeto, caractere);
                var index = indexCaractereAlfabeto - decrescimosParaIndex;
                if (index < 0)
                    index += alfabeto.Length;

                textoDescriptografado.Append(alfabeto[index]);

                if (++indexCaractereChave > chaveParaDescriptografia.Length - 1)
                    indexCaractereChave = 0;
            }

            return textoDescriptografado.ToString();
        }
    }
}