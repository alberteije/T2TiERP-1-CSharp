using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace UniNFeLibrary
{
    /// <summary>
    /// Classe responsável pela conversão de TXT para XML da Nota Fiscal Eletrônica
    /// TXT no mesmo formato do aplicativo de NFe do estado de São Paulo
    /// </summary>
    public class UnitxtTOxmlClass
    {
        #region --- public properties

        public List<txtTOxmlClassRetorno> cRetorno = new List<txtTOxmlClassRetorno>();
        public string cMensagemErro { get; private set; }

        #endregion

        #region --- private properties

        private string[] fields = { "xLgr", "nro", "xCpl", "xBairro", "cMun", "xMun", "UF" };
        private string cChave = ""; // Monta string com a chave Nota fiscal
        private int serie = 0;
        private int nNF = 0; //Numero Nf
        private int cNF = 0; //Código Numérico que compõe a Chave de Acesso
        private int cDV = 0; //Dígito Verificador da Chave de Acesso
        private string cLinhaTXT = "";
        private int iLinhaLida = 0; //controla a linha que foi lida

        #endregion

        /// <summary>
        /// Converte o arquivo TXT gerado para ser importado através do programa emissor de Nfe SFAZ/SP para XML versão 1.10
        /// </summary>
        /// <param name="cFile">Arquivo TXT a ser convertido</param>
        /// <param name="cDestino">Pasta de destino onde é para ser gerado o XML da NFe</param>
        /// <by>Marcos Paulo Gomes - marcos@delphibr.com.br</by>
        /// <date>16/05/2009</date>
        public void Converter(string cFile, string cDestino)
        {
            cRetorno.Clear();

            //Lê o arquivo texto passado do padrao do software emissor de Nfe Sefaz/SP 
            //Variaveis utilizadas na função

            this.cMensagemErro = "";
            if (!File.Exists(cFile))
            {
                ///
                /// danasa 8-2009
                /// 
                this.cMensagemErro = "Arquivo [" + cFile + "] não encontrado";
                return;
            }

            TextReader txt = new StreamReader(cFile, Encoding.Default, true);

            try
            {
                cLinhaTXT = txt.ReadLine();
                string[] dados;
                dados = cLinhaTXT.Split('|');
                iLinhaLida++;

                if (dados[0] != "NOTAFISCAL" && dados[0] != "NOTA FISCAL")
                {
                    throw new ArgumentException("Este arquivo não é um arquivo de NOTA FISCAL. A Primeira linha do arquivo deve conter o texto 'NOTAFISCAL'");
                }

                // 26-10-2010 Frare
                // Podendo ter mais de um arquivo txt, o mesmo converte os xml's separados. 
                int nNotas = 1;
                //if (dados.GetLength(0) == 2)
                nNotas = Convert.ToInt32("0" + dados[1]);
                //if (nNotas == 0)
                //    nNotas = 1;

                this.nNF = 0;
                for (int nNota = 0; nNota < nNotas; ++nNota)
                {
                    this.ProcessaNota(txt, cDestino);
                }
                if (this.cMensagemErro != "")
                {
                    throw new Exception(this.cMensagemErro);
                }
            }
            catch (Exception ex)
            {
                cMensagemErro = "Arquivo texto: " + cFile + Environment.NewLine + Environment.NewLine + (ex.InnerException != null ? ex.InnerException.Message : ex.Message + Environment.NewLine + "Linha: " + iLinhaLida.ToString() + " :: " + cLinhaTXT);
                ///
                /// danasa 8-2009
                /// Exclui os XML convertidos
                /// 
                foreach (txtTOxmlClassRetorno txtClass in this.cRetorno)
                {
                    FileInfo fi = new FileInfo(cDestino + "\\convertidos\\" + txtClass.XMLFileName);
                    fi.Delete();
                }
            }
            finally
            {
                txt.Close();
            }
        }

        private string limpa_texto(string cTexto)
        {
            string cRetorno = cTexto;
            while (cRetorno.IndexOf("> ") > -1)
            {
                cRetorno = cRetorno.Replace("> ", ">");
            }
            cRetorno = cRetorno.Replace(" />", "/>").Replace(" </", "</");
            return cRetorno;
        }

        public Int32 GerarCodigoNumerico(Int32 numeroNF)
        {
            // alterado jhs e samuel passado de 9 digitos para 8 digitos
            string s;
            Int32 i, j, k;

            // Essa função gera um código numerico atravéz de calculos realizados sobre o parametro numero
            s = numeroNF.ToString("00000000");
            for (i = 0; i < 8; ++i)
            {
                k = 0;
                for (j = 0; j < 8; ++j)
                    k += Convert.ToInt32(s[j]) * (j + 1);
                s = (k % 11).ToString().Trim() + s;
            }
            return Convert.ToInt32(s.Substring(0, 8));
        }

        public Int32 GerarDigito(string chave)
        {
            int i, j, Digito;
            const string PESO = "4329876543298765432987654329876543298765432";
            //                   4329876543298765432987654329876543298765432

            chave = chave.Replace("NFe", "");
            if (chave.Length != 43)
                throw new Exception("Erro na composição da chave para obter o DV (" + chave.Length.ToString() + ")");

            // Manual Integracao Contribuinte v2.02a - Página: 70 //
            j = 0;
            Digito = -1;
            try
            {
                for (i = 0; i < 43; ++i)
                    j += Convert.ToInt32(chave.Substring(i, 1)) * Convert.ToInt32(PESO.Substring(i, 1));
                Digito = 11 - (j % 11);
                if ((j % 11) < 2)
                    Digito = 0;
            }
            catch
            {
                Digito = -1;
            }
            if (Digito == -1)
                throw new Exception("Erro no cálculo do DV");
            return Digito;
        }

        #region --- Validacao das tags

        /// <summary>
        /// valida uma tag
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="field"></param>
        /// <param name="dataRow"></param>
        /// <param name="optional"></param>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        private void Check(string segment, string field, DataRow dataRow, ObOp optional, int minLength, int maxLength)
        {
            int len = dataRow[field].ToString().Trim().Length;
            if (len == 0 && optional == ObOp.Opcional)
                return;

            if (len == 0 && minLength > 0)
            {
                this.cMensagemErro += string.Format("Segmento [{0}]: tag <{1}> deve ser informada. Conteudo: {2}" +
                                                    Environment.NewLine +
                                                    "\tLinha: {3}: Conteudo do segmento: {4}",
                                                    segment, field, dataRow[field].ToString(), iLinhaLida, cLinhaTXT) + Environment.NewLine;
            }
            else
                if (len > maxLength || len < minLength)
                {
                    this.cMensagemErro += string.Format("Segmento [{0}]: tag <{1}> deve ter seu tamanho entre {2} e {3}" +
                                                        ". Conteudo: " + dataRow[field].ToString() +
                                                        Environment.NewLine +
                                                        "\tLinha: {4}: Conteudo do segmento: {5}",
                                                        segment, field, minLength, maxLength, iLinhaLida, cLinhaTXT) + Environment.NewLine;
                }
        }

        /// <summary>
        /// valida uma tag do tipo numerico
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="field"></param>
        /// <param name="dataRow"></param>
        /// <param name="optional"></param>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <param name="decimals"></param>
        private void Check(string segment, string field, DataRow dataRow, ObOp optional, int minLength, int maxLength, int decimals)
        {
            this.Check(segment, field, dataRow, optional, minLength, maxLength);

            if (optional == ObOp.Obrigatorio || (optional == ObOp.Opcional && dataRow[field].ToString().Trim() != ""))
            {
                int pos = dataRow[field].ToString().Trim().IndexOf(".") + 1;
                int ndec = dataRow[field].ToString().Trim().Substring(pos).Length;
                if (ndec != decimals)
                    this.cMensagemErro += string.Format("Segmento [{0}]: tag <{1}> número de casas decimais deve ser de {2} e existe(m) {3}" +
                                                        Environment.NewLine +
                                                        "\tLinha: {4}: Conteudo do segmento: {5}",
                                                        segment, field, decimals, ndec, iLinhaLida, cLinhaTXT) + Environment.NewLine;
            }
        }

        private void CheckMaxDecimal(string segment, string field, DataRow dataRow, ObOp optional, int minLength, int maxLength, int decimals)
        {
            this.Check(segment, field, dataRow, optional, minLength, maxLength);

            if (optional == ObOp.Obrigatorio || (optional == ObOp.Opcional && dataRow[field].ToString().Trim() != ""))
            {
                int pos = dataRow[field].ToString().Trim().IndexOf(".") + 1;
                int ndec = dataRow[field].ToString().Trim().Substring(pos).Length;
                if (ndec > decimals)
                    this.cMensagemErro += string.Format("Segmento [{0}]: tag <{1}> número de casas decimais deve ser de {2} e existe(m) {3}" +
                                                        Environment.NewLine +
                                                        "\tLinha: {4}: Conteudo do segmento: {5}",
                                                        segment, field, decimals, ndec, iLinhaLida, cLinhaTXT) + Environment.NewLine;
            }
        }
        /// <summary>
        /// Valida uma tag do tipo data
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="field"></param>
        /// <param name="dataRow"></param>
        /// <param name="optional"></param>
        private void Check(string segment, string field, DataRow dataRow, ObOp optional)
        {
            this.Check(segment, field, dataRow, optional, 10, 10);
            if (optional == ObOp.Obrigatorio || (optional == ObOp.Opcional && dataRow[field].ToString().Trim() != ""))
            {
                string content = dataRow[field].ToString().Trim();
                int pos = content.IndexOf("-");
                if (pos > -1)
                {
                    string[] dados = content.Split('-');
                    if (dados.Length > 0)
                    {
                        int _ano = Convert.ToInt16("0" + dados[0]);
                        int _mes = Convert.ToInt16("0" + dados[1]);
                        int _dia = Convert.ToInt16("0" + dados[2]);
                        if (_ano == 0 || _mes < 0 || _mes > 12 || _dia < 0 || _dia > 31)
                            pos = -1;
                    }
                    else
                        pos = -1;
                }
                if (pos == -1)
                    this.cMensagemErro += string.Format("Segmento [{0}]: tag <{1}> data inválida. Conteudo: {2}" +
                                                        Environment.NewLine +
                                                        "\tLinha: {3}: Conteudo do segmento: {4}",
                                                        segment, field, dataRow[field].ToString(), iLinhaLida, cLinhaTXT) + Environment.NewLine;
            }
        }

        #endregion

        #region ConverToOEM
        private string ConvertToOEM(string FBuffer)
        {
            const string FAnsi = (" áéíóúÁÉÍÓÚçÇàèìòùÀÈÌÒÙãõÃÕºª§ÑâäåêëïîÄÅôûÿÖÜñüÂ?");
            const string FOEM = (" aeiouAEIOUcCaeiouAEIOUaoAOoa.NaaaeeiiAAouyOUnuAØ");
            int L, P;
            char X;
            string result = "";

            for (L = 0; L < FBuffer.Length; ++L)
            {
                X = (char)FBuffer[L];
                P = FAnsi.IndexOf(X);
                if (P >= 0) X = FOEM[P];

                result += X;
            }
            result = result.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&#39;");
            result = result.Replace("\r\n", " ");
            return result;
        }
        #endregion

        /// <summary>
        /// Atribui ao elementos a um DataRow o conteudo do que fora lido do arquivo texto
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="dados"></param>
        private bool PopulateDataRow(DataRow dr, string[] dados, int maxElementos)
        {
            bool result = false;
            for (int iLeitura = 0; iLeitura <= Math.Min(dados.GetLength(0) - 1, maxElementos); iLeitura++)
            {
                if (iLeitura > 0 && dados[iLeitura] != null && dados[iLeitura].Trim() != "")
                {
                    dr[iLeitura - 1] = dados[iLeitura].Trim();
                    result = true;
                }
            }
            return result;
        }

        private bool linhaValida(TextReader txt)
        {
            //cLinhaTXT += "!@#$%^&*()_+";
            bool reLe = true;
            for (int x = 0; x < cLinhaTXT.Length - 1; ++x)
                //if (cLinhaTXT[x] != '|' && cLinhaTXT[x] != '=' && cLinhaTXT[x] != '%')
                if (/*char.IsSymbol(cLinhaTXT, x) ||*/ char.IsControl(cLinhaTXT, x))
                {
                    this.cMensagemErro += "Linha [" + this.iLinhaLida.ToString() + "] coluna [" + (x + 1).ToString() + "] contem o caracter [" + cLinhaTXT.Substring(x, 1) + "] que não é permitido" + Environment.NewLine;
                    //this.cMensagemErro += "\t"+cLinhaTXT + Environment.NewLine;

                    cLinhaTXT = txt.ReadLine();
                    iLinhaLida++;
                    reLe = false;
                    break;
                }

            return reLe;
        }

        /// <summary>
        /// Processa o arquivo texto
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="cDestino"></param>
        private void ProcessaNota(TextReader txt, string cDestino)
        {
            string baseDir = InfoApp.PastaSchemas() + "\\nfe_v2.00.xsd";

            if (!File.Exists(baseDir))
            {
                this.cMensagemErro += "Arquivo: " + baseDir + " não encontrado" + Environment.NewLine;
                return;
            }
            DataSet dsNfe = new DataSet();
            dsNfe.ReadXmlSchema(baseDir);
            dsNfe.EnforceConstraints = false; //permite campos nulos

            DataRow dremit = dsNfe.Tables["emit"].NewRow();
            DataRow drdest = dsNfe.Tables["dest"].NewRow();
            DataRow drPISOutr = null;
            DataRow drPISST = null;
            DataRow drCOFINSOutr = null;
            DataRow drCOFINSST = null;
            DataRow drtransporta = null;
            DataRow drIPITrib = null;
            DataRow drVol = null;

            string idprod = ""; // Guarda o Id do produto, usado para gravar os dados referente a impostos
            int iControle = 1;
            int nElementos;
            int iLeitura;
            string[] dados;
            Int64 iTmp = 0;
            bool vNovaNota = false;
            bool vTiraxFant = false;
            bool transpAdd = false;

            this.nNF = 0;

            int DIid = 0;//danasa 27-9-2009
            int prodID = 0;//danasa 27-9-2009
            int idcomb = 0;//danasa 27-9-2009
            int volid = 0;//danasa 27-9-2009
            int indadicid = 0;//danasa 27-9-2009

            while (cLinhaTXT != null)
            {
                cLinhaTXT = this.ConvertToOEM(this.cLinhaTXT);

                if (!linhaValida(txt)) continue;
/*
                //cLinhaTXT += "!@#$%^&*()_+";
                bool reLe = false;
                for (int x = 0; x < cLinhaTXT.Length - 1; ++x)
                    if (char.IsControl(cLinhaTXT, x))
                    {
                        this.cMensagemErro += "Linha [" + this.iLinhaLida.ToString() + "] coluna [" + (x + 1).ToString() + "] contem o caracter [" + cLinhaTXT.Substring(x, 1) + "] que não é permitido" + Environment.NewLine;

                        cLinhaTXT = txt.ReadLine();
                        iLinhaLida++;
                        reLe = true;
                        break;
                    }

                if (reLe)
                    continue;
                */
                dados = cLinhaTXT.Split('|');
                dados[0] = dados[0].ToUpper();
                nElementos = dados.GetLength(0) - 1;
                for (int n = 0; n < nElementos; ++n)
                    dados[n] = dados[n].Trim();

                #region -- Segmentos

                switch (dados[0])
                {
                    case "NOTAFISCAL":
                    case "NOTA FISCAL":
                        break;

                    case "A":
                        #region -- A

                        if (this.nNF > 0)
                        {
                            vNovaNota = true;
                        }
                        else
                        {
                            //A|1.10|NFe35090504176770000140550010000176500000176506|
                            DataRow dr = dsNfe.Tables["infNFe"].NewRow();
                            if (nElementos >= 1)
                                dr["versao"] = dados[1].Trim();
                            if (nElementos >= 2)
                                dr["id"] = dados[2]; //id
                            dr["infNFe_Id"] = 0;
                            dsNfe.Tables["infNFe"].Rows.Add(dr);

                            this.Check(dados[0], "versao", dr, ObOp.Obrigatorio, 1, 4);
                        }
                        break;

                        #endregion

                    case "B": //tag <infNFe><ide
                        #region -- B
                        {
                            DataRow dr = dsNfe.Tables["ide"].NewRow();

                            cChave = "";
                            this.PopulateDataRow(dr, dados, 23);
                            dr["infNFe_Id"] = 0;
                            dr["ide_Id"] = 0;
                            dr["procEmi"] = 0;  //0 - emissão de NF-e com aplicativo do contribuinte;
                            dsNfe.Tables["ide"].Rows.Add(dr);

                            //B|cUF|cNF|natOp|indPag|mod|serie|nNF|dEmi|dSaiEnt|hSaiEnt|tpNF|cMunFG|tpImp|tpEmis|cDV|tpAmb|finNFe|procEmi|verProc|
                            if (dr["cUF"].ToString() == "")
                            {
                                ///
                                /// Assume a UF da configuracao
                                ///
                                int emp = new FindEmpresaThread(Thread.CurrentThread).Index; 

                                dr["cUF"] = Empresa.Configuracoes[emp].UFCod;
                            }

                            this.Check(dados[0], "cUF", dr, ObOp.Obrigatorio, 2, 2);
                            this.Check(dados[0], "natOp", dr, ObOp.Obrigatorio, 1, 60);
                            this.Check(dados[0], "mod", dr, ObOp.Obrigatorio, 2, 2);
                            this.Check(dados[0], "serie", dr, ObOp.Obrigatorio, 1, 3);
                            this.Check(dados[0], "nNF", dr, ObOp.Obrigatorio, 1, 9);
                            this.Check(dados[0], "dEmi", dr, ObOp.Obrigatorio);
                            this.Check(dados[0], "dSaiEnt", dr, ObOp.Opcional);

                            // alterado jhs e samuel
                            Check(dados[0], "hSaiEnt", dr, ObOp.Opcional, 8, 8);

                            this.Check(dados[0], "tpNF", dr, ObOp.Obrigatorio, 1, 1);
                            this.Check(dados[0], "cMunFG", dr, ObOp.Obrigatorio, 7, 7);
                            this.Check(dados[0], "tpImp", dr, ObOp.Obrigatorio, 1, 1);
                            this.Check(dados[0], "tpEmis", dr, ObOp.Obrigatorio, 1, 1);
                            this.Check(dados[0], "cDV", dr, ObOp.Opcional, 1, 1);
                            this.Check(dados[0], "tpAmb", dr, ObOp.Obrigatorio, 1, 1);
                            this.Check(dados[0], "finNFe", dr, ObOp.Obrigatorio, 1, 1);
                            this.Check(dados[0], "procEmi", dr, ObOp.Obrigatorio, 1, 1);
                            this.Check(dados[0], "verProc", dr, ObOp.Obrigatorio, 1, 20);
                            this.Check(dados[0], "xJust", dr, ObOp.Opcional, 0, 256);

                            ///
                            /// danasa 8-2009 (adicionado o "0" para que não haja erro de conversão)
                            /// 
                            serie = Convert.ToInt32("0" + dr["serie"].ToString());
                            nNF = Convert.ToInt32("0" + dr["nNF"].ToString());   //Numero Nf
                            cNF = Convert.ToInt32("0" + dr["cNF"].ToString());   //Código Numérico que compõe a Chave de Acesso
                            cDV = Convert.ToInt32("0" + dr["cDV"].ToString()); ; //Dígito Verificador da Chave de Acesso

                            cChave = dr["cUF"].ToString() + dr["dEmi"].ToString().Substring(2, 2) +
                                     dr["dEmi"].ToString().Substring(5, 2); //data AAMM
                        }
                        break;

                        #endregion

                    case "B13":
                    case "B14": //tag <infNFe><ide><refNF>
                        #region -- B13 ou B14
                        {
                            //esse codigo foi montado dessa forma para que possa mater a tag <NFref><refNf>
                            DataRow drNFref = dsNfe.Tables["NFref"].NewRow();
                            drNFref["ide_Id"] = 0;
                            drNFref["NFref_Id"] = iControle;
                            if (dados[0] == "B13") //<NFref>
                            {
                                if (nElementos >= 1)
                                    if (dados[1].Trim() != "")
                                        drNFref[0] = dados[1].Trim(); //caso tenha o segmento B13 preenche o campo chave

                                this.Check(dados[0], "refNFe", drNFref, ObOp.Obrigatorio, 44, 44);
                            }
                            dsNfe.Tables["NFref"].Rows.Add(drNFref);

                            if (dados[0] == "B14")
                            {
                                DataRow dr = dsNfe.Tables["refNF"].NewRow();
                                if (this.PopulateDataRow(dr, dados, 6))
                                {
                                    dr["serie"] = Convert.ToInt32("0" + dr["serie"].ToString());
                                    dr["NFref_Id"] = iControle;
                                    dsNfe.Tables["refNF"].Rows.Add(dr);

                                    this.Check(dados[0], "cUF", dr, ObOp.Obrigatorio, 2, 2);
                                    this.Check(dados[0], "AAMM", dr, ObOp.Obrigatorio, 4, 4);
                                    this.Check(dados[0], "CNPJ", dr, ObOp.Obrigatorio, 14, 14);
                                    this.Check(dados[0], "mod", dr, ObOp.Obrigatorio, 2, 2);
                                    this.Check(dados[0], "serie", dr, ObOp.Obrigatorio, 1, 3);
                                    this.Check(dados[0], "nNF", dr, ObOp.Obrigatorio, 1, 9);
                                }
                            }
                            iControle = iControle + 1;
                        }
                        break;

                        #endregion

                    case "B20A":    //B20a|cUF|AAMM|IE|mod|serie|nNF|
                        #region -- B20a
                        {
                            //if (dsNfe.Tables["NFref"].Rows.Count == 0)
                            {
                                DataRow drNFref = dsNfe.Tables["NFref"].NewRow();
                                drNFref["ide_Id"] = 0;
                                drNFref["NFref_Id"] = iControle++;
                                dsNfe.Tables["NFref"].Rows.Add(drNFref);
                            }
                            DataRow drrefNFP = dsNfe.Tables["refNFP"].NewRow();
                            drrefNFP["NFref_Id"] = iControle - 1;
                            if (nElementos > 0) drrefNFP["cUF"] = dados[1];
                            if (nElementos > 1) drrefNFP["AAMM"] = dados[2];
                            if (nElementos > 2) drrefNFP["IE"] = dados[3];
                            if (nElementos > 3) drrefNFP["mod"] = dados[4];
                            if (nElementos > 4) drrefNFP["serie"] = dados[5];
                            if (nElementos > 5) drrefNFP["nNF"] = dados[6];
                            this.Check(dados[0], "cUF", drrefNFP, ObOp.Obrigatorio, 2, 2);
                            this.Check(dados[0], "AAMM", drrefNFP, ObOp.Obrigatorio, 4, 4);
                            this.Check(dados[0], "mod", drrefNFP, ObOp.Obrigatorio, 2, 2);
                            this.Check(dados[0], "serie", drrefNFP, ObOp.Obrigatorio, 1, 3);
                            this.Check(dados[0], "nNF", drrefNFP, ObOp.Obrigatorio, 1, 9);
                            dsNfe.Tables["refNFP"].Rows.Add(drrefNFP);
                        }
                        #endregion

                        break;

                    case "B20D":    //B20d|CNPJ|
                        #region -- B20d
                        {
                            if (dsNfe.Tables["refNFP"].Rows.Count == 0)
                                this.cMensagemErro += "Falta definir o segmento [B20a] no segmento [B20d]" + Environment.NewLine;
                            else
                                if (nElementos >= 1)
                                    if (dados[1].Trim() != "")
                                    {
                                        DataRow ds = dsNfe.Tables["refNFP"].Rows[dsNfe.Tables["refNFP"].Rows.Count - 1];
                                        ds["CNPJ"] = dados[1].Trim();
                                        this.Check(dados[0], "CNPJ", ds, ObOp.Obrigatorio, 14, 14);
                                    }
                        }
                        #endregion
                        break;

                    case "B20E":    //B20e|CPF|
                        #region -- B20e
                        {
                            if (dsNfe.Tables["refNFP"].Rows.Count == 0)
                                this.cMensagemErro += "Falta definir o segmento [B20a] no segmento [B20e]" + Environment.NewLine;
                            else
                                if (nElementos >= 1)
                                    if (dados[1].Trim() != "")
                                    {
                                        DataRow ds = dsNfe.Tables["refNFP"].Rows[dsNfe.Tables["refNFP"].Rows.Count - 1];
                                        ds["CPF"] = dados[1].Trim();
                                        this.Check(dados[0], "CPF", ds, ObOp.Obrigatorio, 11, 11);
                                    }
                        }
                        #endregion
                        break;

                    case "B20I":    //B20i|refCTe|
                        #region -- B20i
                        {
                            //if (dsNfe.Tables["NFref"].Rows.Count == 0)
                            {
                                DataRow drNFref = dsNfe.Tables["NFref"].NewRow();
                                drNFref["ide_Id"] = 0;
                                drNFref["NFref_Id"] = iControle++;
                                dsNfe.Tables["NFref"].Rows.Add(drNFref);
                            }
                            DataRow drrefCTe = dsNfe.Tables["NFref"].Rows[dsNfe.Tables["NFref"].Rows.Count - 1];
                            drrefCTe["refCTe"] = dados[1];
                            this.Check(dados[0], "refCTe", drrefCTe, ObOp.Obrigatorio, 44, 44);
                        }
                        #endregion

                        break;

                    case "B20J":    //B20j|mod|nECF|nCOO|
                        #region -- B20i
                        {
                            //if (dsNfe.Tables["NFref"].Rows.Count == 0)
                            {
                                DataRow drNFref = dsNfe.Tables["NFref"].NewRow();
                                drNFref["ide_Id"] = 0;
                                drNFref["NFref_Id"] = iControle++;
                                dsNfe.Tables["NFref"].Rows.Add(drNFref);
                            }
                            DataRow drrefECF = dsNfe.Tables["refECF"].NewRow();
                            drrefECF["NFref_Id"] = iControle - 1;
                            if (nElementos > 0) drrefECF["mod"] = dados[1];
                            if (nElementos > 1) drrefECF["nECF"] = dados[2];
                            if (nElementos > 2) drrefECF["nCOO"] = dados[3];
                            this.Check(dados[0], "mod", drrefECF, ObOp.Obrigatorio, 2, 2);
                            this.Check(dados[0], "nECF", drrefECF, ObOp.Obrigatorio, 1, 3);
                            this.Check(dados[0], "nCOO", drrefECF, ObOp.Obrigatorio, 1, 6);
                            dsNfe.Tables["refECF"].Rows.Add(drrefECF);
                        }
                        #endregion

                        break;

                    case "C": //tag <infNFe><ide><emit>
                        #region -- C
                        {
                            dremit["IE"] = "";
                            //nao preenche o campo cnpj ou cpf, sera preenchido mais abaixo
                            dremit["infNFe_Id"] = 0;
                            dremit["emit_Id"] = 0;

                            dremit["xNome"] = dados[1];
                            if (nElementos > 1)
                            {
                                if (dados[2] != "")
                                    dremit["xFant"] = dados[2];
                                if (nElementos > 2)
                                {
                                    if (dados[3] != "")
                                        dremit["IE"] = dados[3];
                                    if (nElementos > 3)
                                    {
                                        if (dados[4] != "")
                                            dremit["IEST"] = dados[4];
                                        if (nElementos > 4)
                                        {
                                            if (dados[5] != "")
                                                dremit["IM"] = dados[5];
                                            if (nElementos > 5)
                                            {
                                                if (dados[6] != "")
                                                    dremit["CNAE"] = dados[6];
                                            }

                                            // alterado jhs e samuel
                                            if (nElementos > 6)
                                            {
                                                if (dados[7] != "")
                                                    dremit["CRT"] = dados[7];
                                            }
                                        }
                                    }
                                }
                            }
                            this.Check(dados[0], "xNome", dremit, ObOp.Obrigatorio, 1, 60);
                            this.Check(dados[0], "xFant", dremit, ObOp.Opcional, 1, 60);
                            this.Check(dados[0], "IE", dremit, ObOp.Obrigatorio, 0, 14);
                            this.Check(dados[0], "IEST", dremit, ObOp.Opcional, 2, 14);
                            this.Check(dados[0], "IM", dremit, ObOp.Opcional, 1, 15);
                            this.Check(dados[0], "CNAE", dremit, ObOp.Opcional, 7, 7);

                            ///
                            /// danasa 8-2009
                            /// Se não definido o nome fantasia, atribuo o proprio nome da empresa
                            /// pois quando for ajustar a IE, é obrigratorio ter a tag <xFant>
                            /// 
                            if (dremit["xFant"].ToString() == "")
                            {
                                vTiraxFant = true;
                                dremit["xFant"] = dremit["xNome"].ToString();
                            }
                        }
                        break;

                        #endregion

                    case "C02": //ainda tag <infNFe><ide><emit>, preenche o cnpj
                        #region -- C02

                        if (nElementos >= 1)
                            if (dados[1].Trim() != "")
                                dremit[0] = dados[1].Trim();

                        this.Check(dados[0], "CNPJ", dremit, ObOp.Obrigatorio, 14, 14);
                        iTmp = Convert.ToInt64("0" + dremit["CNPJ"]);
                        cChave = cChave + iTmp.ToString("00000000000000") + "55";
                        break;

                        #endregion

                    case "C02A": //ainda tag <infNFe><ide><emit>, preenche o cpf
                        #region -- C02A
                        if (nElementos >= 1)
                            if (dados[1].Trim() != "")
                                dremit[1] = dados[1].Trim();

                        this.Check(dados[0], "CPF", dremit, ObOp.Obrigatorio, 11, 11);
                        break;
                        #endregion

                    case "C05":
                        #region -- C05
                        {
                            DataRow dr = dsNfe.Tables["enderEmit"].NewRow();

                            dr["emit_Id"] = 0;

                            dsNfe.Tables["emit"].Rows.Add(dremit);
                            if (this.PopulateDataRow(dr, dados, 11))
                            {
                                dsNfe.Tables["enderEmit"].Rows.Add(dr);

                                this.Check(dados[0], "xLgr", dr, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "nro", dr, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "xCpl", dr, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "xBairro", dr, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "cMun", dr, ObOp.Obrigatorio, 1, 7);
                                this.Check(dados[0], "xMun", dr, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "UF", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "CEP", dr, ObOp.Opcional, 8, 8);
                                this.Check(dados[0], "cPais", dr, ObOp.Opcional, 4, 4);
                                this.Check(dados[0], "xPais", dr, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "fone", dr, ObOp.Opcional, 1, 10);
                            }
                        }
                        break;
                        #endregion

                    case "D":
                        #region -- D
                        {
                            DataRow dr = dsNfe.Tables["avulsa"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 11))
                            {
                                dr["infNFe_Id"] = 0;
                                dsNfe.Tables["avulsa"].Rows.Add(dr);

                                this.Check(dados[0], "CNPJ", dr, ObOp.Obrigatorio, 14, 14);
                                this.Check(dados[0], "xOrgao", dr, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "matr", dr, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "xAgente", dr, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "fone", dr, ObOp.Obrigatorio, 1, 10);
                                this.Check(dados[0], "UF", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "nDAR", dr, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "dEmi", dr, ObOp.Obrigatorio);
                                this.Check(dados[0], "vDAR", dr, ObOp.Obrigatorio, 1, 15);
                                this.Check(dados[0], "repEmi", dr, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "dPag", dr, ObOp.Opcional);
                            }
                        }
                        break;
                        #endregion

                    case "E": //tag <infNFe><ide><emit>
                        #region -- E
                        {
                            dsNfe.Tables["dest"].Columns["IE"].AllowDBNull = true;
                            drdest["IE"] = ""; //deve sempre gerar essa tag mesmo que em branco se nao ha problemas na hora dele inveter o enderdest

                            // alterado jhs e samuel
                            for (iLeitura = 0; iLeitura <= Math.Min(nElementos, 5); iLeitura++)
                            {
                                //nao preenche o campo cnpj ou cpf, sera preenchido mais abaixo
                                if (iLeitura > 1 & dados[iLeitura] != null && dados[iLeitura - 1].Trim() != "")
                                    drdest[iLeitura] = dados[iLeitura - 1].Trim();
                            }
                            drdest["dest_Id"] = 0;
                            drdest["infNFe_Id"] = 0;

                            this.Check(dados[0], "xNome", drdest, ObOp.Obrigatorio, 1, 60);
                            this.Check(dados[0], "IE", drdest, ObOp.Obrigatorio, 0, 14);
                            this.Check(dados[0], "ISUF", drdest, ObOp.Opcional, 1, 9);
                            // alterado jhs e samuel
                            this.Check(dados[0], "email", drdest, ObOp.Opcional, 1, 60);
                        }
                        break;
                        #endregion

                    case "E02": //ainda tag <infNFe><ide><emit>, preenche o cnpj
                        #region -- E02
                        if (nElementos >= 1)
                            if (dados[1].Trim() != "")
                            {
                                drdest["CNPJ"] = dados[1].Trim();
                                this.Check(dados[0], "CNPJ", drdest, ObOp.Obrigatorio, 14, 14);
                            }
                        dsNfe.Tables["dest"].Rows.Add(drdest);

                        break;
                        #endregion

                    case "E03": //ainda tag <infNFe><ide><emit>, preenche o cpf
                        #region -- E03
                        if (nElementos >= 1)
                            if (dados[1].Trim() != "")
                            {
                                drdest["CPF"] = dados[1].Trim();
                                this.Check(dados[0], "CPF", drdest, ObOp.Obrigatorio, 11, 11);
                            }
                        dsNfe.Tables["dest"].Rows.Add(drdest);

                        break;
                        #endregion

                    case "E05":
                        #region -- E05
                        {
                            DataRow drenderDest = dsNfe.Tables["enderDest"].NewRow();
                            if (this.PopulateDataRow(drenderDest, dados, 11))
                            {
                                drenderDest["dest_Id"] = 0;
                                dsNfe.Tables["enderDest"].Rows.Add(drenderDest);

                                this.Check(dados[0], "xLgr", drenderDest, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "nro", drenderDest, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "xCpl", drenderDest, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "xBairro", drenderDest, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "cMun", drenderDest, ObOp.Obrigatorio, 1, 7);
                                this.Check(dados[0], "xMun", drenderDest, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "UF", drenderDest, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "CEP", drenderDest, ObOp.Opcional, 8, 8);
                                this.Check(dados[0], "cPais", drenderDest, ObOp.Opcional, 1, 4);
                                this.Check(dados[0], "xPais", drenderDest, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "fone", drenderDest, ObOp.Opcional, 6, 14);

                                ///
                                /// danasa 9-2009
                                /// Exportacao não tem CNPJ nem IE
                                /// 
                                if (drenderDest["UF"].ToString() == "EX")
                                {
                                    if (dsNfe.Tables["dest"].Rows.Count == 0)
                                        dsNfe.Tables["dest"].Rows.Add(drdest);

                                    dsNfe.Tables["dest"].Rows[0]["IE"] = "";
                                    dsNfe.Tables["dest"].Rows[0]["CNPJ"] = "";
                                }
                                else
                                    if (dsNfe.Tables["dest"].Rows.Count == 0)
                                        this.cMensagemErro += "Falta definir o segmento [E02] ou [E03]" + Environment.NewLine;
                            }
                        }
                        break;
                        #endregion

                    case "F":
                        #region -- F
                        {
                            DataRow drretirada = dsNfe.Tables["retirada"].NewRow();
                            //if (this.PopulateDataRow(drretirada, dados, 8))
                            {
                                drretirada["infNFe_Id"] = 0;    //<<< Adicionado em 27-9-2009

                                //danasa 27-2-2011
                                for (int i = 1; i < dados.GetLength(0) - 1; ++i)
                                    if (dados[i] != "")
                                        drretirada[fields[i - 1]] = dados[i];

                                dsNfe.Tables["retirada"].Rows.Add(drretirada);
                                /*
                                this.Check(dados[0], "CNPJ", drretirada, ObOp.Obrigatorio, 14, 14);
                                // 22-10-2010 - Frare
                                // Campo CPF para montar o arquivo de xml, seguindo o leiaoute 2.0.
                                this.Check(dados[0], "CPF", drretirada, ObOp.Opcional, 11, 11);
                                 */ 
                                this.Check(dados[0], "xLgr", drretirada, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "nro", drretirada, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "xCpl", drretirada, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "xBairro", drretirada, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "cMun", drretirada, ObOp.Obrigatorio, 1, 7);
                                this.Check(dados[0], "xMun", drretirada, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "UF", drretirada, ObOp.Obrigatorio, 2, 2);
                            }
                        }
                        break;
                        #endregion

                    case "F02":
                        #region -- F02
                        {
                            if (dsNfe.Tables["retirada"].Rows.Count == 0)
                                this.cMensagemErro += "Falta definir o segmento [F]" + Environment.NewLine;
                            else
                            {
                                DataRow drretirada = dsNfe.Tables["retirada"].Rows[0];
                                drretirada["CNPJ"] = dados[1];
                                this.Check(dados[0], "CNPJ", drretirada, ObOp.Obrigatorio, 14, 14);
                            }
                        }
                        break;

                        #endregion

                    case "F02A":
                        #region -- F02a
                        {
                            if (dsNfe.Tables["retirada"].Rows.Count == 0)
                                this.cMensagemErro += "Falta definir o segmento [F]" + Environment.NewLine;
                            else
                            {
                                DataRow drretirada = dsNfe.Tables["retirada"].Rows[0];
                                drretirada["CPF"] = dados[1];
                                this.Check(dados[0], "CPF", drretirada, ObOp.Opcional, 11, 11);
                            }
                        }
                        break;

                        #endregion

                    case "G":
                        #region -- G
                        {
                            DataRow drentrega = dsNfe.Tables["entrega"].NewRow();
                            //if (this.PopulateDataRow(drentrega, dados, 8))
                            {
                                drentrega["infNFe_Id"] = 0;

                                //danasa 27-2-2011
                                for (int i = 1; i < dados.GetLength(0)-1; ++i)
                                    if (dados[i] != "")
                                        drentrega[fields[i-1]] = dados[i];

                                dsNfe.Tables["entrega"].Rows.Add(drentrega);

                                /*
                                this.Check(dados[0], "CNPJ", drentrega, ObOp.Obrigatorio, 14, 14);
                                // 22-10-2010 - Frare
                                // Campo CPF para montar o arquivo de xml, seguindo o leiaoute 2.0.
                                this.Check(dados[0], "CPF", drentrega, ObOp.Opcional, 11, 11);
                                 */
                                this.Check(dados[0], "xLgr", drentrega, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "nro", drentrega, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "xCpl", drentrega, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "xBairro", drentrega, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "cMun", drentrega, ObOp.Obrigatorio, 1, 7);
                                this.Check(dados[0], "xMun", drentrega, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "UF", drentrega, ObOp.Obrigatorio, 2, 2);
                            }
                        }
                        break;
                        #endregion

                    case "G02":
                        #region -- G02
                        {
                            if (dsNfe.Tables["entrega"].Rows.Count == 0)
                                this.cMensagemErro += "Falta definir o segmento [G]" + Environment.NewLine;
                            else
                            {
                                DataRow drentrega = dsNfe.Tables["entrega"].Rows[0];
                                drentrega["CNPJ"] = dados[1];
                                this.Check(dados[0], "CNPJ", drentrega, ObOp.Obrigatorio, 14, 14);
                            }
                        }
                        break;

                        #endregion

                    case "G02A":
                        #region -- G02a
                        {
                            if (dsNfe.Tables["entrega"].Rows.Count == 0)
                                this.cMensagemErro += "Falta definir o segmento [G]" + Environment.NewLine;
                            else
                            {
                                DataRow drentrega = dsNfe.Tables["entrega"].Rows[0];
                                drentrega["CPF"] = dados[1];
                                this.Check(dados[0], "CPF", drentrega, ObOp.Opcional, 11, 11);
                            }
                        }
                        break;

                        #endregion

                    case "H":
                        #region -- H
                        {
                            bool r = false;
                            DataRow drdet = dsNfe.Tables["det"].NewRow();
                            if (nElementos >= 1)
                            {
                                if (dados[1].Trim() != "")
                                {
                                    drdet["nItem"] = dados[1].Trim();
                                    r = true;
                                }
                            }
                            if (nElementos >= 2)
                            {
                                if (dados[2].Trim() != "")
                                    drdet["infAdProd"] = dados[2].Trim();
                                else
                                    drdet["infAdProd"] = "~-?-~";
                            }
                            else
                                drdet["infAdProd"] = "~-?-~";

                            if (r)
                            {
                                idprod = drdet["nItem"].ToString();
                                drdet["det_Id"] = idprod; //det_Id
                                drdet["infNFe_Id"] = 0;
                                dsNfe.Tables["det"].Rows.Add(drdet);

                                this.Check(dados[0], "nItem", drdet, ObOp.Obrigatorio, 1, 3);
                                this.Check(dados[0], "infAdProd", drdet, ObOp.Opcional, 0, 500);
                            }
                            else
                                idprod = "";
                            prodID = Convert.ToInt32("0" + idprod);//danasa 27-9-2009
                        }
                        break;
                        #endregion

                    case "I":
                        #region -- I
                        {
                            DataRow drprod = dsNfe.Tables["prod"].NewRow();
                            drprod["cEAN"] = ""; //se nao deixa-lo em branco da erro
                            drprod["CEANTrib"] = ""; //se nao deixa-lo em branco da erro.
                            // alterado jhs e samuel
                            if (this.PopulateDataRow(drprod, dados, 21))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [I] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                // alterado jhs e samuel
                                drprod[22] = idprod.ToString(); //det_Id
                                drprod["prod_ID"] = prodID.ToString();
                                ++prodID;
                                //drprod["det_Id"] = idprod.ToString();
                                dsNfe.Tables["prod"].Rows.Add(drprod);

                                this.Check(dados[0], "cProd", drprod, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "cEAN", drprod, ObOp.Obrigatorio, 0, 14);
                                this.Check(dados[0], "xProd", drprod, ObOp.Obrigatorio, 1, 120);
                                this.Check(dados[0], "NCM", drprod, ObOp.Opcional, 2, 8);
                                this.Check(dados[0], "EXTIPI", drprod, ObOp.Opcional, 2, 3);
                                // alterado jhs e samuel
                                //Coluna removida na versao 2.00
                                //this.Check(dados[0], "genero", drprod, ObOp.Opcional, 2, 2);
                                this.Check(dados[0], "CFOP", drprod, ObOp.Obrigatorio, 4, 4);
                                this.Check(dados[0], "uCom", drprod, ObOp.Obrigatorio, 1, 6);
                                this.Check(dados[0], "qCom", drprod, ObOp.Obrigatorio, 1, 12, 4);
                                this.CheckMaxDecimal(dados[0], "vUnCom", drprod, ObOp.Obrigatorio, 1, 21, 10);
                                this.Check(dados[0], "vProd", drprod, ObOp.Obrigatorio, 1, 15, 2);
                                this.Check(dados[0], "cEANTrib", drprod, ObOp.Obrigatorio, 0, 14);
                                this.Check(dados[0], "uTrib", drprod, ObOp.Obrigatorio, 1, 6);
                                this.Check(dados[0], "qTrib", drprod, ObOp.Obrigatorio, 1, 12, 4);
                                this.CheckMaxDecimal(dados[0], "vUnTrib", drprod, ObOp.Obrigatorio, 1, 21, 10);
                                this.Check(dados[0], "vFrete", drprod, ObOp.Opcional, 1, 15, 2);
                                this.Check(dados[0], "vSeg", drprod, ObOp.Opcional, 1, 15, 2);
                                this.Check(dados[0], "vDesc", drprod, ObOp.Opcional, 1, 15, 2);
                                this.Check(dados[0], "vOutro", drprod, ObOp.Opcional, 1, 15, 2);
                            }
                        }
                        break;
                        #endregion

                    case "I18":
                        #region -- I18
                        {
                            DataRow drDI = dsNfe.Tables["DI"].NewRow();
                            if (this.PopulateDataRow(drDI, dados, 6))
                            {
                                ++DIid;
                                drDI["prod_Id"] = idprod.ToString();
                                drDI["DI_Id"] = DIid.ToString();
                                dsNfe.Tables["DI"].Rows.Add(drDI);

                                this.Check(dados[0], "nDI", drDI, ObOp.Obrigatorio, 1, 10);
                                this.Check(dados[0], "dDI", drDI, ObOp.Obrigatorio);
                                this.Check(dados[0], "xLocDesemb", drDI, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "UFDesemb", drDI, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "dDesemb", drDI, ObOp.Obrigatorio);
                                this.Check(dados[0], "cExportador", drDI, ObOp.Obrigatorio, 1, 60);
                            }
                        }
                        break;
                        #endregion

                    case "I25":
                        #region -- I25
                        {
                            DataRow dradi = dsNfe.Tables["adi"].NewRow();
                            if (this.PopulateDataRow(dradi, dados, 4))
                            {
                                dradi["DI_Id"] = DIid.ToString();
                                dsNfe.Tables["adi"].Rows.Add(dradi);

                                this.Check(dados[0], "nAdicao", dradi, ObOp.Obrigatorio, 1, 3);
                                this.Check(dados[0], "nSeqAdic", dradi, ObOp.Obrigatorio, 1, 3);
                                this.Check(dados[0], "cFabricante", dradi, ObOp.Obrigatorio, 1, 60);
                                this.Check(dados[0], "vDescDI", dradi, ObOp.Opcional, 1, 15, 2);
                            }
                        }
                        break;
                        #endregion

                    case "J":
                        #region -- J
                        {
                            DataRow drveicProd = dsNfe.Tables["veicProd"].NewRow();
                            if (this.PopulateDataRow(drveicProd, dados, 24))
                            {
                                drveicProd["prod_id"] = idprod;
                                dsNfe.Tables["veicProd"].Rows.Add(drveicProd);

                                this.Check(dados[0], "tpOp", drveicProd, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "chassi", drveicProd, ObOp.Obrigatorio, 1, 17);
                                this.Check(dados[0], "cCor", drveicProd, ObOp.Obrigatorio, 1, 4);
                                this.Check(dados[0], "xCor", drveicProd, ObOp.Obrigatorio, 1, 40);
                                this.Check(dados[0], "pot", drveicProd, ObOp.Obrigatorio, 1, 4);
                                this.Check(dados[0], "cilin", drveicProd, ObOp.Obrigatorio, 1, 4);
                                this.Check(dados[0], "pesoL", drveicProd, ObOp.Obrigatorio, 1, 9);
                                this.Check(dados[0], "pesoB", drveicProd, ObOp.Obrigatorio, 1, 9);
                                this.Check(dados[0], "nSerie", drveicProd, ObOp.Obrigatorio, 1, 9);
                                this.Check(dados[0], "tpComb", drveicProd, ObOp.Obrigatorio, 1, 8);
                                this.Check(dados[0], "nMotor", drveicProd, ObOp.Obrigatorio, 1, 21);
                                this.Check(dados[0], "CMT", drveicProd, ObOp.Obrigatorio, 1, 9);
                                this.Check(dados[0], "dist", drveicProd, ObOp.Obrigatorio, 1, 4);
                                //this.Check(dados[0], "RENAVAM", drveicProd, ObOp.Opcional, 1, 9);
                                this.Check(dados[0], "anoMod", drveicProd, ObOp.Obrigatorio, 4, 4);
                                this.Check(dados[0], "anoFab", drveicProd, ObOp.Obrigatorio, 4, 4);
                                this.Check(dados[0], "tpPint", drveicProd, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "tpVeic", drveicProd, ObOp.Obrigatorio, 1, 2);
                                this.Check(dados[0], "espVeic", drveicProd, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "VIN", drveicProd, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "condVeic", drveicProd, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "cMod", drveicProd, ObOp.Obrigatorio, 1, 6);
                                this.Check(dados[0], "cCorDENATRAN", drveicProd, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "lota", drveicProd, ObOp.Obrigatorio, 1, 3);
                                this.Check(dados[0], "tpRest", drveicProd, ObOp.Obrigatorio, 1, 1);
                            }
                        }
                        break;
                        #endregion

                    case "K":   //K - Detalhamento Específico de Medicamento
                        #region -- K
                        {
                            DataRow drmed = dsNfe.Tables["med"].NewRow();

                            if (this.PopulateDataRow(drmed, dados, 5))
                            {
                                drmed["prod_Id"] = idprod;
                                dsNfe.Tables["med"].Rows.Add(drmed);

                                this.Check(dados[0], "nLote", drmed, ObOp.Obrigatorio, 20, 20);
                                this.Check(dados[0], "qLote", drmed, ObOp.Obrigatorio, 1, 11, 3);
                                this.Check(dados[0], "dFab", drmed, ObOp.Obrigatorio);
                                this.Check(dados[0], "dVal", drmed, ObOp.Obrigatorio);
                                this.Check(dados[0], "vPMC", drmed, ObOp.Obrigatorio, 1, 15, 2);
                            }
                        }
                        break;
                        #endregion

                    case "L":   //L - Detalhamento Específico de Armamentos
                        #region -- L
                        {
                            DataRow drarma = dsNfe.Tables["arma"].NewRow();
                            if (this.PopulateDataRow(drarma, dados, 4))
                            {
                                drarma["prod_Id"] = idprod;
                                dsNfe.Tables["arma"].Rows.Add(drarma);

                                this.Check(dados[0], "tpArma", drarma, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "nSerie", drarma, ObOp.Obrigatorio, 1, 9);
                                this.Check(dados[0], "nCano", drarma, ObOp.Obrigatorio, 1, 9);
                                this.Check(dados[0], "descr", drarma, ObOp.Obrigatorio, 1, 256);
                            }
                        }
                        break;
                        #endregion

                    case "L01":  //Combustível - Informar apenas para operações com combustíveis líquidos.
                    case "L1":
                        #region -- L01 ou L1
                        {
                            DataRow drcomb = dsNfe.Tables["comb"].NewRow();
                            if (this.PopulateDataRow(drcomb, dados, 4))
                            {
                                ++idcomb;
                                drcomb["prod_Id"] = idprod;
                                drcomb["comb_Id"] = idcomb.ToString();
                                dsNfe.Tables["comb"].Rows.Add(drcomb);

                                this.Check(dados[0], "cProdANP", drcomb, ObOp.Opcional, 9, 9);
                                this.Check(dados[0], "CODIF", drcomb, ObOp.Opcional, 0, 21);
                                this.Check(dados[0], "qTemp", drcomb, ObOp.Opcional, 1, 16, 4);
                                this.Check(dados[0], "UFCons", drcomb, ObOp.Opcional, 0, 2);
                            }
                        }
                        break;
                        #endregion

                    case "L05": //CIDE
                    case "L105":
                        #region -- L05 ou L105
                        {
                            DataRow drCIDE = dsNfe.Tables["CIDE"].NewRow();
                            if (this.PopulateDataRow(drCIDE, dados, 3))
                            {
                                drCIDE["comb_Id"] = idcomb.ToString();
                                dsNfe.Tables["CIDE"].Rows.Add(drCIDE);

                                this.Check(dados[0], "qBCprod", drCIDE, ObOp.Obrigatorio, 1, 16, 4);
                                this.Check(dados[0], "vAliqProd", drCIDE, ObOp.Obrigatorio, 1, 15, 4);
                                this.Check(dados[0], "vCIDE", drCIDE, ObOp.Obrigatorio, 1, 15, 2);
                            }
                        }
                        break;
                        #endregion

                    case "L09": //ICMS próprio e ST retido
                    case "L109":
                        #region -- L09 ou L109
                        {
                            DataRow drICMSComb = dsNfe.Tables["ICMSComb"].NewRow();
                            if (this.PopulateDataRow(drICMSComb, dados, 4))
                            {
                                drICMSComb["comb_Id"] = idcomb.ToString();
                                dsNfe.Tables["ICMSComb"].Rows.Add(drICMSComb);

                                this.Check(dados[0], "vBCICMS", drICMSComb, ObOp.Obrigatorio, 1, 15, 2);
                                this.Check(dados[0], "vICMS", drICMSComb, ObOp.Obrigatorio, 1, 15, 2);
                                this.Check(dados[0], "vBCICMSST", drICMSComb, ObOp.Obrigatorio, 1, 15, 2);
                                this.Check(dados[0], "vICMSST", drICMSComb, ObOp.Obrigatorio, 1, 15, 2);
                            }
                        }
                        break;
                        #endregion

                    case "L114":    //ICMS para UF devido para a UF de destino, nas operações interestaduais 
                        //de produtos que tiveram retenção antecipada de ICMS por ST para a UF 
                        //do remetente
                        #region -- L114
                        {
                            DataRow drICMSInter = dsNfe.Tables["ICMSInter"].NewRow();
                            if (this.PopulateDataRow(drICMSInter, dados, 2))
                            {
                                drICMSInter["comb_Id"] = idcomb.ToString();
                                dsNfe.Tables["ICMSInter"].Rows.Add(drICMSInter);

                                this.Check(dados[0], "vBCICMSSTDest", drICMSInter, ObOp.Obrigatorio, 1, 15, 2);
                                this.Check(dados[0], "vICMSSTDest", drICMSInter, ObOp.Obrigatorio, 1, 15, 2);
                            }
                        }
                        break;
                        #endregion

                    case "L117":    //ICMS para consumo em UF diversa - informar quando o produto for adquirido 
                        //para consumo em UF diversa da UF de localização do estabelecimento do 
                        //destinatário da nota fiscal
                        #region -- L117
                        {
                            DataRow drICMSCons = dsNfe.Tables["ICMSCons"].NewRow();
                            if (this.PopulateDataRow(drICMSCons, dados, 3))
                            {
                                drICMSCons["comb_Id"] = idcomb.ToString();
                                dsNfe.Tables["ICMSCons"].Rows.Add(drICMSCons);

                                this.Check(dados[0], "vBCICMSSTCons", drICMSCons, ObOp.Obrigatorio, 1, 15, 2);
                                this.Check(dados[0], "vICMSSTCons", drICMSCons, ObOp.Obrigatorio, 1, 15, 2);
                                this.Check(dados[0], "UFcons", drICMSCons, ObOp.Obrigatorio, 2, 2);
                            }
                        }
                        break;
                        #endregion

                    case "M":
                        #region -- M
                        {
                            //danasa 27-2-2011
                            if (idprod != "")
                            {
                                DataRow dr = dsNfe.Tables["imposto"].NewRow();
                                dr["imposto_Id"] = idprod.ToString();
                                dr["det_Id"] = idprod.ToString();

                                dsNfe.Tables["imposto"].Rows.Add(dr);
                            }
                            else
                                cMensagemErro += "Segmento [M] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                        }
                        break;
                        #endregion

                    case "N":   //N - ICMS Normal e ST
                        #region -- N
                        {
                            if (idprod != "")
                            {
                                //danasa 27-2-2011
                                DataRow dr;
                                if (dsNfe.Tables["imposto"].Rows.Count == 0)
                                {
                                    dr = dsNfe.Tables["imposto"].NewRow();
                                    dr["imposto_Id"] = idprod.ToString();
                                    dr["det_Id"] = idprod.ToString();
                                    dsNfe.Tables["imposto"].Rows.Add(dr);
                                }
                                dr = dsNfe.Tables["ICMS"].NewRow();
                                dr["ICMS_Id"] = idprod.ToString();
                                dr["imposto_Id"] = idprod.ToString();
                                dsNfe.Tables["ICMS"].Rows.Add(dr);
                            }
                            else
                                cMensagemErro += "Segmento [N] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                        }
                        break;
                        #endregion

                    case "N02": //CST – 00 – Tributada integralmente
                        #region -- N02
                        {
                            DataRow dr = dsNfe.Tables["ICMS00"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 6))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [N02] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["ICMS_Id"] = idprod.ToString();
                                dsNfe.Tables["ICMS00"].Rows.Add(dr);

                                this.Check(dados[0], "orig", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "CST", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "modBC", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "vBC", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "pICMS", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vICMS", dr, ObOp.Obrigatorio, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "N03": //CST - 10 - Tributada e com cobrança do ICMS por substituição tributária
                        #region -- N03
                        {
                            DataRow dr = dsNfe.Tables["ICMS10"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 12))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [N03] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["ICMS_Id"] = idprod.ToString();
                                dsNfe.Tables["ICMS10"].Rows.Add(dr);

                                this.Check(dados[0], "orig", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "CST", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "modBC", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "vBC", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "pICMS", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vICMS", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "modBCST", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "pMVAST", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "pRedBCST", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "vBCST", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "pICMSST", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vICMSST", dr, ObOp.Obrigatorio, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "N04": //CST – 20 - Com redução de base de cálculo
                        #region -- N04
                        {
                            DataRow dr = dsNfe.Tables["ICMS20"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 7))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [N04] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["ICMS_Id"] = idprod.ToString();
                                dsNfe.Tables["ICMS20"].Rows.Add(dr);

                                this.Check(dados[0], "orig", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "CST", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "modBC", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "pRedBC", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vBC", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "pICMS", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vICMS", dr, ObOp.Obrigatorio, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "N05": //CST – 30 - Isenta ou não tributada e com cobrança do ICMS por 
                        //substituição tributária
                        #region -- N05
                        {
                            DataRow dr = dsNfe.Tables["ICMS30"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 8))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [N05] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["ICMS_Id"] = idprod.ToString();
                                dsNfe.Tables["ICMS30"].Rows.Add(dr);

                                this.Check(dados[0], "orig", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "CST", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "modBCST", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "pMVAST", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "pRedBCST", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "vBCST", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "pICMSST", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vICMSST", dr, ObOp.Obrigatorio, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "N06": //CST – 40 - Isenta, 41 - Não tributada e 50 - Suspensão
                        #region -- N06
                        {
                            DataRow dr = dsNfe.Tables["ICMS40"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 2))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [N06] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["ICMS_Id"] = idprod.ToString();
                                dsNfe.Tables["ICMS40"].Rows.Add(dr);

                                this.Check(dados[0], "orig", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "CST", dr, ObOp.Obrigatorio, 2, 2);
                            }
                        }
                        break;
                        #endregion

                    case "N07": //CST – 51 - Diferimento - A exigência do preenchimento das informações 
                        //do ICMS diferido fica à critério de cada UF.
                        #region -- N07
                        {
                            DataRow dr = dsNfe.Tables["ICMS51"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 7))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [N07] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["ICMS_Id"] = idprod.ToString();
                                dsNfe.Tables["ICMS51"].Rows.Add(dr);

                                this.Check(dados[0], "orig", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "CST", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "modBC", dr, ObOp.Opcional, 1, 1);
                                this.Check(dados[0], "pRedBC", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "vBC", dr, ObOp.Opcional, 1, 16, 2);
                                this.Check(dados[0], "pICMS", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "vICMS", dr, ObOp.Opcional, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "N08": //CST – 60 - ICMS cobrado anteriormente por substituição tributária
                        #region -- N08
                        {
                            DataRow dr = dsNfe.Tables["ICMS60"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 4))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [N08] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["ICMS_Id"] = idprod.ToString();
                                dsNfe.Tables["ICMS60"].Rows.Add(dr);

                                this.Check(dados[0], "orig", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "CST", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "vBCSTRet", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vICMSSTRet", dr, ObOp.Obrigatorio, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "N09": //CST - 70 - Com redução de base de cálculo e cobrança do ICMS por 
                        //substituição tributária
                        #region -- N09
                        {
                            DataRow dr = dsNfe.Tables["ICMS70"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 13))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [N09] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["ICMS_Id"] = idprod.ToString();
                                dsNfe.Tables["ICMS70"].Rows.Add(dr);
                                
                                this.Check(dados[0], "orig", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "CST", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "modBC", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "pRedBC", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vBC", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "pICMS", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vICMS", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "modBCST", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "pMVAST", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "pRedBCST", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "vBCST", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "pICMSST", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vICMSST", dr, ObOp.Obrigatorio, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "N10": //CST - 90 – Outros
                        #region -- N10
                        {
                            DataRow dr = dsNfe.Tables["ICMS90"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 13))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [N10] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["ICMS_Id"] = idprod.ToString();
                                dsNfe.Tables["ICMS90"].Rows.Add(dr);
                                
                                string o1 = dr["vBC"].ToString();
                                string o2 = dr["pRedBC"].ToString();
                                dr["vBC"] = o2;
                                dr["pRedBC"] = o1;

                                this.Check(dados[0], "orig", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "CST", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "modBC", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "pRedBC", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "vBC", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "pICMS", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vICMS", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "modBCST", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "pMVAST", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "pRedBCST", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "vBCST", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "pICMSST", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vICMSST", dr, ObOp.Obrigatorio, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    // simples nacional
                    case "N10C": //CST – 101 
                        //simples com aproveitamento de credito
                        #region -- N10c
                        {
                            DataRow dr = dsNfe.Tables["ICMSSN101"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 5))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [N10c] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["ICMS_Id"] = idprod.ToString();
                                dsNfe.Tables["ICMSSN101"].Rows.Add(dr);

                                this.Check(dados[0], "orig", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "CSOSN", dr, ObOp.Obrigatorio, 3, 3);
                                this.Check(dados[0], "pCredSN", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vCredICMSSN", dr, ObOp.Obrigatorio, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion
                    case "N10D": //CST – 102 
                        //simples sem aproveitamento de credito
                        #region -- N10d

                        {
                            string _CSOSN = dados[2];
                            DataRow dr = dsNfe.Tables["ICMSSN102"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 3))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [N10d] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["ICMS_Id"] = idprod.ToString();
                                dsNfe.Tables["ICMSSN102"].Rows.Add(dr);

                                this.Check(dados[0], "orig", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "CSOSN", dr, ObOp.Obrigatorio, 3, 3);
                            }
                        }
                        break;
                        #endregion

                    case "N10E": //CST – 201
                        //simples com aproveitamento de credito st
                        #region -- N10e
                        {
                            DataRow dr = dsNfe.Tables["ICMSSN201"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 11))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [N10e] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["ICMS_Id"] = idprod.ToString();
                                dsNfe.Tables["ICMSSN201"].Rows.Add(dr);

                                this.Check(dados[0], "orig", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "CSOSN", dr, ObOp.Obrigatorio, 3, 3);
                                this.Check(dados[0], "modBCST", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "pMVAST", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "pRedBCST", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "vBCST", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "pICMSST", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vICMSST", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "pCredSN", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vCredICMSSN", dr, ObOp.Obrigatorio, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "N10F": //CST – 202
                        //simples sem aproveitamento de credito st
                        #region -- N10f
                        {
                            DataRow dr = dsNfe.Tables["ICMSSN202"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 9))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [N10f] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["ICMS_Id"] = idprod.ToString();
                                dsNfe.Tables["ICMSSN202"].Rows.Add(dr);

                                this.Check(dados[0], "orig", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "CSOSN", dr, ObOp.Obrigatorio, 3, 3);
                                this.Check(dados[0], "modBCST", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "pMVAST", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "pRedBCST", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "vBCST", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "pICMSST", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vICMSST", dr, ObOp.Obrigatorio, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "N10G": //CST – 500 
                        //simples st cst -> [60]
                        #region -- N10g
                        {
                            DataRow dr = dsNfe.Tables["ICMSSN500"].NewRow();

                            int tamanho = dados.Length;
                            // Layout da Receita e o Emissor gratuito estao errados nao existe o campo modBCST no Layout
                            /*if (tamanho == 7)
                            {
                                string[] dados2 = new string[5];
                                dados2[0] = dados[0];
                                dados2[1] = dados[1];
                                dados2[2] = dados[2];
                                dados2[3] = dados[4];
                                dados2[4] = dados[5];
                                //dos2[0] = dados[0];
                                dados = null;
                                dados = dados2;
                            }*/
                            //N10g| Orig | CSOSN | modBCST | vBCSTRet | vICMSSTRet |
                            //N10g|0     |500    |         |0.00      |0.0|
                            dr["orig"] = dados[1];
                            dr["CSOSN"] = dados[2];
                            if (nElementos > 3) dr["vBCSTRet"] = dados[4];
                            if (nElementos > 4) dr["vICMSSTRet"] = dados[5];

                            //if (this.PopulateDataRow(dr, dados, 5))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [N10g] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["ICMS_Id"] = idprod.ToString();
                                dsNfe.Tables["ICMSSN500"].Rows.Add(dr);

                                this.Check(dados[0], "orig", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "CSOSN", dr, ObOp.Obrigatorio, 3, 3);
                                this.Check(dados[0], "vBCSTRet", dr, ObOp.Obrigatorio, 1, 15, 2);
                                this.Check(dados[0], "vICMSSTRet", dr, ObOp.Obrigatorio, 1, 15, 2);
                            }
                        }
                        break;
                        #endregion

                    case "N10H": //CST – 900
                        //simples st cst -> [60]
                        #region -- N10H
                        {
                            DataRow dr = dsNfe.Tables["ICMSSN900"].NewRow();

                            if (this.PopulateDataRow(dr, dados, 16))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [N10h] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["ICMS_Id"] = idprod.ToString();
                                dsNfe.Tables["ICMSSN900"].Rows.Add(dr);

                                this.Check(dados[0], "orig", dr, ObOp.Obrigatorio, 1, 1);
                                this.Check(dados[0], "CSOSN", dr, ObOp.Obrigatorio, 3, 3);

                                this.Check(dados[0], "modBC", dr, ObOp.Opcional, 1, 1);
                                this.Check(dados[0], "vBC", dr, ObOp.Opcional, 1, 16, 2);
                                this.Check(dados[0], "pRedBC", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "pICMS", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "vICMS", dr, ObOp.Opcional, 1, 16, 2);

                                this.Check(dados[0], "modBCST", dr, ObOp.Opcional, 1, 1);
                                this.Check(dados[0], "pMVAST", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "pRedBCST", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "vBCST", dr, ObOp.Opcional, 1, 16, 2);
                                this.Check(dados[0], "pICMSST", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "vICMSST", dr, ObOp.Opcional, 1, 16, 2);

                                this.Check(dados[0], "pCredSN", dr, ObOp.Opcional, 1, 6, 2);
                                this.Check(dados[0], "vCredICMSSN", dr, ObOp.Opcional, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "O": //IPI - Informar apenas quando o item for sujeito ao IPI
                        #region -- O
                        {
                            DataRow dr = dsNfe.Tables["IPI"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 5))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [O] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["IPI_Id"] = idprod.ToString();
                                dr["imposto_Id"] = idprod.ToString();
                                dsNfe.Tables["IPI"].Rows.Add(dr);

                                this.Check(dados[0], "clEnq", dr, ObOp.Opcional, 5, 5);
                                this.Check(dados[0], "CNPJProd", dr, ObOp.Opcional, 14, 14);
                                this.Check(dados[0], "cSelo", dr, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "qSelo", dr, ObOp.Opcional, 1, 12);
                                this.Check(dados[0], "cEnq", dr, ObOp.Obrigatorio, 3, 3);
                            }
                        }
                        break;
                        #endregion

                    case "O07":
                    case "O10":
                    case "O11":  //IPI Tributável
                        #region -- O07, O08 e O11
                        {
                            if (dados[0] == "O07")
                            {
                                drIPITrib = dsNfe.Tables["IPITrib"].NewRow();
                                if (nElementos >= 1)
                                    if (dados[1].Trim() != "")
                                        drIPITrib["CST"] = dados[1].Trim();
                                if (nElementos >= 2)
                                    if (dados[2].Trim() != "")
                                        drIPITrib["vIPI"] = dados[2].Trim();

                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [O07] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                this.Check(dados[0], "CST", drIPITrib, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "vIPI", drIPITrib, ObOp.Obrigatorio, 1, 16, 2);
                            }
                            if (dados[0] == "O10")
                            {
                                if (nElementos >= 1)
                                    if (dados[1].Trim() != "")
                                        drIPITrib["vBC"] = dados[1].Trim();
                                if (nElementos >= 2)
                                    if (dados[2].Trim() != "")
                                        drIPITrib["pIPI"] = dados[2].Trim();

                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [O10] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                this.Check(dados[0], "vBC", drIPITrib, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "pIPI", drIPITrib, ObOp.Obrigatorio, 1, 6, 2);
                            }
                            if (dados[0] == "O11")
                            {
                                if (nElementos >= 1)
                                    if (dados[1].Trim() != "")
                                        drIPITrib["vUnid"] = dados[1].Trim();
                                if (nElementos >= 2)
                                    if (dados[2].Trim() != "")
                                        drIPITrib["qUnid"] = dados[2].Trim();

                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [O11] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                this.Check(dados[0], "vUnid", drIPITrib, ObOp.Obrigatorio, 1, 16, 4);
                                this.Check(dados[0], "qUnid", drIPITrib, ObOp.Obrigatorio, 1, 17, 4);
                            }
                            drIPITrib["IPI_Id"] = idprod.ToString();
                            if (dados[0] != "O07")
                                dsNfe.Tables["IPITrib"].Rows.Add(drIPITrib);
                        }
                        break;
                        #endregion

                    case "O08": //IPI Não Tributável
                        #region -- O08
                        {
                            if (idprod == "")
                            {
                                cMensagemErro += "Segmento [O08] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                            }
                            DataRow dr = dsNfe.Tables["IPINT"].NewRow();
                            if (nElementos >= 1)
                                if (dados[1].Trim() != "")
                                    dr["CST"] = dados[1].Trim();
                            dr["IPI_Id"] = idprod.ToString();
                            dsNfe.Tables["IPINT"].Rows.Add(dr);

                            this.Check(dados[0], "CST", dr, ObOp.Obrigatorio, 2, 2);
                        }
                        break;
                        #endregion

                    case "P": //II - Informar apenas quando o item for sujeito ao II
                        #region -- P
                        {
                            DataRow dr = dsNfe.Tables["II"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 4))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [P] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["imposto_Id"] = idprod.ToString();
                                dsNfe.Tables["II"].Rows.Add(dr);

                                this.Check(dados[0], "vBC", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vDespAdu", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vII", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vIOF", dr, ObOp.Obrigatorio, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "Q": //II
                        #region -- Q
                        {
                            if (idprod == "")
                            {
                                cMensagemErro += "Segmento [Q] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                            }
                            DataRow dr = dsNfe.Tables["PIS"].NewRow();
                            dr["PIS_Id"] = idprod.ToString();
                            dr["imposto_Id"] = idprod.ToString();
                            dsNfe.Tables["PIS"].Rows.Add(dr);
                        }
                        break;
                        #endregion

                    case "Q02": //PIS - grupo de PIS tributado pela alíquota
                        #region -- Q02
                        {
                            DataRow dr = dsNfe.Tables["PISAliq"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 4))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [Q02] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["PIS_Id"] = idprod.ToString();
                                dsNfe.Tables["PISAliq"].Rows.Add(dr);

                                this.Check(dados[0], "CST", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "vBC", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "pPIS", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vPIS", dr, ObOp.Obrigatorio, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "Q03": //PIS - grupo de PIS tributado por Qtde
                        #region -- Q03
                        {
                            DataRow dr = dsNfe.Tables["PISQtde"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 4))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [Q03] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["PIS_Id"] = idprod.ToString();
                                dsNfe.Tables["PISQtde"].Rows.Add(dr);

                                this.Check(dados[0], "CST", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "qBCProd", dr, ObOp.Obrigatorio, 1, 17, 4);
                                this.Check(dados[0], "vAliqProd", dr, ObOp.Obrigatorio, 1, 16, 4);
                                this.Check(dados[0], "vPIS", dr, ObOp.Obrigatorio, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "Q04": //PIS - grupo de PIS não tributado
                        #region -- Q04
                        {
                            if (idprod == "")
                            {
                                cMensagemErro += "Segmento [Q04] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                            }
                            DataRow dr = dsNfe.Tables["PISNT"].NewRow();
                            if (nElementos >= 1)
                                if (dados[1].Trim() != "")
                                    dr["CST"] = dados[1].Trim();
                            dr["PIS_Id"] = idprod.ToString();
                            dsNfe.Tables["PISNT"].Rows.Add(dr);

                            this.Check(dados[0], "CST", dr, ObOp.Obrigatorio, 2, 2);
                        }
                        break;
                        #endregion

                    case "Q05": //PIS - grupo de PIS Outras Operações
                        #region -- Q05
                        if (idprod == "")
                        {
                            cMensagemErro += "Segmento [Q05] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                        }
                        drPISOutr = dsNfe.Tables["PISOutr"].NewRow();
                        if (nElementos >= 1)
                            if (dados[1].Trim() != "")
                                drPISOutr["CST"] = dados[1].Trim();
                        if (nElementos >= 2)
                            if (dados[2].Trim() != "")
                                drPISOutr["vPIS"] = dados[2].Trim();
                        drPISOutr["PIS_Id"] = idprod.ToString();
                        dsNfe.Tables["PISOutr"].Rows.Add(drPISOutr);

                        this.Check(dados[0], "CST", drPISOutr, ObOp.Obrigatorio, 2, 2);
                        this.Check(dados[0], "vPIS", drPISOutr, ObOp.Obrigatorio, 1, 16, 2);
                        break;
                        #endregion

                    case "Q07":
                        #region -- Q07
                        if (nElementos >= 1)
                            if (dados[1].Trim() != "")
                                drPISOutr["vBC"] = dados[1].Trim();
                        if (nElementos >= 2)
                            if (dados[2].Trim() != "")
                                drPISOutr["pPIS"] = dados[2].Trim();

                        this.Check(dados[0], "vBC", drPISOutr, ObOp.Obrigatorio, 1, 16, 2);
                        this.Check(dados[0], "pPIS", drPISOutr, ObOp.Obrigatorio, 1, 6, 2);
                        break;
                        #endregion

                    case "Q10":
                        #region -- Q10
                        if (nElementos >= 1)
                            if (dados[1].Trim() != "")
                                drPISOutr["qBCProd"] = dados[1];
                        if (nElementos >= 2)
                            if (dados[2].Trim() != "")
                                drPISOutr["vAliqProd"] = dados[2];

                        this.Check(dados[0], "qBCProd", drPISOutr, ObOp.Obrigatorio, 1, 17, 4);
                        this.Check(dados[0], "vAliqProd", drPISOutr, ObOp.Obrigatorio, 1, 16, 4);
                        break;
                        #endregion

                    case "R":   //PIS Substituição Tributária
                        #region -- R
                        drPISST = dsNfe.Tables["PISST"].NewRow();
                        if (nElementos >= 1)
                            if (dados[1].Trim() != "")
                                drPISST[4] = dados[1].Trim(); //vPIS

                        this.Check(dados[0], "vPIS", drPISST, ObOp.Obrigatorio, 1, 16, 2);
                        break;
                        #endregion

                    case "R02":
                        #region -- R02
                        if (nElementos >= 1)
                            if (dados[1].Trim() != "")
                                drPISST[0] = dados[1].Trim(); //VBC
                        if (nElementos >= 2)
                            if (dados[2].Trim() != "")
                                drPISST[1] = dados[2].Trim(); //pPIS

                        this.Check(dados[0], "vBC", drPISST, ObOp.Obrigatorio, 1, 16, 2);
                        this.Check(dados[0], "pPIS", drPISST, ObOp.Obrigatorio, 1, 6, 2);
                        break;
                        #endregion

                    case "R04":
                        #region -- R04
                        if (nElementos >= 1)
                            if (dados[1].Trim() != "")
                                drPISST[2] = dados[1].Trim(); //qBCProd
                        if (nElementos >= 2)
                            if (dados[2].Trim() != "")
                                drPISST[3] = dados[2].Trim(); //vAliqProd

                        this.Check(dados[0], "qBCProd", drPISST, ObOp.Obrigatorio, 1, 17, 4);
                        this.Check(dados[0], "vAliqProd", drPISST, ObOp.Obrigatorio, 1, 16, 4);
                        break;
                        #endregion

                    case "S": //cofins
                        #region -- S
                        {
                            if (idprod == "")
                            {
                                cMensagemErro += "Segmento [S] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                            }
                            DataRow drCOFINS = dsNfe.Tables["COFINS"].NewRow();
                            drCOFINS["COFINS_Id"] = idprod.ToString();
                            drCOFINS["imposto_Id"] = idprod.ToString();
                            dsNfe.Tables["COFINS"].Rows.Add(drCOFINS);
                        }
                        break;
                        #endregion

                    case "S02": //COFINS - grupo de COFINS tributado pela alíquota
                        #region -- S02
                        {
                            DataRow dr = dsNfe.Tables["COFINSAliq"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 4))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [S02] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["COFINS_Id"] = idprod.ToString();
                                dsNfe.Tables["COFINSAliq"].Rows.Add(dr);

                                this.Check(dados[0], "CST", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "vBC", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "pCOFINS", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vCOFINS", dr, ObOp.Obrigatorio, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "S03": //COFINS - grupo de COFINS tributado por Qtde
                        #region -- S03
                        {
                            DataRow dr = dsNfe.Tables["COFINSQtde"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 4))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [S03] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["COFINS_Id"] = idprod.ToString();    //danasa 27-9-2009
                                dsNfe.Tables["COFINSQtde"].Rows.Add(dr);

                                this.Check(dados[0], "CST", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "qBCProd", dr, ObOp.Obrigatorio, 1, 17, 4);
                                this.Check(dados[0], "vAliqProd", dr, ObOp.Obrigatorio, 1, 16, 4);
                                this.Check(dados[0], "vCOFINS", dr, ObOp.Obrigatorio, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "S04": //COFINS - grupo de COFINS não tributado
                        #region -- S04
                        {
                            if (idprod == "")
                            {
                                cMensagemErro += "Segmento [S04] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                            }
                            DataRow dr = dsNfe.Tables["COFINSNT"].NewRow();
                            if (nElementos >= 1)
                                if (dados[1].Trim() != "")
                                    dr["CST"] = dados[1].Trim();
                            dr["COFINS_Id"] = idprod.ToString();
                            dsNfe.Tables["COFINSNT"].Rows.Add(dr);

                            this.Check(dados[0], "CST", dr, ObOp.Obrigatorio, 2, 2);
                        }
                        break;
                        #endregion

                    case "S05": //COFINS - grupo de COFINS Outras Operações
                        #region -- S05
                        if (idprod == "")
                        {
                            cMensagemErro += "Segmento [S05] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                        }
                        drCOFINSOutr = dsNfe.Tables["COFINSOutr"].NewRow();
                        if (nElementos >= 1)
                            if (dados[1].Trim() != "")
                                drCOFINSOutr["CST"] = dados[1].Trim();
                        if (nElementos >= 2)
                            if (dados[2].Trim() != "")
                                drCOFINSOutr["vCOFINS"] = dados[2].Trim();
                        // drCOFINSOutr["qBCProd"] = null;
                        drCOFINSOutr["COFINS_Id"] = idprod.ToString();

                        this.Check(dados[0], "CST", drCOFINSOutr, ObOp.Obrigatorio, 2, 2);
                        this.Check(dados[0], "vCOFINS", drCOFINSOutr, ObOp.Obrigatorio, 1, 16, 2);
                        break;
                        #endregion

                    case "S07": //COFINS - grupo de COFINS Outras Operações
                        #region -- S07
                        if (nElementos >= 1)
                            if (dados[1].Trim() != "")
                                drCOFINSOutr["vBC"] = dados[1].Trim();
                        if (nElementos >= 2)
                            if (dados[2].Trim() != "")
                                drCOFINSOutr["pCOFINS"] = dados[2].Trim();
                        dsNfe.Tables["COFINSOutr"].Rows.Add(drCOFINSOutr); //executa  o Add, porque sempre tera o S07 ou S09

                        this.Check(dados[0], "vBC", drCOFINSOutr, ObOp.Obrigatorio, 1, 16, 2);
                        this.Check(dados[0], "pCOFINS", drCOFINSOutr, ObOp.Obrigatorio, 1, 6, 2);
                        break;
                        #endregion

                    case "S09": //COFINS - grupo de COFINS Outras Operações
                        #region -- S09
                        if (nElementos >= 1)
                            if (dados[1].Trim() != "")
                                drCOFINSOutr["qBCProd"] = dados[1].Trim();
                        if (nElementos >= 2)
                            if (dados[2].Trim() != "")
                                drCOFINSOutr["vAliqProd"] = dados[2].Trim();
                        dsNfe.Tables["COFINSOutr"].Rows.Add(drCOFINSOutr); //executa  o Add, porque sempre tera o S07 ou S09

                        this.Check(dados[0], "qBCProd", drCOFINSOutr, ObOp.Obrigatorio, 1, 17, 4);
                        this.Check(dados[0], "vAliqProd", drCOFINSOutr, ObOp.Obrigatorio, 1, 16, 4);
                        break;
                        #endregion

                    case "T": //COFINS Substituição Tributária
                        #region -- T
                        drCOFINSST = dsNfe.Tables["COFINSST"].NewRow();
                        if (nElementos >= 1)
                            if (dados[1].Trim() != "")
                                drCOFINSST["vCOFINS"] = dados[1].Trim();
                        drCOFINSST["imposto_Id"] = idprod.ToString();

                        this.Check(dados[0], "vCOFINS", drCOFINSST, ObOp.Obrigatorio, 1, 16, 2);
                        break;
                        #endregion

                    case "T02": //COFINS Substituição Tributária
                        #region -- T02

                        //danasa: tirado em 27-9-2009
                        //drCOFINSST["qBCProd"] = 0;
                        //drCOFINSST["vAliqProd"] = 0;
                        if (nElementos >= 1)
                            if (dados[1].Trim() != "")
                                drCOFINSST["vBC"] = dados[1].Trim();
                        if (nElementos >= 2)
                            if (dados[2].Trim() != "")
                                drCOFINSST["pCOFINS"] = dados[2].Trim();
                        dsNfe.Tables["COFINSST"].Rows.Add(drCOFINSST);

                        this.Check(dados[0], "vBC", drCOFINSST, ObOp.Obrigatorio, 1, 16, 2);
                        this.Check(dados[0], "pCOFINS", drCOFINSST, ObOp.Obrigatorio, 1, 6, 2);
                        break;
                        #endregion

                    case "T04": //COFINS Substituição Tributária
                        #region -- T04
                        if (nElementos >= 1)
                            if (dados[1].Trim() != "")
                                drCOFINSST["qBCProd"] = dados[1].Trim();
                        if (nElementos >= 2)
                            if (dados[2].Trim() != "")
                                drCOFINSST["vAliqProd"] = dados[2].Trim();

                        //danasa: tirado em 27-9-2009
                        //drCOFINSST["vBC"] = 0;
                        //drCOFINSST["pCOFINS"] = 0;
                        dsNfe.Tables["COFINSST"].Rows.Add(drCOFINSST);

                        this.Check(dados[0], "qBCProd", drCOFINSST, ObOp.Obrigatorio, 1, 17, 4);
                        this.Check(dados[0], "vAliqProd", drCOFINSST, ObOp.Obrigatorio, 1, 16, 4);
                        break;
                        #endregion

                    case "U":   //ISS - Informar os campos para cálculo do ISSQN nas NFe conjugadas, 
                        //onde há a prestação de serviços sujeitos ao ISSQN e fornecimento de 
                        //peças sujeitas ao ICMS
                        #region -- U
                        {
                            DataRow dr = dsNfe.Tables["ISSQN"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 6))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [U] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }

                                //Criar a tag Imposto se não informada a tag "M"
                                if (dsNfe.Tables["imposto"].Rows.Count == 0) //danasa 27-2-2011
                                {
                                    DataRow dr2 = dsNfe.Tables["imposto"].NewRow();
                                    dr2["imposto_Id"] = idprod.ToString();
                                    dr2["det_Id"] = idprod.ToString();
                                    dsNfe.Tables["imposto"].Rows.Add(dr2);
                                }
                                dr["imposto_Id"] = idprod;
                                dsNfe.Tables["ISSQN"].Rows.Add(dr);

                                this.Check(dados[0], "vBC", dr, ObOp.Obrigatorio, 1, 15, 2);
                                this.Check(dados[0], "vAliq", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vISSQN", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "cMunFG", dr, ObOp.Obrigatorio, 7, 7);
                                this.Check(dados[0], "cListServ", dr, ObOp.Obrigatorio, 3, 4);
                                this.Check(dados[0], "cSitTrib", dr, ObOp.Opcional,1,1);
                            }
                        }
                        break;
                        #endregion

                    case "W": //total
                        #region -- W
                        {
                            if (idprod == "")
                            {
                                cMensagemErro += "Segmento [W] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                            }
                            DataRow dr = dsNfe.Tables["total"].NewRow();
                            dr["total_Id"] = idprod.ToString();
                            dr["infNFe_Id"] = 0;
                            dsNfe.Tables["total"].Rows.Add(dr);
                        }
                        break;
                        #endregion

                    case "W02": //Grupo de Valores Totais referentes ao ICMS
                        #region -- W02
                        {
                            DataRow dr = dsNfe.Tables["ICMSTot"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 14))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [W02] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["total_Id"] = idprod;
                                dsNfe.Tables["ICMSTot"].Rows.Add(dr);

                                this.Check(dados[0], "vBC", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vICMS", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vBCST", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vST", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vProd", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vFrete", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vSeg", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vDesc", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vII", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vIPI", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vPIS", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vCOFINS", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vOutro", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vNF", dr, ObOp.Obrigatorio, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "W17": //Grupo de Valores Totais referentes ao ISSQN
                        #region -- W17
                        {
                            DataRow dr = dsNfe.Tables["ISSQNtot"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 5))
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [W17] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["total_Id"] = idprod.ToString();
                                dsNfe.Tables["ISSQNtot"].Rows.Add(dr);  //danasa 27-9-2009

                                this.Check(dados[0], "vServ", dr, ObOp.Opcional, 1, 16, 2);
                                this.Check(dados[0], "vBC", dr, ObOp.Opcional, 1, 16, 2);
                                this.Check(dados[0], "vISS", dr, ObOp.Opcional, 1, 16, 2);
                                this.Check(dados[0], "vPIS", dr, ObOp.Opcional, 1, 16, 2);
                                this.Check(dados[0], "vCOFINS", dr, ObOp.Opcional, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "W23": //Grupo de Retenções de Tributos
                        #region -- W23
                        {
                            DataRow dr = dsNfe.Tables["retTrib"].NewRow();
                            bool lEntrou = false;
                            for (iLeitura = 0; iLeitura <= Math.Min(nElementos, 7); iLeitura++)
                            {
                                if (iLeitura > 0 & dados[iLeitura] != null && dados[iLeitura].Trim() != "")
                                {
                                    dr[iLeitura - 1] = dados[iLeitura].Trim();
                                    lEntrou = true;
                                }
                            }
                            if (lEntrou == true)
                            {
                                if (idprod == "")
                                {
                                    cMensagemErro += "Segmento [W23] sem segmento [H]. Linha: " + iLinhaLida.ToString() + Environment.NewLine;
                                }
                                dr["total_Id"] = idprod.ToString();
                                dsNfe.Tables["retTrib"].Rows.Add(dr);

                                this.Check(dados[0], "vRetPIS", dr, ObOp.Opcional, 1, 16, 2);
                                this.Check(dados[0], "vRetCOFINS", dr, ObOp.Opcional, 1, 16, 2);
                                this.Check(dados[0], "vRetCSLL", dr, ObOp.Opcional, 1, 16, 2);
                                this.Check(dados[0], "vBCIRRF", dr, ObOp.Opcional, 1, 16, 2);
                                this.Check(dados[0], "vIRRF", dr, ObOp.Opcional, 1, 16, 2);
                                this.Check(dados[0], "vBCRetPrev", dr, ObOp.Opcional, 1, 16, 2);
                                this.Check(dados[0], "vRetPrev", dr, ObOp.Opcional, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "X": //transp
                        #region -- X
                        {
                            DataRow dr = dsNfe.Tables["transp"].NewRow();
                            if (nElementos >= 1)
                                if (dados[1].Trim() != "")
                                    dr["modFrete"] = dados[1].Trim();
                            dr["transp_Id"] = 0;
                            dr["infNFe_Id"] = 0;

                            dsNfe.Tables["transp"].Rows.Add(dr);

                            this.Check(dados[0], "modFrete", dr, ObOp.Obrigatorio, 1, 1);
                        }
                        break;
                        #endregion

                    case "X03": //transporta
                        #region -- X03
                        {
                            //         1       3   3         4    5
                            //X03 | XNome | IE | XEnder | UF | XMun |
                            string temp = "";
                            for (int ii = nElementos - 1; ii >= 1; --ii)
                                temp += dados[ii].Trim();

                            if (temp != "")
                            {
                                transpAdd = false;
                                drtransporta = dsNfe.Tables["transporta"].NewRow();

                                if (nElementos >= 1)
                                    if (dados[1].Trim() != "")
                                        drtransporta["xNome"] = dados[1].Trim();
                                if (nElementos >= 2)
                                    if (dados[2].Trim() != "")
                                        drtransporta["IE"] = dados[2].Trim();
                                if (nElementos >= 3)
                                    if (dados[3].Trim() != "")
                                        drtransporta["xEnder"] = dados[3].Trim();
                                if (nElementos >= 4)
                                    if (dados[4].Trim() != "")
                                        drtransporta["UF"] = dados[4].Trim();
                                if (nElementos >= 5)
                                    if (dados[5].Trim() != "")
                                        drtransporta["xMun"] = dados[5].Trim();

                                drtransporta["transp_Id"] = 0;

                                this.Check(dados[0], "xNome", drtransporta, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "IE", drtransporta, ObOp.Opcional, 2, 14);
                                this.Check(dados[0], "xEnder", drtransporta, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "UF", drtransporta, ObOp.Opcional, 2, 2);
                                this.Check(dados[0], "xMun", drtransporta, ObOp.Opcional, 1, 60);
                            }
                        }
                        break;
                        #endregion

                    case "X04":  //CNPJ|
                        #region -- X04
                        /* dsNfe.Tables["transporta"].Columns["CPF"].AllowDBNull = true; */
                        if (nElementos >= 1 && drtransporta != null)
                        {
                            if (dados[1].Trim() != "")
                                drtransporta["CNPJ"] = dados[1].Trim();
                            dsNfe.Tables["transporta"].Rows.Add(drtransporta);
                            transpAdd = true;

                            this.Check(dados[0], "CNPJ", drtransporta, ObOp.Opcional, 14, 14);
                        }
                        break;
                        #endregion

                    case "X05":  //CPF
                        #region --- X05
                        /* dsNfe.Tables["transporta"].Columns["CNPJ"].AllowDBNull = true; */
                        if (nElementos >= 1 && drtransporta != null)
                        {
                            if (dados[1].Trim() != "")
                                drtransporta["CPF"] = dados[1].Trim();
                            dsNfe.Tables["transporta"].Rows.Add(drtransporta);
                            transpAdd = true;

                            this.Check(dados[0], "CPF", drtransporta, ObOp.Opcional, 11, 11);
                        }
                        break;
                        #endregion

                    case "X11": //ICMS do serviço de transporte retido.
                        #region -- X11
                        {
                            DataRow dr = dsNfe.Tables["retTransp"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 7))
                            {
                                dr["transp_Id"] = 0;
                                dsNfe.Tables["retTransp"].Rows.Add(dr);

                                this.Check(dados[0], "vServ", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "vBCRet", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "pICMSRet", dr, ObOp.Obrigatorio, 1, 6, 2);
                                this.Check(dados[0], "vICMSRet", dr, ObOp.Obrigatorio, 1, 16, 2);
                                this.Check(dados[0], "CFOP", dr, ObOp.Obrigatorio, 4, 4);
                                this.Check(dados[0], "cMunFG", dr, ObOp.Obrigatorio, 7, 7);
                            }
                        }
                        break;
                        #endregion

                    case "X18": //veicTransp
                        #region -- X18
                        {
                            DataRow dr = dsNfe.Tables["veicTransp"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 3))
                            {
                                //dr["placa"] = dados[1];
                                //dr["UF"] = dados[2];
                                //dr["RNTC"] = dados[3];
                                dr["transp_Id"] = 0;
                                dsNfe.Tables["veicTransp"].Rows.Add(dr);

                                this.Check(dados[0], "placa", dr, ObOp.Obrigatorio, 1, 8);
                                this.Check(dados[0], "UF", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "RNTC", dr, ObOp.Opcional, 1, 20);
                            }
                        }
                        break;
                        #endregion

                    case "X22": //reboque
                        #region -- X22
                        {
                            DataRow dr = dsNfe.Tables["reboque"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 3))
                            {
                                //dr["placa"] = dados[1];
                                //dr["UF"] = dados[2];
                                //dr["RNTC"] = dados[3];
                                dr["transp_Id"] = 0;
                                dsNfe.Tables["reboque"].Rows.Add(dr);

                                this.Check(dados[0], "placa", dr, ObOp.Obrigatorio, 1, 8);
                                this.Check(dados[0], "UF", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "RNTC", dr, ObOp.Opcional, 1, 20);
                            }
                        }
                        break;
                        #endregion

                    case "X26": //vol
                        #region -- X26
                        {
                            drVol = dsNfe.Tables["vol"].NewRow();
                            if (this.PopulateDataRow(drVol, dados, 6))
                            {
                                ++volid;
                                drVol["vol_Id"] = volid.ToString();
                                drVol["transp_Id"] = 0;
                                dsNfe.Tables["vol"].Rows.Add(drVol);

                                this.Check(dados[0], "qVol", drVol, ObOp.Opcional, 1, 15);
                                this.Check(dados[0], "esp", drVol, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "marca", drVol, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "nVol", drVol, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "pesoL", drVol, ObOp.Opcional, 1, 16, 3);
                                this.Check(dados[0], "pesoB", drVol, ObOp.Opcional, 1, 16, 3);
                            }
                        }
                        break;

                        #endregion

                    case "X33": //lacres
                        #region -- X33
                        {
                            DataRow dr = dsNfe.Tables["lacres"].NewRow();
                            if (nElementos >= 1)
                                if (dados[1].Trim() != "")
                                    dr["nLacre"] = dados[1].Trim();
                            dr["vol_Id"] = volid.ToString();
                            dsNfe.Tables["lacres"].Rows.Add(dr);

                            this.Check(dados[0], "nLacre", dr, ObOp.Obrigatorio, 1, 60);
                        }
                        break;
                        #endregion

                    case "Y": //cobr
                        #region -- Y
                        {
                            DataRow dr = dsNfe.Tables["cobr"].NewRow();
                            dr["cobr_Id"] = 0;
                            dr["infNFe_Id"] = 0;
                            dsNfe.Tables["cobr"].Rows.Add(dr);
                        }
                        break;
                        #endregion

                    case "Y02": //fat
                        #region -- Y02
                        {
                            DataRow dr = dsNfe.Tables["fat"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 4))
                            {
                                //dr["nFat"] = dados[1];
                                //dr["vOrig"] = dados[2];
                                //dr["vDesc"] = dados[3];
                                //dr["vLiq"] = dados[4];

                                dr["cobr_Id"] = 0;
                                dsNfe.Tables["fat"].Rows.Add(dr);

                                this.Check(dados[0], "nFat", dr, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "vOrig", dr, ObOp.Opcional, 1, 16, 2);
                                this.Check(dados[0], "vDesc", dr, ObOp.Opcional, 1, 16, 2);
                                this.Check(dados[0], "vLiq", dr, ObOp.Opcional, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "Y07": //dup
                        #region -- Y07
                        {
                            DataRow dr = dsNfe.Tables["dup"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 3))
                            {
                                //dr["nDup"] = dados[1];
                                //dr["dVenc"] = dados[2];
                                //dr["vDup"] = dados[3];
                                dr["cobr_Id"] = 0;
                                dsNfe.Tables["dup"].Rows.Add(dr);

                                this.Check(dados[0], "nDup", dr, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "dVenc", dr, ObOp.Opcional);
                                this.Check(dados[0], "vDup", dr, ObOp.Opcional, 1, 16, 2);
                            }
                        }
                        break;
                        #endregion

                    case "Z":   //infAdic
                        #region -- Z
                        {
                            bool r = false;
                            DataRow dr = dsNfe.Tables["infAdic"].NewRow();
                            if (nElementos >= 1)
                                if (dados[1].Trim() != "")
                                {
                                    dr["infAdFisco"] = dados[1].Trim();
                                    r = true;
                                }
                            if (nElementos >= 2)
                                if (dados[2].Trim() != "")
                                {
                                    dr["infCpl"] = dados[2].Trim();
                                    r = true;
                                }

                            if (r)
                            {
                                ++indadicid;//danasa 27-9-2009;
                                dr["infAdic_Id"] = indadicid.ToString();
                                dr["infNFe_Id"] = 0;
                                dsNfe.Tables["infAdic"].Rows.Add(dr);

                                this.Check(dados[0], "infAdFisco", dr, ObOp.Opcional, 1, 2000);
                                this.Check(dados[0], "infCpl", dr, ObOp.Opcional, 1, 5000);

                                dsNfe.Tables["infAdic"].Columns["infAdFisco"].AllowDBNull = true;
                            }
                        }
                        break;

                        #endregion

                    case "Z04": //obsCont
                        #region -- Z04
                        {
                            if (nElementos > 0)
                            {
                                #region Criar a tag infAdic se necessário
                                // Se a tag infAdic ainda não foi criada por que não tem a infCpl e a infAdFisco no registro Z, vou forçar neste ponto pq a obsCont deve ficar dentro dela
                                // Wandrey 27/05/2010
                                if (indadicid == 0)
                                {
                                    DataRow drInfAdic = dsNfe.Tables["infAdic"].NewRow();

                                    ++indadicid;//danasa 27-9-2009;
                                    drInfAdic["infAdic_Id"] = indadicid.ToString();
                                    drInfAdic["infNFe_Id"] = 0;
                                    dsNfe.Tables["infAdic"].Rows.Add(drInfAdic);
                                }
                                #endregion

                                DataRow dr = dsNfe.Tables["obsCont"].NewRow();
                                if (this.PopulateDataRow(dr, dados, 2))
                                {
                                    dr["infAdic_Id"] = indadicid.ToString();
                                    dsNfe.Tables["obsCont"].Rows.Add(dr);

                                    this.Check(dados[0], "xCampo", dr, ObOp.Obrigatorio, 1, 20);
                                    this.Check(dados[0], "xTexto", dr, ObOp.Obrigatorio, 1, 60);
                                }
                            }
                        }
                        //dsNfe.Tables["obsFisco"]; não encontrei na documentação do TXT nada que fala sobre o conteudo dessa tebela
                        break;

                        #endregion

                    case "Z10": //procRef
                        #region -- Z10
                        {
                            if (nElementos > 0)
                            {
                                #region Criar a tag infAdic se necessário
                                // Se a tag infAdic ainda não foi criada por que não tem a infCpl, infAdFisco ou obsCont no registro Z ou Z04, 
                                // vou forçar neste ponto pq a procRef deve ficar dentro dela
                                // Wandrey 27/05/2010
                                if (indadicid == 0)
                                {
                                    DataRow drInfAdic = dsNfe.Tables["infAdic"].NewRow();

                                    ++indadicid;//danasa 27-9-2009;
                                    drInfAdic["infAdic_Id"] = indadicid.ToString();
                                    drInfAdic["infNFe_Id"] = 0;
                                    dsNfe.Tables["infAdic"].Rows.Add(drInfAdic);
                                }
                                #endregion

                                DataRow dr = dsNfe.Tables["procRef"].NewRow();
                                if (this.PopulateDataRow(dr, dados, 2))
                                {
                                    dr["infAdic_Id"] = indadicid.ToString();
                                    dsNfe.Tables["procRef"].Rows.Add(dr);

                                    this.Check(dados[0], "nProc", dr, ObOp.Obrigatorio, 1, 60);
                                    this.Check(dados[0], "indProc", dr, ObOp.Obrigatorio, 1, 1);
                                }
                            }
                        }
                        break;

                        #endregion

                    case "ZA": //EXPORTA
                        #region -- ZA
                        {
                            DataRow dr = dsNfe.Tables["exporta"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 2))
                            {
                                dr["infNFe_Id"] = 0;
                                dsNfe.Tables["exporta"].Rows.Add(dr);

                                this.Check(dados[0], "UFEmbarq", dr, ObOp.Obrigatorio, 2, 2);
                                this.Check(dados[0], "xLocEmbarq", dr, ObOp.Obrigatorio, 1, 60);
                            }
                        }
                        break;

                        #endregion

                    case "ZB": //compra
                        #region -- ZB
                        {
                            DataRow dr = dsNfe.Tables["compra"].NewRow();
                            if (this.PopulateDataRow(dr, dados, 3))
                            {
                                dr["infNFe_Id"] = 0;
                                dsNfe.Tables["compra"].Rows.Add(dr);

                                this.Check(dados[0], "xNEmp", dr, ObOp.Opcional, 1, 17);
                                this.Check(dados[0], "xPed", dr, ObOp.Opcional, 1, 60);
                                this.Check(dados[0], "xCont", dr, ObOp.Opcional, 1, 60);
                            }
                        }
                        break;

                        #endregion

                }
                #endregion

                if (vNovaNota)
                    break;

                cLinhaTXT = txt.ReadLine();
                iLinhaLida++;
            }
            if (cMensagemErro != "")
                return;

            //            MessageBox.Show("AAAAAAAA");

            if (cNF == 0)
            {
                ///
                /// gera codigo aleatorio
                /// 
                //Random x = new Random();
                //cNF = (int)((double)x.Next(1000));
                cNF = GerarCodigoNumerico(Convert.ToInt32(dsNfe.Tables["ide"].Rows[0]["nNF"].ToString()));
            }

            /// alterado jhs e samuel
            string _tpEmis = dsNfe.Tables["ide"].Rows[0]["tpEmis"].ToString();
            if (cDV == 0)
            {
                ///
                /// calcula do digito verificador
                /// 
                string ccChave = cChave + serie.ToString("000") + nNF.ToString("000000000") + _tpEmis + cNF.ToString("00000000");

                cDV = this.GerarDigito(ccChave);
            }
            cChave += serie.ToString("000") + nNF.ToString("000000000") + _tpEmis + cNF.ToString("00000000") + cDV.ToString("0");

            if (drtransporta != null && !transpAdd)
                dsNfe.Tables["transporta"].Rows.Add(drtransporta);
            dsNfe.Tables["ide"].Rows[0]["serie"] = serie;
            dsNfe.Tables["ide"].Rows[0]["nNF"] = nNF;
            // alterado de 9 digitos para 8 digitos jhs e samuel
            dsNfe.Tables["ide"].Rows[0]["cNF"] = cNF.ToString("00000000");
            dsNfe.Tables["ide"].Rows[0]["cDV"] = cDV.ToString("0");
            dsNfe.Tables["infNFe"].Rows[0]["Id"] = "NFe" + cChave;
            dsNfe.AcceptChanges();

            StringWriter TextoXml = new StringWriter();
            TextoXml.NewLine = "";

            dsNfe.WriteXml(TextoXml, XmlWriteMode.IgnoreSchema);

            string sAux = limpa_texto(TextoXml.ToString());

            //remove os espacos entre as tags
            TextoXml.GetStringBuilder().Remove(0, TextoXml.ToString().Length);
            TextoXml.GetStringBuilder().Append(sAux);

            //Ajustando a Tag de <NFref>
            if (TextoXml.ToString().IndexOf("<NFref>") > -1 && TextoXml.ToString().LastIndexOf("</NFref>") > -1)
            {
                sAux = TextoXml.ToString().Substring(TextoXml.ToString().IndexOf("<NFref>"), TextoXml.ToString().LastIndexOf("</NFref>") - TextoXml.ToString().IndexOf("<NFref>") + 8);
                //remove o texto que foi jogado para variavel
                TextoXml.GetStringBuilder().Remove(TextoXml.ToString().IndexOf("<NFref>"), TextoXml.ToString().LastIndexOf("</NFref>") - TextoXml.ToString().IndexOf("<NFref>") + 8);
                //insere o texto antes da tag tpImp
                TextoXml.GetStringBuilder().Replace("<tpImp>", sAux + "<tpImp>");
            }

            //movendo os dados do </infAdProd> para apos a tag imposto
            sAux = "";
            while (TextoXml.ToString().IndexOf("</infAdProd><prod>") > -1)
            {
                sAux = TextoXml.ToString().Substring(TextoXml.ToString().IndexOf("\"><infAdProd>") + 2, TextoXml.ToString().IndexOf("</infAdProd><prod>") - TextoXml.ToString().IndexOf("\"><infAdProd>") + 10);

                if (TextoXml.ToString().IndexOf("</imposto></det>") == -1)
                    throw new Exception("tag </imposto> não encontrada");

                if (TextoXml.ToString().IndexOf("\"><infAdProd>") == -1)
                    throw new Exception("tag <infAdProd> não encontrada");

                TextoXml.GetStringBuilder().Remove(TextoXml.ToString().IndexOf("\"><infAdProd>") + 2, sAux.Length);
                TextoXml.GetStringBuilder().Insert(TextoXml.ToString().IndexOf("</imposto></det>") + 10, sAux);
            }
            //Ajustando a tag IE do emitente 
            if (TextoXml.ToString().IndexOf("<xFant>") == -1)
                throw new Exception("tag <xFant> não encontrada");

            if (TextoXml.ToString().IndexOf("<enderEmit>") == -1)
                throw new Exception("tag <enderEmit> não encontada");

            sAux = TextoXml.ToString().Substring(TextoXml.ToString().IndexOf("<enderEmit>"), (TextoXml.ToString().IndexOf("</enderEmit>") - TextoXml.ToString().IndexOf("<enderEmit>")) + 12);
            TextoXml.GetStringBuilder().Replace(sAux, "").Replace("NewDataSet", "NFe"); //.Replace("\n","ç").Replace("\r","Í");
            TextoXml.GetStringBuilder().Replace("</xFant>", "</xFant>" + sAux);
            if (vTiraxFant)
            {
                ///
                /// danasa 8-2009
                /// no arquivo texto não tem o nome fantasia e não é obrigatorio no arquivo XML
                /// 
                sAux = TextoXml.ToString().Substring(TextoXml.ToString().IndexOf("<xFant>"));
                sAux = sAux.Substring(0, sAux.IndexOf("</xFant>") + 8);
                TextoXml.GetStringBuilder().Replace(sAux, "");
            }

            //MessageBox.Show(TextoXml.ToString());

            // Ajustando a tag IE do destinatario 
            if (TextoXml.ToString().IndexOf("<enderDest>") == -1)
                throw new Exception("tag <enderDest> não encontrada");

            sAux = TextoXml.ToString().Substring(TextoXml.ToString().IndexOf("<enderDest>"), (TextoXml.ToString().IndexOf("</enderDest>") - TextoXml.ToString().IndexOf("<enderDest>")) + 12);
            TextoXml.GetStringBuilder().Replace(sAux, "");

            if (TextoXml.ToString().IndexOf("<dest>") == -1)
                throw new Exception("tag <dest> não encontrada");

            string sAux2 = TextoXml.ToString().Substring(TextoXml.ToString().IndexOf("<dest>"), (TextoXml.ToString().IndexOf("</dest>") - TextoXml.ToString().IndexOf("<dest>")) + 7);
            if (sAux2.IndexOf("<IE>") == -1 && sAux2.IndexOf("<IE/>") == -1)
                throw new Exception("tag <IE> não encontrada na tag <dest>");

            if (sAux2.IndexOf("<IE>") == -1)
            {
                TextoXml.GetStringBuilder().Insert(TextoXml.ToString().IndexOf("<IE/>", TextoXml.ToString().IndexOf("<dest>")), sAux);
            }
            else
            {
                TextoXml.GetStringBuilder().Insert(TextoXml.ToString().IndexOf("<IE>", TextoXml.ToString().IndexOf("<dest>")), sAux);
            }

            TextoXml.GetStringBuilder().Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", ""); //Retirar o namespace xsi, pois tem estado que não tá aceitando. Wandrey 16/11/2010
            TextoXml.GetStringBuilder().Replace("<infAdProd>~-?-~</infAdProd>", "");

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(TextoXml.ToString());

            ///
            /// varre todos os <det><prod>
            XmlNodeList prodList = xdoc.GetElementsByTagName("det");
            foreach(XmlNode prodItem in prodList)
            {
                foreach (XmlNode xItem0 in prodItem.ChildNodes)
                {
                    if (xItem0.Name == "prod")
                    {
                        XmlNode xItemxPed_ok = null;
                        XmlNode xItemnItemPed_ok = null;
                        bool DI_found = false;

                        foreach (XmlNode xItemxPed in xItem0.ChildNodes)
                        {
                            switch(xItemxPed.Name)
                            {
                                case "xPed":
                                   xItemxPed_ok = xItemxPed;
                                   break;
                            
                                case "nItemPed":
                                    xItemnItemPed_ok = xItemxPed;
                                    break;

                                case "DI":
                                    {
                                        DI_found = true;
                                        if (xItemxPed_ok != null)
                                        {
                                            //se a tag <xPed> for encontrada, insere logo apos a tag <DI>
                                            xItem0.InsertAfter(xItemxPed_ok, xItemxPed);
                                        }
                                        if (xItemnItemPed_ok != null)
                                        {
                                            if (xItemxPed_ok == null)
                                                //se a tag <nItemPed> for encontrada, insere logo apos a tag <DI><xPed>
                                                xItem0.InsertAfter(xItemnItemPed_ok, xItemxPed);
                                            else
                                                //se a tag <nItemPed> for encontrada, insere logo apos a tag <DI>
                                                xItem0.InsertAfter(xItemnItemPed_ok, xItemxPed_ok);
                                        }
                                    }
                                    break;
                            }
                            if (DI_found)
                                break;
                        }
                    }
                    break;
                }
            }

            ///
            /// danasa 8-2009
            /// 
            if (cDestino.Substring(cDestino.Length - 1, 1) == @"\")
                cDestino = cDestino.Substring(0, cDestino.Length - 1);

            if (!Directory.Exists(cDestino + "\\convertidos"))
            {
                ///
                /// cria uma pasta temporária para armazenar o XML convertido
                /// 
                System.IO.Directory.CreateDirectory(cDestino + "\\convertidos");
            }
            ///
            /// danasa 8-2009
            /// Adiciona o XML na lista de arquivos convertidos
            /// 
            this.cRetorno.Add(new txtTOxmlClassRetorno(cChave + ExtXml.Nfe, cChave, nNF, serie));

            ///
            /// danasa 8-2009
            /// Salva o XML convertido na pasta temporária
            /// 
            string strRetorno = cDestino + "\\convertidos\\" + cChave + ExtXml.Nfe;
            XmlTextWriter xWriter = new XmlTextWriter(@strRetorno, Encoding.UTF8);
            xWriter.Formatting = Formatting.None;
            xdoc.Save(xWriter);

            xWriter.Close();
        }

        private void colunas(DataSet dsNfe, string tablename)
        {
            DataTable ar = dsNfe.Tables[tablename];
            for (int i = 0; i < ar.Columns.Count; ++i)
                MessageBox.Show(i.ToString() + ": " + ar.Columns[i].ColumnName);
        }
    }
    public class txtTOxmlClassRetorno
    {
        public Int32 NotaFiscal { get; set; }
        public Int32 Serie { get; set; }
        public string XMLFileName { get; set; }
        public string ChaveNFe { get; set; }

        public txtTOxmlClassRetorno(string xmlFileName, string chaveNFe, Int32 notaFiscal, Int32 serie)
        {
            this.XMLFileName = xmlFileName;
            this.ChaveNFe = chaveNFe;
            this.NotaFiscal = notaFiscal;
            this.Serie = serie;
        }
    }
    enum ObOp
    {
        Obrigatorio,
        Opcional
    }
}

