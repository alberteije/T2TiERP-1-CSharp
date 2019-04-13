using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.IO;
using System.Xml;
using UniNFeLibrary;

namespace UniNFeLibrary.Formulario
{
    public partial class FormConsultaCadastro : Form
    {
        ArrayList arrUF = new ArrayList();
        ArrayList empresa = new ArrayList();
        ArrayList arrAmb = new ArrayList();
        int Emp;

        public FormConsultaCadastro()
        {
            InitializeComponent();
        }

        private void FormConsultaCadastro_Load(object sender, EventArgs e)
        {
            PreencheEstados();
            PopulateCbEmpresa();
            XMLIniFile iniFile = new XMLIniFile(InfoApp.NomeArqXMLParams());
            iniFile.LoadForm(this, (this.MdiParent == null ? "\\Normal" : "\\MDI"));
        }

        private void FormConsultaCadastro_FormClosed(object sender, FormClosedEventArgs e)
        {
            XMLIniFile iniFile = new XMLIniFile(InfoApp.NomeArqXMLParams());
            iniFile.SaveForm(this, (this.MdiParent == null ? "\\Normal" : "\\MDI"));
            iniFile.Save();
        }

        private void PreencheEstados()
        {
            try
            {
                arrUF = Auxiliar.CarregaUF();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            comboUf.DataSource = arrUF;
            comboUf.DisplayMember = "nome";
            comboUf.ValueMember = "valor";

            this.textConteudo.Text =
               this.toolStripStatusLabel1.Text = "";

            this.buttonPesquisa.Enabled = false;

            this.cbEmissao.Items.Clear();
            this.cbEmissao.Items.Add(UniNFeConsts.tpEmissao[TipoEmissao.teNormal]);
            this.cbEmissao.Items.Add(UniNFeConsts.tpEmissao[TipoEmissao.teContingencia]);
            this.cbEmissao.Items.Add(UniNFeConsts.tpEmissao[TipoEmissao.teSCAN]);
            this.cbEmissao.Items.Add(UniNFeConsts.tpEmissao[TipoEmissao.teDPEC]);
            this.cbEmissao.Items.Add(UniNFeConsts.tpEmissao[TipoEmissao.teFSDA]);

            #region Montar Array DropList do Ambiente
            arrAmb.Add(new ComboElem("Produção", TipoAmbiente.taProducao));
            arrAmb.Add(new ComboElem("Homologação", TipoAmbiente.taHomologacao));

            cbAmbiente.DataSource = arrAmb;
            cbAmbiente.DisplayMember = "valor";
            cbAmbiente.ValueMember = "codigo";
            #endregion

        }

        #region PopulateCbEmpresa()
        /// <summary>
        /// Popular a ComboBox das empresas
        /// </summary>
        /// <remarks>
        /// Observações: Tem que popular separadamente do Método Populate() para evitar ficar recarregando na hora que selecionamos outra empresa
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 30/07/2010
        /// </remarks>
        private void PopulateCbEmpresa()
        {
            try
            {
                empresa = Auxiliar.CarregaEmpresa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (empresa.Count > 0)
            {
                cbEmpresa.DataSource = empresa;
                cbEmpresa.DisplayMember = "Nome";
                cbEmpresa.ValueMember = "Valor";
            }
        }
        #endregion

        private const string _wait = "Consultando o servidor. Aguarde....";

        private void buttonStatusServidor_Click(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = _wait;
            this.textResultado.Text = "";
            this.Refresh();

            this.Cursor = Cursors.WaitCursor;
            try
            {
                GerarXML oGerar = new GerarXML(Emp);
                Auxiliar oAux = new Auxiliar();

                int cUF = ((ComboElem)(new System.Collections.ArrayList(arrUF))[comboUf.SelectedIndex]).Codigo;
                int amb = ((ComboElem)(new System.Collections.ArrayList(arrAmb))[cbAmbiente.SelectedIndex]).Codigo;

                string XmlNfeDadosMsg = Empresa.Configuracoes[Emp].PastaEnvio + "\\" + oGerar.StatusServico(this.cbEmissao.SelectedIndex + 1, cUF, amb);

                //Demonstrar o status do serviço
                this.textResultado.Text = VerStatusServico(XmlNfeDadosMsg);
            }
            catch (Exception ex)
            {
                this.textResultado.Text = ex.Message;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.toolStripStatusLabel1.Text = "";
            }
        }

        private void buttonPesquisaCNPJ_Click(object sender, EventArgs e)
        {
            this.textConteudo.Focus();
            this.toolStripStatusLabel1.Text = _wait;
            this.textResultado.Text = "";
            this.Refresh();

            this.Cursor = Cursors.WaitCursor;

            UniNFeLibrary.RetConsCad vConsCad = null;
            try
            {
                object vvConsCad = null;

                if (rbCNPJ.Checked)
                    vvConsCad = ConsultaCadastro((string)this.comboUf.SelectedValue, this.textConteudo.Text, string.Empty, string.Empty);
                else
                    if (rbCPF.Checked)
                        vvConsCad = ConsultaCadastro((string)this.comboUf.SelectedValue, string.Empty, string.Empty, this.textConteudo.Text);
                    else
                        vvConsCad = ConsultaCadastro((string)this.comboUf.SelectedValue, string.Empty, this.textConteudo.Text, string.Empty);

                if (vvConsCad is UniNFeLibrary.RetConsCad)
                {
                    vConsCad = (vvConsCad as UniNFeLibrary.RetConsCad);
                    if (vConsCad == null)
                        this.textResultado.Text = "Não pode obter a resposta do Sefaz";
                }
                else
                {
                    throw new Exception((string)vvConsCad);
                }
            }
            catch (Exception ex)
            {
                this.textResultado.Text = ex.Message;
                vConsCad = null;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.toolStripStatusLabel1.Text = "";
                if (vConsCad != null)
                {
                    if (vConsCad.infCad.Count > 0)
                    {
                        using (FormConsultaCadastroResult fResult = new FormConsultaCadastroResult(vConsCad))
                        {
                            fResult.ShowDialog();
                        }
                    }
                    else
                        this.textResultado.Text = vConsCad.xMotivo;
                }
            }
        }

        private void rbCNPJ_CheckedChanged(object sender, EventArgs e)
        {
            this.textConteudo.Mask = "00,000,000/0000-00";
            this.textConteudo.SelectionStart = 0;
        }

        private void rbCPF_CheckedChanged(object sender, EventArgs e)
        {
            this.textConteudo.Mask = "000,000,000-00";
            this.textConteudo.SelectionStart = 0;
        }

        private void rbIE_CheckedChanged(object sender, EventArgs e)
        {
            this.textConteudo.Mask = "";
            this.textConteudo.SelectionStart = 0;
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.buttonPesquisa.Enabled = this.textConteudo.Text.Replace(",", "").Replace(".", "").Replace("-", "").Replace("/", "").Trim() != "";
        }

        private void cbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDetalheForm();
        }

        /// <summary>
        /// Popular detalhes do form de acordo com a empresa selecionada
        /// </summary>
        private void PopulateDetalheForm()
        {
            string cnpj = ((ComboElem)(new System.Collections.ArrayList(empresa))[cbEmpresa.SelectedIndex]).Valor;
            Emp = Empresa.FindConfEmpresaIndex(cnpj);        
            
            //Posicionar o elemento da combo UF
            foreach (ComboElem elem in arrUF)
            {
                if (elem.Codigo == Empresa.Configuracoes[Emp].UFCod)
                {
                    comboUf.SelectedValue = elem.Valor;
                    break;
                }
            }
            //Posicionar o elemento da combo Ambiente
            cbAmbiente.SelectedValue = Empresa.Configuracoes[Emp].tpAmb;

            //Posicionar o elemento da combo tipo de emissão
            this.cbEmissao.SelectedIndex = Empresa.Configuracoes[Emp].tpEmis - 1;
        }

        #region VerStatusServico() e ConsultaCadastro()

        /// <summary>
        /// Verifica e retorna o Status do Servido da NFE. Para isso este método gera o arquivo XML necessário
        /// para obter o status do serviõ e faz a leitura do XML de retorno, disponibilizando uma string com a mensagem
        /// obtida.
        /// </summary>
        /// <returns>Retorna uma string com a mensagem obtida do webservice de status do serviço da NFe</returns>
        /// <example>string vPastaArq = this.CriaArqXMLStatusServico();</example>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>17/06/2008</date>
        public string VerStatusServico(string XmlNfeDadosMsg)
        {
            Auxiliar oAux = new Auxiliar();

            string ArqXMLRetorno = Empresa.Configuracoes[Emp].PastaRetorno + "\\" +
                      oAux.ExtrairNomeArq(XmlNfeDadosMsg, ExtXml.PedSta) +
                      "-sta.xml";

            string ArqERRRetorno = Empresa.Configuracoes[Emp].PastaRetorno + "\\" +
                      oAux.ExtrairNomeArq(XmlNfeDadosMsg, ExtXml.PedSta) +
                      "-sta.err";

            string result = string.Empty;

            try
            {
                result = (string)EnviaArquivoERecebeResposta(1,ArqXMLRetorno, ArqERRRetorno);
            }
            finally
            {
                oAux.DeletarArquivo(ArqERRRetorno);
                oAux.DeletarArquivo(ArqXMLRetorno);
            }

            return result;
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

        /// <summary>
        /// VerConsultaCadastroClass
        /// </summary>
        /// <param name="XmlNfeDadosMsg"></param>
        /// <returns></returns>
        public object VerConsultaCadastro(string XmlNfeDadosMsg)
        {
            Auxiliar oAux = new Auxiliar();
            GerarXML oGerar = new GerarXML(Emp);

            string ArqXMLRetorno = Empresa.Configuracoes[Emp].PastaRetorno + "\\" +
                       oAux.ExtrairNomeArq(XmlNfeDadosMsg, ExtXml.ConsCad) +
                       "-ret-cons-cad.xml";

            string ArqERRRetorno = Empresa.Configuracoes[Emp].PastaRetorno + "\\" +
                      oAux.ExtrairNomeArq(XmlNfeDadosMsg, ExtXml.ConsCad) +
                      "-ret-cons-cad.err";

            object vRetorno = null;
            try
            {
                vRetorno = EnviaArquivoERecebeResposta(2,ArqXMLRetorno, ArqERRRetorno);
                //vRetorno = ProcessaConsultaCadastroClass(@"c:\usr\nfe\uninfe\modelos\retorno-cons-cad.txt");
            }
            finally
            {
                oAux.DeletarArquivo(ArqERRRetorno);
                oAux.DeletarArquivo(ArqXMLRetorno);
            }
            return vRetorno;
        }

        #region ConsultaCadastro()
        /// <summary>
        /// Verifica um cadastro no site da receita.
        /// Voce deve preencher o estado e mais um dos tres itens: CPNJ, IE ou CPF.
        /// </summary>
        /// <param name="uf">Sigla do UF do cadastro a ser consultado. Tem que ter dois dígitos. SU para suframa.</param>
        /// <param name="cnpj"></param>
        /// <param name="ie"></param>
        /// <param name="cpf"></param>
        /// <returns>String com o resultado da consuta do cadastro</returns>
        /// <by>Marcos Diez</by>
        /// <date>29/08/2009</date>
        public object ConsultaCadastro(string uf, string cnpj, string ie, string cpf)
        {
            GerarXML oGerar = new GerarXML(Emp);

            //Criar XML para obter o cadastro do contribuinte
            string XmlNfeConsultaCadastro = oGerar.ConsultaCadastro(string.Empty, uf, cnpj, ie, cpf);

            return VerConsultaCadastro(XmlNfeConsultaCadastro);
        }
        #endregion

        /// <summary>
        /// Envia um arquivo para o webservice da NFE e recebe a resposta. 
        /// </summary>
        /// <returns>Retorna uma string com a mensagem obtida do webservice de status do serviço da NFe</returns>
        /// <example>string vPastaArq = this.CriaArqXMLStatusServico();</example>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>17/06/2009</date>
        private object EnviaArquivoERecebeResposta(int tipo, string arqXMLRetorno, string arqERRRetorno)
        {
            object vStatus = "Ocorreu uma falha ao tentar obter a situação do serviço junto ao SEFAZ.\r\n\r\n" +
                "O problema pode ter ocorrido por causa dos seguintes fatores:\r\n\r\n" +
                "- Problema com o certificado digital\r\n" +
                "- Necessidade de atualização da cadeia de certificados digitais\r\n" +
                "- Falha de conexão com a internet\r\n" +
                "- Falha nos servidores do SEFAZ\r\n\r\n" +
                "Afirmamos que a produtora do software não se responsabiliza por decisões tomadas e/ou execuções realizadas com base nas informações acima.\r\n\r\n";

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

                if (elapsedMillieconds >= 120000) //120.000 ms que corresponde á 120 segundos que corresponde a 2 minutos
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
                                GerarXML oGerar = new GerarXML(Emp);

                                if (tipo == 1)
                                    vStatus = ProcessaStatusServico(arqXMLRetorno);
                                else
                                    vStatus = oGerar.ProcessaConsultaCadastro(arqXMLRetorno);
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
                }
                Thread.Sleep(3000);
            }

            //Retornar o status do serviço
            return vStatus;
        }
        #endregion
    }
}