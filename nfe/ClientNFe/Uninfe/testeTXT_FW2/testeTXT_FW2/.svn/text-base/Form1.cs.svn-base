using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

using UniNFeLibrary;

namespace TXTtoXML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.txtFileName.Text = "Modelo-TXT.txt";
            this.txtFolderOutput.Text = Directory.GetCurrentDirectory();
            this.txtResultado.Text = "";

            if (File.Exists("uninfeconfig.xml"))
            {
                XmlTextReader oLerXml = new XmlTextReader("uninfeconfig.xml");

                while (oLerXml.Read())
                {
                    if (oLerXml.NodeType == XmlNodeType.Element)
                    {
                        if (oLerXml.Name == "nfe_configuracoes")
                        {
                            while (oLerXml.Read())
                            {
                                if (oLerXml.NodeType == XmlNodeType.Element)
                                {
                                    if (oLerXml.Name == "PastaValidar") 
                                    { 
                                        oLerXml.Read();
                                        this.txtFolderOutput.Text =
                                        ConfiguracaoApp.PastaValidar = oLerXml.Value;
                                    }
                                    else if (oLerXml.Name == "PastaXmlRetorno") { oLerXml.Read(); ConfiguracaoApp.vPastaXMLRetorno = oLerXml.Value; }
                                    else if (oLerXml.Name == "PastaXmlErro") { oLerXml.Read(); ConfiguracaoApp.vPastaXMLErro = oLerXml.Value; }
                                }
                            }
                            break;
                        }
                    }
                }
                oLerXml.Close();
            }
        }

        List<monitora> listaMonitora = new List<monitora>();
        Timer timer = null;

        private void button2_Click(object sender, EventArgs e)
        {
            UniNFeTXT.ConversaoTXT txt_xml = new UniNFeTXT.ConversaoTXT("1.10");
            if (txt_xml.Converter(this.txtFileName.Text, txtFolderOutput.Text))
            {
                foreach (UniNFeTXT.txtTOxmlClassRetorno retorno in txt_xml.cRetorno)
                {
                    txtResultado.Text = retorno.XMLFileName + Environment.NewLine;
                    if (File.Exists("uninfeconfig.xml"))
                    {
                        if (txtFolderOutput.Text.Equals(ConfiguracaoApp.PastaValidar))
                        {
                            ///
                            /// cria um timer para ver o status de cada classe sendo executada
                            /// 
                            if (timer == null)
                            {
                                timer = new Timer();
                                timer.Tick += new EventHandler(timer_Tick);
                                timer.Interval = 1000;
                                timer.Start();
                            }
                            timer.Enabled = false;
                            try
                            {
                                listaMonitora.Add(new monitora(this.txtFileName.Text,
                                        ConfiguracaoApp.vPastaXMLRetorno,
                                        Path.GetFileNameWithoutExtension(retorno.XMLFileName).Replace("-nfe", "") + "-nfe-ret.xml"));

                                ///
                                /// move o arquivo XML criado na pasta Validar\Convertidos para a pasta Validar
                                /// 
                                FileInfo oArquivo = new FileInfo(ConfiguracaoApp.PastaValidar + "\\convertidos\\" + retorno.XMLFileName);

                                string vNomeArquivoDestino = ConfiguracaoApp.PastaValidar + "\\" + retorno.XMLFileName;
                                if (File.Exists(vNomeArquivoDestino))
                                {
                                    new FileInfo(vNomeArquivoDestino).Delete();
                                }
                                ///
                                /// move o arquivo da pasta "Validar\Convertidos" para a pasta "Validar"
                                /// 
                                oArquivo.MoveTo(vNomeArquivoDestino);
                            }
                            finally
                            {
                                timer.Enabled = true;
                            }
                        }
                    }
                }
            }
            else
            {
                this.txtResultado.Text = txt_xml.cMensagemErro;
                return;
            }
#if _x

            //MessageBox.Show("OK");

            /*
            */
            UniNFeLibrary.UnitxtTOxmlClass oTxtToXml = new UniNFeLibrary.UnitxtTOxmlClass();
            oTxtToXml.Converter(txtFileName.Text, txtFolderOutput.Text);
            txtResultado.Text = "";

            if (oTxtToXml.cMensagemErro != "")
                txtResultado.Text = oTxtToXml.cMensagemErro;
            else
            {
                //MessageBox.Show(oTxtToXml.cRetorno.Count.ToString() + " arquivo(s) convertido(s) com sucesso");
                for (int i = 0; i <= oTxtToXml.cRetorno.Count - 1; i++)
                    txtResultado.Text += oTxtToXml.cRetorno[i].XMLFileName + Environment.NewLine;
            }
#endif
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            try
            {
                foreach (monitora m in listaMonitora)
                {
                    if (m.encerra || m.Processando || m.Abortado || string.IsNullOrEmpty(m.XMLFileName)) continue;

                    if (m.terminado)
                    {
                        this.txtResultado.Text += m.XMLFileName + " terminado.......\r\n";
                        m.encerra = true;
                    }
                    else
                    {
                        this.txtResultado.Text += m.XMLFileName + " em execução." + "\r\n";
                    }
                }
            }
            finally
            {
                timer.Enabled = true;
            }
        }

        private void button_selectxmlenvio_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            DialogResult result = this.openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.txtFileName.Text = this.openFileDialog1.FileName;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timer != null)
            {
                ///
                /// o que fazer com os processos ainda não terminados???
                /// 
                timer.Stop();
            }
            timer = null;
        }

        // OBTENDO O ENDEREÇO FÍSICO DE UM WINDOWS SERVICE

        // Obtêm o endereço físico (diretório) do serviço
        // defaultDir representa um diretório padrão caso não seja possível obter a chave
        // this.ServiceName é o nome do serviço
        /*
        private string GetPhysicalPath(string defaultDir)
        {
            string diretorio;

            //' Obtêm a chave de registro.
            Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\" + this.ServiceName, false);

            diretorio = (string)Key.GetValue("ImagePath", defaultDir);

            //' Remove diretórios de executáveis e debug, no caso de querer o path padrão
            diretorio = diretorio.Replace("\\bin", "").Replace("\\Debug", "");

            //' Retorna o diretório sem o arquivo executável
            //' Obtêm a última barra do diretório e seleciona a string até este ponto.
            int UltBarra = diretorio.LastIndexOf("\\");
            return diretorio.Substring(0, UltBarra + 1);

            //' Assim, caso seu Windows Service esteja em:
            //'c:\Projetos\WinService\bin\debug\WinService.exe, esse método obterá o path:
            //'c:\Projetos\WinService
        }*/
    }

    public class monitora
    {
        private FileSystemWatcher fs = null;
        private string txtFile;
        private Timer timer = null;
        private int nCount = 0;
        public bool encerra
        {
            get;
            set;
        }
        public bool Processando
        {
            get;
            set;
        }
        public bool Abortado
        {
            get;
            set;
        }
        public bool terminado
        {
            get 
            {
                return (fs != null ? !fs.EnableRaisingEvents : false);
            }
        }
        public string XMLFileName
        {
            get 
            { 
                return (fs != null ? fs.Filter : ""); 
            }
            private set 
            { 
                if (fs != null)
                    fs.Filter = value; 
            }
        }

        public monitora(string txtfile, string path, string Filter)
        {
            this.Processando = false;
            this.Abortado = false;
            this.encerra = false;
            this.XMLFileName = "";
            this.txtFile = txtfile;

            if (File.Exists(path + "\\" + Filter))
                new FileInfo(path + "\\" + Filter).Delete();

            fs = new FileSystemWatcher(path, Filter);
            fs.EnableRaisingEvents = false;
            fs.NotifyFilter = NotifyFilters.FileName;
            fs.Created += new FileSystemEventHandler(fs_Created);
            fs.Error += new ErrorEventHandler(fs_Error);
            fs.WaitForChanged(WatcherChangeTypes.All, 1000);
            fs.EnableRaisingEvents = true;
            this.XMLFileName = Filter;
        }

        void fs_Error(object sender, ErrorEventArgs e)
        {
            this.Abortado = true;
            MessageBox.Show("????????? ERROR ?????????");
        }

        private bool FileInUse(string file)
        {
            bool ret = false;

            try
            {
                using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
                {
                    fs.Close();//fechar o arquivo para nao dar erro em outras aplicações
                }
                return ret;
            }
            catch
            {
                ret = true;
            }
            return ret;
        }

        void fs_Created(object sender, FileSystemEventArgs e)
        {
            if (this.Abortado)  return;

            if (FileInUse(fs.Path + "\\" + fs.Filter))
            {
                ++nCount;
                if (nCount == 100)  //cem tentativas
                    this.Abortado = true;
                else
                {
                    MessageBox.Show(fs.Path + "\\" + fs.Filter + " em uso");

                    if (timer == null)
                    {
                        timer = new Timer();
                        timer.Interval = 20000;
                        timer.Tick += new EventHandler(timer_Tick);
                        timer.Start();
                    }
                }
                return;
            }

            fs.EnableRaisingEvents = false;
            this.Processando = true;
            try
            {
                XmlTextReader oLerXml = null;
                oLerXml = new XmlTextReader(fs.Path + "\\" + fs.Filter);

                while (oLerXml.Read())
                {
                    if (oLerXml.NodeType == XmlNodeType.Element)
                    {
                        if (oLerXml.Name == "Validacao")
                        {
                            while (oLerXml.Read())
                            {
                                if (oLerXml.NodeType == XmlNodeType.Element)
                                {
                                    if (oLerXml.Name == "xMotivo")
                                    {
                                        oLerXml.Read();
                                        MessageBox.Show("Arquivo Texto: " + txtFile + "\r\n" + fs.Path + "\\" + fs.Filter + "\r\n\r\n" + oLerXml.Value);
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
                oLerXml.Close();
                if (File.Exists(fs.Path + "\\" + fs.Filter))
                    new FileInfo(fs.Path + "\\" + fs.Filter).Delete();

                if (File.Exists(ConfiguracaoApp.vPastaXMLErro + "\\" + fs.Filter))
                    new FileInfo(ConfiguracaoApp.vPastaXMLErro + "\\" + fs.Filter).Delete();
            }
            finally
            {
                this.Processando = false;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (fs.EnableRaisingEvents)
            {
                timer.Enabled = false;
                fs_Created(sender, null);
                timer.Enabled = true;
            }
        }
    }
}
