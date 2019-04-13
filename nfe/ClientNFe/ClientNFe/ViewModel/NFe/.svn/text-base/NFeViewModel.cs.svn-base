using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Serialization;
using ClientNFe.Model;
using ClientNFe.NFeServiceReference;
using ClientNFe.ViewModel;
using SearchWindow;
using uninfe;
using UniNFeLibrary;
using UniNFeLibrary.Enums;
using CloseableTabItemDemo;

namespace ClientNFe.View.NFe
{
    public class NFeViewModel : ERPViewModelBase
    {
        public Boolean isSelectedTabLista { get; set; }
        public Boolean isSelectedTabDados { get; set; }
        public ContentPresenter contentPresenterTabDados { get; set; }
        public ObservableCollection<NFeCabecalhoDTO> listaNFe { get; set; }
        public NFeCabecalhoDTO nfeSelected { get; set; }
        public ProdutoDTO produtoSelected { get; set; }
        private EmpresaDTO empresa { get; set; }
        private Dictionary<ServicoUniNFe, Servicos> servicosUniNfe;
        private Dictionary<Thread, ParametroThread> threads;
        public TRetEnviNFe retEnviNFe { get; set; }
        public TRetConsReciNFe retConsReci { get; set; }
        public NFeDetalheDTO detalheNFe { get; set; }

        public NFeViewModel()
        {
            try
            {
                contentPresenterTabDados = new ContentPresenter();
                listaNFe = new ObservableCollection<NFeCabecalhoDTO>();

                using (NFeClient serv = new NFeClient())
                {
                    empresa = serv.selectEmpresaId(1);
                }

                Empresa.CarregaConfiguracao();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void desativarServicosNFE()
        {
            foreach (KeyValuePair<Thread, int> t in Auxiliar.threads)
            {
                try
                {
                    Thread thread = t.Key;
                    thread.Abort();
                }
                catch (Exception ex)
                {
                }
            }
        }

        public bool montarNFe()
        {
            try
            {
                string FORMATO_DATA = "yyyy-MM-dd";

                //EnviNFe
                //TEnviNFe enviNFe = new TEnviNFe();
                //enviNFe.versao = "2.00";
                //enviNFe.idLote = DateTime.Now.ToString("yyyyMMddHHmmss");
                //enviNFe.NFe = new TNFe[1];

                //NFe
                TNFe nfe = new TNFe();
                //enviNFe.NFe[0] = nfe;

                //Informacoes NFe
                TNFeInfNFe infNFe = new TNFeInfNFe();
                infNFe.Id = "NFe" + nfeSelected.chaveAcesso + nfeSelected.digitoChaveAcesso;
                infNFe.versao = "2.00";
                nfe.infNFe = infNFe;

                //Ide
                TNFeInfNFeIde infNFeIde = new TNFeInfNFeIde();
                infNFeIde.cNF = nfeSelected.codigoNumerico;
                infNFeIde.natOp = nfeSelected.naturezaOperacao;
                infNFeIde.indPag = (TNFeInfNFeIdeIndPag) int.Parse(nfeSelected.indicadorFormaPagamento);
                infNFeIde.mod = (TMod) Enum.Parse(typeof(TMod), "Item"+(nfeSelected.codigoModelo));
                infNFeIde.serie = nfeSelected.serie;
                infNFeIde.nNF = int.Parse( nfeSelected.numero).ToString();
                infNFeIde.dEmi =((DateTime) nfeSelected.dataEmissao).ToString(FORMATO_DATA);
                infNFeIde.dSaiEnt = ((DateTime)nfeSelected.dataEntradaSaida).ToString(FORMATO_DATA);
                infNFeIde.tpEmis = (TNFeInfNFeIdeTpEmis)Enum.Parse(typeof(TNFeInfNFeIdeTpEmis), "Item" + nfeSelected.tipoEmissao);
                infNFeIde.verProc = nfeSelected.versaoProcessoEmissao;
                infNFeIde.cUF = (TCodUfIBGE)Enum.Parse(typeof(TCodUfIBGE),"Item"+ empresa.CodigoIbgeUf);
                infNFeIde.cMunFG = empresa.endereco.municipioIbge.ToString();
                infNFeIde.finNFe = (TFinNFe)Enum.Parse(typeof(TFinNFe), "Item" + nfeSelected.finalidadeEmissao);
                infNFeIde.tpNF = (TNFeInfNFeIdeTpNF)Enum.Parse(typeof(TNFeInfNFeIdeTpNF), "Item" + nfeSelected.tipoEmissao);
                infNFeIde.cDV = nfeSelected.digitoChaveAcesso;
                infNFeIde.tpImp = (TNFeInfNFeIdeTpImp)Enum.Parse(typeof(TNFeInfNFeIdeTpImp), "Item" + nfeSelected.formatoImpressaoDanfe);
                infNFeIde.procEmi = TProcEmi.Item0;
                infNFeIde.tpAmb = (TAmb)Enum.Parse(typeof(TAmb), "Item" + nfeSelected.ambiente);
                nfe.infNFe.ide = infNFeIde;

                //Endereco Emitente
                TEnderEmi enderEmit = new TEnderEmi();
                enderEmit.xLgr = empresa.endereco.logradouro;
                enderEmit.nro = empresa.endereco.numero;
                if(!string.IsNullOrEmpty(empresa.endereco.complemento))
                    enderEmit.xCpl = empresa.endereco.complemento;
                enderEmit.xBairro = empresa.endereco.bairro;
                enderEmit.cMun = empresa.endereco.municipioIbge.ToString();
                enderEmit.xMun = nfeSelected.emitente.nomeMunicipio;
                enderEmit.UF = (TUfEmi)Enum.Parse(typeof(TUfEmi), empresa.endereco.uf);
                enderEmit.CEP = nfeSelected.emitente.cep;
                enderEmit.cPais = (TEnderEmiCPais)Enum.Parse(typeof(TEnderEmiCPais), "Item" + nfeSelected.emitente.codigoPais);
                enderEmit.xPais = (TEnderEmiXPais)Enum.Parse(typeof(TEnderEmiXPais), nfeSelected.emitente.nomePais);
                enderEmit.fone = nfeSelected.emitente.telefone;

                //Emitente
                TNFeInfNFeEmit emit = new TNFeInfNFeEmit();
                emit.CRT = (TNFeInfNFeEmitCRT) Enum.Parse(typeof(TNFeInfNFeEmitCRT), "Item" + empresa.Crt);
                emit.IE = empresa.InscricaoEstadual;
                emit.xNome = empresa.RazaoSocial;
                emit.xFant = empresa.NomeFantasia;
                emit.Item = empresa.Cnpj;

                emit.enderEmit = enderEmit;
                nfe.infNFe.emit = emit;

                //Endereco destinatario
                TEndereco enderDest = new TEndereco();
                enderDest.xLgr = nfeSelected.destinatario.logradouro;
                enderDest.nro = nfeSelected.destinatario.numero;
                enderDest.xCpl = nfeSelected.destinatario.complemento;
                enderDest.xBairro = nfeSelected.destinatario.bairro;
                enderDest.cMun = nfeSelected.destinatario.codigoMunicipio.ToString();
                enderDest.xMun = nfeSelected.destinatario.nomeMunicipio;
                enderDest.UF = (TUf)Enum.Parse(typeof(TUf), nfeSelected.destinatario.uf);
                enderDest.CEP = nfeSelected.destinatario.cep;
                enderDest.cPais = (Tpais)  nfeSelected.destinatario.codigoPais;
                enderDest.xPais = nfeSelected.destinatario.nomePais;
                enderDest.fone = nfeSelected.destinatario.telefone;

                //Destinatario
                TNFeInfNFeDest dest = new TNFeInfNFeDest();
                //dest.xNome = nfeSelected.destinatario.razaoSocial;
                dest.xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
                dest.IE = nfeSelected.destinatario.inscricaoEstadual;
                dest.Item = nfeSelected.destinatario.cpfCnpj;
                dest.ItemElementName = ItemChoiceType3.CPF;
                if (nfeSelected.destinatario.cpfCnpj.Length > 11)
                    dest.ItemElementName = ItemChoiceType3.CNPJ;
                nfe.infNFe.dest = dest;
                dest.enderDest = enderDest;

                //Informacoes Adicionais
                TNFeInfNFeInfAdic infAdic = new TNFeInfNFeInfAdic();
                infAdic.infAdFisco = nfeSelected.informacoesAddFisco;
                infAdic.infCpl = nfeSelected.informacoesAddContribuinte;
                nfe.infNFe.infAdic = infAdic;

                //detalhes
                List<TNFeInfNFeDet> listaNFeDet = new List<TNFeInfNFeDet>();

                foreach (NFeDetalheDTO detalhe in nfeSelected.listaDetalhe)
                {
                    TNFeInfNFeDetProd prod = new TNFeInfNFeDetProd();
                    prod.cProd = detalhe.codigoProduto;
                    prod.cEAN = detalhe.gtin;
                    prod.cEANTrib = detalhe.gtinUnidadeTributavel;
                    prod.xProd = detalhe.nomeProduto;
                    prod.NCM = detalhe.ncm;
                    if(!string.IsNullOrEmpty(detalhe.exTipi.ToString()))
                        prod.EXTIPI = detalhe.exTipi.ToString();
                    prod.CFOP = (TCfop)Enum.Parse(typeof(TCfop), "Item" + detalhe.cfop);
                    prod.uCom = detalhe.unidadeComercial;
                    prod.qCom = formataValorNFe(detalhe.quantidadeComercial);
                    prod.vUnCom = formataValorNFe(detalhe.valorUnitarioComercial);
                    prod.vProd = formataValorNFe(detalhe.valorTotal);
                    prod.uTrib = detalhe.unidadeTributavel;
                    prod.qTrib = formataQtdNFe(detalhe.quantidadeTributavel);
                    prod.vUnTrib = formataValorNFe(detalhe.valorUnitarioTributavel);
                    prod.indTot = (TNFeInfNFeDetProdIndTot)1;
                    if(detalhe.valorFrete != null && detalhe.valorFrete > 0)
                        prod.vFrete = formataValorNFe(detalhe.valorFrete);
                    if (detalhe.valorSeguro != null && detalhe.valorSeguro > 0)
                        prod.vSeg = formataValorNFe(detalhe.valorSeguro);
                    if (detalhe.valorDesconto != null && detalhe.valorDesconto > 0)
                        prod.vDesc = formataValorNFe(detalhe.valorDesconto);

                    TNFeInfNFeDetImpostoICMSICMS00 icms00 = new TNFeInfNFeDetImpostoICMSICMS00();
                    icms00.CST = (TNFeInfNFeDetImpostoICMSICMS00CST)Enum.Parse(typeof(TNFeInfNFeDetImpostoICMSICMS00CST), "Item" + detalhe.impostoIcms.CstIcms);
                    icms00.orig = (Torig) int.Parse(detalhe.impostoIcms.OrigemMercadoria);
                    icms00.modBC = (TNFeInfNFeDetImpostoICMSICMS00ModBC)int.Parse(detalhe.impostoIcms.ModalidadeBcIcms);
                    icms00.vBC = formataValorNFe(detalhe.impostoIcms.BaseCalculoIcms);
                    icms00.pICMS = formataValorNFe(detalhe.impostoIcms.AliquotaIcms);
                    icms00.vICMS = formataValorNFe(detalhe.impostoIcms.ValorIcms);

                    TNFeInfNFeDetImpostoICMS icms = new TNFeInfNFeDetImpostoICMS();
                    icms.Item = icms00;

                    TNFeInfNFeDetImposto imp = new TNFeInfNFeDetImposto();
                    imp.Items = new object[] { icms };

                    TNFeInfNFeDetImpostoPISPISOutr pisOutr = new TNFeInfNFeDetImpostoPISPISOutr();
                    pisOutr.CST = (TNFeInfNFeDetImpostoPISPISOutrCST)Enum.Parse(typeof(TNFeInfNFeDetImpostoPISPISOutrCST), "Item" + detalhe.impostoPis.CstPis);
                    pisOutr.vPIS = formataValorNFe(detalhe.impostoPis.ValorPis);
                    pisOutr.Items = new string[2];
                    pisOutr.Items[0] = formataValorNFe(detalhe.impostoPis.ValorBaseCalculoPis);
                    pisOutr.Items[1] = formataValorNFe(detalhe.impostoPis.ValorPis);
                    pisOutr.ItemsElementName = new ItemsChoiceType1[2];
                    pisOutr.ItemsElementName[0] = ItemsChoiceType1.vBC;
                    pisOutr.ItemsElementName[1] = ItemsChoiceType1.pPIS;

                    TNFeInfNFeDetImpostoPIS pis = new TNFeInfNFeDetImpostoPIS();
                    pis.Item = pisOutr;
                    imp.PIS = pis;

                    TNFeInfNFeDetImpostoCOFINSCOFINSOutr cofinsOutr = new TNFeInfNFeDetImpostoCOFINSCOFINSOutr();
                    cofinsOutr.CST = (TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST)Enum.Parse(typeof(TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST), "Item" + detalhe.impostoCofins.CstCofins);
                    cofinsOutr.vCOFINS = formataValorNFe(detalhe.impostoCofins.ValorCofins);
                    cofinsOutr.Items = new string[2];
                    cofinsOutr.Items[0] = formataValorNFe(detalhe.impostoCofins.BaseCalculoCofins);
                    cofinsOutr.Items[1] = formataValorNFe(detalhe.impostoCofins.ValorCofins);
                    cofinsOutr.ItemsElementName = new ItemsChoiceType3[2];
                    cofinsOutr.ItemsElementName[0] = ItemsChoiceType3.vBC;
                    cofinsOutr.ItemsElementName[1] = ItemsChoiceType3.pCOFINS;


                    TNFeInfNFeDetImpostoCOFINS cofins = new TNFeInfNFeDetImpostoCOFINS();
                    cofins.Item = cofinsOutr;
                    imp.COFINS = cofins;

                    TNFeInfNFeDet nfeDet = new TNFeInfNFeDet();
                    nfeDet.imposto = imp;
                    nfeDet.prod = prod;
                    nfeDet.infAdProd = detalhe.informacoesAdicionais;
                    nfeDet.nItem = detalhe.numeroItem.ToString();

                    listaNFeDet.Add(nfeDet);
                }
                nfe.infNFe.det = listaNFeDet.ToArray();

                TNFeInfNFeTotalICMSTot icmsTot = new TNFeInfNFeTotalICMSTot();
                icmsTot.vBC = formataValorNFe(nfeSelected.baseCalculoIcms);
                icmsTot.vICMS = formataValorNFe(nfeSelected.valorIcms);
                icmsTot.vBCST = formataValorNFe(nfeSelected.baseCalculoIcmsSt);
                icmsTot.vST = formataValorNFe(nfeSelected.valorIcmsSt);
                icmsTot.vProd = formataValorNFe(nfeSelected.valorTotalProdutos);
                icmsTot.vFrete = formataValorNFe(nfeSelected.valorFrete);
                icmsTot.vSeg = formataValorNFe(nfeSelected.valorSeguro);
                icmsTot.vDesc = formataValorNFe(nfeSelected.valorDesconto);
                icmsTot.vDesc = formataValorNFe(nfeSelected.valorDesconto);
                icmsTot.vII = formataValorNFe(0);
                icmsTot.vIPI = formataValorNFe(nfeSelected.valorIpi);
                icmsTot.vPIS = formataValorNFe(nfeSelected.valorPis);
                icmsTot.vCOFINS = formataValorNFe(nfeSelected.valorCofins);
                icmsTot.vOutro = formataValorNFe(nfeSelected.valorDespesasAcessorias);
                icmsTot.vNF = formataValorNFe(nfeSelected.valorTotal);

                TNFeInfNFeTotal total = new TNFeInfNFeTotal();
                total.ICMSTot = icmsTot;
                nfe.infNFe.total = total;

                TNFeInfNFeTranspTransporta transporta = new TNFeInfNFeTranspTransporta();
                TNFeInfNFeTransp transp = new TNFeInfNFeTransp();
                transp.transporta = transporta;
                transp.modFrete = TNFeInfNFeTranspModFrete.Item1;

                nfe.infNFe.transp = transp;

                MemoryStream memStream = new MemoryStream();
                XmlSerializer serializer = new XmlSerializer(typeof(TNFe));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

                ns.Add("", "http://www.portalfiscal.inf.br/nfe");
                serializer.Serialize(memStream, nfe, ns);
                memStream.Position = 0;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(memStream);
                xmlDoc.Save(""+nfeSelected.chaveAcesso + nfeSelected.digitoChaveAcesso + "-nfe.xml");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool receberMensagemProcLoteNFe()
        {
            try
            {
                bool resultado = false;
                retConsReci = null;
                string xmlNFeMsgLote = retEnviNFe.infRec.nRec + "-pro-rec.xml";
                bool arquivoExiste = false;
                int cont = 0;
                while (!arquivoExiste || cont > 120)
                {
                    arquivoExiste = File.Exists(Empresa.Configuracoes[0].PastaRetorno + "\\" + xmlNFeMsgLote);
                    if (arquivoExiste)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(TRetConsReciNFe));
                        StreamReader sReader = new StreamReader(Empresa.Configuracoes[0].PastaRetorno + "\\" + xmlNFeMsgLote);
                        retConsReci = (TRetConsReciNFe)serializer.Deserialize(sReader);
                        if(retConsReci != null)
                            resultado = true;
                        sReader.Close();
                    }
                    else
                    {
                        cont++;
                        Thread.Sleep(1000);
                    }

                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool receberMensagemNrLoteNFe(out string numLote)
        {
            try
            {
                bool resultado = false;
                string xmlNFeNumLote = nfeSelected.chaveAcesso + nfeSelected.digitoChaveAcesso + "-num-lot.xml";
                string nfeErro = nfeSelected.chaveAcesso + nfeSelected.digitoChaveAcesso + "-nfe.err";
                bool arquivoExiste = false;
                bool erroExiste = false;
                numLote = "";
                int cont = 0;
                while((!arquivoExiste && !erroExiste) || cont > 120)
                {
                    arquivoExiste = File.Exists(Empresa.Configuracoes[0].PastaRetorno + "\\" + xmlNFeNumLote);
                    erroExiste = File.Exists(Empresa.Configuracoes[0].PastaRetorno + "\\" + nfeErro);
                    if (arquivoExiste)
                    {
                        XmlTextReader reader = new XmlTextReader(Empresa.Configuracoes[0].PastaRetorno + "\\" + xmlNFeNumLote);
                        XmlDocument doc = new XmlDocument();
                        doc.Load(reader);
                        numLote = doc.SelectSingleNode("//NumeroLoteGerado").InnerText;
                        resultado = true;
                        reader.Close();
                    }else if(erroExiste)
                    {
                        numLote = File.ReadAllText(Empresa.Configuracoes[0].PastaRetorno + "\\" + nfeErro, Encoding.GetEncoding(28591));
                    }
                    else 
                    {
                        cont++;
                        Thread.Sleep(1000);
                    }

                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool receberMensagemLoteNFe(int numLote)
        {
            try
            {
                bool resultado = false;
                retEnviNFe = null;
                string xmlNFeMsgLote = numLote.ToString("000000000000000") + "-rec.xml";
                bool arquivoExiste = false;
                int cont = 0;
                while (!arquivoExiste || cont > 120)
                {
                    arquivoExiste = File.Exists(Empresa.Configuracoes[0].PastaRetorno + "\\" + xmlNFeMsgLote);
                    if (arquivoExiste)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(TRetEnviNFe));
                        StreamReader sReader = new StreamReader(Empresa.Configuracoes[0].PastaRetorno + "\\" + xmlNFeMsgLote);
                        retEnviNFe = (TRetEnviNFe)serializer.Deserialize(sReader);
                        if(retEnviNFe != null)
                            resultado = true;
                        sReader.Close();
                    }
                    else
                    {
                        cont++;
                        Thread.Sleep(1000);
                    }

                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool enviarNFe()
        {
            try
            {
                bool resultado = false;
                string xmlNFeOrig = nfeSelected.chaveAcesso + nfeSelected.digitoChaveAcesso + "-nfe.xml";
                string xmlNFeDest = Empresa.Configuracoes[0].PastaEnvio + "\\" + nfeSelected.chaveAcesso + nfeSelected.digitoChaveAcesso + "-nfe.xml";

                if (File.Exists(xmlNFeOrig))
                {
                    File.Move(xmlNFeOrig, xmlNFeDest);
                    resultado = true;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool excluirArquivos()
        {
            try
            {
                bool resultado = false;
                string diretorioErro = Empresa.Configuracoes[0].PastaErro;
                string diretorioRetorno = Empresa.Configuracoes[0].PastaRetorno;
                string[] arquivos = Directory.GetFiles(diretorioErro);
                foreach (string arquivo in arquivos)
                {
                    File.Delete(arquivo);
                }

                arquivos = Directory.GetFiles(diretorioRetorno);
                foreach (string arquivo in arquivos)
                {
                    File.Delete(arquivo);
                }
                resultado = true;

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string formataValorNFe(decimal? valor)
        {
            try
            {
                if (valor == null)
                    valor = 0;

                return ((decimal)valor).ToString("0.00", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private string formataQtdNFe(decimal? quantidade)
        {
            try
            {
                if (quantidade == null)
                    quantidade = 0;

                return ((decimal)quantidade).ToString("0.0000", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ativarServicosNFE()
        {
            try
            {
                servicosUniNfe = new Dictionary<ServicoUniNFe, Servicos>();
                threads = new Dictionary<Thread, ParametroThread>();

                ConfiguracaoApp.TipoAplicativo = TipoAplicativo.Nfe;
                ConfiguracaoApp.CarregarDados();
                ConfiguracaoApp.VersaoXMLCanc = "2.00";
                ConfiguracaoApp.VersaoXMLConsCad = "2.00";
                ConfiguracaoApp.VersaoXMLInut = "2.00";
                ConfiguracaoApp.VersaoXMLNFe = "2.00";
                ConfiguracaoApp.VersaoXMLPedRec = "2.00";
                ConfiguracaoApp.VersaoXMLPedSit = "2.00";
                ConfiguracaoApp.VersaoXMLStatusServico = "2.00";
                ConfiguracaoApp.VersaoXMLCabecMsg = "2.00";
                ConfiguracaoApp.VersaoXMLEnvDPEC = "1.01";
                ConfiguracaoApp.VersaoXMLConsDPEC = "1.01";
                ConfiguracaoApp.nsURI = "http://www.portalfiscal.inf.br/nfe";
                SchemaXML.CriarListaIDXML();

                Auxiliar.threads.Clear();
                threads.Clear();

                //Primeiro eu preparo as thread´s a serem executadas, atualizo a
                //lista de thread´s e a empresa que está sendo executada nela
                //para depois iniciá-las, ou gera erros nas pesquisas pela empresa da
                //thread. Wandrey 02/08/2010
                for (int i = 0; i < Empresa.Configuracoes.Count; i++)
                {
                    if (Empresa.Configuracoes[i].Certificado == string.Empty)
                        continue;

                    //Criar uma lista dos serviços a serem executados
                    servicosUniNfe.Clear();
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.EnviarLoteNfe);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.AssinarNFePastaEnvio);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.MontarLoteUmaNFe);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.PedidoSituacaoLoteNFe);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.PedidoConsultaSituacaoNFe);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.AssinarNFePastaEnvioEmLote);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.MontarLoteVariasNFe);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.ValidarAssinar);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.CancelarNFe);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.InutilizarNumerosNFe);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.PedidoConsultaStatusServicoNFe);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.ConsultaCadastroContribuinte);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.ConsultaInformacoesUniNFe);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.AlterarConfiguracoesUniNFe);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.GerarChaveNFe);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.EmProcessamento);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.ConverterTXTparaXML);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.EnviarDPEC);
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.ConsultarDPEC);    //danasa 21/10/2010
                    if (Empresa.Configuracoes[i].DiasLimpeza != 0)  //danasa 27-2-2011
                        servicosUniNfe.Add(new ServicoUniNFe(), Servicos.LimpezaTemporario);

                    //Preparar as thread´s a serem executadas
                    foreach (KeyValuePair<ServicoUniNFe, Servicos> item in servicosUniNfe)
                    {
                        ServicoUniNFe servico = item.Key;
                        Thread t = new Thread(new ParameterizedThreadStart(servico.BuscaXML));
                        t.Name = (item.Value.ToString().Trim() + Empresa.Configuracoes[i].CNPJ.Trim()).ToUpper();

                        //Atualiza a coleção de thread´s e a empresa que será executada enal
                        Auxiliar.threads.Add(t, i);

                        //Atualizar a coleção das thread´s a serem executadas.
                        threads.Add(t, new ParametroThread(item.Value));
                    }
                }
                //Executar as thread´s de todas as empresas
                foreach (KeyValuePair<Thread, ParametroThread> item in threads)
                {
                    Thread t = item.Key;
                    t.Start(item.Value);
                    if (Empresa.Configuracoes.Count > 1)
                        Thread.Sleep(100);  //danasa 9-2010
                }
                //Limpar para tirar o conteúdo da memória pois não vamos mais precisar
                threads.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public bool consultarStatusNFe(out object retorno)
        {
            try
            {

                bool resultado = false;
                GerarXML gerarXML = new GerarXML(0);

                string XmlNfeDadosMsg = Empresa.Configuracoes[0].PastaEnvio + "\\" + gerarXML.StatusServico(TipoEmissao.teNormal, 53, TipoAmbiente.taHomologacao);

                Auxiliar oAux = new Auxiliar();

                string ArqXMLRetorno = Empresa.Configuracoes[0].PastaRetorno + "\\" +
                          oAux.ExtrairNomeArq(XmlNfeDadosMsg, ExtXml.PedSta) +
                          "-sta.xml";

                string ArqERRRetorno = Empresa.Configuracoes[0].PastaRetorno + "\\" +
                          oAux.ExtrairNomeArq(XmlNfeDadosMsg, ExtXml.PedSta) +
                          "-sta.err";

                object vRetorno = null;
                try
                {
                    resultado = EnviaArquivoERecebeResposta(1, ArqXMLRetorno, ArqERRRetorno, out retorno);
                }
                finally
                {
                    oAux.DeletarArquivo(ArqERRRetorno);
                    oAux.DeletarArquivo(ArqXMLRetorno);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Envia um arquivo para o webservice da NFE e recebe a resposta. 
        /// </summary>
        /// <returns>Retorna uma string com a mensagem obtida do webservice de status do serviço da NFe</returns>
        /// <example>string vPastaArq = this.CriaArqXMLStatusServico();</example>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>17/06/2009</date>
        private bool EnviaArquivoERecebeResposta(int tipo, string arqXMLRetorno, string arqERRRetorno, out object retorno)
        {
            object vStatus = "Ocorreu uma falha ao tentar obter a situação do serviço junto ao SEFAZ.\r\n\r\n" +
                "O problema pode ter ocorrido por causa dos seguintes fatores:\r\n\r\n" +
                "- Problema com o certificado digital\r\n" +
                "- Necessidade de atualização da cadeia de certificados digitais\r\n" +
                "- Falha de conexão com a internet\r\n" +
                "- Falha nos servidores do SEFAZ\r\n\r\n" +
                "Afirmamos que a produtora do software não se responsabiliza por decisões tomadas e/ou execuções realizadas com base nas informações acima.\r\n\r\n";

            bool resultado = false;
            DateTime startTime;
            DateTime stopTime;
            TimeSpan elapsedTime;

            long elapsedMillieconds;
            startTime = DateTime.Now;

            while (true)
            {
                stopTime = DateTime.Now;
                elapsedTime = stopTime.Subtract(startTime);
                elapsedMillieconds = (int)elapsedTime.TotalMilliseconds;

                if (elapsedMillieconds >= 30000) //120.000 ms que corresponde á 120 segundos que corresponde a 2 minutos
                {
                    break;
                }

                if (File.Exists(arqXMLRetorno))
                {
                    if (!Auxiliar.FileInUse(arqXMLRetorno))
                    {
                        try
                        {
                            //Ler o status do serviço no XML retornado pelo WebService
                            //XmlTextReader oLerXml = new XmlTextReader(ArqXMLRetorno);

                            try
                            {
                                GerarXML oGerar = new GerarXML(0);

                                if (tipo == 1)
                                    vStatus = ProcessaStatusServico(arqXMLRetorno);
                                else
                                    vStatus = oGerar.ProcessaConsultaCadastro(arqXMLRetorno);

                                resultado = true;
                            }
                            catch (Exception ex)
                            {
                                vStatus = ex.Message;
                                break;
                                //Se não conseguir ler o arquivo vai somente retornar ao loop para tentar novamente, pois 
                                //pode ser que o arquivo esteja em uso ainda.
                            }

                            //Detetar o arquivo de retorno
                            try
                            {
                                FileInfo oArquivoDel = new FileInfo(arqXMLRetorno);
                                oArquivoDel.Delete();
                                break;
                            }
                            catch
                            {
                                //Somente deixa fazer o loop novamente e tentar deletar
                            }
                        }
                        catch (Exception ex)
                        {
                            vStatus += ex.Message;
                        }
                    }

                }
                else if (File.Exists(arqERRRetorno))
                {
                    //Retornou um arquivo com a extensão .ERR, ou seja, deu um erro,
                    //futuramente tem que retornar esta mensagem para a MessageBox do usuário.

                    //Detetar o arquivo de retorno
                    try
                    {
                        vStatus += System.IO.File.ReadAllText(arqERRRetorno, Encoding.Default);
                        System.IO.File.Delete(arqERRRetorno);
                        break;
                    }
                    catch
                    {
                        //Somente deixa fazer o loop novamente e tentar deletar
                    }
                    resultado = false;
                }
                Thread.Sleep(3000);
            }

            //Retornar o status do serviço
            retorno = vStatus;
            return resultado;
        }
        public void pesquisarProduto()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(ProdutoDTO), typeof(ServicoClientNFe));
                searchWindow.definirColunas(new string[] { "gtin", "nome", "valorVenda" });
                if (searchWindow.ShowDialog() == true)
                {
                    produtoSelected  = (ProdutoDTO)searchWindow.itemSelecionado;
                    detalheNFe = new NFeDetalheDTO();
                    notifyPropertyChanged("produtoSelected");
                    notifyPropertyChanged("detalheNFe");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void pesquisarOperacaoFiscal()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(TributOperacaoFiscalDTO), typeof(ServicoClientNFe));
                searchWindow.definirColunas(new string[] { "Id", "Descricao", "DescricaoNaNf" });
                if (searchWindow.ShowDialog() == true)
                {
                    nfeSelected.TributOperacaoFiscal = (TributOperacaoFiscalDTO)searchWindow.itemSelecionado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Função Callback que analisa a resposta do Status do Servido
        /// </summary>
        /// <param name="elem"></param>
        /// <by>Marcos Diez</by>
        /// <date>30/8/2009</date>
        /// <returns></returns>
        private static string ProcessaStatusServico(string cArquivoXML)//XmlTextReader elem)
        {
            string rst = "Erro na leitura do XML " + cArquivoXML;
            XmlTextReader elem = new XmlTextReader(cArquivoXML);
            try
            {
                while (elem.Read())
                {
                    if (elem.NodeType == XmlNodeType.Element)
                    {
                        if (elem.Name == "xMotivo")
                        {
                            elem.Read();
                            rst = elem.Value;
                            break;
                        }
                    }
                }
            }
            finally
            {
                elem.Close();
            }

            return rst;
        }

        public void excluirCupomVinculado(int index)
        {
            try
            {
                if(nfeSelected.listaCupomFiscal.Count > index )
                    nfeSelected.listaCupomFiscal.RemoveAt(index);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void excluirDuplicata(int index)
        {
            try
            {
                if (nfeSelected.listaDuplicata.Count > index)
                    nfeSelected.listaDuplicata.RemoveAt(index);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void excluirProduto(int index)
        {
            try
            {
                if (nfeSelected.listaDetalhe.Count > index)
                {
                    nfeSelected.listaDetalhe.RemoveAt(index);
                    atualizarNumeroItemDetalhe();
                    atualizarValoresNFe();
                }
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void incluirCupomVinculado(NFeCupomFiscalDTO cupomVinculado)
        {
            try
            {
                if (nfeSelected.listaCupomFiscal == null)
                    nfeSelected.listaCupomFiscal = new List<NFeCupomFiscalDTO>();

                nfeSelected.listaCupomFiscal.Add(cupomVinculado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void incluirDuplicata(NFeDuplicataDTO duplicata)
        {
            try
            {
                if (nfeSelected.listaDuplicata == null)
                    nfeSelected.listaDuplicata = new List<NFeDuplicataDTO>();

                nfeSelected.listaDuplicata.Add(duplicata);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void carregarTabLista()
        {
            try
            {
                contentPresenterTabDados.Content = null;
                atualizarListaNFe();
                isSelectedTabLista = true;
                notifyPropertyChanged("isSelectedTabLista");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void carregarTabDados()
        {
            try
            {
                carregarNFeSelected();
                contentPresenterTabDados.Content = new NFeDados();
                isSelectedTabDados = true;
                notifyPropertyChanged("isSelectedTabDados");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void carregarNFeSelected()
        {
            try
            {
                if (nfeSelected != null && nfeSelected.id != 0)
                {
                    using (NFeClient serv = new NFeClient())
                    {
                        nfeSelected = serv.selectNFeCabecalhoId((int)nfeSelected.id);
                    }
                }

                if(nfeSelected.destinatario == null)
                    nfeSelected.destinatario = new NFeDestinatarioDTO();
                if (nfeSelected.listaCupomFiscal == null)
                    nfeSelected.listaCupomFiscal = new List<NFeCupomFiscalDTO>();
                if (nfeSelected.localEntrega == null)
                    nfeSelected.localEntrega = new NFeLocalEntregaDTO();
                if (nfeSelected.localRetirada == null)
                    nfeSelected.localRetirada = new NFeLocalRetiradaDTO();
                if (nfeSelected.transporte == null)
                    nfeSelected.transporte = new NFeTransporteDTO();
                if (nfeSelected.fatura == null)
                    nfeSelected.fatura = new NFeFaturaDTO();
                if (nfeSelected.listaDuplicata == null)
                    nfeSelected.listaDuplicata = new List<NFeDuplicataDTO>();
                if (nfeSelected.listaDetalhe == null)
                    nfeSelected.listaDetalhe = new List<NFeDetalheDTO>();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaNFe()
        {
            try
            {
                using (NFeClient serv = new NFeClient())
                {
                    List<NFeCabecalhoDTO> listaNFeServ = serv.selectNFeCabecalho(new NFeCabecalhoDTO());

                    listaNFe.Clear();

                    foreach (NFeCabecalhoDTO nfe in listaNFeServ)
                    {
                        listaNFe.Add(nfe);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void salvarNFe()
        {
            try
            {
                if (nfeSelected.TributOperacaoFiscal == null)
                    throw new Exception("Selecione a Operação Fiscal.");

                using (NFeClient serv = new NFeClient())
                {
                    nfeSelected.idEmpresa = empresa.Id;
                    nfeSelected.versaoProcessoEmissao = "100";
                    nfeSelected.destinatario.inscricaoEstadual = "";

                    if (nfeSelected.emitente == null)
                    {
                        NFeEmitenteDTO emitente = new NFeEmitenteDTO();
                        emitente.cpfCnpj = empresa.Cnpj;
                        emitente.razaoSocial = empresa.RazaoSocial;
                        emitente.fantasia = empresa.NomeFantasia;
                        emitente.logradouro = empresa.endereco.logradouro;
                        emitente.numero = empresa.endereco.numero;
                        emitente.complemento = empresa.endereco.complemento;
                        emitente.bairro = empresa.endereco.bairro;
                        emitente.codigoMunicipio = empresa.endereco.municipioIbge;
                        emitente.nomeMunicipio = "Brasilia";
                        emitente.uf = empresa.endereco.uf;
                        emitente.cep = empresa.endereco.cep;
                        emitente.crt = empresa.Crt;
                        emitente.codigoPais = 1058;
                        emitente.nomePais = "Brasil";
                        emitente.telefone = empresa.endereco.fone;
                        emitente.inscricaoEstadual = empresa.InscricaoEstadual;
                        emitente.inscricaoEstadualSt = empresa.InscricaoEstadualSt;
                        emitente.inscricaoMunicipal = empresa.InscricaoMunicipal;

                        nfeSelected.emitente = emitente;
                    }                                        

                    serv.salvarNFeCabecalho(nfeSelected);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void imprimirDANFE()
        {
            try
            {
                Process unidanfe = new Process();
                unidanfe.StartInfo.FileName = @"C:\Unimake\UniNFe\unidanfe.exe";
                unidanfe.StartInfo.Arguments = " arquivo=\"" + Empresa.Configuracoes[0].PastaEnviado + "\\Autorizados\\"
                    + nfeSelected.dataEmissao.Value.Year
                    + nfeSelected.dataEmissao.Value.Month.ToString("00") + "\\"
                    + nfeSelected.chaveAcesso + nfeSelected.digitoChaveAcesso + "-nfe.xml\" ";
                unidanfe.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void atualizarNumeroItemDetalhe()
        {
            try
            {
                int aux = 0;
                foreach (NFeDetalheDTO det in nfeSelected.listaDetalhe)
                {
                    det.numeroItem = ++aux;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void atualizarValoresNFe()
        {
            try
            {
                nfeSelected.baseCalculoIcms = 0;
                nfeSelected.valorIcms = 0;
                nfeSelected.baseCalculoIcmsSt = 0;
                nfeSelected.valorIcmsSt = 0;
                nfeSelected.valorCofins = 0;
                nfeSelected.valorTotalProdutos = 0;
                nfeSelected.valorFrete = 0;
                nfeSelected.valorSeguro = 0;
                nfeSelected.valorDespesasAcessorias = 0;
                nfeSelected.valorPis = 0;
                nfeSelected.valorDesconto = 0;
                nfeSelected.valorTotal = 0;

                foreach(NFeDetalheDTO detalhe in nfeSelected.listaDetalhe)
                {
                    nfeSelected.valorTotal += detalhe.valorTotal;
                    nfeSelected.baseCalculoIcms += detalhe.impostoIcms.BaseCalculoIcms;
                    nfeSelected.valorIcms += detalhe.impostoIcms.ValorIcms;
                    nfeSelected.valorTotalProdutos += detalhe.valorTotal;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void incluirProduto(decimal quantidade)
        {

            // Cálculos serão apurados no segundo ciclo após novo levantamento de requisitos da Tributação


            try
            {
                if(produtoSelected == null)
                    throw new Exception("Selecione o produto.");

                if (nfeSelected.listaDetalhe == null)
                    nfeSelected.listaDetalhe = new List<NFeDetalheDTO>();

                detalheNFe.idProduto = produtoSelected.id;
                detalheNFe.codigoProduto = produtoSelected.gtin;
                detalheNFe.gtin = produtoSelected.gtin;
                detalheNFe.valorBrutoProduto = quantidade * produtoSelected.valorVenda;
                detalheNFe.gtinUnidadeTributavel = produtoSelected.gtin;
                detalheNFe.quantidadeTributavel = quantidade;
                detalheNFe.valorUnitarioTributavel = produtoSelected.valorVenda;
                detalheNFe.nomeProduto = produtoSelected.nome;
                detalheNFe.quantidadeComercial = quantidade;
                detalheNFe.valorUnitarioComercial = produtoSelected.valorVenda;
                detalheNFe.valorSubtotal = quantidade * produtoSelected.valorVenda;
                detalheNFe.valorTotal = quantidade * produtoSelected.valorVenda;
                detalheNFe.ncm = produtoSelected.ncm;
                detalheNFe.unidadeComercial = produtoSelected.UnidadeProduto.Sigla;
                detalheNFe.unidadeTributavel = produtoSelected.UnidadeProduto.Sigla;

                // ICMS
                ViewTributacaoIcmsDTO viewIcms = new ViewTributacaoIcmsDTO();
                using (NFeClient serv = new NFeClient())
                {
                    viewIcms.IdTributOperacaoFiscal = nfeSelected.TributOperacaoFiscal.Id;
                    viewIcms.IdTributGrupoTributario = produtoSelected.TributGrupoTributario.Id;
                    viewIcms.UfDestino = nfeSelected.destinatario.uf;
                    viewIcms = serv.selectViewTributacaoIcms(viewIcms);

                    if (viewIcms == null)
                        throw new Exception("Não existe tributação definida para o para o produto selecionado.");
                }
                detalheNFe.cfop = viewIcms.Cfop;
                detalheNFe.impostoIcms = new NfeDetalheImpostoIcmsDTO();
                detalheNFe.impostoIcms.OrigemMercadoria = viewIcms.OrigemMercadoria;
                detalheNFe.impostoIcms.CstIcms = viewIcms.CstB;
                detalheNFe.impostoIcms.Csosn = viewIcms.CsosnB;
                detalheNFe.impostoIcms.ModalidadeBcIcms = viewIcms.ModalidadeBc;
                detalheNFe.impostoIcms.TaxaReducaoBcIcms = viewIcms.PorcentoBc;
                detalheNFe.impostoIcms.AliquotaIcms = viewIcms.Aliquota;
                detalheNFe.impostoIcms.ModalidadeBcIcmsSt = viewIcms.ModalidadeBcSt;
                detalheNFe.impostoIcms.PercentualMvaIcmsSt = viewIcms.Mva;
                detalheNFe.impostoIcms.PercentualReducaoBcIcmsSt = viewIcms.PorcentoBcSt;
                detalheNFe.impostoIcms.AliquotaIcmsSt = viewIcms.AliquotaIcmsSt;
                detalheNFe.impostoIcms.BaseCalculoIcms = produtoSelected.valorVenda;
                detalheNFe.impostoIcms.ValorIcms = (produtoSelected.valorVenda * viewIcms.Aliquota) / 100;



                ViewTributacaoPisDTO viewPis = new ViewTributacaoPisDTO();
                using (NFeClient serv = new NFeClient())
                {
                    viewPis.IdTributOperacaoFiscal = nfeSelected.TributOperacaoFiscal.Id;
                    viewPis.IdTributGrupoTributario = produtoSelected.TributGrupoTributario.Id;
                    viewPis = serv.selectViewTributacaoPis(viewPis);

                    detalheNFe.impostoPis = new NfeDetalheImpostoPisDTO();

                    detalheNFe.impostoPis.CstPis = viewPis.CstPis;
                    detalheNFe.impostoPis.AliquotaPisPercentual = viewPis.AliquotaPorcento;
                    detalheNFe.impostoPis.AliquotaPisReais = viewPis.AliquotaUnidade;
                    detalheNFe.impostoPis.ValorBaseCalculoPis = produtoSelected.valorVenda;
                    detalheNFe.impostoPis.ValorPis = (produtoSelected.valorVenda * viewPis.AliquotaPorcento) / 100; ;
                }

                ViewTributacaoCofinsDTO viewCofins = new ViewTributacaoCofinsDTO();
                using (NFeClient serv = new NFeClient())
                {
                    viewCofins.IdTributOperacaoFiscal = nfeSelected.TributOperacaoFiscal.Id;
                    viewCofins.IdTributGrupoTributario = produtoSelected.TributGrupoTributario.Id;
                    viewCofins = serv.selectViewTributacaoCofins(viewCofins);

                    detalheNFe.impostoCofins = new NfeDetalheImpostoCofinsDTO();

                    detalheNFe.impostoCofins.CstCofins = viewCofins.CstCofins;
                    detalheNFe.impostoCofins.AliquotaCofinsPercentual = viewCofins.AliquotaPorcento;
                    detalheNFe.impostoCofins.AliquotaCofinsReais = viewCofins.AliquotaUnidade;
                    detalheNFe.impostoCofins.BaseCalculoCofins = produtoSelected.valorVenda;
                    detalheNFe.impostoCofins.ValorCofins = (produtoSelected.valorVenda * viewCofins.AliquotaPorcento) / 100; ;
                }

                nfeSelected.listaDetalhe.Add(detalheNFe);

                atualizarNumeroItemDetalhe();
                atualizarValoresNFe();

                produtoSelected = null;
                detalheNFe = null;

                notifyPropertyChanged("produtoSelected");
                notifyPropertyChanged("detalheNFe");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void novaPagina(UserControl janela, String cabecalho)
        {
            Boolean achou = false;

            CloseableTabItem tabItem = new CloseableTabItem();
            tabItem.Header = cabecalho;
            tabItem.Content = janela;

            foreach (TabItem tab in NFePrincipal.TabPrincipal.Items)
            {
                if (tab.Header == tabItem.Header)
                {
                    achou = true;
                    tab.Focus();
                }
            }

            if (!achou)
            {
                NFePrincipal.TabPrincipal.Items.Add(tabItem);
                tabItem.Focus();
            }
        }

    }
}
