using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace UniNFeLibrary.Formulario
{
    public partial class FormEmpresa : Form
    {
        ///
        /// danasa 9-2010
        private EventHandler OnMyClose;
        private bool Salvos;
        private bool Editando = false;

        #region Métodos
        public FormEmpresa(EventHandler _OnClose)
        {
            InitializeComponent();

            this.PopulateGridEmpresa();
            ///
            /// danasa 9-2010
            this.OnMyClose = _OnClose;
            this.Salvos = false;

            //Nenhuma célula está sendo editada no momento - Wandrey 11/02/2011
            this.Editando = false;
        }
        #endregion

        #region Eventos
        private void tsbtnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void gridEmpresa_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    break;

                case 1:
                    //Validar o CNPJ
                    if (e.FormattedValue.ToString().Trim() != string.Empty)
                    {
                        if (!UniNFeLibrary.CNPJ.Validate(Auxiliar.OnlyNumbers(e.FormattedValue.ToString(), ".,-/").ToString()))
                        {
                            MessageBox.Show("CNPJ inválido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            e.Cancel = true;
                        }
                    }

                    break;

                default:
                    break;
            }
        }

        private void FormEmpresa_FormClosed(object sender, FormClosedEventArgs e)
        {
            ///
            /// danasa 20-9-2010
            /// 
            if (OnMyClose != null)
            {
                if (this.Salvos)
                {
                    OnMyClose(sender, null);
                }
            }
        }

        private void tsbtnSalvar_Click(object sender, EventArgs e)
        {
            if (Editando)
            {
                MessageBox.Show("Existe um registro de empresa sendo editado no momento. Para salvar é necessário a finalização da edição.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            #region Verificar se existe CNPJ´s duplicados
            bool duplicou = false;
            int ContaEmpresa = 0;

            List<string> cnpjs = new List<string>();
            for (int i = 0; i < gridEmpresa.RowCount; i++)
            {
                if (gridEmpresa.Rows[i].Cells["CNPJ"].Value != null &&
                    gridEmpresa.Rows[i].Cells["Nome"].Value != null)
                {

                    if (gridEmpresa.Rows[i].Cells["CNPJ"].Value.ToString() != string.Empty &&
                        gridEmpresa.Rows[i].Cells["Nome"].Value.ToString() != string.Empty)
                    {
                        ContaEmpresa++;
                    }


                    if (!cnpjs.Contains(gridEmpresa.Rows[i].Cells["CNPJ"].Value.ToString()))
                    {
                        cnpjs.Add(gridEmpresa.Rows[i].Cells["CNPJ"].Value.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Não pode haver repetições de um mesmo CNPJ! Verifique.\r\nCNPJ repetido: " + gridEmpresa.Rows[i].Cells["CNPJ"].Value.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        duplicou = true;
                        break;
                    }
                }
            }

            //Se duplicou algum registro não vou deixar gravar
            if (duplicou)
            {
                return;
            }

            //Se não tiver nenhuma empresa cadastrada, não vai permitir salvar
            if (ContaEmpresa <= 0)
            {
                if (File.Exists(InfoApp.NomeArqEmpresa))
                {
                    try
                    {
                        File.Delete(InfoApp.NomeArqEmpresa);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao tentar gravar as empresas.\r\n\r\nErro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                return;
            }
            #endregion

            #region Gravar XML com as empresas cadastradas
            XmlWriterSettings oSettings = new XmlWriterSettings();
            UTF8Encoding c = new UTF8Encoding(false);

            //Para começar, vamos criar um XmlWriterSettings para configurar nosso XML
            oSettings.Encoding = c;
            oSettings.Indent = true;
            oSettings.IndentChars = "";
            oSettings.NewLineOnAttributes = false;
            oSettings.OmitXmlDeclaration = false;

            try
            {

                //Agora vamos criar um XML Writer
                XmlWriter oXmlGravar = XmlWriter.Create(InfoApp.NomeArqEmpresa, oSettings);

                //Agora vamos gravar os dados
                oXmlGravar.WriteStartDocument();
                oXmlGravar.WriteStartElement("Empresa");

                for (int i = 0; i < gridEmpresa.RowCount; i++)
                {
                    if (gridEmpresa.Rows[i].Cells["CNPJ"].Value != null &&
                        gridEmpresa.Rows[i].Cells["Nome"].Value != null)
                    {
                        if (gridEmpresa.Rows[i].Cells["CNPJ"].Value.ToString() != string.Empty &&
                            gridEmpresa.Rows[i].Cells["Nome"].Value.ToString() != string.Empty)
                        {
                            try
                            {
                                //Abrir a tag <Registro>
                                oXmlGravar.WriteStartElement("Registro");

                                //Criar o atributo CNPJ dentro da tag Registro
                                oXmlGravar.WriteStartAttribute("CNPJ");

                                //Setar o conteúdo do atributo CNPJ
                                oXmlGravar.WriteString(gridEmpresa.Rows[i].Cells["CNPJ"].Value.ToString());

                                //Encerrar o atributo CNPJ
                                oXmlGravar.WriteEndAttribute(); // Encerrar o atributo CNPJ

                                //Criar a tag <Nome> com seu conteúdo </Nome>
                                oXmlGravar.WriteElementString("Nome", gridEmpresa.Rows[i].Cells["Nome"].Value.ToString());

                                //Encerrar a tag </Registro>
                                oXmlGravar.WriteEndElement();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ocorreu um erro ao tentar gravar as empresas cadastradas.\r\n\r\nErro: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }

                oXmlGravar.WriteEndElement(); //Encerrar o elemento Empresa
                oXmlGravar.WriteEndDocument();
                oXmlGravar.Flush();
                oXmlGravar.Close();

                MessageBox.Show("Cadastros foram gravados com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar gravar as empresas cadastradas.\r\n\r\nErro: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            this.Salvos = true;
            this.Close();
        }
        #endregion

        /// <summary>
        /// Popular a grid das empresas com o conteúdo gravado no XML
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 21/07/2010
        /// </remarks>
        private void PopulateGridEmpresa()
        {
            try
            {
                if (System.IO.File.Exists(InfoApp.NomeArqEmpresa))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(InfoApp.NomeArqEmpresa);
                    gridEmpresa.DataSource = ds.Tables[0];
                }
                else
                {
                    gridEmpresa.Columns.Add("Nome", "Nome da Empresa");
                    gridEmpresa.Columns.Add("CNPJ", "CNPJ");
                }

                this.FormatarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar carregar os registros das empresas.\r\n\r\nErro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Formatar a grid
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 21/07/2010
        /// </remarks>
        private void FormatarGrid()
        {
            //Formatação da coluna Nome da Empresa
            DataGridViewTextBoxColumn cNome = (DataGridViewTextBoxColumn)gridEmpresa.Columns["Nome"];
            cNome.HeaderText = "Nome da Empresa";
            cNome.MaxInputLength = 70;
            cNome.Width = 350;

            //Formatação da coluna CNPJ
            DataGridViewTextBoxColumn cCNPJ = (DataGridViewTextBoxColumn)gridEmpresa.Columns["CNPJ"];
            cCNPJ.MaxInputLength = 14;
            cCNPJ.Width = 170;
        }

        private void gridEmpresa_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //Célula está sendo editada no momento - Wandrey 11/02/2011
            this.Editando = true;
        }

        private void gridEmpresa_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Nenhuma célula está sendo editada no momento - Wandrey 11/02/2011
            this.Editando = false;
        }
    }
}
