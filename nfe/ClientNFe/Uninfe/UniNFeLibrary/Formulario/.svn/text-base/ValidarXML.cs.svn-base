using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniNFeLibrary;
using System.Threading;

namespace UniNFeLibrary.Formulario
{
    public partial class ValidarXML : Form
    {
        private int Emp;
        ArrayList empresa = new ArrayList();

        UniNFeLibrary.ValidarXMLs oValidarXML = new UniNFeLibrary.ValidarXMLs();

        public ValidarXML()
        {
            InitializeComponent();
        }      

        private void button1_Click(object sender, EventArgs e)
        {
            this.openFileDialog_arqxml.FileName = "";
            this.openFileDialog_arqxml.RestoreDirectory = true; //Para não mudar o diretório corrente da aplicação. Wandrey 06/08/2008
            this.openFileDialog_arqxml.Filter = "Arquivos XML|*.xml";
            if (this.openFileDialog_arqxml.ShowDialog() == DialogResult.OK)
            {
                this.textBox_arqxml.Text = this.openFileDialog_arqxml.FileName.ToString();

                oValidarXML.TipoArquivoXML(this.textBox_arqxml.Text);
                this.textBox_tipoarq.Text = oValidarXML.cRetornoTipoArq;
            }
            
            this.textBox_resultado.Text = "";
        }

        private void toolStripButton_validar_Click(object sender, EventArgs e)
        {
            this.textBox_resultado.Text = "";
            if (this.textBox_arqxml.Text == "")
            {
                this.textBox_resultado.Text="Arquivo não encontrado.";
                return;
            }

            //Copiar o arquivo XML para temporários para assinar e depois vou validar o que está nos temporários
            Auxiliar oAux = new Auxiliar();
            FileInfo oArquivo = new FileInfo(this.textBox_arqxml.Text);
            string cArquivo = System.IO.Path.GetTempPath() + oAux.ExtrairNomeArq(this.textBox_arqxml.Text, ".xml");

            FileInfo oArqDestino = new FileInfo(cArquivo);

            if (File.Exists(cArquivo)) //Deletar o arquivo antes de copiar se existir na pasta de temporários
            {
                oArqDestino.Delete();
            }

            oArquivo.CopyTo(cArquivo);

            //Detectar o tipo do arquivo
            oValidarXML.TipoArquivoXML(cArquivo);

            //Assinar o arquivo XML copiado para a pasta TEMP
            bool lAssinar = false;
            string cTagAssinar = "";
            if (oValidarXML.TagAssinar != string.Empty)
            {
                lAssinar = true;
                cTagAssinar = oValidarXML.TagAssinar;
            }

            bool lValidar = false;
            if (lAssinar == true)
            {
                AssinaturaDigital oAD = new AssinaturaDigital();
                try
                {
                    oAD.Assinar(cArquivo, cTagAssinar, Empresa.Configuracoes[Emp].X509Certificado);
                    lValidar = true;
                }
                catch (Exception ex)
                {
                    lValidar = false;
                    this.textBox_tipoarq.Text = oValidarXML.cRetornoTipoArq;
                    this.textBox_resultado.Text = "Ocorreu um erro ao tentar assinar o XML: \r\n\r\n" + ex.Message;
                }
            }
            else
            {
                lValidar = true;
            }

            if (lValidar == true)
            {
                //Validar o arquivo
                if (oValidarXML.nRetornoTipoArq >= 1 && oValidarXML.nRetornoTipoArq <= SchemaXML.MaxID)
                {
                    oValidarXML.Validar(cArquivo, oValidarXML.cArquivoSchema);
                    if (oValidarXML.Retorno == 0)
                    {
                        this.textBox_resultado.Text = "Arquivo validado com sucesso;";
                    }
                    else
                    {
                        this.textBox_resultado.Text = "XML INCONSISTENTE!\r\n\r\n" + oValidarXML.RetornoString;
                    }
                }
                else
                {
                    this.textBox_tipoarq.Text = oValidarXML.cRetornoTipoArq;
                    this.textBox_resultado.Text = "XML INCONSISTENTE!\r\n\r\n" + oValidarXML.cRetornoTipoArq;
                }
            }

            oArqDestino.Delete();
        }

        private void textBox_arqxml_TextChanged(object sender, EventArgs e)
        {
            oValidarXML.TipoArquivoXML(this.textBox_arqxml.Text);
            this.textBox_tipoarq.Text = oValidarXML.cRetornoTipoArq;
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

        private void ValidarXML_Load(object sender, EventArgs e)
        {
            PopulateCbEmpresa();
        }
    }
}