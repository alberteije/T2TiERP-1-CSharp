using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CadastrosBaseService.Model;
using NHibernate;
using CadastrosBaseService.NHibernate;
using NHibernate.Criterion;

namespace CadastrosBaseService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServicoCadastrosPrincipais" in code, svc and config file together.
    public class ServicoCadastrosPrincipais : IServicoCadastrosPrincipais
    {

        #region Pessoa
        public int deletePessoa(PessoaDTO Pessoa)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    PessoaDAL DAL = new PessoaDAL(session);
                    DAL.delete(Pessoa);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public PessoaDTO salvarAtualizarPessoa(PessoaDTO Pessoa)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    PessoaDAL DAL = new PessoaDAL(session);
                    DAL.saveOrUpdate(Pessoa);
                    session.Flush();
                }
                return Pessoa;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<PessoaDTO> selectPessoa(PessoaDTO Pessoa)
        {
            try
            {
                IList<PessoaDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    PessoaDAL DAL = new PessoaDAL(session);
                    resultado = DAL.select(Pessoa);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<PessoaDTO> selectPessoaPagina(int primeiroResultado, int quantidadeResultados, PessoaDTO Pessoa)
        {
            try
            {
                IList<PessoaDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    PessoaDAL DAL = new PessoaDAL(session);
                    resultado = DAL.selectPagina(primeiroResultado, quantidadeResultados, Pessoa);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }
        #endregion 

        #region Cliente
        public int deleteCliente(ClienteDTO cliente)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ClienteDTO> DAL = new NHibernateDAL<ClienteDTO>(session);
                    DAL.delete(cliente);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public ClienteDTO salvarAtualizarCliente(ClienteDTO cliente)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ClienteDTO> DAL = new NHibernateDAL<ClienteDTO>(session);
                    DAL.saveOrUpdate(cliente);
                    session.Flush();
                }
                return cliente;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ClienteDTO> selectCliente(ClienteDTO cliente)
        {
            try
            {
                IList<ClienteDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ClienteDTO> DAL = new NHibernateDAL<ClienteDTO>(session);
                    resultado = DAL.select(cliente);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ClienteDTO> selectClientePagina(int primeiroResultado, int quantidadeResultados, ClienteDTO cliente)
        {
            try
            {
                IList<ClienteDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ClienteDTO> DAL = new NHibernateDAL<ClienteDTO>(session);
                    resultado = DAL.selectPagina<ClienteDTO>(primeiroResultado, quantidadeResultados, cliente);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region Fornecedor
        public int deleteFornecedor(FornecedorDTO fornecedor)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<FornecedorDTO> DAL = new NHibernateDAL<FornecedorDTO>(session);
                    DAL.delete(fornecedor);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public FornecedorDTO salvarAtualizarFornecedor(FornecedorDTO fornecedor)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<FornecedorDTO> DAL = new NHibernateDAL<FornecedorDTO>(session);
                    DAL.saveOrUpdate(fornecedor);
                    session.Flush();
                }
                return fornecedor;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<FornecedorDTO> selectFornecedor(FornecedorDTO fornecedor)
        {
            try
            {
                IList<FornecedorDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<FornecedorDTO> DAL = new NHibernateDAL<FornecedorDTO>(session);
                    resultado = DAL.select(fornecedor);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<FornecedorDTO> selectFornecedorPagina(int primeiroResultado, int quantidadeResultados, FornecedorDTO fornecedor)
        {
            try
            {
                IList<FornecedorDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<FornecedorDTO> DAL = new NHibernateDAL<FornecedorDTO>(session);
                    resultado = DAL.selectPagina<FornecedorDTO>(primeiroResultado, quantidadeResultados, fornecedor);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }
        #endregion 
    
        #region Transportadora
        public int deleteTransportadora(TransportadoraDTO transportadora)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TransportadoraDTO> DAL = new NHibernateDAL<TransportadoraDTO>(session);
                    DAL.delete(transportadora);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public TransportadoraDTO salvarAtualizarTransportadora(TransportadoraDTO transportadora)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TransportadoraDTO> DAL = new NHibernateDAL<TransportadoraDTO>(session);
                    DAL.saveOrUpdate(transportadora);
                    session.Flush();
                }
                return transportadora;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TransportadoraDTO> selectTransportadora(TransportadoraDTO transportadora)
        {
            try
            {
                IList<TransportadoraDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TransportadoraDTO> DAL = new NHibernateDAL<TransportadoraDTO>(session);
                    resultado = DAL.select(transportadora);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TransportadoraDTO> selectTransportadoraPagina(int primeiroResultado, int quantidadeResultados, TransportadoraDTO transportadora)
        {
            try
            {
                IList<TransportadoraDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TransportadoraDTO> DAL = new NHibernateDAL<TransportadoraDTO>(session);
                    resultado = DAL.selectPagina<TransportadoraDTO>(primeiroResultado, quantidadeResultados, transportadora);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion
    }
}
