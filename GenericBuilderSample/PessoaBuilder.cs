namespace GenericBuilderSample
{
    using System;
    using GenericBuilder;

    public class PessoaBuilder : GenericBuilder<Pessoa>
    {
        public PessoaBuilder() : base() { }

        public PessoaBuilder ComNome(string nome)
        {
            this.AddPart(_result => _result.Nome, nome);
            return this;
        }

        public PessoaBuilder ComViewModel(PessoaViewModel viewModel) => this
            .ComNome(viewModel.Nome)
            .ComDataNascimento(DateTime.Parse(viewModel.DataNascimento))
            .ComEndereco
            (
                viewModel.Logradouro,
                viewModel.Numero,
                viewModel.Complemento,
                viewModel.Bairro,
                viewModel.Cep,
                viewModel.Cidade,
                viewModel.Estado
            );

        public PessoaBuilder ComEndereco(
            string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado)
        {
            AddPart(_result => _result.Endereco.Logradouro, logradouro);
            AddPart(_result => _result.Endereco.Numero, numero);
            AddPart(_result => _result.Endereco.Complemento, complemento);
            AddPart(_result => _result.Endereco.Bairro, bairro);
            AddPart(_result => _result.Endereco.Cep, cep);
            AddPart(_result => _result.Endereco.Cidade, cidade);
            AddPart(_result => _result.Endereco.Estado, estado);
            return this;
        }

        public PessoaBuilder ComDataNascimento(DateTime dataNasc)
        {
            AddPart(_result => _result.DataNascimento, dataNasc);
            return this;
        }
    }
}
