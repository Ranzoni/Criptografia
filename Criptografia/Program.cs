// See https://aka.ms/new-console-template for more information
using Criptografia.Core;

do
{
    Console.Clear();
    Console.WriteLine("Digite o número da ação que você deseja realizar?");
    Console.WriteLine("1-Criptografar");
    Console.WriteLine("2-Descriptografar");

    int opcao;
    do
    {
        string resultado = Console.ReadLine() ?? "";
        if (int.TryParse(resultado, out opcao) && opcao > 0 && opcao <= 2)
            break;

        Console.WriteLine("Escolha uma opção válida");
    } while (true);

    Console.Clear();

    switch (opcao)
    {
        case 1:
            Criptografar();
            break;
        case 2:
            Descriptografar();
            break;
    }

    Console.WriteLine();
    Console.WriteLine("-----------------");
    Console.WriteLine();

    Console.WriteLine("Deseja continuar?");
    Console.WriteLine("1-Sim");
    Console.WriteLine("2-Não");
    int opcaoFinal;
    do
    {
        string resultado = Console.ReadLine() ?? "";
        if (int.TryParse(resultado, out opcaoFinal) && opcaoFinal > 0 && opcaoFinal <= 2)
            break;

        Console.WriteLine("Escolha uma opção válida");
    } while (true);

    if (opcaoFinal == 2)
        break;
} while (true);

Console.Clear();

void Criptografar()
{
    var servico = new ServicoCriptografia();

    Console.WriteLine($"Chave privada (CONFIDENCIAL): {servico.RecuperarChave()}");

    Console.WriteLine("Digite o texto que deve ser criptografado:");
    var texto = Console.ReadLine();

    var textoCriptografado = servico.Criptografar(texto);

    Console.WriteLine("Texto criptografado:");
    Console.WriteLine(textoCriptografado);
}

void Descriptografar()
{
    Console.WriteLine("Digite a sua chave privada:");
    var chave = Console.ReadLine();
    Console.WriteLine("Digite o texto que deseja descriptografar:");
    var textoCriptografado = Console.ReadLine();

    Console.Clear();

    var textoDescriptografado = ServicoCriptografia.Descriptograr(chave, textoCriptografado);

    Console.WriteLine("Texto descriptografado:");
    Console.WriteLine(textoDescriptografado);
}
