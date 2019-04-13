using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using SpedService.Model;
using NHibernate;
using SpedService.NHibernate;

namespace SpedService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class ServicoSped : IServicoSped
    {

        #region Geração do Arquivo
        public ArquivoDTO gerarSped(string pDataIni, string pDataFim, int pVersao, int pFinalidade, int pPerfil, int pIdEmpresa, int pInventario, int pIdContador)
        {
            SpedFiscalDAL sped = new SpedFiscalDAL();

            try
            {
                if (sped.GerarArquivoSpedFiscal(pDataIni, pDataFim, pVersao, pFinalidade, pPerfil, pIdEmpresa, pInventario, pIdContador))
                {
                    FileInfo fi = new FileInfo("C:\\T2Ti\\Arquivos\\SpedFiscal.txt");
                    FileStream fs = fi.OpenRead();
                    MemoryStream ms = new MemoryStream((int)fs.Length);
                    fs.CopyTo(ms);
                    fs.Close();
                    ms.Position = 0;

                    ArquivoDTO arquivo = new ArquivoDTO();
                    arquivo.fileInfo = fi;
                    arquivo.memoryStream = ms;

                    return arquivo;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Empresa
        public IList<EmpresaDTO> selectEmpresa(EmpresaDTO empresa)
        {
            try
            {
                IList<EmpresaDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    EmpresaDAL DAL = new EmpresaDAL(session);
                    resultado = DAL.select(empresa);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<EmpresaDTO> selectEmpresaPagina(int primeiroResultado, int quantidadeResultados, EmpresaDTO empresa)
        {
            try
            {
                IList<EmpresaDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    EmpresaDAL DAL = new EmpresaDAL(session);
                    resultado = DAL.selectPagina(primeiroResultado, quantidadeResultados, empresa);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region Contador

        public IList<ContadorDTO> selectContador(ContadorDTO contador)
        {
            try
            {
                IList<ContadorDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ContadorDTO> DAL = new NHibernateDAL<ContadorDTO>(session);
                    resultado = DAL.select(contador);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ContadorDTO> selectContadorPagina(int primeiroResultado, int quantidadeResultados, ContadorDTO contador)
        {
            try
            {
                IList<ContadorDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ContadorDTO> DAL = new NHibernateDAL<ContadorDTO>(session);
                    resultado = DAL.selectPagina<ContadorDTO>(primeiroResultado, quantidadeResultados, contador);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region UnidadeProduto

        public IList<UnidadeProdutoDTO> selectUnidadeProduto(UnidadeProdutoDTO unidadeProduto)
        {
            try
            {
                IList<UnidadeProdutoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<UnidadeProdutoDTO> DAL = new NHibernateDAL<UnidadeProdutoDTO>(session);
                    resultado = DAL.select(unidadeProduto);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<UnidadeProdutoDTO> selectUnidadeProdutoPagina(int primeiroResultado, int quantidadeResultados, UnidadeProdutoDTO unidadeProduto)
        {
            try
            {
                IList<UnidadeProdutoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<UnidadeProdutoDTO> DAL = new NHibernateDAL<UnidadeProdutoDTO>(session);
                    resultado = DAL.selectPagina<UnidadeProdutoDTO>(primeiroResultado, quantidadeResultados, unidadeProduto);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region Produto
        public IList<ProdutoDTO> selectProduto(ProdutoDTO produto)
        {
            try
            {
                IList<ProdutoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoDTO> DAL = new NHibernateDAL<ProdutoDTO>(session);
                    resultado = DAL.select(produto);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ProdutoDTO> selectProdutoPagina(int primeiroResultado, int quantidadeResultados, ProdutoDTO produto)
        {
            try
            {
                IList<ProdutoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoDTO> DAL = new NHibernateDAL<ProdutoDTO>(session);
                    resultado = DAL.selectPagina<ProdutoDTO>(primeiroResultado, quantidadeResultados, produto);
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

        #region EcfImpressora

        public IList<EcfImpressoraDTO> selectEcfImpressora(EcfImpressoraDTO ecfImpressora)
        {
            try
            {
                IList<EcfImpressoraDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EcfImpressoraDTO> DAL = new NHibernateDAL<EcfImpressoraDTO>(session);
                    resultado = DAL.select(ecfImpressora);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<EcfImpressoraDTO> selectEcfImpressoraPagina(int primeiroResultado, int quantidadeResultados, EcfImpressoraDTO ecfImpressora)
        {
            try
            {
                IList<EcfImpressoraDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EcfImpressoraDTO> DAL = new NHibernateDAL<EcfImpressoraDTO>(session);
                    resultado = DAL.selectPagina<EcfImpressoraDTO>(primeiroResultado, quantidadeResultados, ecfImpressora);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region EcfNotaFiscalCabecalho
        public IList<EcfNotaFiscalCabecalhoDTO> selectEcfNotaFiscalCabecalho(EcfNotaFiscalCabecalhoDTO ecfNotaFiscalCabecalho)
        {
            try
            {
                IList<EcfNotaFiscalCabecalhoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EcfNotaFiscalCabecalhoDTO> DAL = new NHibernateDAL<EcfNotaFiscalCabecalhoDTO>(session);
                    resultado = DAL.select(ecfNotaFiscalCabecalho);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<EcfNotaFiscalCabecalhoDTO> selectEcfNotaFiscalCabecalhoPagina(int primeiroResultado, int quantidadeResultados, EcfNotaFiscalCabecalhoDTO ecfNotaFiscalCabecalho)
        {
            try
            {
                IList<EcfNotaFiscalCabecalhoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EcfNotaFiscalCabecalhoDTO> DAL = new NHibernateDAL<EcfNotaFiscalCabecalhoDTO>(session);
                    resultado = DAL.selectPagina<EcfNotaFiscalCabecalhoDTO>(primeiroResultado, quantidadeResultados, ecfNotaFiscalCabecalho);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region NfeCabecalho
        public IList<NfeCabecalhoDTO> selectNfeCabecalho(NfeCabecalhoDTO nfeCabecalho)
        {
            try
            {
                IList<NfeCabecalhoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<NfeCabecalhoDTO> DAL = new NHibernateDAL<NfeCabecalhoDTO>(session);
                    resultado = DAL.select(nfeCabecalho);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<NfeCabecalhoDTO> selectNfeCabecalhoPagina(int primeiroResultado, int quantidadeResultados, NfeCabecalhoDTO nfeCabecalho)
        {
            try
            {
                IList<NfeCabecalhoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<NfeCabecalhoDTO> DAL = new NHibernateDAL<NfeCabecalhoDTO>(session);
                    resultado = DAL.selectPagina<NfeCabecalhoDTO>(primeiroResultado, quantidadeResultados, nfeCabecalho);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region NfeCupomFiscalReferenciado
        public IList<NfeCupomFiscalReferenciadoDTO> selectNfeCupomFiscalReferenciado(NfeCupomFiscalReferenciadoDTO nfeCupomFiscalReferenciado)
        {
            try
            {
                IList<NfeCupomFiscalReferenciadoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<NfeCupomFiscalReferenciadoDTO> DAL = new NHibernateDAL<NfeCupomFiscalReferenciadoDTO>(session);
                    resultado = DAL.select(nfeCupomFiscalReferenciado);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<NfeCupomFiscalReferenciadoDTO> selectNfeCupomFiscalReferenciadoPagina(int primeiroResultado, int quantidadeResultados, NfeCupomFiscalReferenciadoDTO nfeCupomFiscalReferenciado)
        {
            try
            {
                IList<NfeCupomFiscalReferenciadoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<NfeCupomFiscalReferenciadoDTO> DAL = new NHibernateDAL<NfeCupomFiscalReferenciadoDTO>(session);
                    resultado = DAL.selectPagina<NfeCupomFiscalReferenciadoDTO>(primeiroResultado, quantidadeResultados, nfeCupomFiscalReferenciado);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ViewSpedC190
        public IList<ViewSpedC190DTO> selectViewSpedC190(ViewSpedC190DTO viewSpedC190)
        {
            try
            {
                IList<ViewSpedC190DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedC190DTO> DAL = new NHibernateDAL<ViewSpedC190DTO>(session);
                    resultado = DAL.select(viewSpedC190);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ViewSpedC190DTO> selectViewSpedC190Pagina(int primeiroResultado, int quantidadeResultados, ViewSpedC190DTO viewSpedC190)
        {
            try
            {
                IList<ViewSpedC190DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedC190DTO> DAL = new NHibernateDAL<ViewSpedC190DTO>(session);
                    resultado = DAL.selectPagina<ViewSpedC190DTO>(primeiroResultado, quantidadeResultados, viewSpedC190);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ViewSpedC300
        public IList<ViewSpedC300DTO> selectViewSpedC300(ViewSpedC300DTO viewSpedC300)
        {
            try
            {
                IList<ViewSpedC300DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedC300DTO> DAL = new NHibernateDAL<ViewSpedC300DTO>(session);
                    resultado = DAL.select(viewSpedC300);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ViewSpedC300DTO> selectViewSpedC300Pagina(int primeiroResultado, int quantidadeResultados, ViewSpedC300DTO viewSpedC300)
        {
            try
            {
                IList<ViewSpedC300DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedC300DTO> DAL = new NHibernateDAL<ViewSpedC300DTO>(session);
                    resultado = DAL.selectPagina<ViewSpedC300DTO>(primeiroResultado, quantidadeResultados, viewSpedC300);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion
        
        #region ViewSpedC321
        public IList<ViewSpedC321DTO> selectViewSpedC321(ViewSpedC321DTO viewSpedC321)
        {
            try
            {
                IList<ViewSpedC321DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedC321DTO> DAL = new NHibernateDAL<ViewSpedC321DTO>(session);
                    resultado = DAL.select(viewSpedC321);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ViewSpedC321DTO> selectViewSpedC321Pagina(int primeiroResultado, int quantidadeResultados, ViewSpedC321DTO viewSpedC321)
        {
            try
            {
                IList<ViewSpedC321DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedC321DTO> DAL = new NHibernateDAL<ViewSpedC321DTO>(session);
                    resultado = DAL.selectPagina<ViewSpedC321DTO>(primeiroResultado, quantidadeResultados, viewSpedC321);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ViewSpedC370
        public IList<ViewSpedC370DTO> selectViewSpedC370(ViewSpedC370DTO viewSpedC370)
        {
            try
            {
                IList<ViewSpedC370DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedC370DTO> DAL = new NHibernateDAL<ViewSpedC370DTO>(session);
                    resultado = DAL.select(viewSpedC370);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ViewSpedC370DTO> selectViewSpedC370Pagina(int primeiroResultado, int quantidadeResultados, ViewSpedC370DTO viewSpedC370)
        {
            try
            {
                IList<ViewSpedC370DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedC370DTO> DAL = new NHibernateDAL<ViewSpedC370DTO>(session);
                    resultado = DAL.selectPagina<ViewSpedC370DTO>(primeiroResultado, quantidadeResultados, viewSpedC370);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ViewSpedC390
        public IList<ViewSpedC390DTO> selectViewSpedC390(ViewSpedC390DTO viewSpedC390)
        {
            try
            {
                IList<ViewSpedC390DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedC390DTO> DAL = new NHibernateDAL<ViewSpedC390DTO>(session);
                    resultado = DAL.select(viewSpedC390);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ViewSpedC390DTO> selectViewSpedC390Pagina(int primeiroResultado, int quantidadeResultados, ViewSpedC390DTO viewSpedC390)
        {
            try
            {
                IList<ViewSpedC390DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedC390DTO> DAL = new NHibernateDAL<ViewSpedC390DTO>(session);
                    resultado = DAL.selectPagina<ViewSpedC390DTO>(primeiroResultado, quantidadeResultados, viewSpedC390);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ViewSpedC425
        public IList<ViewSpedC425DTO> selectViewSpedC425(ViewSpedC425DTO viewSpedC425)
        {
            try
            {
                IList<ViewSpedC425DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedC425DTO> DAL = new NHibernateDAL<ViewSpedC425DTO>(session);
                    resultado = DAL.select(viewSpedC425);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ViewSpedC425DTO> selectViewSpedC425Pagina(int primeiroResultado, int quantidadeResultados, ViewSpedC425DTO viewSpedC425)
        {
            try
            {
                IList<ViewSpedC425DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedC425DTO> DAL = new NHibernateDAL<ViewSpedC425DTO>(session);
                    resultado = DAL.selectPagina<ViewSpedC425DTO>(primeiroResultado, quantidadeResultados, viewSpedC425);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ViewSpedC490
        public IList<ViewSpedC490DTO> selectViewSpedC490(ViewSpedC490DTO viewSpedC490)
        {
            try
            {
                IList<ViewSpedC490DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedC490DTO> DAL = new NHibernateDAL<ViewSpedC490DTO>(session);
                    resultado = DAL.select(viewSpedC490);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ViewSpedC490DTO> selectViewSpedC490Pagina(int primeiroResultado, int quantidadeResultados, ViewSpedC490DTO viewSpedC490)
        {
            try
            {
                IList<ViewSpedC490DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedC490DTO> DAL = new NHibernateDAL<ViewSpedC490DTO>(session);
                    resultado = DAL.selectPagina<ViewSpedC490DTO>(primeiroResultado, quantidadeResultados, viewSpedC490);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region EcfR02

        public IList<EcfR02DTO> selectEcfR02(EcfR02DTO ecfR02)
        {
            try
            {
                IList<EcfR02DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EcfR02DTO> DAL = new NHibernateDAL<EcfR02DTO>(session);
                    resultado = DAL.select(ecfR02);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<EcfR02DTO> selectEcfR02Pagina(int primeiroResultado, int quantidadeResultados, EcfR02DTO ecfR02)
        {
            try
            {
                IList<EcfR02DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EcfR02DTO> DAL = new NHibernateDAL<EcfR02DTO>(session);
                    resultado = DAL.selectPagina<EcfR02DTO>(primeiroResultado, quantidadeResultados, ecfR02);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region EcfR03
        public IList<EcfR03DTO> selectEcfR03(EcfR03DTO ecfR03)
        {
            try
            {
                IList<EcfR03DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EcfR03DTO> DAL = new NHibernateDAL<EcfR03DTO>(session);
                    resultado = DAL.select(ecfR03);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<EcfR03DTO> selectEcfR03Pagina(int primeiroResultado, int quantidadeResultados, EcfR03DTO ecfR03)
        {
            try
            {
                IList<EcfR03DTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EcfR03DTO> DAL = new NHibernateDAL<EcfR03DTO>(session);
                    resultado = DAL.selectPagina<EcfR03DTO>(primeiroResultado, quantidadeResultados, ecfR03);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region EcfVendaCabecalho
        public IList<EcfVendaCabecalhoDTO> selectEcfVendaCabecalho(EcfVendaCabecalhoDTO ecfVendaCabecalho)
        {
            try
            {
                IList<EcfVendaCabecalhoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EcfVendaCabecalhoDTO> DAL = new NHibernateDAL<EcfVendaCabecalhoDTO>(session);
                    resultado = DAL.select(ecfVendaCabecalho);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<EcfVendaCabecalhoDTO> selectEcfVendaCabecalhoPagina(int primeiroResultado, int quantidadeResultados, EcfVendaCabecalhoDTO ecfVendaCabecalho)
        {
            try
            {
                IList<EcfVendaCabecalhoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EcfVendaCabecalhoDTO> DAL = new NHibernateDAL<EcfVendaCabecalhoDTO>(session);
                    resultado = DAL.selectPagina<EcfVendaCabecalhoDTO>(primeiroResultado, quantidadeResultados, ecfVendaCabecalho);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region EcfVendaDetalhe
        public IList<EcfVendaDetalheDTO> selectEcfVendaDetalhe(EcfVendaDetalheDTO ecfVendaDetalhe)
        {
            try
            {
                IList<EcfVendaDetalheDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EcfVendaDetalheDTO> DAL = new NHibernateDAL<EcfVendaDetalheDTO>(session);
                    resultado = DAL.select(ecfVendaDetalhe);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<EcfVendaDetalheDTO> selectEcfVendaDetalhePagina(int primeiroResultado, int quantidadeResultados, EcfVendaDetalheDTO ecfVendaDetalhe)
        {
            try
            {
                IList<EcfVendaDetalheDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EcfVendaDetalheDTO> DAL = new NHibernateDAL<EcfVendaDetalheDTO>(session);
                    resultado = DAL.selectPagina<EcfVendaDetalheDTO>(primeiroResultado, quantidadeResultados, ecfVendaDetalhe);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region FiscalApuracaoIcms
        public IList<FiscalApuracaoIcmsDTO> selectFiscalApuracaoIcms(FiscalApuracaoIcmsDTO fiscalApuracaoIcms)
        {
            try
            {
                IList<FiscalApuracaoIcmsDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<FiscalApuracaoIcmsDTO> DAL = new NHibernateDAL<FiscalApuracaoIcmsDTO>(session);
                    resultado = DAL.select(fiscalApuracaoIcms);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<FiscalApuracaoIcmsDTO> selectFiscalApuracaoIcmsPagina(int primeiroResultado, int quantidadeResultados, FiscalApuracaoIcmsDTO fiscalApuracaoIcms)
        {
            try
            {
                IList<FiscalApuracaoIcmsDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<FiscalApuracaoIcmsDTO> DAL = new NHibernateDAL<FiscalApuracaoIcmsDTO>(session);
                    resultado = DAL.selectPagina<FiscalApuracaoIcmsDTO>(primeiroResultado, quantidadeResultados, fiscalApuracaoIcms);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ProdutoAlteracaoItem
        public IList<ProdutoAlteracaoItemDTO> selectProdutoAlteracaoItem(ProdutoAlteracaoItemDTO produtoAlteracaoItem)
        {
            try
            {
                IList<ProdutoAlteracaoItemDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoAlteracaoItemDTO> DAL = new NHibernateDAL<ProdutoAlteracaoItemDTO>(session);
                    resultado = DAL.select(produtoAlteracaoItem);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ProdutoAlteracaoItemDTO> selectProdutoAlteracaoItemPagina(int primeiroResultado, int quantidadeResultados, ProdutoAlteracaoItemDTO produtoAlteracaoItem)
        {
            try
            {
                IList<ProdutoAlteracaoItemDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoAlteracaoItemDTO> DAL = new NHibernateDAL<ProdutoAlteracaoItemDTO>(session);
                    resultado = DAL.selectPagina<ProdutoAlteracaoItemDTO>(primeiroResultado, quantidadeResultados, produtoAlteracaoItem);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ViewSpedNfeEmitente
        public IList<ViewSpedNfeEmitenteDTO> selectViewSpedNfeEmitente(ViewSpedNfeEmitenteDTO viewSpedNfeEmitente)
        {
            try
            {
                IList<ViewSpedNfeEmitenteDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedNfeEmitenteDTO> DAL = new NHibernateDAL<ViewSpedNfeEmitenteDTO>(session);
                    resultado = DAL.select(viewSpedNfeEmitente);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ViewSpedNfeEmitenteDTO> selectViewSpedNfeEmitentePagina(int primeiroResultado, int quantidadeResultados, ViewSpedNfeEmitenteDTO viewSpedNfeEmitente)
        {
            try
            {
                IList<ViewSpedNfeEmitenteDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedNfeEmitenteDTO> DAL = new NHibernateDAL<ViewSpedNfeEmitenteDTO>(session);
                    resultado = DAL.selectPagina<ViewSpedNfeEmitenteDTO>(primeiroResultado, quantidadeResultados, viewSpedNfeEmitente);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ViewSpedNfeDestinatario
        public IList<ViewSpedNfeDestinatarioDTO> selectViewSpedNfeDestinatario(ViewSpedNfeDestinatarioDTO viewSpedNfeDestinatario)
        {
            try
            {
                IList<ViewSpedNfeDestinatarioDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedNfeDestinatarioDTO> DAL = new NHibernateDAL<ViewSpedNfeDestinatarioDTO>(session);
                    resultado = DAL.select(viewSpedNfeDestinatario);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ViewSpedNfeDestinatarioDTO> selectViewSpedNfeDestinatarioPagina(int primeiroResultado, int quantidadeResultados, ViewSpedNfeDestinatarioDTO viewSpedNfeDestinatario)
        {
            try
            {
                IList<ViewSpedNfeDestinatarioDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedNfeDestinatarioDTO> DAL = new NHibernateDAL<ViewSpedNfeDestinatarioDTO>(session);
                    resultado = DAL.selectPagina<ViewSpedNfeDestinatarioDTO>(primeiroResultado, quantidadeResultados, viewSpedNfeDestinatario);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ViewSpedNfeItem
        public IList<ViewSpedNfeItemDTO> selectViewSpedNfeItem(ViewSpedNfeItemDTO viewSpedNfeItem)
        {
            try
            {
                IList<ViewSpedNfeItemDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedNfeItemDTO> DAL = new NHibernateDAL<ViewSpedNfeItemDTO>(session);
                    resultado = DAL.select(viewSpedNfeItem);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ViewSpedNfeItemDTO> selectViewSpedNfeItemPagina(int primeiroResultado, int quantidadeResultados, ViewSpedNfeItemDTO viewSpedNfeItem)
        {
            try
            {
                IList<ViewSpedNfeItemDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedNfeItemDTO> DAL = new NHibernateDAL<ViewSpedNfeItemDTO>(session);
                    resultado = DAL.selectPagina<ViewSpedNfeItemDTO>(primeiroResultado, quantidadeResultados, viewSpedNfeItem);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region UnidadeConversao
        public IList<UnidadeConversaoDTO> selectUnidadeConversao(UnidadeConversaoDTO unidadeConversao)
        {
            try
            {
                IList<UnidadeConversaoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<UnidadeConversaoDTO> DAL = new NHibernateDAL<UnidadeConversaoDTO>(session);
                    resultado = DAL.select(unidadeConversao);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<UnidadeConversaoDTO> selectUnidadeConversaoPagina(int primeiroResultado, int quantidadeResultados, UnidadeConversaoDTO unidadeConversao)
        {
            try
            {
                IList<UnidadeConversaoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<UnidadeConversaoDTO> DAL = new NHibernateDAL<UnidadeConversaoDTO>(session);
                    resultado = DAL.selectPagina<UnidadeConversaoDTO>(primeiroResultado, quantidadeResultados, unidadeConversao);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region TributOperacaoFiscal
        public IList<TributOperacaoFiscalDTO> selectTributOperacaoFiscal(TributOperacaoFiscalDTO tributOperacaoFiscal)
        {
            try
            {
                IList<TributOperacaoFiscalDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributOperacaoFiscalDTO> DAL = new NHibernateDAL<TributOperacaoFiscalDTO>(session);
                    resultado = DAL.select(tributOperacaoFiscal);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TributOperacaoFiscalDTO> selectTributOperacaoFiscalPagina(int primeiroResultado, int quantidadeResultados, TributOperacaoFiscalDTO tributOperacaoFiscal)
        {
            try
            {
                IList<TributOperacaoFiscalDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributOperacaoFiscalDTO> DAL = new NHibernateDAL<TributOperacaoFiscalDTO>(session);
                    resultado = DAL.selectPagina<TributOperacaoFiscalDTO>(primeiroResultado, quantidadeResultados, tributOperacaoFiscal);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region NfeTransporte
        public IList<NfeTransporteDTO> selectNfeTransporte(NfeTransporteDTO nfeTransporte)
        {
            try
            {
                IList<NfeTransporteDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<NfeTransporteDTO> DAL = new NHibernateDAL<NfeTransporteDTO>(session);
                    resultado = DAL.select(nfeTransporte);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<NfeTransporteDTO> selectNfeTransportePagina(int primeiroResultado, int quantidadeResultados, NfeTransporteDTO nfeTransporte)
        {
            try
            {
                IList<NfeTransporteDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<NfeTransporteDTO> DAL = new NHibernateDAL<NfeTransporteDTO>(session);
                    resultado = DAL.selectPagina<NfeTransporteDTO>(primeiroResultado, quantidadeResultados, nfeTransporte);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ViewSpedNfeDetalhe
        public IList<ViewSpedNfeDetalheDTO> selectViewSpedNfeDetalhe(ViewSpedNfeDetalheDTO viewSpedNfeDetalhe)
        {
            try
            {
                IList<ViewSpedNfeDetalheDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedNfeDetalheDTO> DAL = new NHibernateDAL<ViewSpedNfeDetalheDTO>(session);
                    resultado = DAL.select(viewSpedNfeDetalhe);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ViewSpedNfeDetalheDTO> selectViewSpedNfeDetalhePagina(int primeiroResultado, int quantidadeResultados, ViewSpedNfeDetalheDTO viewSpedNfeDetalhe)
        {
            try
            {
                IList<ViewSpedNfeDetalheDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewSpedNfeDetalheDTO> DAL = new NHibernateDAL<ViewSpedNfeDetalheDTO>(session);
                    resultado = DAL.selectPagina<ViewSpedNfeDetalheDTO>(primeiroResultado, quantidadeResultados, viewSpedNfeDetalhe);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ViewPessoaContador

        public IList<ViewPessoaContadorDTO> selectViewPessoaContador(ViewPessoaContadorDTO viewPessoaContador)
        {
            try
            {
                IList<ViewPessoaContadorDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewPessoaContadorDTO> DAL = new NHibernateDAL<ViewPessoaContadorDTO>(session);
                    resultado = DAL.select(viewPessoaContador);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ViewPessoaContadorDTO> selectViewPessoaContadorPagina(int primeiroResultado, int quantidadeResultados, ViewPessoaContadorDTO viewPessoaContador)
        {
            try
            {
                IList<ViewPessoaContadorDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewPessoaContadorDTO> DAL = new NHibernateDAL<ViewPessoaContadorDTO>(session);
                    resultado = DAL.selectPagina<ViewPessoaContadorDTO>(primeiroResultado, quantidadeResultados, viewPessoaContador);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 


        #region Usuario
        public UsuarioDTO selectUsuario(String login, String senha)
        {
            try
            {
                UsuarioDTO resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    UsuarioDAL DAL = new UsuarioDAL(session);
                    resultado = DAL.select(login, senha);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }
        #endregion

        #region ControleAcesso
        public IList<ViewControleAcessoDTO> selectControleAcesso(ViewControleAcessoDTO viewControleAcesso)
        {
            try
            {
                IList<ViewControleAcessoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewControleAcessoDTO> DAL = new NHibernateDAL<ViewControleAcessoDTO>(session);
                    resultado = DAL.select(viewControleAcesso);
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
