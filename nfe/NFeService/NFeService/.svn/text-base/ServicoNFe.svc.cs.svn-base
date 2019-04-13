using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using NFeService.Model;
using NHibernate;
using NFeService.NHibernate;
using NFeService.Util;

namespace NFeService
{
    public class ServicoNFe : INFe
    {
        public ServicoNFe()
        {
            NHibernateHelper.getSessionFactory().OpenSession();
        }

        public IList<NFeCabecalhoDTO> selectNFeCabecalho(NFeCabecalhoDTO nfeCabecalho)
        {
            try
            {
                IList<NFeCabecalhoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<NFeCabecalhoDTO> nfeDAL = new NHibernateDAL<NFeCabecalhoDTO>(session);
                    resultado = nfeDAL.select(nfeCabecalho);
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
                        resultado.localRetirada= listLR.First();
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

        public IList<ProdutoDTO> selectProduto(ProdutoDTO produto)
        {
            try
            {
                IList<ProdutoDTO> resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoDTO> produtoDAL = new NHibernateDAL<ProdutoDTO>(session);
                    resultado = produtoDAL.select(produto);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public int salvarNFeCabecalho(NFeCabecalhoDTO nfeCabecalho)
        {
            try
            {
                int resultado = -1;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<NFeCabecalhoDTO> nfeDAL = new NHibernateDAL<NFeCabecalhoDTO>(session);

                    nfeCabecalho.chaveAcesso = nfeCabecalho.emitente.codigoMunicipio.ToString().Substring(0, 2) +
                    ((DateTime)nfeCabecalho.dataEmissao).ToString("yy") +
                    ((DateTime)nfeCabecalho.dataEmissao).ToString("MM") +
                    nfeCabecalho.emitente.cpfCnpj +
                    (int.Parse(nfeCabecalho.codigoModelo)).ToString("00") +
                    (int.Parse(nfeCabecalho.serie)).ToString("000") +
                    (int.Parse(nfeCabecalho.numero)).ToString("000000000") +
                    nfeCabecalho.finalidadeEmissao +
                    (int.Parse(nfeCabecalho.numero)).ToString("00000000");
                    nfeCabecalho.digitoChaveAcesso = Biblioteca.DigitoModulo11(nfeCabecalho.chaveAcesso);

                    nfeCabecalho.numero = (int.Parse(nfeCabecalho.numero)).ToString("000000000");
                    nfeCabecalho.codigoNumerico = (int.Parse(nfeCabecalho.numero)).ToString("00000000");

                    //Ambiente, 2 - homologacao
                    nfeCabecalho.ambiente = "2";

                    nfeDAL.saveOrUpdate(nfeCabecalho);

                    if (nfeCabecalho.destinatario != null)
                    {
                        NHibernateDAL<NFeDestinatarioDTO> nfeDest = new NHibernateDAL<NFeDestinatarioDTO>(session);
                        nfeCabecalho.destinatario.idNFeCabecalho = nfeCabecalho.id;
                        nfeDest.saveOrUpdate(nfeCabecalho.destinatario);
                    }

                    if (nfeCabecalho.emitente != null)
                    {
                        NHibernateDAL<NFeEmitenteDTO> nfeEmit = new NHibernateDAL<NFeEmitenteDTO>(session);
                        nfeCabecalho.emitente.idNFeCabecalho = nfeCabecalho.id;
                        nfeEmit.saveOrUpdate(nfeCabecalho.emitente);
                    }

                    if (nfeCabecalho.fatura != null)
                    {
                        NHibernateDAL<NFeFaturaDTO> nfeFatura = new NHibernateDAL<NFeFaturaDTO>(session);
                        nfeCabecalho.fatura.idNFeCabecalho = nfeCabecalho.id;
                        nfeFatura.saveOrUpdate(nfeCabecalho.fatura);
                    }

                    if (nfeCabecalho.listaDuplicata.Count > 0)
                    {
                        NHibernateDAL<NFeDuplicataDTO> nfeDuplicata = new NHibernateDAL<NFeDuplicataDTO>(session);

                        IList<NFeDuplicataDTO> listaDuplicataExistente = nfeDuplicata.select(new NFeDuplicataDTO { idNFeCabecalho = nfeCabecalho.id });
                        foreach (NFeDuplicataDTO duplicata in listaDuplicataExistente)
                        {
                            nfeDuplicata.delete(duplicata);
                        }

                        IList<NFeDuplicataDTO> listaDuplic = nfeCabecalho.listaDuplicata;
                        foreach (NFeDuplicataDTO duplic in listaDuplic)
                        {
                            duplic.idNFeCabecalho = nfeCabecalho.id;
                            nfeDuplicata.saveOrUpdate((NFeDuplicataDTO)session.Merge(duplic));
                        }
                    }

                    if (nfeCabecalho.listaCupomFiscal.Count > 0)
                    {
                        NHibernateDAL<NFeCupomFiscalDTO> nfeCF = new NHibernateDAL<NFeCupomFiscalDTO>(session);

                        IList<NFeCupomFiscalDTO> listaCFExistente = nfeCF.select(new NFeCupomFiscalDTO { idNFeCabecalho = nfeCabecalho.id });
                        foreach (NFeCupomFiscalDTO cf in listaCFExistente)
                        {
                            nfeCF.delete(cf);
                        }

                        IList<NFeCupomFiscalDTO> listaCupom = nfeCabecalho.listaCupomFiscal;
                        foreach (NFeCupomFiscalDTO nfeCupom in listaCupom)
                        {
                            nfeCupom.idNFeCabecalho = nfeCabecalho.id;
                            nfeCF.saveOrUpdate((NFeCupomFiscalDTO)session.Merge(nfeCupom));
                        }
                    }

                    if (nfeCabecalho.listaDetalhe.Count > 0)
                    {                                             
                        NHibernateDAL<NFeDetalheDTO> nfeDetDAL = new NHibernateDAL<NFeDetalheDTO>(session);

                        IList<NFeDetalheDTO> listaDetalhe = nfeCabecalho.listaDetalhe;
                        foreach (NFeDetalheDTO nfeDet in listaDetalhe)
                        {
                            nfeDet.idNFeCabecalho = nfeCabecalho.id;
                            nfeDetDAL.saveOrUpdate(nfeDet);

                            nfeDetDAL.saveOrUpdate((NFeDetalheDTO)session.Merge(nfeDet));

                            if (nfeDet.impostoIcms != null)
                            {
                                NHibernateDAL<NfeDetalheImpostoIcmsDTO> impostoIcms = new NHibernateDAL<NfeDetalheImpostoIcmsDTO>(session);
                                nfeDet.impostoIcms.idNFeDetalhe = nfeDet.id;
                                impostoIcms.saveOrUpdate(nfeDet.impostoIcms);
                            }

                            if (nfeDet.impostoCofins != null)
                            {
                                NHibernateDAL<NfeDetalheImpostoCofinsDTO> impostoCofins = new NHibernateDAL<NfeDetalheImpostoCofinsDTO>(session);
                                nfeDet.impostoCofins.idNFeDetalhe = nfeDet.id;
                                impostoCofins.saveOrUpdate(nfeDet.impostoIcms);
                            }

                            if (nfeDet.impostoPis != null)
                            {
                                NHibernateDAL<NfeDetalheImpostoPisDTO> impostoPis = new NHibernateDAL<NfeDetalheImpostoPisDTO>(session);
                                nfeDet.impostoPis.idNFeDetalhe = nfeDet.id;
                                impostoPis.saveOrUpdate(nfeDet.impostoIcms);
                            }

                        }
                    }

                    if (nfeCabecalho.localEntrega != null)
                    {
                        NHibernateDAL<NFeLocalEntregaDTO> nfeLE = new NHibernateDAL<NFeLocalEntregaDTO>(session);
                        nfeCabecalho.localEntrega.idNFeCabecalho = nfeCabecalho.id;
                        nfeLE.saveOrUpdate(nfeCabecalho.localEntrega);
                    }

                    if (nfeCabecalho.localRetirada != null)
                    {
                        NHibernateDAL<NFeLocalRetiradaDTO> nfeLR = new NHibernateDAL<NFeLocalRetiradaDTO>(session);
                        nfeCabecalho.localRetirada.idNFeCabecalho = nfeCabecalho.id;
                        nfeLR.saveOrUpdate(nfeCabecalho.localRetirada);
                    }

                    if (nfeCabecalho.transporte != null)
                    {
                        NHibernateDAL<NFeTransporteDTO> nfeTransporte = new NHibernateDAL<NFeTransporteDTO>(session);
                        nfeCabecalho.transporte.idNFeCabecalho = nfeCabecalho.id;
                        nfeTransporte.saveOrUpdate(nfeCabecalho.transporte);
                    }



                    session.Flush();
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


        #region Apenas Consultas

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


        #endregion 

        #region ViewTributacaoPis

        public ViewTributacaoPisDTO selectViewTributacaoPis(ViewTributacaoPisDTO viewTributacaoPis)
        {
            try
            {
                ViewTributacaoPisDTO resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewTributacaoPisDTO> DAL = new NHibernateDAL<ViewTributacaoPisDTO>(session);
                    resultado = DAL.selectObjeto(viewTributacaoPis);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ViewTributacaoCofins

        public ViewTributacaoCofinsDTO selectViewTributacaoCofins(ViewTributacaoCofinsDTO viewTributacaoCofins)
        {
            try
            {
                ViewTributacaoCofinsDTO resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewTributacaoCofinsDTO> DAL = new NHibernateDAL<ViewTributacaoCofinsDTO>(session);
                    resultado = DAL.selectObjeto(viewTributacaoCofins);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ViewTributacaoIcms
        public ViewTributacaoIcmsDTO selectViewTributacaoIcms(ViewTributacaoIcmsDTO viewTributacaoIcms)
        {
            try
            {
                ViewTributacaoIcmsDTO resultado = null;
                using (ISession session = NHibernateHelper.getSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewTributacaoIcmsDTO> DAL = new NHibernateDAL<ViewTributacaoIcmsDTO>(session);
                    resultado = DAL.selectObjeto(viewTributacaoIcms);
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
