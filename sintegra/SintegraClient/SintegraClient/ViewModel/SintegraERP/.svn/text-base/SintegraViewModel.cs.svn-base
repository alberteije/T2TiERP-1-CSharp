using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintegra;
using SintegraClient.SintegraReference;
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;
using CloseableTabItemDemo;
using SintegraClient.View.Sintegra;

namespace SintegraClient.ViewModel.SintegraERP
{
    public class SintegraViewModel : ERPViewModelBase
    {
        public void gerarArquivoSintegra(DateTime dataInicio, DateTime dataFim, int finalidadeArquivo, int naturezaInformacao, int codigoConvenio)
        {
            try
            {
                using (ServicoSintegraClient serv = new ServicoSintegraClient())
                {
                    Empresa = serv.selectEmpresaId(Empresa.Id);
                }

                Sintegra.Sintegra sintegra = new Sintegra.Sintegra(dataInicio, dataFim, 1);
                
                Reg10 registro10 = new Reg10();
                registro10.cnpj = Empresa.Cnpj;
                registro10.inscricaoestadual = Empresa.InscricaoEstadual;
                registro10.nomecontribuinte = Empresa.RazaoSocial;
                registro10.municipio = Empresa.endereco.cidade;
                registro10.uf = Empresa.endereco.uf;
                registro10.codigoFinalidadeArqMagnetico = finalidadeArquivo;
                registro10.codigoIdentificacaoNatOp = naturezaInformacao;
                registro10.codigoIdentificacaoConvenio = codigoConvenio;
                registro10.dataInicial = dataInicio;
                registro10.dataFinal = dataFim;
                registro10.tipo = 10;

                sintegra.reg10 = registro10;

                Reg11 registro11 = new Reg11();
                registro11.bairro = Empresa.endereco.bairro;
                registro11.cep = Empresa.endereco.cep;
                registro11.complemento = Empresa.endereco.complemento;
                registro11.logradouro = Empresa.endereco.logradouro;
                registro11.numero = int.Parse(Empresa.endereco.numero); 
                registro11.nomeContato = Empresa.Contato;
                registro11.telefone = Empresa.endereco.fone; 
                registro11.tipo = 11;

                sintegra.reg11 = registro11;

                List<Reg50> listaRegistro50 = new List<Reg50>();
                List<Reg54> listaRegistro54 = new List<Reg54>();
                List<Reg75> listaRegistro75 = new List<Reg75>();

                using (ServicoSintegraClient serv = new ServicoSintegraClient())
                {
                    List<NFeCabecalhoDTO> listaNFe = serv.selectNFeCabecalho(dataInicio, dataFim, new NFeCabecalhoDTO());

                    foreach (NFeCabecalhoDTO nfeCab in listaNFe)
                    {
                        Reg50 registro50 = new Reg50();
                        registro50.cnpj = Empresa.Cnpj;
                        registro50.inscricaoestadual = Empresa.InscricaoEstadual;
                        registro50.dataEmissaoRecebimento = (DateTime)nfeCab.dataEmissao;
                        registro50.uf = Empresa.endereco.uf;
                        registro50.modelo = nfeCab.codigoModelo;
                        registro50.numero = int.Parse(nfeCab.numero);
                        registro50.serie = nfeCab.serie;
                        registro50.cfop = "5929";
                        registro50.emitente = "P";
                        registro50.valorTotal = (decimal)nfeCab.valorTotal;
                        registro50.baseCalculoICMS = (decimal) nfeCab.baseCalculoIcms;
                        registro50.valorICMS = (decimal)nfeCab.valorIcms;
                        registro50.isentaOuNaoTributada = 1;
                        registro50.outras = (decimal)nfeCab.valorDespesasAcessorias;
                        registro50.situacaoCancelamento = "N";
                        listaRegistro50.Add(registro50);

                        // REGISTRO TIPO 51 - TOTAL DE NOTA FISCAL QUANTO AO IPI
                        // REGISTRO TIPO 53 - SUBSTITUIÇÃO TRIBUTÁRIA
                        // Implementados a critério do Participante do T2Ti ERP 

                        // REGISTRO TIPO 54 - PRODUTO
                        ViewSpedNfeDetalheDTO nfeDetalhe = new ViewSpedNfeDetalheDTO { IdNfeCabecalho = nfeCab.id };
                        List<ViewSpedNfeDetalheDTO> ListaNFeDetalhe = serv.selectViewSpedNfeDetalhe(nfeDetalhe);

                        foreach (ViewSpedNfeDetalheDTO detalhe in ListaNFeDetalhe)
                        {
                            Reg54 registro54 = new Reg54();
                            registro54.cnpj = Empresa.Cnpj;
                            registro54.modelo = nfeCab.codigoModelo;
                            registro54.serie = nfeCab.serie;
                            registro54.numero = int.Parse(nfeCab.numero);
                            registro54.cfop = detalhe.Cfop.ToString();
                            registro54.cst = detalhe.CstIcms;
                            registro54.numeroItem = detalhe.NumeroItem.ToString();
                            registro54.codigoProduto = detalhe.Gtin;
                            registro54.qtd = detalhe.QuantidadeComercial;
                            registro54.valorProduto = detalhe.ValorTotal;
                            registro54.valorDescontoDespAcessoria = detalhe.ValorDesconto;
                            registro54.baseCalcICMS = detalhe.BaseCalculoIcms;
                            registro54.baseCalcIcmsST = detalhe.ValorBaseCalculoIcmsSt;
                            registro54.valorIPI = detalhe.ValorIpi;
                            registro54.aliqICMS = detalhe.AliquotaIcms;
                            listaRegistro54.Add(registro54);

                            
                            // REGISTRO TIPO 75 - CÓDIGO DE PRODUTO OU SERVIÇO
                            Reg75 registro75 = new Reg75();
                            registro75.dataInicial = dataInicio;
                            registro75.dataInicial = dataFim;
                            registro75.codigoProduto = detalhe.Gtin;
                            registro75.descricao = detalhe.NomeProduto;
                            registro75.unidadeMedidaComercializacao = detalhe.UnidadeComercial;
                            registro75.aliqIPI = detalhe.AliquotaIpi;
                            registro75.aliqICMS = detalhe.AliquotaIcms;
                            listaRegistro75.Add(registro75);

                        }
                        sintegra.regs54 = listaRegistro54;
                        sintegra.regs75 = listaRegistro75;

                    }
                }
                sintegra.regs50 = listaRegistro50;

                // REGISTRO TIPO 55 - GUIA NACIONAL DE RECOLHIMENTO DE TRIBUTOS ESTADUAIS
                // REGISTRO TIPO 56 - OPERAÇÕES COM VEÍCULOS AUTOMOTORES NOVOS
                // REGISTRO TIPO 57 - NÚMERO DE LOTE DE FABRICAÇÃO DE PRODUTO
                // Registro Tipo 60 - Mestre (60M): Identificador do equipamento
                // Registro Tipo 60 - Analítico (60A): Identificador de cada Situação Tributária no final do dia de cada equipamento emissor de cupom fiscal
                // Registro Tipo 60 - Resumo Diário (60D): Registro de mercadoria/produto ou serviço constante em documento fiscal emitido por Terminal Ponto de Venda (PDV) ou equipamento Emissor de Cupom Fiscal (ECF)
                // Registro Tipo 60 - Resumo Mensal (60R): Registro de mercadoria/produto ou serviço processado em equipamento Emissor de Cupom Fiscal
                // Registro Tipo 60 - Item (60I): Item do documento fiscal emitido por Terminal Ponto de Venda (PDV) ou equipamento Emissor de Cupom Fiscal (ECF)
                // Registro Tipo 61 - Resumo Mensal por Item (61R): Registro de mercadoria/produto ou serviço comercializados através de Nota Fiscal de Produtor ou Nota Fiscal de Venda a Consumidor não emitida por ECF
                // Implementados a critério do Participante do T2Ti ERP 

                Reg90 registro90 = new Reg90();
                registro90.tipo = 90;
                registro90.cnpj = Empresa.Cnpj;
                registro90.inscricaoEstadual = Empresa.InscricaoEstadual;
                registro90.numeroRegistrosTipo90 = "1";
                registro90.tipoTotalizado = 50;
                registro90.totalRegistos = listaRegistro50.Count + listaRegistro54.Count + listaRegistro75.Count;
                registro90.totalGeralRegistros = listaRegistro50.Count + listaRegistro54.Count + listaRegistro75.Count + 3;

                sintegra.regs90 = new List<Reg90> { registro90 };

                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Arquivos SINTEGRA (*.txt) | *.txt";
                dialog.Title = "Selecione o arquivo";
                dialog.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                if (dialog.ShowDialog() == true)
                {
                    sintegra.gerarArquivoSintegra(dialog.FileName);
                }                
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

            foreach (TabItem tab in SintegraPrincipal.TabPrincipal.Items)
            {
                if (tab.Header == tabItem.Header)
                {
                    achou = true;
                    tab.Focus();
                }
            }

            if (!achou)
            {
                SintegraPrincipal.TabPrincipal.Items.Add(tabItem);
                tabItem.Focus();
            }
        }

    }
}
