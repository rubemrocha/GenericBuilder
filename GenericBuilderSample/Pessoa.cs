namespace GenericBuilderSample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    // Model
    public class Pessoa : GenericBuilder.ValueObject
    {
        readonly EnderecoPessoa _endereco;

        public Pessoa()
        {
            this._endereco = new EnderecoPessoa();
        }

        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public EnderecoPessoa Endereco => _endereco;

        public override string ToString()
        {
            return $"Nome: {Nome}\nData Nasc.: {DataNascimento:d}\nEndereço: {Endereco}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Nome;
            yield return DataNascimento;
            yield return Endereco.Logradouro;
            yield return Endereco.Numero;
            yield return Endereco.Complemento;
            yield return Endereco.Bairro;
            yield return Endereco.Cep;
            yield return Endereco.Cidade;
            yield return Endereco.Estado;
        }

        public class EnderecoPessoa
        {
            public string Logradouro { get; set; }
            public string Numero { get; set; } = "s/n";
            public string Complemento { get; set; } = default;
            public string Bairro { get; set; }
            public string Cep { get; set; }
            public string Cidade { get; set; }
            public string Estado { get; set; }

            internal EnderecoPessoa() { }

            public override string ToString() =>
                new string[]
                {
                    Logradouro,
                    Numero,
                    Complemento,
                    Bairro,
                    Cep,
                    Cidade,
                    Estado
                }
                .Where(addressItem => !string.IsNullOrEmpty(addressItem) && addressItem.Length > 0)
                .Aggregate("", (current, next) => $"{current}{(string.IsNullOrEmpty(current) ? "" : (next == Numero ? " nº " : next == Cep ? ", CEP " : next == Estado ? "-" : ", "))}{next}");
        }
    }
}