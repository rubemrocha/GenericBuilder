namespace GenericBuilderSample
{
    using System;

    class Program
    {
        static void Main()
        {
            Pessoa pessoa = new PessoaBuilder()
                .ComNome("Rubem Nascimento da Rocha")
                .ComDataNascimento(new DateTime(1975, 11, 17))
                .ComEndereco("Rua Frei Mauro", "127", null, "Adrianópolis", "69057-056", "Manaus", "AM")
                .Build();

            Console.WriteLine(pessoa.ToString());
        }
    }
}
