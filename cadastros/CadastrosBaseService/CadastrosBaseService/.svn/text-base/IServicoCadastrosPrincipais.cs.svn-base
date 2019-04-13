using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CadastrosBaseService.Model;

namespace CadastrosBaseService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServicoCadastrosPrincipais" in both code and config file together.
    [ServiceContract]
    public interface IServicoCadastrosPrincipais
    {
        #region Pessoa
        [OperationContract]
        int deletePessoa(PessoaDTO pessoa);
        [OperationContract]
        PessoaDTO salvarAtualizarPessoa(PessoaDTO pessoa);
        [OperationContract]
        IList<PessoaDTO> selectPessoa(PessoaDTO pessoa);
        [OperationContract]
        IList<PessoaDTO> selectPessoaPagina(int primeiroResultado, int quantidadeResultados, PessoaDTO pessoa);
        #endregion 

        #region Cliente
        [OperationContract]
        int deleteCliente(ClienteDTO cliente);
        [OperationContract]
        ClienteDTO salvarAtualizarCliente(ClienteDTO cliente);
        [OperationContract]
        IList<ClienteDTO> selectCliente(ClienteDTO cliente);
        [OperationContract]
        IList<ClienteDTO> selectClientePagina(int primeiroResultado, int quantidadeResultados, ClienteDTO cliente);
        #endregion 

        #region Fornecedor
        [OperationContract]
        int deleteFornecedor(FornecedorDTO fornecedor);
        [OperationContract]
        FornecedorDTO salvarAtualizarFornecedor(FornecedorDTO fornecedor);
        [OperationContract]
        IList<FornecedorDTO> selectFornecedor(FornecedorDTO fornecedor);
        [OperationContract]
        IList<FornecedorDTO> selectFornecedorPagina(int primeiroResultado, int quantidadeResultados, FornecedorDTO fornecedor);
        #endregion 

        #region Transportadora
        [OperationContract]
        int deleteTransportadora(TransportadoraDTO transportadora);
        [OperationContract]
        TransportadoraDTO salvarAtualizarTransportadora(TransportadoraDTO transportadora);
        [OperationContract]
        IList<TransportadoraDTO> selectTransportadora(TransportadoraDTO transportadora);
        [OperationContract]
        IList<TransportadoraDTO> selectTransportadoraPagina(int primeiroResultado, int quantidadeResultados, TransportadoraDTO transportadora);
        #endregion 

    }
}
