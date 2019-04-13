using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CadastrosBaseService.Model;
using NHibernate;
using CadastrosBaseService.NHibernate;

namespace CadastrosBaseService
{
    
    public class ServicoCadastrosBase : IServicoCadastrosBase
    {
        #region Operações Cbo

        public int deleteCbo(CboDTO cbo)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CboDTO> cboDAL = new NHibernateDAL<CboDTO>(session);
                    cboDAL.delete(cbo);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarCbo(CboDTO cbo)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CboDTO> cboDAL = new NHibernateDAL<CboDTO>(session);
                    cboDAL.saveOrUpdate(cbo);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<CboDTO> selectCbo(CboDTO cbo)
        {
            try
            {
                IList<CboDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CboDTO> cboDAL = new NHibernateDAL<CboDTO>(session);
                    resultado = cboDAL.select(cbo);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<CboDTO> selectCboPagina(int primeiroResultado, int quantidadeResultados, CboDTO cbo)
        {
            try
            {
                IList<CboDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CboDTO> cboDAL = new NHibernateDAL<CboDTO>(session);
                    resultado = cboDAL.selectPagina(primeiroResultado, quantidadeResultados, cbo);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        #endregion

        #region SitucaoDocumento

        public int deleteSituacaoDocumento(SituacaoDocumentoDTO dto)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SituacaoDocumentoDTO> DAL = new NHibernateDAL<SituacaoDocumentoDTO>(session);
                    DAL.delete(dto);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarSituacaoDocumento(SituacaoDocumentoDTO dto)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SituacaoDocumentoDTO> DAL = new NHibernateDAL<SituacaoDocumentoDTO>(session);
                    DAL.saveOrUpdate(dto);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<SituacaoDocumentoDTO> selectSituacaoDocumento(SituacaoDocumentoDTO dto)
        {
            try
            {
                IList<SituacaoDocumentoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SituacaoDocumentoDTO> DAL = new NHibernateDAL<SituacaoDocumentoDTO>(session);
                    resultado = DAL.select(dto);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<SituacaoDocumentoDTO> selectSituacaoDocumentoPagina(int primeiroResultado, int quantidadeResultados, SituacaoDocumentoDTO dto)
        {
            try
            {
                IList<SituacaoDocumentoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SituacaoDocumentoDTO> DAL = new NHibernateDAL<SituacaoDocumentoDTO>(session);
                    resultado = DAL.selectPagina(primeiroResultado, quantidadeResultados, dto);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        #endregion

        #region Cfop
        public int deleteCfop(CfopDTO cfop)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CfopDTO> DAL = new NHibernateDAL<CfopDTO>(session);
                    DAL.delete(cfop);
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


        public CfopDTO salvarAtualizarCfop(CfopDTO cfop)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CfopDTO> DAL = new NHibernateDAL<CfopDTO>(session);
                    DAL.saveOrUpdate(cfop);
                    session.Flush();
                }
                return cfop;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<CfopDTO> selectCfop(CfopDTO cfop)
        {
            try
            {
                IList<CfopDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CfopDTO> DAL = new NHibernateDAL<CfopDTO>(session);
                    resultado = DAL.select(cfop);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<CfopDTO> selectCfopPagina(int primeiroResultado, int quantidadeResultados, CfopDTO cfop)
        {
            try
            {
                IList<CfopDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CfopDTO> DAL = new NHibernateDAL<CfopDTO>(session);
                    resultado = DAL.selectPagina<CfopDTO>(primeiroResultado, quantidadeResultados, cfop);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region CsosnA
        public int deleteCsosnA(CsosnADTO dto)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CsosnADTO> DAL = new NHibernateDAL<CsosnADTO>(session);
                    DAL.delete(dto);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarCsosnA(CsosnADTO dto)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CsosnADTO> DAL = new NHibernateDAL<CsosnADTO>(session);
                    DAL.saveOrUpdate(dto);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<CsosnADTO> selectCsosnA(CsosnADTO dto)
        {
            try
            {
                IList<CsosnADTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CsosnADTO> DAL = new NHibernateDAL<CsosnADTO>(session);
                    resultado = DAL.select(dto);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<CsosnADTO> selectCsosnAPagina(int primeiroResultado, int quantidadeResultados, CsosnADTO dto)
        {
            try
            {
                IList<CsosnADTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CsosnADTO> DAL = new NHibernateDAL<CsosnADTO>(session);
                    resultado = DAL.selectPagina(primeiroResultado, quantidadeResultados, dto);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region CsosnB
        public int deleteCsosnB(CsosnBDTO dto)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CsosnBDTO> DAL = new NHibernateDAL<CsosnBDTO>(session);
                    DAL.delete(dto);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarCsosnB(CsosnBDTO dto)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CsosnBDTO> DAL = new NHibernateDAL<CsosnBDTO>(session);
                    DAL.saveOrUpdate(dto);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<CsosnBDTO> selectCsosnB(CsosnBDTO dto)
        {
            try
            {
                IList<CsosnBDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CsosnBDTO> DAL = new NHibernateDAL<CsosnBDTO>(session);
                    resultado = DAL.select(dto);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<CsosnBDTO> selectCsosnBPagina(int primeiroResultado, int quantidadeResultados, CsosnBDTO dto)
        {
            try
            {
                IList<CsosnBDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CsosnBDTO> DAL = new NHibernateDAL<CsosnBDTO>(session);
                    resultado = DAL.selectPagina(primeiroResultado, quantidadeResultados, dto);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region Tipo Credio Pis

        public int deleteTipoCredioPis(TipoCreditoPisDTO dto)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoCreditoPisDTO> DAL = new NHibernateDAL<TipoCreditoPisDTO>(session);
                    DAL.delete(dto);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarTipoCredioPis(TipoCreditoPisDTO dto)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoCreditoPisDTO> DAL = new NHibernateDAL<TipoCreditoPisDTO>(session);
                    DAL.saveOrUpdate(dto);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<TipoCreditoPisDTO> selectTipoCredioPis(TipoCreditoPisDTO dto)
        {
            try
            {
                IList<TipoCreditoPisDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoCreditoPisDTO> DAL = new NHibernateDAL<TipoCreditoPisDTO>(session);
                    resultado = DAL.select(dto);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<TipoCreditoPisDTO> selectTipoCredioPisPagina(int primeiroResultado, int quantidadeResultados, TipoCreditoPisDTO dto)
        {
            try
            {
                IList<TipoCreditoPisDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoCreditoPisDTO> DAL = new NHibernateDAL<TipoCreditoPisDTO>(session);
                    resultado = DAL.selectPagina(primeiroResultado, quantidadeResultados, dto);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        #endregion    
    
        #region CEP
        public int deleteCep(CepDTO cep)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CepDTO> cepDAL = new NHibernateDAL<CepDTO>(session);
                    cepDAL.delete(cep);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }     

        public int salvarAtualizarCep(CepDTO cep)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CepDTO> cepDAL = new NHibernateDAL<CepDTO>(session);
                    cepDAL.saveOrUpdate(cep);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<CepDTO> selectCep(CepDTO cep)
        {
            try
            {
                IList<CepDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CepDTO> cepDAL = new NHibernateDAL<CepDTO>(session);
                    resultado = cepDAL.select(cep);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<CepDTO> selectCepPagina(int primeiroResultado, int quantidadeResultado, CepDTO cep)
        {
            try
            {
                IList<CepDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CepDTO> cepDAL = new NHibernateDAL<CepDTO>(session);
                    resultado = cepDAL.selectPagina(primeiroResultado, quantidadeResultado, cep);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        #endregion

        #region CodigoGpsDTO Service
        public int deleteCodigoGps(CodigoGpsDTO codigo_gps)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CodigoGpsDTO> codigo_gpsDAL = new NHibernateDAL<CodigoGpsDTO>(session);
                    codigo_gpsDAL.delete(codigo_gps);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarCodigoGps(CodigoGpsDTO codigo_gps)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CodigoGpsDTO> codigo_gpsDAL = new NHibernateDAL<CodigoGpsDTO>(session);
                    codigo_gpsDAL.saveOrUpdate(codigo_gps);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        public IList<CodigoGpsDTO> selectCodigoGps(CodigoGpsDTO codigo_gps)
        {
            try
            {
                IList<CodigoGpsDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CodigoGpsDTO> codigo_gpsDAL = new NHibernateDAL<CodigoGpsDTO>(session);
                    resultado = codigo_gpsDAL.select(codigo_gps);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        public IList<CodigoGpsDTO> selectCodigoGpsPagina(int primeiroResultado, int quantidadeResultados, CodigoGpsDTO codigo_gps)
        {
            try
            {
                IList<CodigoGpsDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CodigoGpsDTO> codigo_gpsDAL = new NHibernateDAL<CodigoGpsDTO>(session);
                    resultado = codigo_gpsDAL.selectPagina(primeiroResultado, quantidadeResultados, codigo_gps);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region SalarioMinimoDTO Service

        public int deleteSalarioMinimo(SalarioMinimoDTO salario_minimo)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SalarioMinimoDTO> salario_minimoDAL = new NHibernateDAL<SalarioMinimoDTO>(session);
                    salario_minimoDAL.delete(salario_minimo);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
 
        public int salvarAtualizarSalarioMinimo(SalarioMinimoDTO salario_minimo)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SalarioMinimoDTO> salario_minimoDAL = new NHibernateDAL<SalarioMinimoDTO>(session);
                    salario_minimoDAL.saveOrUpdate(salario_minimo);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<SalarioMinimoDTO> selectSalarioMinimo(SalarioMinimoDTO salario_minimo)
        {
            try
            {
                IList<SalarioMinimoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SalarioMinimoDTO> salario_minimoDAL = new NHibernateDAL<SalarioMinimoDTO>(session);
                    resultado = salario_minimoDAL.select(salario_minimo);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
 
        public IList<SalarioMinimoDTO> selectSalarioMinimoPagina(int primeiroResultado, int quantidadeResultados, SalarioMinimoDTO salario_minimo)
        {
            try
            {
                IList<SalarioMinimoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SalarioMinimoDTO> salario_minimoDAL = new NHibernateDAL<SalarioMinimoDTO>(session);
                    resultado = salario_minimoDAL.selectPagina(primeiroResultado, quantidadeResultados, salario_minimo);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region TipoDesligamento
        public int deleteTipoDesligamento(TipoDesligamentoDTO tipoDesligamento)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoDesligamentoDTO> DAL = new NHibernateDAL<TipoDesligamentoDTO>(session);
                    DAL.delete(tipoDesligamento);
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


        public TipoDesligamentoDTO salvarAtualizarTipoDesligamento(TipoDesligamentoDTO tipoDesligamento)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoDesligamentoDTO> DAL = new NHibernateDAL<TipoDesligamentoDTO>(session);
                    DAL.saveOrUpdate(tipoDesligamento);
                    session.Flush();
                }
                return tipoDesligamento;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TipoDesligamentoDTO> selectTipoDesligamento(TipoDesligamentoDTO tipoDesligamento)
        {
            try
            {
                IList<TipoDesligamentoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoDesligamentoDTO> DAL = new NHibernateDAL<TipoDesligamentoDTO>(session);
                    resultado = DAL.select(tipoDesligamento);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TipoDesligamentoDTO> selectTipoDesligamentoPagina(int primeiroResultado, int quantidadeResultados, TipoDesligamentoDTO tipoDesligamento)
        {
            try
            {
                IList<TipoDesligamentoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoDesligamentoDTO> DAL = new NHibernateDAL<TipoDesligamentoDTO>(session);
                    resultado = DAL.selectPagina<TipoDesligamentoDTO>(primeiroResultado, quantidadeResultados, tipoDesligamento);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region SefipCodigoMovimentacao Service

        public int deleteSefipCodigoMovimentacao(SefipCodigoMovimentacaoDTO sefip_codigo_movimentacao)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SefipCodigoMovimentacaoDTO> sefip_codigo_movimentacaoDAL = new NHibernateDAL<SefipCodigoMovimentacaoDTO>(session);
                    sefip_codigo_movimentacaoDAL.delete(sefip_codigo_movimentacao);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarSefipCodigoMovimentacao(SefipCodigoMovimentacaoDTO sefip_codigo_movimentacao)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SefipCodigoMovimentacaoDTO> sefip_codigo_movimentacaoDAL = new NHibernateDAL<SefipCodigoMovimentacaoDTO>(session);
                    sefip_codigo_movimentacaoDAL.saveOrUpdate(sefip_codigo_movimentacao);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<SefipCodigoMovimentacaoDTO> selectSefipCodigoMovimentacao(SefipCodigoMovimentacaoDTO sefip_codigo_movimentacao)
        {
            try
            {
                IList<SefipCodigoMovimentacaoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SefipCodigoMovimentacaoDTO> sefip_codigo_movimentacaoDAL = new NHibernateDAL<SefipCodigoMovimentacaoDTO>(session);
                    resultado = sefip_codigo_movimentacaoDAL.select(sefip_codigo_movimentacao);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<SefipCodigoMovimentacaoDTO> selectSefipCodigoMovimentacaoPagina(int primeiroResultado, int quantidadeResultados, SefipCodigoMovimentacaoDTO sefip_codigo_movimentacao)
        {
            try
            {
                IList<SefipCodigoMovimentacaoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SefipCodigoMovimentacaoDTO> sefip_codigo_movimentacaoDAL = new NHibernateDAL<SefipCodigoMovimentacaoDTO>(session);
                    resultado = sefip_codigo_movimentacaoDAL.selectPagina(primeiroResultado, quantidadeResultados, sefip_codigo_movimentacao);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region BaseCreditoPis
        public int deleteBaseCreditoPis(BaseCreditoPisDTO basecreditopis)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<BaseCreditoPisDTO> basecreditopisDAL = new NHibernateDAL<BaseCreditoPisDTO>(session);
                    basecreditopisDAL.delete(basecreditopis);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarBaseCreditoPis(BaseCreditoPisDTO basecreditopis)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<BaseCreditoPisDTO> basecreditopisDAL = new NHibernateDAL<BaseCreditoPisDTO>(session);
                    basecreditopisDAL.saveOrUpdate(basecreditopis);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<BaseCreditoPisDTO> selectBaseCreditoPis(BaseCreditoPisDTO basecreditopis)
        {
            try
            {
                IList<BaseCreditoPisDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<BaseCreditoPisDTO> basecreditopisDAL = new NHibernateDAL<BaseCreditoPisDTO>(session);
                    resultado = basecreditopisDAL.select(basecreditopis);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<BaseCreditoPisDTO> selectBaseCreditoPisPagina(int primeiroResultado, int quantidadeResultados, BaseCreditoPisDTO basecreditopis)
        {
            try
            {
                IList<BaseCreditoPisDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<BaseCreditoPisDTO> basecreditopisDAL = new NHibernateDAL<BaseCreditoPisDTO>(session);
                    resultado = basecreditopisDAL.selectPagina(primeiroResultado, quantidadeResultados, basecreditopis);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region CstIpi
        public int deleteCstIpi(CstIpiDTO cstipi)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstIpiDTO> cstipiDAL = new NHibernateDAL<CstIpiDTO>(session);
                    cstipiDAL.delete(cstipi);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarCstIpi(CstIpiDTO cstipi)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstIpiDTO> cstipiDAL = new NHibernateDAL<CstIpiDTO>(session);
                    cstipiDAL.saveOrUpdate(cstipi);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<CstIpiDTO> selectCstIpi(CstIpiDTO cstipi)
        {
            try
            {
                IList<CstIpiDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstIpiDTO> cstipiDAL = new NHibernateDAL<CstIpiDTO>(session);
                    resultado = cstipiDAL.select(cstipi);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<CstIpiDTO> selectCstIpiPagina(int primeiroResultado, int quantidadeResultados, CstIpiDTO cstipi)
        {
            try
            {
                IList<CstIpiDTO> resultado = null;
                using (ISession sessioin = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstIpiDTO> cstipiDAL = new NHibernateDAL<CstIpiDTO>(sessioin);
                    resultado = cstipiDAL.selectPagina(primeiroResultado, quantidadeResultados, cstipi);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region Ncm
        public int deleteNcm(NcmDTO ncm)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<NcmDTO> ncmDAL = new NHibernateDAL<NcmDTO>(session);
                    ncmDAL.delete(ncm);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarNcm(NcmDTO ncm)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<NcmDTO> ncmDAL = new NHibernateDAL<NcmDTO>(session);
                    ncmDAL.saveOrUpdate(ncm);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<NcmDTO> selectNcm(NcmDTO ncm)
        {
            try
            {
                IList<NcmDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<NcmDTO> ncmDAL = new NHibernateDAL<NcmDTO>(session);
                    resultado = ncmDAL.select(ncm);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<NcmDTO> selectNcmPagina(int primeiroResultado, int quantidadeResultados, NcmDTO ncm)
        {
            try
            {
                IList<NcmDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<NcmDTO> ncmDAL = new NHibernateDAL<NcmDTO>(session);
                    resultado = ncmDAL.selectPagina(primeiroResultado, quantidadeResultados, ncm);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        #endregion    
    
        #region Feriados
        public int deleteFeriados(FeriadosDTO feriados)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<FeriadosDTO> feriadosDAL = new NHibernateDAL<FeriadosDTO>(session);
                    feriadosDAL.delete(feriados);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarFeriados(FeriadosDTO feriados)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<FeriadosDTO> feriadosDAL = new NHibernateDAL<FeriadosDTO>(session);
                    feriadosDAL.saveOrUpdate(feriados);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<FeriadosDTO> selectFeriados(FeriadosDTO feriados)
        {
            IList<FeriadosDTO> resultado = null;
            using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
            {
                NHibernateDAL<FeriadosDTO> feriadosDAL = new NHibernateDAL<FeriadosDTO>(session);
                resultado = feriadosDAL.select(feriados);
            }
            return resultado;
        }

        public IList<FeriadosDTO> selectFeriadosPagina(int primeiroResultado, int quantidadeResultados, FeriadosDTO feriados)
        {
            try
            {
                IList<FeriadosDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<FeriadosDTO> feriadosDAL = new NHibernateDAL<FeriadosDTO>(session);
                    resultado = feriadosDAL.selectPagina(primeiroResultado, quantidadeResultados, feriados);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        #endregion    
    
        #region CstCofins
        public int deleteCstCofins(CstCofinsDTO cstcofins)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstCofinsDTO> cstcofinsDAL = new NHibernateDAL<CstCofinsDTO>(session);
                    cstcofinsDAL.delete(cstcofins);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarCstCofins(CstCofinsDTO cstcofins)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstCofinsDTO> cstcofinsDAL = new NHibernateDAL<CstCofinsDTO>(session);
                    cstcofinsDAL.saveOrUpdate(cstcofins);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<CstCofinsDTO> selectCstCofins(CstCofinsDTO cstcofins)
        {
            try
            {
                IList<CstCofinsDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstCofinsDTO> cstcofinsDAL = new NHibernateDAL<CstCofinsDTO>(session);
                    resultado = cstcofinsDAL.select(cstcofins);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<CstCofinsDTO> selectCstCofinsPagina(int primeiroResultado, int quantidadeResultado, CstCofinsDTO cstcofins)
        {
            try
            {
                IList<CstCofinsDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstCofinsDTO> cstcofinsDAL = new NHibernateDAL<CstCofinsDTO>(session);
                    resultado = cstcofinsDAL.selectPagina(primeiroResultado, quantidadeResultado, cstcofins);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region CstIcmsA
        public int deleteCstIcmsA(CstIcmsADTO csticmsa)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstIcmsADTO> csticmsaDAL = new NHibernateDAL<CstIcmsADTO>(session);
                    csticmsaDAL.delete(csticmsa);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarCstIcmsA(CstIcmsADTO csticmsa)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstIcmsADTO> csticmsaDAL = new NHibernateDAL<CstIcmsADTO>(session);
                    csticmsaDAL.saveOrUpdate(csticmsa);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<CstIcmsADTO> selectCstIcmsA(CstIcmsADTO csticmsa)
        {
            try
            {
                IList<CstIcmsADTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstIcmsADTO> csticmsaDAL = new NHibernateDAL<CstIcmsADTO>(session);
                    resultado = csticmsaDAL.select(csticmsa);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<CstIcmsADTO> selectCstIcmsAPagina(int primeiroResultado, int quantidadeResultados, CstIcmsADTO csticmsa)
        {
            try
            {
                IList<CstIcmsADTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstIcmsADTO> csticmsaDAL = new NHibernateDAL<CstIcmsADTO>(session);
                    resultado = csticmsaDAL.selectPagina(primeiroResultado, quantidadeResultados, csticmsa);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region CstIcmsB
        public int deleteCstIcmsB(CstIcmsBDTO csticmsb)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstIcmsBDTO> csticmsbDAL = new NHibernateDAL<CstIcmsBDTO>(session);
                    csticmsbDAL.delete(csticmsb);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarCstIcmsB(CstIcmsBDTO csticmsb)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstIcmsBDTO> csticmsbDAL = new NHibernateDAL<CstIcmsBDTO>(session);
                    csticmsbDAL.saveOrUpdate(csticmsb);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<CstIcmsBDTO> selectCstIcmsB(CstIcmsBDTO csticmsb)
        {
            try
            {
                IList<CstIcmsBDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstIcmsBDTO> csticmsbDAL = new NHibernateDAL<CstIcmsBDTO>(session);
                    resultado = csticmsbDAL.select(csticmsb);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<CstIcmsBDTO> selectCstIcmsBPagina(int primeiroResultado, int quantidadeResultados, CstIcmsBDTO csticmsb)
        {
            try
            {
                IList<CstIcmsBDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstIcmsBDTO> csticmsbDAL = new NHibernateDAL<CstIcmsBDTO>(session);
                    resultado = csticmsbDAL.selectPagina(primeiroResultado, quantidadeResultados, csticmsb);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region CstPis
        public int deleteCstPis(CstPisDTO cstpis)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstPisDTO> cstpisDAL = new NHibernateDAL<CstPisDTO>(session);
                    cstpisDAL.delete(cstpis);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarCstPis(CstPisDTO cstpis)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstPisDTO> cstpisDAL = new NHibernateDAL<CstPisDTO>(session);
                    cstpisDAL.saveOrUpdate(cstpis);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<CstPisDTO> selectCstPis(CstPisDTO cstpis)
        {
            try
            {
                IList<CstPisDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstPisDTO> cstpisDAL = new NHibernateDAL<CstPisDTO>(session);
                    resultado = cstpisDAL.select(cstpis);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<CstPisDTO> selectCstPisPagina(int primeiroResultado, int quantidadeResultados, CstPisDTO cstpis)
        {
            try
            {
                IList<CstPisDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CstPisDTO> cstpisDAL = new NHibernateDAL<CstPisDTO>(session);
                    resultado = cstpisDAL.selectPagina(primeiroResultado, quantidadeResultados, cstpis);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region SituacaoForCli
        public int deleteSituacaoForCli(SituacaoForCliDTO situacaoforcli)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SituacaoForCliDTO> situacaoforcliDAL = new NHibernateDAL<SituacaoForCliDTO>(session);
                    situacaoforcliDAL.delete(situacaoforcli);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarSituacaoForCli(SituacaoForCliDTO situacaoforcli)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SituacaoForCliDTO> situacaoforcliDAL = new NHibernateDAL<SituacaoForCliDTO>(session);
                    situacaoforcliDAL.saveOrUpdate(situacaoforcli);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<SituacaoForCliDTO> selectSituacaoForCli(SituacaoForCliDTO situacaoforcli)
        {
            try
            {
                IList<SituacaoForCliDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SituacaoForCliDTO> situacaoforcliDAL = new NHibernateDAL<SituacaoForCliDTO>(session);
                    resultado = situacaoforcliDAL.select(situacaoforcli);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<SituacaoForCliDTO> selectSituacaoForCliPagina(int primeiroResultado, int quantidadeResultados, SituacaoForCliDTO situacaoforcli)
        {
            try
            {
                IList<SituacaoForCliDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SituacaoForCliDTO> situacaoforcliDAL = new NHibernateDAL<SituacaoForCliDTO>(session);
                    resultado = situacaoforcliDAL.selectPagina(primeiroResultado, quantidadeResultados, situacaoforcli);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region AtividadeForCli
        public int deleteAtividadeForCli(AtividadeForCliDTO atividadeforcli)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<AtividadeForCliDTO> atividadeforcliDAL = new NHibernateDAL<AtividadeForCliDTO>(session);
                    atividadeforcliDAL.delete(atividadeforcli);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarAtividadeForCli(AtividadeForCliDTO atividadeforcli)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<AtividadeForCliDTO> atividadeforcliDAL = new NHibernateDAL<AtividadeForCliDTO>(session);
                    atividadeforcliDAL.saveOrUpdate(atividadeforcli);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<AtividadeForCliDTO> selectAtividadeForCli(AtividadeForCliDTO atividadeforcli)
        {
            try
            {
                IList<AtividadeForCliDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<AtividadeForCliDTO> atividadeforcliDAL = new NHibernateDAL<AtividadeForCliDTO>(session);
                    resultado = atividadeforcliDAL.select(atividadeforcli);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<AtividadeForCliDTO> selectAtividadeForCliPagina(int primeiroResultado, int quantiadeResultados, AtividadeForCliDTO atividadeforcli)
        {
            try
            {
                IList<AtividadeForCliDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<AtividadeForCliDTO> atividadeforcliDAL = new NHibernateDAL<AtividadeForCliDTO>(session);
                    resultado = atividadeforcliDAL.selectPagina(primeiroResultado, quantiadeResultados, atividadeforcli);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region Cheque
        public int deleteCheque(ChequeDTO cheque)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ChequeDTO> chequeDAL = new NHibernateDAL<ChequeDTO>(session);
                    chequeDAL.delete(cheque);
                    session.Flush();
                    resultado = 0;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarCheque(ChequeDTO cheque)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ChequeDTO> chequeDAL = new NHibernateDAL<ChequeDTO>(session);
                    chequeDAL.saveOrUpdate(cheque);
                    session.Flush();
                    resultado = 0;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<ChequeDTO> selectCheque(ChequeDTO cheque)
        {
            try
            {
                IList<ChequeDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ChequeDTO> chequeDAL = new NHibernateDAL<ChequeDTO>(session);
                    resultado = chequeDAL.select(cheque);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<ChequeDTO> selectChequePagina(int primeiroResultado, int quantidadeResultados, ChequeDTO cheque)
        {
            try
            {
                IList<ChequeDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ChequeDTO> chequeDAL = new NHibernateDAL<ChequeDTO>(session);
                    resultado = chequeDAL.selectPagina(primeiroResultado, quantidadeResultados, cheque);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region TalonarioCheque
        public int deleteTalonarioCheque(TalonarioChequeDTO talonariocheque)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TalonarioChequeDTO> talonariochequeDAL = new NHibernateDAL<TalonarioChequeDTO>(session);
                    talonariochequeDAL.delete(talonariocheque);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarTalonarioCheque(TalonarioChequeDTO talonariocheque)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TalonarioChequeDTO> talonariochequeDAL = new NHibernateDAL<TalonarioChequeDTO>(session);
                    talonariochequeDAL.saveOrUpdate(talonariocheque);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<TalonarioChequeDTO> selectTalonarioCheque(TalonarioChequeDTO talonariocheque)
        {
            try
            {
                IList<TalonarioChequeDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TalonarioChequeDTO> talonariochequeDAL = new NHibernateDAL<TalonarioChequeDTO>(session);
                    resultado = talonariochequeDAL.select(talonariocheque);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<TalonarioChequeDTO> selectTalonarioChequePagina(int primeiroResultado, int quantidadeResultados, TalonarioChequeDTO talonariocheque)
        {
            try
            {
                IList<TalonarioChequeDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TalonarioChequeDTO> talonariochequeDAL = new NHibernateDAL<TalonarioChequeDTO>(session);
                    resultado = talonariochequeDAL.selectPagina(primeiroResultado, quantidadeResultados, talonariocheque);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region ContaCaixa
        public int deleteContaCaixa(ContaCaixaDTO contacaixa)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ContaCaixaDTO> contacaixaDAL = new NHibernateDAL<ContaCaixaDTO>(session);
                    contacaixaDAL.delete(contacaixa);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarContaCaixa(ContaCaixaDTO contacaixa)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ContaCaixaDTO> contacaixaDAL = new NHibernateDAL<ContaCaixaDTO>(session);
                    contacaixaDAL.saveOrUpdate(contacaixa);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<ContaCaixaDTO> selectContaCaixa(ContaCaixaDTO contacaixa)
        {
            try
            {
                IList<ContaCaixaDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ContaCaixaDTO> contacaixaDAL = new NHibernateDAL<ContaCaixaDTO>(session);
                    resultado = contacaixaDAL.select(contacaixa);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<ContaCaixaDTO> selectContaCaixaPagina(int primeiroResultado, int quantidadeResultados, ContaCaixaDTO contacaixa)
        {
            try
            {
                IList<ContaCaixaDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ContaCaixaDTO> contacaixaDAL = new NHibernateDAL<ContaCaixaDTO>(session);
                    resultado = contacaixaDAL.selectPagina(primeiroResultado, quantidadeResultados, contacaixa);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region Convenio
        public int deleteConvenio(ConvenioDTO convenio)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ConvenioDTO> convenioDAL = new NHibernateDAL<ConvenioDTO>(session);
                    convenioDAL.delete(convenio);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarConvenio(ConvenioDTO convenio)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ConvenioDTO> convenioDAL = new NHibernateDAL<ConvenioDTO>(session);
                    convenioDAL.saveOrUpdate(convenio);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<ConvenioDTO> selectConvenio(ConvenioDTO convenio)
        {
            try
            {
                IList<ConvenioDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ConvenioDTO> convenioDAL = new NHibernateDAL<ConvenioDTO>(session);
                    resultado = convenioDAL.select(convenio);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public IList<ConvenioDTO> selectConvenioPagina(int primeiroResultado, int quantidadeResultados, ConvenioDTO convenio)
        {
            try
            {
                IList<ConvenioDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ConvenioDTO> convenioDAL = new NHibernateDAL<ConvenioDTO>(session);
                    resultado = convenioDAL.selectPagina(primeiroResultado, quantidadeResultados, convenio);
                }
                return resultado;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region OperadoraCartao
        public int deleteOperadoraCartao(OperadoraCartaoDTO operadoracartao)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<OperadoraCartaoDTO> operadoracartaoDAL = new NHibernateDAL<OperadoraCartaoDTO>(session);
                    operadoracartaoDAL.delete(operadoracartao);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarOperadoraCartao(OperadoraCartaoDTO operadoracartao)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<OperadoraCartaoDTO> operadoracartaoDAL = new NHibernateDAL<OperadoraCartaoDTO>(session);
                    operadoracartaoDAL.saveOrUpdate(operadoracartao);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<OperadoraCartaoDTO> selectOperadoraCartao(OperadoraCartaoDTO operadoracartao)
        {
            try
            {
                IList<OperadoraCartaoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<OperadoraCartaoDTO> operadoracartaoDAL = new NHibernateDAL<OperadoraCartaoDTO>(session);
                    resultado = operadoracartaoDAL.select(operadoracartao);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<OperadoraCartaoDTO> selectOperadoraCartaoPagina(int primeiroResultado, int quantidadeResultados, OperadoraCartaoDTO operadoracartao)
        {
            try
            {
                IList<OperadoraCartaoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<OperadoraCartaoDTO> operadoracartaoDAL = new NHibernateDAL<OperadoraCartaoDTO>(session);
                    resultado = operadoracartaoDAL.selectPagina(primeiroResultado, quantidadeResultados, operadoracartao);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region OperadoraPlanoSaude
        public int deleteOperadoraPlanoSaude(OperadoraPlanoSaudeDTO operadoraplanosaude)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<OperadoraPlanoSaudeDTO> operadoraplanosaudeDAL = new NHibernateDAL<OperadoraPlanoSaudeDTO>(session);
                    operadoraplanosaudeDAL.delete(operadoraplanosaude);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarOperadoraPlanoSaude(OperadoraPlanoSaudeDTO operadoraplanosaude)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<OperadoraPlanoSaudeDTO> operadoraplanosaudeDAL = new NHibernateDAL<OperadoraPlanoSaudeDTO>(session);
                    operadoraplanosaudeDAL.saveOrUpdate(operadoraplanosaude);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<OperadoraPlanoSaudeDTO> selectOperadoraPlanoSaude(OperadoraPlanoSaudeDTO operadoraplanosaude)
        {
            try
            {
                IList<OperadoraPlanoSaudeDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<OperadoraPlanoSaudeDTO> operadoraplanosaudeDAL = new NHibernateDAL<OperadoraPlanoSaudeDTO>(session);
                    resultado = operadoraplanosaudeDAL.select(operadoraplanosaude);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<OperadoraPlanoSaudeDTO> selectOperadoraPlanoSaudePagina(int primeiroResultado, int quantidadeResultados, OperadoraPlanoSaudeDTO operadoraplanosaude)
        {
            try
            {
                IList<OperadoraPlanoSaudeDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<OperadoraPlanoSaudeDTO> operadoraplanosaudeDAL = new NHibernateDAL<OperadoraPlanoSaudeDTO>(session);
                    resultado = operadoraplanosaudeDAL.selectPagina(primeiroResultado, quantidadeResultados, operadoraplanosaude);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region Sindicato
        public int deleteSindicato(SindicatoDTO sindicato)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SindicatoDTO> DAL = new NHibernateDAL<SindicatoDTO>(session);
                    DAL.delete(sindicato);
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


        public SindicatoDTO salvarAtualizarSindicato(SindicatoDTO sindicato)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SindicatoDTO> DAL = new NHibernateDAL<SindicatoDTO>(session);
                    DAL.saveOrUpdate(sindicato);
                    session.Flush();
                }
                return sindicato;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<SindicatoDTO> selectSindicato(SindicatoDTO sindicato)
        {
            try
            {
                IList<SindicatoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SindicatoDTO> DAL = new NHibernateDAL<SindicatoDTO>(session);
                    resultado = DAL.select(sindicato);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<SindicatoDTO> selectSindicatoPagina(int primeiroResultado, int quantidadeResultados, SindicatoDTO sindicato)
        {
            try
            {
                IList<SindicatoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SindicatoDTO> DAL = new NHibernateDAL<SindicatoDTO>(session);
                    resultado = DAL.selectPagina<SindicatoDTO>(primeiroResultado, quantidadeResultados, sindicato);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region SituacaoColaborador
        public int deleteSituacaoColaborador(SituacaoColaboradorDTO situacaoColaborador)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SituacaoColaboradorDTO> DAL = new NHibernateDAL<SituacaoColaboradorDTO>(session);
                    DAL.delete(situacaoColaborador);
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


        public SituacaoColaboradorDTO salvarAtualizarSituacaoColaborador(SituacaoColaboradorDTO situacaoColaborador)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SituacaoColaboradorDTO> DAL = new NHibernateDAL<SituacaoColaboradorDTO>(session);
                    DAL.saveOrUpdate(situacaoColaborador);
                    session.Flush();
                }
                return situacaoColaborador;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<SituacaoColaboradorDTO> selectSituacaoColaborador(SituacaoColaboradorDTO situacaoColaborador)
        {
            try
            {
                IList<SituacaoColaboradorDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SituacaoColaboradorDTO> DAL = new NHibernateDAL<SituacaoColaboradorDTO>(session);
                    resultado = DAL.select(situacaoColaborador);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<SituacaoColaboradorDTO> selectSituacaoColaboradorPagina(int primeiroResultado, int quantidadeResultados, SituacaoColaboradorDTO situacaoColaborador)
        {
            try
            {
                IList<SituacaoColaboradorDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SituacaoColaboradorDTO> DAL = new NHibernateDAL<SituacaoColaboradorDTO>(session);
                    resultado = DAL.selectPagina<SituacaoColaboradorDTO>(primeiroResultado, quantidadeResultados, situacaoColaborador);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region EstadoCivil
        public int deleteEstadoCivil(EstadoCivilDTO estadoCivil)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EstadoCivilDTO> DAL = new NHibernateDAL<EstadoCivilDTO>(session);
                    DAL.delete(estadoCivil);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public int salvarAtualizarEstadoCivil(EstadoCivilDTO estadoCivil)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EstadoCivilDTO> DAL = new NHibernateDAL<EstadoCivilDTO>(session);
                    DAL.saveOrUpdate(estadoCivil);
                    session.Flush();
                    resultado = 0;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<EstadoCivilDTO> selectEstadoCivil(EstadoCivilDTO estadoCivil)
        {
            try
            {
                IList<EstadoCivilDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EstadoCivilDTO> DAL = new NHibernateDAL<EstadoCivilDTO>(session);
                    resultado = DAL.select(estadoCivil);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<EstadoCivilDTO> selectEstadoCivilPagina(int primeiroResultado, int quantidadeResultados, EstadoCivilDTO estadoCivil)
        {
            try
            {
                IList<EstadoCivilDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EstadoCivilDTO> situacaocolaboradorDAL = new NHibernateDAL<EstadoCivilDTO>(session);
                    resultado = situacaocolaboradorDAL.selectPagina(primeiroResultado, quantidadeResultados, estadoCivil);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region TipoAdmissao
        public int deleteTipoAdmissao(TipoAdmissaoDTO tipoAdmissao)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoAdmissaoDTO> DAL = new NHibernateDAL<TipoAdmissaoDTO>(session);
                    DAL.delete(tipoAdmissao);
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


        public TipoAdmissaoDTO salvarAtualizarTipoAdmissao(TipoAdmissaoDTO tipoAdmissao)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoAdmissaoDTO> DAL = new NHibernateDAL<TipoAdmissaoDTO>(session);
                    DAL.saveOrUpdate(tipoAdmissao);
                    session.Flush();
                }
                return tipoAdmissao;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TipoAdmissaoDTO> selectTipoAdmissao(TipoAdmissaoDTO tipoAdmissao)
        {
            try
            {
                IList<TipoAdmissaoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoAdmissaoDTO> DAL = new NHibernateDAL<TipoAdmissaoDTO>(session);
                    resultado = DAL.select(tipoAdmissao);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TipoAdmissaoDTO> selectTipoAdmissaoPagina(int primeiroResultado, int quantidadeResultados, TipoAdmissaoDTO tipoAdmissao)
        {
            try
            {
                IList<TipoAdmissaoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoAdmissaoDTO> DAL = new NHibernateDAL<TipoAdmissaoDTO>(session);
                    resultado = DAL.selectPagina<TipoAdmissaoDTO>(primeiroResultado, quantidadeResultados, tipoAdmissao);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region TipoRelacionamento
        public int deleteTipoRelacionamento(TipoRelacionamentoDTO tipoRelacionamento)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoRelacionamentoDTO> DAL = new NHibernateDAL<TipoRelacionamentoDTO>(session);
                    DAL.delete(tipoRelacionamento);
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


        public TipoRelacionamentoDTO salvarAtualizarTipoRelacionamento(TipoRelacionamentoDTO tipoRelacionamento)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoRelacionamentoDTO> DAL = new NHibernateDAL<TipoRelacionamentoDTO>(session);
                    DAL.saveOrUpdate(tipoRelacionamento);
                    session.Flush();
                }
                return tipoRelacionamento;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TipoRelacionamentoDTO> selectTipoRelacionamento(TipoRelacionamentoDTO tipoRelacionamento)
        {
            try
            {
                IList<TipoRelacionamentoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoRelacionamentoDTO> DAL = new NHibernateDAL<TipoRelacionamentoDTO>(session);
                    resultado = DAL.select(tipoRelacionamento);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TipoRelacionamentoDTO> selectTipoRelacionamentoPagina(int primeiroResultado, int quantidadeResultados, TipoRelacionamentoDTO tipoRelacionamento)
        {
            try
            {
                IList<TipoRelacionamentoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoRelacionamentoDTO> DAL = new NHibernateDAL<TipoRelacionamentoDTO>(session);
                    resultado = DAL.selectPagina<TipoRelacionamentoDTO>(primeiroResultado, quantidadeResultados, tipoRelacionamento);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region TipoColaborador
        public int deleteTipoColaborador(TipoColaboradorDTO tipoColaborador)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoColaboradorDTO> DAL = new NHibernateDAL<TipoColaboradorDTO>(session);
                    DAL.delete(tipoColaborador);
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


        public TipoColaboradorDTO salvarAtualizarTipoColaborador(TipoColaboradorDTO tipoColaborador)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoColaboradorDTO> DAL = new NHibernateDAL<TipoColaboradorDTO>(session);
                    DAL.saveOrUpdate(tipoColaborador);
                    session.Flush();
                }
                return tipoColaborador;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TipoColaboradorDTO> selectTipoColaborador(TipoColaboradorDTO tipoColaborador)
        {
            try
            {
                IList<TipoColaboradorDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoColaboradorDTO> DAL = new NHibernateDAL<TipoColaboradorDTO>(session);
                    resultado = DAL.select(tipoColaborador);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TipoColaboradorDTO> selectTipoColaboradorPagina(int primeiroResultado, int quantidadeResultados, TipoColaboradorDTO tipoColaborador)
        {
            try
            {
                IList<TipoColaboradorDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TipoColaboradorDTO> DAL = new NHibernateDAL<TipoColaboradorDTO>(session);
                    resultado = DAL.selectPagina<TipoColaboradorDTO>(primeiroResultado, quantidadeResultados, tipoColaborador);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region Cargo
        public int deleteCargo(CargoDTO cargo)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CargoDTO> DAL = new NHibernateDAL<CargoDTO>(session);
                    DAL.delete(cargo);
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


        public CargoDTO salvarAtualizarCargo(CargoDTO cargo)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CargoDTO> DAL = new NHibernateDAL<CargoDTO>(session);
                    DAL.saveOrUpdate(cargo);
                    session.Flush();
                }
                return cargo;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<CargoDTO> selectCargo(CargoDTO cargo)
        {
            try
            {
                IList<CargoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CargoDTO> DAL = new NHibernateDAL<CargoDTO>(session);
                    resultado = DAL.select(cargo);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<CargoDTO> selectCargoPagina(int primeiroResultado, int quantidadeResultados, CargoDTO cargo)
        {
            try
            {
                IList<CargoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<CargoDTO> DAL = new NHibernateDAL<CargoDTO>(session);
                    resultado = DAL.selectPagina<CargoDTO>(primeiroResultado, quantidadeResultados, cargo);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region Setor
        public int deleteSetor(SetorDTO setor)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SetorDTO> DAL = new NHibernateDAL<SetorDTO>(session);
                    DAL.delete(setor);
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


        public SetorDTO salvarAtualizarSetor(SetorDTO setor)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SetorDTO> DAL = new NHibernateDAL<SetorDTO>(session);
                    DAL.saveOrUpdate(setor);
                    session.Flush();
                }
                return setor;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<SetorDTO> selectSetor(SetorDTO setor)
        {
            try
            {
                IList<SetorDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SetorDTO> DAL = new NHibernateDAL<SetorDTO>(session);
                    resultado = DAL.select(setor);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<SetorDTO> selectSetorPagina(int primeiroResultado, int quantidadeResultados, SetorDTO setor)
        {
            try
            {
                IList<SetorDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<SetorDTO> DAL = new NHibernateDAL<SetorDTO>(session);
                    resultado = DAL.selectPagina<SetorDTO>(primeiroResultado, quantidadeResultados, setor);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region Colaborador
        public int deleteColaborador(ColaboradorDTO colaborador)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ColaboradorDTO> DAL = new NHibernateDAL<ColaboradorDTO>(session);
                    DAL.delete(colaborador);
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


        public ColaboradorDTO salvarAtualizarColaborador(ColaboradorDTO colaborador)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ColaboradorDTO> DAL = new NHibernateDAL<ColaboradorDTO>(session);
                    DAL.saveOrUpdate(colaborador);
                    session.Flush();
                }
                return colaborador;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ColaboradorDTO> selectColaborador(ColaboradorDTO colaborador)
        {
            try
            {
                IList<ColaboradorDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ColaboradorDTO> DAL = new NHibernateDAL<ColaboradorDTO>(session);
                    resultado = DAL.select(colaborador);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ColaboradorDTO> selectColaboradorPagina(int primeiroResultado, int quantidadeResultados, ColaboradorDTO colaborador)
        {
            try
            {
                IList<ColaboradorDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ColaboradorDTO> DAL = new NHibernateDAL<ColaboradorDTO>(session);
                    resultado = DAL.selectPagina<ColaboradorDTO>(primeiroResultado, quantidadeResultados, colaborador);
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
        public int deleteContador(ContadorDTO contador)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ContadorDTO> DAL = new NHibernateDAL<ContadorDTO>(session);
                    DAL.delete(contador);
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


        public ContadorDTO salvarAtualizarContador(ContadorDTO contador)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ContadorDTO> DAL = new NHibernateDAL<ContadorDTO>(session);
                    DAL.saveOrUpdate(contador);
                    session.Flush();
                }
                return contador;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


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

        #region ProdutoMarca
        public int deleteProdutoMarca(ProdutoMarcaDTO produtoMarca)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoMarcaDTO> DAL = new NHibernateDAL<ProdutoMarcaDTO>(session);
                    DAL.delete(produtoMarca);
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


        public ProdutoMarcaDTO salvarAtualizarProdutoMarca(ProdutoMarcaDTO produtoMarca)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoMarcaDTO> DAL = new NHibernateDAL<ProdutoMarcaDTO>(session);
                    DAL.saveOrUpdate(produtoMarca);
                    session.Flush();
                }
                return produtoMarca;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ProdutoMarcaDTO> selectProdutoMarca(ProdutoMarcaDTO produtoMarca)
        {
            try
            {
                IList<ProdutoMarcaDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoMarcaDTO> DAL = new NHibernateDAL<ProdutoMarcaDTO>(session);
                    resultado = DAL.select(produtoMarca);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ProdutoMarcaDTO> selectProdutoMarcaPagina(int primeiroResultado, int quantidadeResultados, ProdutoMarcaDTO produtoMarca)
        {
            try
            {
                IList<ProdutoMarcaDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoMarcaDTO> DAL = new NHibernateDAL<ProdutoMarcaDTO>(session);
                    resultado = DAL.selectPagina<ProdutoMarcaDTO>(primeiroResultado, quantidadeResultados, produtoMarca);
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
        public int deleteUnidadeProduto(UnidadeProdutoDTO unidadeProduto)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<UnidadeProdutoDTO> DAL = new NHibernateDAL<UnidadeProdutoDTO>(session);
                    DAL.delete(unidadeProduto);
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


        public UnidadeProdutoDTO salvarAtualizarUnidadeProduto(UnidadeProdutoDTO unidadeProduto)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<UnidadeProdutoDTO> DAL = new NHibernateDAL<UnidadeProdutoDTO>(session);
                    DAL.saveOrUpdate(unidadeProduto);
                    session.Flush();
                }
                return unidadeProduto;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


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

        #region ProdutoGrupo
        public int deleteProdutoGrupo(ProdutoGrupoDTO produtoGrupo)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoGrupoDTO> DAL = new NHibernateDAL<ProdutoGrupoDTO>(session);
                    DAL.delete(produtoGrupo);
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


        public ProdutoGrupoDTO salvarAtualizarProdutoGrupo(ProdutoGrupoDTO produtoGrupo)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoGrupoDTO> DAL = new NHibernateDAL<ProdutoGrupoDTO>(session);
                    DAL.saveOrUpdate(produtoGrupo);
                    session.Flush();
                }
                return produtoGrupo;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ProdutoGrupoDTO> selectProdutoGrupo(ProdutoGrupoDTO produtoGrupo)
        {
            try
            {
                IList<ProdutoGrupoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoGrupoDTO> DAL = new NHibernateDAL<ProdutoGrupoDTO>(session);
                    resultado = DAL.select(produtoGrupo);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ProdutoGrupoDTO> selectProdutoGrupoPagina(int primeiroResultado, int quantidadeResultados, ProdutoGrupoDTO produtoGrupo)
        {
            try
            {
                IList<ProdutoGrupoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoGrupoDTO> DAL = new NHibernateDAL<ProdutoGrupoDTO>(session);
                    resultado = DAL.selectPagina<ProdutoGrupoDTO>(primeiroResultado, quantidadeResultados, produtoGrupo);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ProdutoSubGrupo
        public int deleteProdutoSubGrupo(ProdutoSubGrupoDTO produtoSubGrupo)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoSubGrupoDTO> DAL = new NHibernateDAL<ProdutoSubGrupoDTO>(session);
                    DAL.delete(produtoSubGrupo);
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


        public ProdutoSubGrupoDTO salvarAtualizarProdutoSubGrupo(ProdutoSubGrupoDTO produtoSubGrupo)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoSubGrupoDTO> DAL = new NHibernateDAL<ProdutoSubGrupoDTO>(session);
                    DAL.saveOrUpdate(produtoSubGrupo);
                    session.Flush();
                }
                return produtoSubGrupo;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ProdutoSubGrupoDTO> selectProdutoSubGrupo(ProdutoSubGrupoDTO produtoSubGrupo)
        {
            try
            {
                IList<ProdutoSubGrupoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoSubGrupoDTO> DAL = new NHibernateDAL<ProdutoSubGrupoDTO>(session);
                    resultado = DAL.select(produtoSubGrupo);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ProdutoSubGrupoDTO> selectProdutoSubGrupoPagina(int primeiroResultado, int quantidadeResultados, ProdutoSubGrupoDTO produtoSubGrupo)
        {
            try
            {
                IList<ProdutoSubGrupoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoSubGrupoDTO> DAL = new NHibernateDAL<ProdutoSubGrupoDTO>(session);
                    resultado = DAL.selectPagina<ProdutoSubGrupoDTO>(primeiroResultado, quantidadeResultados, produtoSubGrupo);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region Almoxarifado
        public int deleteAlmoxarifado(AlmoxarifadoDTO almoxarifado)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<AlmoxarifadoDTO> DAL = new NHibernateDAL<AlmoxarifadoDTO>(session);
                    DAL.delete(almoxarifado);
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


        public AlmoxarifadoDTO salvarAtualizarAlmoxarifado(AlmoxarifadoDTO almoxarifado)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<AlmoxarifadoDTO> DAL = new NHibernateDAL<AlmoxarifadoDTO>(session);
                    DAL.saveOrUpdate(almoxarifado);
                    session.Flush();
                }
                return almoxarifado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<AlmoxarifadoDTO> selectAlmoxarifado(AlmoxarifadoDTO almoxarifado)
        {
            try
            {
                IList<AlmoxarifadoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<AlmoxarifadoDTO> DAL = new NHibernateDAL<AlmoxarifadoDTO>(session);
                    resultado = DAL.select(almoxarifado);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<AlmoxarifadoDTO> selectAlmoxarifadoPagina(int primeiroResultado, int quantidadeResultados, AlmoxarifadoDTO almoxarifado)
        {
            try
            {
                IList<AlmoxarifadoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<AlmoxarifadoDTO> DAL = new NHibernateDAL<AlmoxarifadoDTO>(session);
                    resultado = DAL.selectPagina<AlmoxarifadoDTO>(primeiroResultado, quantidadeResultados, almoxarifado);
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
        public int deleteProduto(ProdutoDTO produto)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoDTO> DAL = new NHibernateDAL<ProdutoDTO>(session);
                    DAL.delete(produto);
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


        public ProdutoDTO salvarAtualizarProduto(ProdutoDTO produto)
        {
            try
            {
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoDTO> DAL = new NHibernateDAL<ProdutoDTO>(session);
                    DAL.saveOrUpdate(produto);
                    session.Flush();
                }
                return produto;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


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

        #region Apenas Consultas

        #region Empresa
        public IList<EmpresaDTO> selectEmpresa(EmpresaDTO empresa)
        {
            try
            {
                IList<EmpresaDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EmpresaDTO> DAL = new NHibernateDAL<EmpresaDTO>(session);
                    resultado = DAL.select(empresa);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        #endregion

        #region ContabilConta
        public IList<ContabilContaDTO> selectContabilConta(ContabilContaDTO contabilConta)
        {
            try
            {
                IList<ContabilContaDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ContabilContaDTO> DAL = new NHibernateDAL<ContabilContaDTO>(session);
                    resultado = DAL.select(contabilConta);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ContabilContaDTO> selectContabilContaPagina(int primeiroResultado, int quantidadeResultados, ContabilContaDTO contabilConta)
        {
            try
            {
                IList<ContabilContaDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ContabilContaDTO> DAL = new NHibernateDAL<ContabilContaDTO>(session);
                    resultado = DAL.selectPagina<ContabilContaDTO>(primeiroResultado, quantidadeResultados, contabilConta);
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

        #region NivelFormacao
        public IList<NivelFormacaoDTO> selectNivelFormacao(NivelFormacaoDTO nivelFormacao)
        {
            try
            {
                IList<NivelFormacaoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<NivelFormacaoDTO> DAL = new NHibernateDAL<NivelFormacaoDTO>(session);
                    resultado = DAL.select(nivelFormacao);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<NivelFormacaoDTO> selectNivelFormacaoPagina(int primeiroResultado, int quantidadeResultados, NivelFormacaoDTO nivelFormacao)
        {
            try
            {
                IList<NivelFormacaoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<NivelFormacaoDTO> DAL = new NHibernateDAL<NivelFormacaoDTO>(session);
                    resultado = DAL.selectPagina<NivelFormacaoDTO>(primeiroResultado, quantidadeResultados, nivelFormacao);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region TributGrupoTributario

        public IList<TributGrupoTributarioDTO> selectTributGrupoTributario(TributGrupoTributarioDTO tributGrupoTributario)
        {
            try
            {
                IList<TributGrupoTributarioDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributGrupoTributarioDTO> DAL = new NHibernateDAL<TributGrupoTributarioDTO>(session);
                    resultado = DAL.select(tributGrupoTributario);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TributGrupoTributarioDTO> selectTributGrupoTributarioPagina(int primeiroResultado, int quantidadeResultados, TributGrupoTributarioDTO tributGrupoTributario)
        {
            try
            {
                IList<TributGrupoTributarioDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributGrupoTributarioDTO> DAL = new NHibernateDAL<TributGrupoTributarioDTO>(session);
                    resultado = DAL.selectPagina<TributGrupoTributarioDTO>(primeiroResultado, quantidadeResultados, tributGrupoTributario);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region TributIcmsCustomCab

        public IList<TributIcmsCustomCabDTO> selectTributIcmsCustomCab(TributIcmsCustomCabDTO tributIcmsCustomCab)
        {
            try
            {
                IList<TributIcmsCustomCabDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributIcmsCustomCabDTO> DAL = new NHibernateDAL<TributIcmsCustomCabDTO>(session);
                    resultado = DAL.select(tributIcmsCustomCab);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TributIcmsCustomCabDTO> selectTributIcmsCustomCabPagina(int primeiroResultado, int quantidadeResultados, TributIcmsCustomCabDTO tributIcmsCustomCab)
        {
            try
            {
                IList<TributIcmsCustomCabDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributIcmsCustomCabDTO> DAL = new NHibernateDAL<TributIcmsCustomCabDTO>(session);
                    resultado = DAL.selectPagina<TributIcmsCustomCabDTO>(primeiroResultado, quantidadeResultados, tributIcmsCustomCab);
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

        #endregion


    }

}
