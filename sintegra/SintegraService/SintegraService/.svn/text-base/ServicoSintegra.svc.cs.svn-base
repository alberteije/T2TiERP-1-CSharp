using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SintegraService.Model;
using NHibernate;
using SintegraService.NHibernate;
using NHibernate.Criterion;

namespace SintegraService
{
    public class ServicoSintegra : IServicoSintegra
    {


        public IList<NFeCabecalhoDTO> selectNFeCabecalho(DateTime dataInicio, DateTime dataFim, NFeCabecalhoDTO nfeCabecalho)
        {
            try
            {
                dataInicio = dataInicio.Date;
                dataFim = dataFim.Date.AddDays(1).AddSeconds(-1);
                IList<NFeCabecalhoDTO> resultado = new List<NFeCabecalhoDTO>();
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    Example example = Example.Create(nfeCabecalho).EnableLike(MatchMode.Anywhere).IgnoreCase().ExcludeNulls().ExcludeZeroes();
                    resultado = session.CreateCriteria(typeof(NFeCabecalhoDTO)).Add(example).Add(Expression.Between("dataEmissao", dataInicio, dataFim)).List<NFeCabecalhoDTO>();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public NFeCabecalhoDTO selectNFeCabecalhoId(int id)
        {
            try
            {
                NFeCabecalhoDTO resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<NFeCabecalhoDTO> nfeDAL = new NHibernateDAL<NFeCabecalhoDTO>(session);
                    resultado = nfeDAL.selectId<NFeCabecalhoDTO>(id);

                    NHibernateDAL<NFeDestinatarioDTO> nfeDest = new NHibernateDAL<NFeDestinatarioDTO>(session);
                    IList<NFeDestinatarioDTO> listDest = nfeDest.select<NFeDestinatarioDTO>(new NFeDestinatarioDTO { idNFeCabecalho = id });
                    if (listDest.Count > 0)
                    {
                        resultado.destinatario = listDest.First();
                    }

                    NHibernateDAL<NFeEmitenteDTO> nfeEmit = new NHibernateDAL<NFeEmitenteDTO>(session);
                    IList<NFeEmitenteDTO> listEmit = nfeDest.select<NFeEmitenteDTO>(new NFeEmitenteDTO { idNFeCabecalho = id });
                    if (listEmit.Count > 0)
                    {
                        resultado.emitente = listEmit.First();
                    }

                    NHibernateDAL<NFeLocalEntregaDTO> nfeLE = new NHibernateDAL<NFeLocalEntregaDTO>(session);
                    IList<NFeLocalEntregaDTO> listLE = nfeLE.select<NFeLocalEntregaDTO>(new NFeLocalEntregaDTO { idNFeCabecalho = id });
                    if (listLE.Count > 0)
                    {
                        resultado.localEntrega = listLE.First();
                    }

                    NHibernateDAL<NFeLocalRetiradaDTO> nfeLR = new NHibernateDAL<NFeLocalRetiradaDTO>(session);
                    IList<NFeLocalRetiradaDTO> listLR = nfeLR.select<NFeLocalRetiradaDTO>(new NFeLocalRetiradaDTO { idNFeCabecalho = id });
                    if (listLR.Count > 0)
                    {
                        resultado.localRetirada = listLR.First();
                    }

                    NHibernateDAL<NFeTransporteDTO> nfeTransporte = new NHibernateDAL<NFeTransporteDTO>(session);
                    IList<NFeTransporteDTO> listTransp = nfeTransporte.select<NFeTransporteDTO>(new NFeTransporteDTO { idNFeCabecalho = id });
                    if (listTransp.Count > 0)
                    {
                        resultado.transporte = listTransp.First();
                    }

                    NHibernateDAL<NFeFaturaDTO> nfeFatura = new NHibernateDAL<NFeFaturaDTO>(session);
                    IList<NFeFaturaDTO> listFat = nfeFatura.select<NFeFaturaDTO>(new NFeFaturaDTO { idNFeCabecalho = id });
                    if (listFat.Count > 0)
                    {
                        resultado.fatura = listFat.First();
                    }

                    NHibernateDAL<NFeCupomFiscalDTO> nfeCF = new NHibernateDAL<NFeCupomFiscalDTO>(session);
                    resultado.listaCupomFiscal = nfeCF.select<NFeCupomFiscalDTO>(new NFeCupomFiscalDTO { idNFeCabecalho = id });

                    NHibernateDAL<NFeDetalheDTO> nfeDetDAL = new NHibernateDAL<NFeDetalheDTO>(session);
                    resultado.listaDetalhe = nfeDetDAL.select<NFeDetalheDTO>(new NFeDetalheDTO { idNFeCabecalho = id });

                    foreach (NFeDetalheDTO item in resultado.listaDetalhe)
                    {
                        NHibernateDAL<NfeDetalheImpostoCofinsDTO> nfeDetCofins = new NHibernateDAL<NfeDetalheImpostoCofinsDTO>(session);
                        item.impostoCofins = nfeDetCofins.selectObjeto<NfeDetalheImpostoCofinsDTO>(new NfeDetalheImpostoCofinsDTO { idNFeDetalhe = item.id });

                        NHibernateDAL<NfeDetalheImpostoIcmsDTO> nfeDetIcms = new NHibernateDAL<NfeDetalheImpostoIcmsDTO>(session);
                        item.impostoIcms = nfeDetIcms.selectObjeto<NfeDetalheImpostoIcmsDTO>(new NfeDetalheImpostoIcmsDTO { idNFeDetalhe = item.id });

                        NHibernateDAL<NfeDetalheImpostoIssqnDTO> nfeDetIss = new NHibernateDAL<NfeDetalheImpostoIssqnDTO>(session);
                        item.impostoIss = nfeDetIss.selectObjeto<NfeDetalheImpostoIssqnDTO>(new NfeDetalheImpostoIssqnDTO { idNFeDetalhe = item.id });

                        NHibernateDAL<NfeDetalheImpostoPisDTO> nfeDetPis = new NHibernateDAL<NfeDetalheImpostoPisDTO>(session);
                        item.impostoPis = nfeDetPis.selectObjeto<NfeDetalheImpostoPisDTO>(new NfeDetalheImpostoPisDTO { idNFeDetalhe = item.id });

                        NHibernateDAL<NfeDetalheImpostoIpiDTO> nfeDetIpi = new NHibernateDAL<NfeDetalheImpostoIpiDTO>(session);
                        item.impostoIpi = nfeDetIpi.selectObjeto<NfeDetalheImpostoIpiDTO>(new NfeDetalheImpostoIpiDTO { idNFeDetalhe = item.id });

                        NHibernateDAL<NfeDetalheImpostoIiDTO> nfeDetII = new NHibernateDAL<NfeDetalheImpostoIiDTO>(session);
                        item.impostoII = nfeDetII.selectObjeto<NfeDetalheImpostoIiDTO>(new NfeDetalheImpostoIiDTO { idNFeDetalhe = item.id });
                    }


                    NHibernateDAL<NFeDuplicataDTO> nfeDupl = new NHibernateDAL<NFeDuplicataDTO>(session);
                    resultado.listaDuplicata = nfeDupl.select<NFeDuplicataDTO>(new NFeDuplicataDTO { idNFeCabecalho = id });
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public EmpresaDTO selectEmpresaId(int id)
        {
            try
            {
                EmpresaDTO resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<EmpresaDTO> empresaDAL = new NHibernateDAL<EmpresaDTO>(session);
                    resultado = empresaDAL.selectId<EmpresaDTO>(id);


                    NHibernateDAL<EnderecoDTO> endDAL = new NHibernateDAL<EnderecoDTO>(session);
                    IList<EnderecoDTO> listaEnd = endDAL.select(new EnderecoDTO { idEmpresa = resultado.Id, principal = "S" });
                    if (listaEnd.Count > 0)
                        resultado.endereco = listaEnd.First();

                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

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

    }
}
