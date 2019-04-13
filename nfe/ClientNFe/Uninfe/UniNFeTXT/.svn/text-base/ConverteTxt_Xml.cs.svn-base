using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Globalization;
using System.ComponentModel;

using System.Reflection;
using System.Runtime.InteropServices;

namespace UniNFeTXT
{
    public class ConversaoTXT
    {
        #region --- public properties

        public List<txtTOxmlClassRetorno> cRetorno = new List<txtTOxmlClassRetorno>();
        public string cMensagemErro { get; private set; }

        #endregion

        #region -- private proprieties

        //private const string VERSOES_VALIDAS_LAYOUT_TXT = "|1.10|";
        private NFe NFe = null;

        private string FID;
        private int LinhaLida;
        private int nProduto;
        private string Registro;
        private Dictionary<int, List<string>> xConteudoArquivo;
        private Dictionary<string, string> layout2 = new Dictionary<string, string>();

        #endregion

        private ConversaoTXT() { }

        public ConversaoTXT(string versao)
        {
            if (versao == "1.10")
            {
                layout2.Add("", "§NOTAFISCAL|1");
                layout2.Add("A", "§A|1.10|^id^"); //ok
                layout2.Add("B", "§B|cUF¨|cNF¨|NatOp¨|indPag¨|mod¨|serie¨|nNF¨|dEmi¨|dSaiEnt¨|tpNF¨|cMunFG¨|TpImp¨|TpEmis¨|CDV¨|TpAmb¨|FinNFe¨|ProcEmi¨|VerProc¨"); //ok
                layout2.Add("B13", "§B13|refNFe¨"); //ok
                layout2.Add("B14", "§B14|cUF¨|AAMM¨|CNPJ¨|Mod¨|serie¨|nNF¨"); //ok
                layout2.Add("C", "§C|XNome¨|XFant¨|IE¨|IEST¨|IM¨|CNAE¨"); //ok
                layout2.Add("C02", "§C02|CNPJ¨"); //ok
                layout2.Add("C02a", "§C02a|CPF¨"); //ok
                layout2.Add("C05", "§C05|XLgr¨|Nro¨|xCpl¨|xBairro¨|CMun¨|XMun¨|UF¨|CEP¨|CPais¨|XPais¨|Fone¨"); //ok
                layout2.Add("D", "§D|CNPJ¨|XOrgao¨|Matr¨|XAgente¨|Fone¨|UF¨|NDAR¨|DEmi¨|VDAR¨|RepEmi¨|DPag¨"); //ok
                layout2.Add("E", "§E|XNome¨|IE¨|ISUF¨"); //ok
                layout2.Add("E02", "§E02|CNPJ¨"); //ok
                layout2.Add("E03", "§E03|CPF¨"); //ok
                layout2.Add("E05", "§E05|XLgr¨|Nro¨|XCpl¨|XBairro¨|CMun¨|XMun¨|UF¨|CEP¨|CPais¨|XPais¨|Fone¨"); //ok
                layout2.Add("F", "§F|CNPJ¨|XLgr¨|Nro¨|XCpl¨|XBairro¨|CMun¨|XMun¨|UF¨"); //ok
                layout2.Add("G", "§G|CNPJ¨|XLgr¨|Nro¨|XCpl¨|XBairro¨|CMun¨|XMun¨|UF¨"); //ok
                layout2.Add("H", "§H|NItem¨|InfAdProd¨"); //ok
                layout2.Add("I", "§I|CProd¨|CEAN¨|XProd¨|NCM¨|EXTIPI¨|Genero¨|CFOP¨|UCom¨|QCom¨|VUnCom¨|VProd¨|CEANTrib¨|UTrib¨|QTrib¨|VUnTrib¨|VFrete¨|VSeg¨|VDesc¨"); //ok
                layout2.Add("I18", "§I18|NDI¨|DDI¨|XLocDesemb¨|UFDesemb¨|DDesemb¨|CExportador¨"); //ok
                layout2.Add("I25", "§I25|NAdicao¨|NSeqAdic¨|CFabricante¨|VDescDI¨"); //ok
                layout2.Add("J", "§J|TpOp¨|Chassi¨|CCor¨|XCor¨|Pot¨|CM3¨|PesoL¨|PesoB¨|NSerie¨|TpComb¨|NMotor¨|CMKG¨|Dist¨|RENAVAM¨|AnoMod¨|AnoFab¨|TpPint¨|TpVeic¨|EspVeic¨|VIN¨|CondVeic¨|CMod¨"); //ok
                layout2.Add("K", "§K|NLote¨|QLote¨|DFab¨|DVal¨|VPMC¨"); //ok
                layout2.Add("L", "§L|TpArma¨|NSerie¨|NCano¨|Descr¨"); //ok
                layout2.Add("L01", "§L01|CProdANP¨|CODIF¨|QTemp¨"); //ok
                layout2.Add("L105", "§L105|QBCProd¨|VAliqProd¨|VCIDE¨"); //ok
                layout2.Add("L109", "§L109|VBCICMS¨|VICMS¨|VBCICMSST¨|VICMSST¨"); //ok
                layout2.Add("L114", "§L114|VBCICMSSTDest¨|VICMSSTDest¨"); //ok
                layout2.Add("L117", "§L117|VBCICMSSTCons¨|VICMSSTCons¨|UFCons¨"); //ok
                layout2.Add("M", "§M"); //ok
                layout2.Add("N", "§N"); //ok
                layout2.Add("N02", "§N02|Orig¨|CST¨|ModBC¨|VBC¨|PICMS¨|VICMS¨"); //ok
                layout2.Add("N03", "§N03|Orig¨|CST¨|ModBC¨|VBC¨|PICMS¨|VICMS¨|ModBCST¨|PMVAST¨|PRedBCST¨|VBCST¨|PICMSST¨|VICMSST¨"); //ok
                layout2.Add("N04", "§N04|Orig¨|CST¨|ModBC¨|PRedBC¨|VBC¨|PICMS¨|VICMS¨"); //ok
                layout2.Add("N05", "§N05|Orig¨|CST¨|ModBCST¨|PMVAST¨|PRedBCST¨|VBCST¨|PICMSST¨|VICMSST¨"); //ok
                layout2.Add("N06", "§N06|Orig¨|CST¨"); //ok
                layout2.Add("N07", "§N07|Orig¨|CST¨|ModBC¨|PRedBC¨|VBC¨|PICMS¨|VICMS¨"); //ok
                layout2.Add("N08", "§N08|Orig¨|CST¨|VBCST¨|VICMSST¨"); //ok
                layout2.Add("N09", "§N09|Orig¨|CST¨|ModBC¨|PRedBC¨|VBC¨|PICMS¨|VICMS¨|ModBCST¨|PMVAST¨|PRedBCST¨|VBCST¨|PICMSST¨|VICMSST¨"); //ok
                layout2.Add("N10", "§N10|Orig¨|CST¨|ModBC¨|VBC¨|PRedBC¨|PICMS¨|VICMS¨|ModBCST¨|PMVAST¨|PRedBCST¨|VBCST¨|PICMSST¨|VICMSST¨"); //ok
                layout2.Add("O", "§O|ClEnq¨|CNPJProd¨|CSelo¨|QSelo¨|CEnq¨"); //ok
                layout2.Add("O07", "§O07|CST¨|VIPI¨"); //ok
                layout2.Add("O10", "§O10|VBC¨|PIPI¨"); //ok
                layout2.Add("O11", "§O11|QUnid¨|VUnid¨"); //ok
                layout2.Add("O08", "§O08|CST¨"); //ok
                layout2.Add("P", "§P|VBC¨|VDespAdu¨|VII¨|VIOF¨"); //ok
                layout2.Add("Q", "§Q"); //ok
                layout2.Add("Q02", "§Q02|CST¨|VBC¨|PPIS¨|VPIS¨"); //ok
                layout2.Add("Q03", "§Q03|CST¨|QBCProd¨|VAliqProd¨|VPIS¨"); //ok
                layout2.Add("Q04", "§Q04|CST¨"); //ok
                layout2.Add("Q05", "§Q05|CST¨|VPIS¨"); //ok
                layout2.Add("Q07", "§Q07|VBC¨|PPIS¨"); //ok
                layout2.Add("Q10", "§Q10|QBCProd¨|VAliqProd¨"); //ok
                layout2.Add("R", "§R|VPIS¨"); //ok
                layout2.Add("R02", "§R02|VBC¨|PPIS¨"); //ok
                layout2.Add("R04", "§R04|QBCProd¨|VAliqProd¨"); //ok
                layout2.Add("S", "§S"); //ok
                layout2.Add("S02", "§S02|CST¨|VBC¨|PCOFINS¨|VCOFINS¨"); //ok
                layout2.Add("S03", "§S03|CST¨|QBCProd¨|VAliqProd¨|VCOFINS¨"); //ok
                layout2.Add("S04", "§S04|CST¨"); //ok
                layout2.Add("S05", "§S05|CST¨|VCOFINS¨"); //ok
                layout2.Add("S07", "§S07|VBC¨|PCOFINS¨"); //ok
                layout2.Add("S09", "§S09|QBCProd¨|VAliqProd¨"); //ok
                layout2.Add("T", "§T|VCOFINS¨"); //ok
                layout2.Add("T02", "§T02|VBC¨|PCOFINS¨"); //ok
                layout2.Add("T04", "§T04|QBCProd¨|VAliqProd¨"); //ok
                layout2.Add("U", "§U|VBC¨|VAliq¨|VISSQN¨|CMunFG¨|CListServ¨"); //ok
                layout2.Add("W", "§W"); //ok
                layout2.Add("W02", "§W02|VBC¨|VICMS¨|VBCST¨|VST¨|VProd¨|VFrete¨|VSeg¨|VDesc¨|VII¨|VIPI¨|VPIS¨|VCOFINS¨|VOutro¨|VNF¨"); //ok
                layout2.Add("W17", "§W17|VServ¨|VBC¨|VISS¨|VPIS¨|VCOFINS¨"); //ok
                layout2.Add("W23", "§W23|VRetPIS¨|VRetCOFINS¨|VRetCSLL¨|VBCIRRF¨|VIRRF¨|VBCRetPrev¨|VRetPrev¨"); //ok
                layout2.Add("X", "§X|ModFrete¨"); //ok
                layout2.Add("X03", "§X03|XNome¨|IE¨|XEnder¨|UF¨|XMun¨"); //ok
                layout2.Add("X04", "§X04|CNPJ¨"); //ok
                layout2.Add("X05", "§X05|CPF¨"); //ok
                layout2.Add("X11", "§X11|VServ¨|VBCRet¨|PICMSRet¨|VICMSRet¨|CFOP¨|CMunFG¨"); //ok
                layout2.Add("X18", "§X18|Placa¨|UF¨|RNTC¨"); //ok
                layout2.Add("X22", "§X22|Placa¨|UF¨|RNTC¨"); //ok
                layout2.Add("X26", "§X26|QVol¨|Esp¨|Marca¨|NVol¨|PesoL¨|PesoB¨"); //ok
                layout2.Add("X33", "§X33|NLacre¨"); //ok
                layout2.Add("Y", "§Y"); //ok
                layout2.Add("Y02", "§Y02|NFat¨|VOrig¨|VDesc¨|VLiq¨"); //ok
                layout2.Add("Y07", "§Y07|NDup¨|DVenc¨|VDup¨"); //ok
                layout2.Add("Z", "§Z|InfAdFisco¨|InfCpl¨"); //ok
                layout2.Add("Z04", "§Z04|XCampo¨|XTexto¨"); //ok
                //adLayout2("Z07",  "§Z07|XCampo¨|XTexto¨"); //ok - ?
                layout2.Add("Z10", "§Z10|NProc¨|IndProc¨"); //ok
                layout2.Add("ZA", "§ZA|UFEmbarq¨|XLocEmbarq¨"); //ok
                layout2.Add("ZB", "§ZB|XNEmp¨|XPed¨|XCont¨"); //ok
            }
            this.xConteudoArquivo = new Dictionary<int, List<string>>();
            this.cMensagemErro = "";
            this.LinhaLida = 0;
        }

        private bool CarregarArquivo(string cArquivo)
        {
            if (File.Exists(cArquivo))
            {
                TextReader txt = new StreamReader(cArquivo);
                try
                {
                    int nNota = -1;
                    string cLinhaTXT = txt.ReadLine();
                    if (cLinhaTXT != null)
                    {
                        if (!cLinhaTXT.StartsWith("NOTAFISCAL") && !cLinhaTXT.StartsWith("NOTA FISCAL"))
                        {
                            this.cMensagemErro = Properties.Resources.msg_001;
                        }
                        cLinhaTXT = txt.ReadLine();
                        this.LinhaLida = 1;
                    }
                    while (cLinhaTXT != null)
                    {
                        ++LinhaLida;

                        if (cLinhaTXT.Trim().Length > 0)
                        {
                            if (cLinhaTXT.StartsWith("A|"))
                            {
                                ++nNota;
                                xConteudoArquivo.Add(nNota, new List<string>());
                            }
                            List<string> temp;
                            xConteudoArquivo.TryGetValue(nNota, out temp);
                            temp.Add("§" + cLinhaTXT.Trim() + "|");
                        }
                        cLinhaTXT = txt.ReadLine();
                    }
                }
                catch (IOException ex)
                {
                    this.cMensagemErro += ex.Message;
                }
                catch (Exception ex)
                {
                    this.cMensagemErro += ex.Message;
                }
                finally
                {
                    txt.Close();
                }
            }
            else
                this.cMensagemErro = "Arquivo [" + cArquivo + "] não encontrado";

            return ((this.xConteudoArquivo.Count == 0 || !string.IsNullOrEmpty(this.cMensagemErro)) ? false : true);
        }

        public bool Converter(string cArquivo, string cDestino)
        {
            cRetorno.Clear();

            if (this.CarregarArquivo(cArquivo))
            {
                this.LinhaLida = 0;
                foreach (List<string> content in this.xConteudoArquivo.Values)
                {
                    NFe = null;
                    NFe = new NFe();

                    bool houveErro = false;
                    foreach (string xContent in content)
                    {
                        houveErro = false;
                        ++this.LinhaLida;
                        try
                        {
                            this.LerRegistro(xContent);
                        }
                        catch(Exception ex)
                        {
                            houveErro = true;
                            this.cMensagemErro += "Linha lida: " + this.LinhaLida.ToString()+ Environment.NewLine+
                                                    "Conteudo: " + xContent + Environment.NewLine +
                                                    ex.Message + Environment.NewLine;
                        }
                    }
                    
                    if (!houveErro && this.cMensagemErro == "")
                    {
                        NFeW nfew = new NFeW();
                        try
                        {
                            nfew.cMensagemErro = this.cMensagemErro;
                            nfew.GerarXml(NFe, cDestino);
                            if (nfew.cFileName != "")
                            {
                                ///
                                /// Adiciona o XML na lista de arquivos convertidos
                                /// 
                                this.cRetorno.Add(new txtTOxmlClassRetorno(nfew.cFileName, NFe.infNFe.ID, NFe.ide.nNF, NFe.ide.serie));
                            }
                        }
                        catch (Exception ex)
                        {
                            nfew.cMensagemErro += ex.Message;
                        }
                        this.cMensagemErro = nfew.cMensagemErro;
                    }

                    if (this.cMensagemErro != "")
                    {
                        foreach (txtTOxmlClassRetorno txtClass in this.cRetorno)
                        {
                            string dArquivo = cDestino + "\\convertidos\\" + txtClass.XMLFileName;
                            if (!File.Exists(dArquivo))
                                dArquivo = txtClass.XMLFileName;

                            if (File.Exists(dArquivo))
                            {
                                FileInfo fi = new FileInfo(dArquivo);
                                fi.Delete();
                            }
                        }
                    }
                }
                return string.IsNullOrEmpty(this.cMensagemErro);
            }
            return false;
        }

        private DateTime getDateTime(string value)
        {
            if (string.IsNullOrEmpty(value))
                return DateTime.MinValue;

            try
            {
                int _ano = Convert.ToInt16(value.Substring(0, 4));
                int _mes = Convert.ToInt16(value.Substring(5, 2));
                int _dia = Convert.ToInt16(value.Substring(8, 2));
                if (value.Contains("T") && value.Contains(":"))
                {
                    int _hora = Convert.ToInt16(value.Substring(11, 2));
                    int _min = Convert.ToInt16(value.Substring(14, 2));
                    int _seg = Convert.ToInt16(value.Substring(17, 2));
                    return new DateTime(_ano, _mes, _dia, _hora, _min, _seg);
                }
                return new DateTime(_ano, _mes, _dia);
            }
            catch
            {
                throw new Exception("Data inválida do conteudo [" + value + "]");
            }
        }

        private string RetornarConteudoTag(string TAG)
        {
            string fValue = "";

            if (layout2.ContainsKey(this.FID))
                if (layout2.TryGetValue(this.FID, out fValue))
                {
                    ///
                    /// "§B14|cUF¨|AAMM¨|CNPJ¨|Mod¨|serie¨|nNF¨"); //ok
                    /// 
                    /// se a tag a ser consulta é CNPJ, então é verificada no layout quantoa pipes existem até ela
                    /// neste caso no comand abaixo será retornado "§B14|cUF¨|AAMM¨|" existindo 3 pipes para pegar
                    /// o valor do retorno
                    /// 
                    fValue = fValue.Substring(0, fValue.ToUpper().IndexOf("|" + TAG.ToUpper().Trim() + "¨") + 1);
                    string[] pipes = fValue.Split('|');
                    int j = pipes.GetLength(0) - 2;
                    //j = LocalizarPosicaoTAG(TAG.ToUpper(), fValue);
                    if (j >= 0)
                    {
                        ///
                        /// qual a posicao do conteudo
                        /// 
                        string[] dados = this.Registro.Split('|');
                        if (j > dados.GetLength(0))
                            fValue = "";
                        else
                            fValue = dados[j + 1].Trim();
                    }
                    else
                        fValue = "";
                }

            return fValue;
        }

        private string SomenteNumeros(string entrada)
        {
            if (string.IsNullOrEmpty(entrada)) return "";

            StringBuilder saida = new StringBuilder(entrada.Length);
            foreach (char c in entrada)
            {
                if (char.IsDigit(c))
                {
                    saida.Append(c);
                }
            }
            return saida.ToString();
        }

        private object LerCampo(TpcnTipoCampo Tipo, string TAG)
        {
            string ConteudoTag = "";
            try
            {
                ConteudoTag = RetornarConteudoTag(TAG);

                if (ConteudoTag != "")
                    if (ConteudoTag.StartsWith("§"))
                        ConteudoTag = "";

                switch (Tipo)
                {
                    case TpcnTipoCampo.tcStr:
                        return ConteudoTag.Replace("&", "&amp;").
                                        Replace("<", "&lt;").
                                        Replace(">", "&gt;").
                                        Replace("\"", "&quot;").
                                        Replace("\r\n", " ").
                                        Replace("'", "&#39;").Trim();

                    case TpcnTipoCampo.tcDat:
                    case TpcnTipoCampo.tcDatHor:
                        return this.getDateTime(ConteudoTag);

                    case TpcnTipoCampo.tcDec2:
                    case TpcnTipoCampo.tcDec3:
                    case TpcnTipoCampo.tcDec4:
                        return Convert.ToDouble("0" + ConteudoTag.Replace(".",","));

                    case TpcnTipoCampo.tcInt:
                        return Convert.ToInt32("0" + SomenteNumeros(ConteudoTag));

                    default:
                        return ConteudoTag.Trim();
                }
            }
            catch (Exception ex)
            {
                this.cMensagemErro += string.Format("Segmento [{0}]: tag <{1}> Conteudo: {2}\r\n" +
                                                    "\tLinha: {3}: Conteudo do segmento: {4}\r\n\tMensagem de erro: {5}",
                                                    this.FID, TAG, ConteudoTag, this.LinhaLida, this.Registro,
                                                    ex.Message) + Environment.NewLine;
                switch (Tipo)
                {
                    case TpcnTipoCampo.tcDat:
                    case TpcnTipoCampo.tcDatHor:
                        return DateTime.MinValue;

                    case TpcnTipoCampo.tcDec2:
                    case TpcnTipoCampo.tcDec3:
                    case TpcnTipoCampo.tcDec4:
                        return 0.0;

                    case TpcnTipoCampo.tcInt:
                        return 0;

                    default:
                        return "";
                }
            }
        }

        private void LerRegistro(string aRegistro)
        {
            int nProd = NFe.det.Count - 1;
            this.Registro = aRegistro;
            this.FID = this.Registro.Substring(1, this.Registro.IndexOf("|")-1);

            switch (this.FID)
            {
                case "B":
                    ///
                    /// Grupo da TAG <ide>
                    /// 
                    #region -- <ide>
                    //(*B02*)   
                    NFe.ide.cUF = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.cUF);
                    //(*B03*)   
                    NFe.ide.cNF = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.cNF);
                    //(*B04*)   
                    NFe.ide.natOp = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.natOp);
                    //(*B05*)   
                    NFe.ide.indPag = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.indPag);
                    //(*B06*)   
                    NFe.ide.mod = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.mod);
                    //(*B07*)   
                    NFe.ide.serie = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.serie);
                    //(*B08*)   
                    NFe.ide.nNF = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.nNF);
                    //(*B09*)   
                    NFe.ide.dEmi = (DateTime)LerCampo(TpcnTipoCampo.tcDat, Properties.Resources.dEmi);
                    //(*B10*)   
                    NFe.ide.dSaiEnt = (DateTime)LerCampo(TpcnTipoCampo.tcDat, Properties.Resources.dSaiEnt);
                    //(*B11*)   
                    NFe.ide.tpNF = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.tpNF);
                    //(*B12*)   
                    NFe.ide.cMunFG = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.cMunFG);
                    //(*B21*)   
                    NFe.ide.tpImp = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.tpImp);
                    //(*B22*)   
                    NFe.ide.tpEmis = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.tpEmis);
                    //(*B23*)   
                    NFe.ide.cDV = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.cDV);
                    //(*B24*)   
                    NFe.ide.tpAmb = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.tpAmb);
                    //(*B25*)   
                    NFe.ide.finNFe = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.finNFe);
                    //(*B26*)   
                    NFe.ide.procEmi = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.procEmi);
                    //(*B27*)   
                    NFe.ide.verProc = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.verProc);
                    break;
                    #endregion

                case "B13":
                    ///
                    /// Grupo da TAG <ide><NFref><refNFe>
                    ///
                    //(*B13*)   
                    #region <ide><NFref><refNFe>

                    NFe.ide.NFref.Add(new NFref((string)LerCampo(TpcnTipoCampo.tcEsp, Properties.Resources.refNFe)));

                    #endregion
                    break;

                case "B14":
                    ///
                    /// Grupo da TAG <ide><NFref><RefNF>
                    ///
                    #region <ide><NFref><RefNF>
                    {
                        NFref item = new NFref();

                        //(*B15*)
                        item.RefNF.cUF = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.cUF);
                        //(*B16*)
                        item.RefNF.AAMM = (string)LerCampo(TpcnTipoCampo.tcEsp, Properties.Resources.AAMM);
                        //(*B17*)
                        item.RefNF.CNPJ = (string)LerCampo(TpcnTipoCampo.tcEsp, Properties.Resources.CNPJ);
                        //(*B18*)
                        item.RefNF.mod = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.mod);
                        //(*B19*)
                        item.RefNF.serie = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.serie);
                        //(*B20*)
                        item.RefNF.nNF = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.nNF);

                        NFe.ide.NFref.Add(item);
                    }
                    #endregion
                    break;

                case "C":
                    ///
                    /// Grupo da TAG <emit>
                    ///
                    #region <emit>

                    //(*C  *)   
                    NFe.emit.xNome = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xNome);
                    //(*C  *)   
                    NFe.emit.xFant = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xFant);
                    //(*C17*)   
                    NFe.emit.IE = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.IE);
                    //(*C18*)   
                    NFe.emit.IEST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.IEST);
                    //(*C19*)   
                    NFe.emit.IM = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.IM);
                    //(*C20*)   
                    NFe.emit.CNAE = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CNAE);

                    #endregion
                    break;

                case "C02":
                    //(*C02*)   
                    NFe.emit.CNPJCPF = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CNPJ);
                    break;

                case "C02A":
                    //(*C02A*)  
                    NFe.emit.CNPJCPF = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CPF);
                    break;

                case "C05":
                    ///
                    /// Grupo da TAG <emit><EnderEmit>
                    /// 
                    #region <emit><EnderEmit>

                    //(*C06*)   
                    NFe.emit.enderEmit.xLgr = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xLgr);
                    //(*C07*)   
                    NFe.emit.enderEmit.nro = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.nro);
                    //(*C08*)   
                    NFe.emit.enderEmit.xCpl = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xCpl);
                    //(*C09*)   
                    NFe.emit.enderEmit.xBairro = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xBairro);
                    //(*C10*)   
                    NFe.emit.enderEmit.cMun = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.cMun);
                    //(*C11*)   
                    NFe.emit.enderEmit.xMun = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xMun);
                    //(*C12*)   
                    NFe.emit.enderEmit.UF = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.UF);
                    //(*C13*)   
                    NFe.emit.enderEmit.CEP = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.CEP);
                    //(*C14*)   
                    NFe.emit.enderEmit.cPais = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.cPais);
                    //(*C15*)   
                    NFe.emit.enderEmit.xPais = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xPais);
                    //(*C16*)   
                    NFe.emit.enderEmit.fone = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.fone);

                    #endregion
                    break;

                case "D":
                    ///
                    /// Grupo da TAG <avulsa>
                    /// 
                    #region <avulsa>

                    //(*D02*)   
                    NFe.avulsa.CNPJ = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CNPJ);
                    //(*D03*)   
                    NFe.avulsa.xOrgao = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xOrgao);
                    //(*D04*)   
                    NFe.avulsa.matr = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.matr);
                    //(*D05*)   
                    NFe.avulsa.xAgente = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xAgente);
                    //(*D06*)   
                    NFe.avulsa.fone = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.fone);
                    //(*D07*)   
                    NFe.avulsa.UF = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.UF);
                    //(*D08*)   
                    NFe.avulsa.nDAR = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.nDAR);
                    //(*D09*)   
                    NFe.avulsa.dEmi = (DateTime)LerCampo(TpcnTipoCampo.tcDat, Properties.Resources.dEmi);
                    //(*D10*)   
                    NFe.avulsa.vDAR = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vDAR);
                    //(*D11*)   
                    NFe.avulsa.repEmi = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.repEmi);
                    //(*D12*)   
                    NFe.avulsa.dPag = (DateTime)LerCampo(TpcnTipoCampo.tcDat, Properties.Resources.dPag);

                    #endregion
                    break;

                case "E":
                    ///
                    /// Grupo da TAG <dest>
                    /// 
                    #region <dest>

                    //(*E04*)   
                    NFe.dest.xNome = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xNome);
                    //(*E17*)   
                    NFe.dest.IE = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.IE);
                    //(*E18*)   
                    NFe.dest.ISUF = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.ISUF);

                    #endregion
                    break;

                case "E02":
                    //(*E02*)   
                    NFe.dest.CNPJCPF = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CNPJ);
                    break;

                case "E03":
                    //(*E03*)   
                    NFe.dest.CNPJCPF = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CPF);
                    break;

                case "E05":
                    ///
                    /// Grupo da TAG <dest><EnderDest>
                    /// 
                    //(*E06*) 
                    NFe.dest.enderDest.xLgr = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xLgr);
                    //(*E07*)   
                    NFe.dest.enderDest.nro = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.nro);
                    //(*E08*)   
                    NFe.dest.enderDest.xCpl = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xCpl);
                    //(*E09*)   
                    NFe.dest.enderDest.xBairro = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xBairro);
                    //(*E10*)   
                    NFe.dest.enderDest.cMun = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.cMun);
                    //(*E11*)   
                    NFe.dest.enderDest.xMun = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xMun);
                    //(*E12*)   
                    NFe.dest.enderDest.UF = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.UF);
                    //(*E13*)   
                    NFe.dest.enderDest.CEP = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.CEP);
                    //(*E14*)   
                    NFe.dest.enderDest.cPais = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.cPais);
                    //(*E15*)   
                    NFe.dest.enderDest.xPais = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xPais);
                    //(*E16*)   
                    NFe.dest.enderDest.fone = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.fone);
                    break;

                case "F":
                    ///
                    /// Grupo da TAG <retirada> 
                    /// 
                    //(*F02*)   
                    NFe.retirada.CNPJ = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CNPJ);
                    //(*F03*)   
                    NFe.retirada.xLgr = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xLgr);
                    //(*F04*)   
                    NFe.retirada.nro = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.nro);
                    //(*F05*)   
                    NFe.retirada.xCpl = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xCpl);
                    //(*F06*)   
                    NFe.retirada.xBairro = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xBairro);
                    //(*F07*)   
                    NFe.retirada.cMun = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.cMun);
                    //(*F08*)   
                    NFe.retirada.xMun = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xMun);
                    //(*F09*)   
                    NFe.retirada.UF = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.UF);
                    break;

                case "G":
                    ///
                    /// Grupo da TAG <entrega>
                    /// 
                    //(*G***)   
                    NFe.entrega.CNPJ = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CNPJ);
                    //(*G03*)   
                    NFe.entrega.xLgr = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xLgr);
                    //(*G04*)   
                    NFe.entrega.nro = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.nro);
                    //(*G05*)   
                    NFe.entrega.xCpl = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xCpl);
                    //(*G06*)   
                    NFe.entrega.xBairro = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xBairro);
                    //(*G07*)   
                    NFe.entrega.cMun = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.cMun);
                    //(*G08*)   
                    NFe.entrega.xMun = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xMun);
                    //(*G09*)   
                    NFe.entrega.UF = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.UF);
                    break;

                ///
                /// Grupo da TAG <det>
                /// 
                case "H":
                    nProduto = (int)this.LerCampo(TpcnTipoCampo.tcInt, "NItem");

                    NFe.det.Add(new Det());
                    nProd = NFe.det.Count - 1;
                    NFe.det[nProd].Prod.nItem = nProduto;
                    //(*V01*)
                    NFe.det[nProd].infAdProd = (string)this.LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.infAdProd);
                    break;

                case "I":
                    ///
                    /// Grupo da TAG <det><prod>
                    /// 
                    #region <det><prod>
                    //(*I02*)
                    NFe.det[nProd].Prod.cProd = (string)this.LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.cProd);
                    //(*I03*)
                    NFe.det[nProd].Prod.cEAN = (string)this.LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.cEAN);
                    //(*I04*)
                    NFe.det[nProd].Prod.xProd = (string)this.LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xProd);
                    //(*I05*)
                    NFe.det[nProd].Prod.NCM = (string)this.LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.NCM);
                    //(*I06*)
                    NFe.det[nProd].Prod.EXTIPI = (string)this.LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.EXTIPI);
                    //(*I07*)
                    NFe.det[nProd].Prod.genero = (int)this.LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.genero);
                    //(*I08*)
                    NFe.det[nProd].Prod.CFOP = (string)this.LerCampo(TpcnTipoCampo.tcEsp, Properties.Resources.CFOP);
                    //(*I09*)
                    NFe.det[nProd].Prod.uCom = (string)this.LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.uCom);
                    //(*I10*)
                    NFe.det[nProd].Prod.qCom = (double)this.LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.qCom);
                    //(*I10a*)
                    NFe.det[nProd].Prod.vUnCom = (double)this.LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.vUnCom);
                    //(*I11*)
                    NFe.det[nProd].Prod.vProd = (double)this.LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vProd);
                    //(*I12*)
                    NFe.det[nProd].Prod.cEANTrib = (string)this.LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.cEANTrib);
                    //(*I13*)
                    NFe.det[nProd].Prod.uTrib = (string)this.LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.uTrib);
                    //(*I14*)
                    NFe.det[nProd].Prod.qTrib = (double)this.LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.qTrib);
                    //(*I14a*)
                    NFe.det[nProd].Prod.vUnTrib = (double)this.LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.vUnTrib);
                    //(*I15*)
                    NFe.det[nProd].Prod.vFrete = (double)this.LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vFrete);
                    //(*I16*)
                    NFe.det[nProd].Prod.vSeg = (double)this.LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vSeg);
                    //(*I17*)
                    NFe.det[nProd].Prod.vDesc = (double)this.LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vDesc);
                    #endregion                    
                    break;

                case "I18":
                    ///
                    /// Grupo da TAG <det><prod><DI>
                    /// 
                    #region <det><prod><DI>
                    //i := NFe.Det.Count - 1;
                    //NFe.Det[i].Prod.DI.Add;
                    //j := NFe.Det[i].Prod.DI.Count - 1;

                    DI diItem = new DI();

                    //(*I19*)
                    diItem.nDi = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.nDI);
                    //(*I20*)
                    diItem.dDi = (DateTime)LerCampo(TpcnTipoCampo.tcDat, Properties.Resources.dDI);
                    //(*I21*)
                    diItem.xLocDesemb = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xLocDesemb);
                    //(*I22*)
                    diItem.UFDesemb = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.UFDesemb);
                    //(*I23*)
                    diItem.dDesemb = (DateTime)LerCampo(TpcnTipoCampo.tcDat, Properties.Resources.dDesemb);
                    //(*I24*)
                    diItem.cExportador = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.cExportador);

                    NFe.det[nProd].Prod.DI.Add(diItem);
                    #endregion
                    break;

                case "I25":
                    ///
                    /// Grupo da TAG <det><prod><DI><adi> 
                    /// 
                    #region <det><prod><DI><adi>
                    //i := NFe.Det.Count - 1;
                    //j := NFe.Det[i].Prod.DI.Count - 1;
                    //NFe.Det[i].Prod.DI[j].adi.Add;
                    //k := NFe.Det[i].Prod.DI[j].adi.Count - 1;

                    Adi adiItem;

                    //(*I26*)
                    adiItem.nAdicao = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.nAdicao);
                    //(*I27*)
                    adiItem.nSeqAdi = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.nSeqAdic);
                    //(*I28*)
                    adiItem.cFabricante = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.cFabricante);
                    //(*I29*)
                    adiItem.vDescDI = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vDescDI);

                    NFe.det[nProd].Prod.DI[NFe.det[NFe.det.Count - 1].Prod.DI.Count-1].adi.Add(adiItem);
                    #endregion
                    break;

                case "J":
                    ///
                    /// Grupo da TAG <det><prod><veicProd>
                    /// 
                    #region <det><prod><veicProd>
                    nProd = NFe.det.Count - 1;

                    //veicProd veiculo = NFe.det[NFe.det.Count - 1].Prod.veicProd;
                    //(*J02*)
                    NFe.det[nProd].Prod.veicProd.tpOP = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.tpOp);
                    //(*J03*)   
                    NFe.det[nProd].Prod.veicProd.chassi = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.chassi);
                    //(*J04*)   
                    NFe.det[nProd].Prod.veicProd.cCor = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.cCor);
                    //(*J05*)   
                    NFe.det[nProd].Prod.veicProd.xCor = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xCor);
                    //(*J06*)   
                    NFe.det[nProd].Prod.veicProd.pot = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.pot);
                    //(*J07*)   
                    NFe.det[nProd].Prod.veicProd.CM3 = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CM3);
                    //(*J08*)   
                    NFe.det[nProd].Prod.veicProd.pesoL = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.pesoL);
                    //(*J09*)   
                    NFe.det[nProd].Prod.veicProd.pesoB = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.pesoB);
                    //(*J10*)   
                    NFe.det[nProd].Prod.veicProd.nSerie = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.nSerie);
                    //(*J11*)   
                    NFe.det[nProd].Prod.veicProd.tpComb = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.tpComb);
                    //(*J12*)   
                    NFe.det[nProd].Prod.veicProd.nMotor = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.nMotor);
                    //(*J13*)   
                    NFe.det[nProd].Prod.veicProd.CMKG = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CMKG);
                    //(*J14*)   
                    NFe.det[nProd].Prod.veicProd.dist = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.dist);
                    //(*J15*)   
                    NFe.det[nProd].Prod.veicProd.RENAVAM = (string)LerCampo(TpcnTipoCampo.tcEsp, Properties.Resources.RENAVAM);
                    //(*J16*)   
                    NFe.det[nProd].Prod.veicProd.anoMod = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.anoMod);
                    //(*J17*)   
                    NFe.det[nProd].Prod.veicProd.anoFab = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.anoFab);
                    //(*J18*)   
                    NFe.det[nProd].Prod.veicProd.tpPint = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.tpPint);
                    //(*J19*)   
                    NFe.det[nProd].Prod.veicProd.tpVeic = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.tpVeic);
                    //(*J20*)   
                    NFe.det[nProd].Prod.veicProd.espVeic = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.espVeic);
                    //(*J21*)   
                    NFe.det[nProd].Prod.veicProd.VIN = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.VIN);
                    //(*J22*)   
                    NFe.det[nProd].Prod.veicProd.condVeic = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.condVeic);
                    //(*J23*)   
                    NFe.det[nProd].Prod.veicProd.cMod = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.cMod);
                    #endregion
                    break;

                case "K":
                    ///
                    /// Grupo da TAG <det><prod><med>
                    /// 
                    //i := NFe.Det.Count - 1;
                    //NFe.Det[i].Prod.med.Add;
                    //j := NFe.Det[i].Prod.med.Count - 1;

                    Med medItem = new Med();

                    //(*K02*)
                    medItem.nLote = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.nLote);
                    //(*K03*)
                    medItem.qLote = (double)LerCampo(TpcnTipoCampo.tcDec3, Properties.Resources.qLote);
                    //(*K04*)   
                    medItem.dFab = (DateTime)LerCampo(TpcnTipoCampo.tcDat, Properties.Resources.dFab);
                    //(*K05*)   
                    medItem.dVal = (DateTime)LerCampo(TpcnTipoCampo.tcDat, Properties.Resources.dVal);
                    //(*K06*)   
                    medItem.vPMC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vPMC);

                    NFe.det[nProd].Prod.med.Add(medItem);
                    break;

                case "L":
                    ///
                    /// Grupo da TAG <det><prod><arma>
                    /// 
                    //i := NFe.Det.Count - 1;
                    //NFe.Det[i].Prod.arma.add;
                    //j := NFe.Det[i].Prod.arma.count - 1;

                    Arma armaItem = new Arma();

                    //(*L02*)
                    armaItem.tpArma = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.tpArma);
                    //(*L03*)
                    armaItem.nSerie = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.nSerie);
                    //(*L04*)
                    armaItem.nCano = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.nCano);
                    //(*L05*)
                    armaItem.descr = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.descr);

                    NFe.det[nProd].Prod.arma.Add(armaItem);
                    break;

                case "L01":
                    ///
                    /// Grupo da TAG <det><prod><comb>
                    /// 
                    //i := NFe.Det.Count - 1;

                    //(*L102*)
                    NFe.det[nProd].Prod.comb.cProdANP = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.cProdANP);
                    //(*L103*)
                    NFe.det[nProd].Prod.comb.CODIF = (string)LerCampo(TpcnTipoCampo.tcEsp, Properties.Resources.CODIF);
                    //(*L104*)
                    NFe.det[nProd].Prod.comb.qTemp = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.qTemp);
                    break;

                case "L105":
                    ///
                    /// Grupo da TAG <det><prod><comb><CIDE>
                    /// 
                    //i := nProd;

                    //(*L106*)
                    NFe.det[nProd].Prod.comb.CIDE.qBCprod = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.qBCProd);
                    //(*L107*)
                    NFe.det[nProd].Prod.comb.CIDE.vAliqProd = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.vAliqProd);
                    //(*L108*)
                    NFe.det[nProd].Prod.comb.CIDE.vCIDE = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vCIDE);
                    break;

                case "L109":
                    ///
                    /// Grupo da TAG <det><prod><comb><ICMSComb>
                    /// 
                    //i := nProd;

                    //(*L110*)
                    NFe.det[nProd].Prod.comb.ICMS.vBCICMS  = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBCICMS);
                    //(*L111*)
                    NFe.det[nProd].Prod.comb.ICMS.vICMS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vICMS);
                    //(*L112*)
                    NFe.det[nProd].Prod.comb.ICMS.vBCICMSST =(double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBCICMSST);
                    //(*L113*)
                    NFe.det[nProd].Prod.comb.ICMS.vICMSST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vICMSST);
                    break;

                case "L114":
                    ///
                    /// Grupo da TAG <det><prod><comb><ICMSInter>
                    /// 
                    //i := nProd;

                    //(*L115*)NFe.Det[i].Prod.comb.ICMSInter.vBCICMSSTDest := 
                    NFe.det[nProd].Prod.comb.ICMSInter.vBCICMSSTDest = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBCICMSSTDest);
                    //(*L116*)NFe.Det[i].Prod.comb.ICMSInter.vICMSSTDest := 
                    NFe.det[nProd].Prod.comb.ICMSInter.vICMSSTDest = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vICMSSTDest);
                    break;

                case "L117":
                    ///
                    /// Grupo da TAG <det><prod><comb><ICMSCons>
                    /// 
                    //i := nProd;

                    //(*L118*)NFe.Det[i].Prod.comb.ICMSCons.vBCICMSSTCons := 
                    NFe.det[nProd].Prod.comb.ICMSCons.vBCICMSSTCons = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBCICMSSTCons);
                    //(*L119*)NFe.Det[i].Prod.comb.ICMSCons.vICMSSTCons := 
                    NFe.det[nProd].Prod.comb.ICMSCons.vICMSSTCons = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vICMSSTCons);
                    //(*L120*)NFe.Det[i].Prod.comb.ICMSCons.UFcons := 
                    NFe.det[nProd].Prod.comb.ICMSCons.UFcons = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.UFcons);
                    break;

                ///
                /// Grupo da TAG <det><imposto><ICMS>
                /// 
                case "N02":
                    //(*N11*)
                    NFe.det[nProd].Imposto.ICMS.orig = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.orig);
                    //(*N12*)
                    NFe.det[nProd].Imposto.ICMS.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                    //(*N13*)
                    NFe.det[nProd].Imposto.ICMS.modBC = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.modBC);
                    //(*N15*)
                    NFe.det[nProd].Imposto.ICMS.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    //(*N16*)
                    NFe.det[nProd].Imposto.ICMS.pICMS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pICMS);
                    //(*N17*)
                    NFe.det[nProd].Imposto.ICMS.vICMS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vICMS);
                    break;

                case "N03":
                    //(*N11*)
                    NFe.det[nProd].Imposto.ICMS.orig = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.orig);
                    //(*N12*)
                    NFe.det[nProd].Imposto.ICMS.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                    //(*N13*)
                    NFe.det[nProd].Imposto.ICMS.modBC = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.modBC);
                    //(*N15*)
                    NFe.det[nProd].Imposto.ICMS.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    //(*N16*)
                    NFe.det[nProd].Imposto.ICMS.pICMS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pICMS);
                    //(*N17*)
                    NFe.det[nProd].Imposto.ICMS.vICMS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vICMS);
                    //(*N18*)
                    NFe.det[nProd].Imposto.ICMS.modBCST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.modBCST);
                    //(*N19*)
                    NFe.det[nProd].Imposto.ICMS.pMVAST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pMVAST);
                    //(*N20*)
                    NFe.det[nProd].Imposto.ICMS.pRedBCST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pRedBCST);
                    //(*N21*)
                    NFe.det[nProd].Imposto.ICMS.vBCST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBCST);
                    //(*N22*)
                    NFe.det[nProd].Imposto.ICMS.pICMSST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pICMSST);
                    //(*N23*)
                    NFe.det[nProd].Imposto.ICMS.vICMSST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vICMSST);
                    break;

                case "N04":
                    //(*N11*)   ICMS.orig := 
                    NFe.det[nProd].Imposto.ICMS.orig = (int)this.LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.orig);
                    //(*N12*)   ICMS.CST := 
                    NFe.det[nProd].Imposto.ICMS.CST = (string)this.LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                    //(*N13*)   ICMS.modBC := 
                    NFe.det[nProd].Imposto.ICMS.modBC = (string)this.LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.modBC);
                    //(*N14*)   ICMS.pRedBC := 
                    NFe.det[nProd].Imposto.ICMS.pRedBC = (double)this.LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pRedBC);
                    //(*N15*)   ICMS.vBC := 
                    NFe.det[nProd].Imposto.ICMS.vBC = (double)this.LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    //(*N16*)   ICMS.pICMS := 
                    NFe.det[nProd].Imposto.ICMS.pICMS = (double)this.LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pICMS);
                    //(*N17*)   ICMS.vICMS := 
                    NFe.det[nProd].Imposto.ICMS.vICMS = (double)this.LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vICMS);
                    break;

                case "N05":
                    //(*N11*)
                    NFe.det[nProd].Imposto.ICMS.orig = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.orig);
                    //(*N12*)
                    NFe.det[nProd].Imposto.ICMS.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                    //(*N18*)
                    NFe.det[nProd].Imposto.ICMS.modBCST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.modBCST);
                    //(*N19*)
                    NFe.det[nProd].Imposto.ICMS.pMVAST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pMVAST);
                    //(*N20*)
                    NFe.det[nProd].Imposto.ICMS.pRedBCST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pRedBCST);
                    //(*N21*)
                    NFe.det[nProd].Imposto.ICMS.vBCST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBCST);
                    //(*N22*)
                    NFe.det[nProd].Imposto.ICMS.pICMSST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pICMSST);
                    //(*N23*)
                    NFe.det[nProd].Imposto.ICMS.vICMSST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vICMSST);
                    break;

                case "N06":
                    //(*N11*)   ICMS.orig := 
                    NFe.det[nProd].Imposto.ICMS.orig = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.orig);
                    //(*N12*)   ICMS.CST := 
                    NFe.det[nProd].Imposto.ICMS.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                    break;

                case "N07":
                        //(*N11*)   ICMS.orig := 
                        NFe.det[nProd].Imposto.ICMS.orig = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.orig);
                        //(*N12*)   ICMS.CST := 
                        NFe.det[nProd].Imposto.ICMS.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                        //(*N13*)   ICMS.modBC := 
                        NFe.det[nProd].Imposto.ICMS.modBC = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.modBC);
                        //(*N14*)   ICMS.pRedBC := 
                        NFe.det[nProd].Imposto.ICMS.pRedBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pRedBC);
                        //(*N15*)   ICMS.vBC := 
                        NFe.det[nProd].Imposto.ICMS.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                        //(*N16*)   ICMS.pICMS := 
                        NFe.det[nProd].Imposto.ICMS.pICMS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pICMS);
                        //(*N17*)   ICMS.vICMS := 
                        NFe.det[nProd].Imposto.ICMS.vICMS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vICMS);
                    break;

                case "N08":
                        //(*N11*)   ICMS.orig := 
                        NFe.det[nProd].Imposto.ICMS.orig = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.orig);
                        //(*N12*)   ICMS.CST := 
                        NFe.det[nProd].Imposto.ICMS.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                        //(*N21*)   ICMS.vBCST := 
                        NFe.det[nProd].Imposto.ICMS.vBCST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBCST);
                        //(*N22*)   ICMS.pICMSST := 
                        NFe.det[nProd].Imposto.ICMS.pICMSST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pICMSST);
                    break;

                case "N09":
                        #region ICMS70

                        //(*N11*)   ICMS.orig := 
                        NFe.det[nProd].Imposto.ICMS.orig = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.orig);
                        //(*N12*)   ICMS.CST := 
                        NFe.det[nProd].Imposto.ICMS.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                        //(*N13*)   ICMS.modBC := 
                        NFe.det[nProd].Imposto.ICMS.modBC = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.modBC);
                        //(*N14*)   ICMS.pRedBC := 
                        NFe.det[nProd].Imposto.ICMS.pRedBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pRedBC);
                        //(*N15*)   ICMS.vBC := 
                        NFe.det[nProd].Imposto.ICMS.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                        //(*N16*)   ICMS.pICMS := 
                        NFe.det[nProd].Imposto.ICMS.pICMS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pICMS);
                        //(*N17*)   ICMS.vICMS := 
                        NFe.det[nProd].Imposto.ICMS.vICMS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vICMS);
                        //(*N18*)   ICMS.modBCST := 
                        NFe.det[nProd].Imposto.ICMS.modBCST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.modBCST);
                        //(*N19*)   ICMS.pMVAST := 
                        NFe.det[nProd].Imposto.ICMS.pMVAST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pMVAST);
                        //(*N20*)   ICMS.pRedBCST := 
                        NFe.det[nProd].Imposto.ICMS.pRedBCST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pRedBCST);
                        //(*N21*)   ICMS.vBCST := 
                        NFe.det[nProd].Imposto.ICMS.vBCST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBCST);
                        //(*N22*)   ICMS.pICMSST := 
                        NFe.det[nProd].Imposto.ICMS.pICMSST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pICMSST);
                        //(*N23*)   ICMS.vICMSST := 
                        NFe.det[nProd].Imposto.ICMS.vICMSST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vICMSST);

                        #endregion
                    break;

                case "N10":
                        #region ICMS90

                        //(*N11*)   ICMS.orig := 
                        NFe.det[nProd].Imposto.ICMS.orig = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.orig);
                        //(*N12*)   ICMS.CST := 
                        NFe.det[nProd].Imposto.ICMS.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                        //(*N13*)   ICMS.modBC := 
                        NFe.det[nProd].Imposto.ICMS.modBC = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.modBC);
                        //(*N15*)   ICMS.vBC := 
                        NFe.det[nProd].Imposto.ICMS.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                        //(*N14*)   ICMS.pRedBC := 
                        NFe.det[nProd].Imposto.ICMS.pRedBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pRedBC);
                        //(*N16*)   ICMS.pICMS := 
                        NFe.det[nProd].Imposto.ICMS.pICMS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pICMS);
                        //(*N17*)   ICMS.vICMS := 
                        NFe.det[nProd].Imposto.ICMS.vICMS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vICMS);
                        //(*N18*)   ICMS.modBCST := 
                        NFe.det[nProd].Imposto.ICMS.modBCST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.modBCST);
                        //(*N19*)   ICMS.pMVAST := 
                        NFe.det[nProd].Imposto.ICMS.pMVAST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pMVAST);
                        //(*N20*)   ICMS.pRedBCST := 
                        NFe.det[nProd].Imposto.ICMS.pRedBCST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pRedBCST);
                        //(*N21*)   ICMS.vBCST := 
                        NFe.det[nProd].Imposto.ICMS.vBCST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBCST);
                        //(*N22*)   ICMS.pICMSST := 
                        NFe.det[nProd].Imposto.ICMS.pICMSST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pICMSST);
                        //(*N23*)   ICMS.vICMSST := 
                        NFe.det[nProd].Imposto.ICMS.vICMSST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vICMSST);
 
	                    #endregion                    
                    break;

                case "O":
                    ///
                    /// Grupo da TAG <det><imposto><IPI>
                    /// 
                        //i := nProd;
                        //(*O02*)   IPI.clEnq := 
                        NFe.det[nProd].Imposto.IPI.clEnq = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.clEnq);
                        //(*O03*)   IPI.CNPJProd := 
                        NFe.det[nProd].Imposto.IPI.CNPJProd = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CNPJProd);
                        //(*O04*)   IPI.cSelo := 
                        NFe.det[nProd].Imposto.IPI.cSelo = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.cSelo);
                        //(*O05*)   IPI.qSelo := 
                        NFe.det[nProd].Imposto.IPI.qSelo = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.qSelo);
                        //(*O06*)   IPI.cEnq := 
                        NFe.det[nProd].Imposto.IPI.cEnq = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.cEnq);
                    break;

                case "O07":
                    ///
                    /// Grupo da TAG <det><imposto><IPITrib>
                    /// 
                    //i := nProd;

                    //(*O09*)
                    NFe.det[nProd].Imposto.IPI.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                    //(*O14*)
                    NFe.det[nProd].Imposto.IPI.vIPI = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vIPI);
                    break;

                case "O08":
                    ///
                    /// Grupo da TAG <det><imposto><IPINT>
                    /// 
                    //i := nProd;
                    //(*O09*)
                    NFe.det[nProd].Imposto.IPI.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                    break;

                case "O10":
                    ///
                    /// Grupo da TAG <det><imposto><IPI>
                    /// 
                    //i := nProd;

                    //(*O10*)
                    NFe.det[nProd].Imposto.IPI.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    //(*O13*)
                    NFe.det[nProd].Imposto.IPI.pIPI = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pIPI);
                    break;

                case "O11":
                    ///
                    /// Grupo da TAG <det><imposto><IPI>
                    /// 
                    //i := nProd;

                    //(*O11*)
                    NFe.det[nProd].Imposto.IPI.qUnid = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.qUnid);
                    //(*O12*)
                    NFe.det[nProd].Imposto.IPI.vUnid = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.vUnid);
                    break;

                case "P":
                    ///
                    /// Grupo da TAG <det><imposto><II>
                    /// 
                    //i := nProd;

                    //(*P02*)
                    NFe.det[nProd].Imposto.II.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    //(*P03*)
                    NFe.det[nProd].Imposto.II.vDespAdu = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vDespAdu);
                    //(*P04*)
                    NFe.det[nProd].Imposto.II.vII = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vII);
                    //(*P05*)
                    NFe.det[nProd].Imposto.II.vIOF = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vIOF);
                    break;

                case "Q02":
                    ///
                    /// Grupo da TAG <det><imposto><pis><pisaliq>
                    /// 
                    //i := nProd;

                    //(*Q06*)
                    NFe.det[nProd].Imposto.PIS.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                    //(*Q07*)
                    NFe.det[nProd].Imposto.PIS.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    //(*Q08*)
                    NFe.det[nProd].Imposto.PIS.pPIS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pPIS);
                    //(*Q09*)
                    NFe.det[nProd].Imposto.PIS.vPIS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vPIS);
                    break;

                case "Q03":
                    ///
                    /// Grupo da TAG <det><imposto><pis><pisqtde>
                    /// 
                    //i := nProd;
                    
                    //(*Q06*)
                    NFe.det[nProd].Imposto.PIS.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                    //(*Q10*)
                    NFe.det[nProd].Imposto.PIS.qBCProd = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.qBCProd);
                    //(*Q11*)
                    NFe.det[nProd].Imposto.PIS.vAliqProd = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.vAliqProd);
                    //(*Q09*)
                    NFe.det[nProd].Imposto.PIS.vPIS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vPIS);
                    break;

                case "Q04":
                    ///
                    /// Grupo da TAG <det><imposto><pis><pisNT>
                    /// 
                    //i := nProd;
                    
                    //(*Q06*)
                    NFe.det[nProd].Imposto.PIS.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                    break;

                case "Q05":
                    ///
                    /// Grupo da TAG <det><imposto><pis><pisPOutr>
                    /// 
                    //i := nProd;
                    //(*Q06*)
                    NFe.det[nProd].Imposto.PIS.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                    //(*Q09*)
                    NFe.det[nProd].Imposto.PIS.vPIS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vPIS);
                    break;

                case "Q07":
                    ///
                    /// Grupo da TAG <det><imposto><pis><pisqtde>
                    /// 
                    //i := nProd;

                    //(*Q07*)
                    NFe.det[nProd].Imposto.PIS.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    //(*Q08*)
                    NFe.det[nProd].Imposto.PIS.pPIS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pPIS);
                    break;

                case "Q10":
                    ///
                    /// Grupo da TAG <det><imposto><pis><pisqtde>
                    /// 
                    //i := nProd;

                    //(*Q10*)
                    NFe.det[nProd].Imposto.PIS.qBCProd = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.qBCProd);
                    //(*Q11*)
                    NFe.det[nProd].Imposto.PIS.vAliqProd = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.vAliqProd);
                    break;

                case "R":
                    ///
                    /// Grupo da TAG <det><imposto><pisST>
                    /// 
                    //i := nProd;

                    //(*R06*)
                    NFe.det[nProd].Imposto.PISST.vPIS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vPIS);
                    break;

                case "R02":
                    ///
                    /// Grupo da TAG <det><imposto><pisST>
                    /// 
                    //i := nProd;

                    //(*R02*)
                    NFe.det[nProd].Imposto.PISST.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    //(*R03*)
                    NFe.det[nProd].Imposto.PISST.pPis = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pPIS);
                    break;

                case "R04":
                    ///
                    /// Grupo da TAG <det><imposto><pisST>
                    /// 
                    //i := nProd;
                    //(*R04*)
                    NFe.det[nProd].Imposto.PISST.qBCProd = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.qBCProd);
                    //(*R05*)
                    NFe.det[nProd].Imposto.PISST.vAliqProd = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.vAliqProd);
                    break;

                case "S02":
                    ///
                    /// Grupo da TAG <det><imposto><COFINS>
                    /// 
                    //i := nProd;
                    //(*S06*)
                    NFe.det[nProd].Imposto.COFINS.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                    //(*S07*)
                    NFe.det[nProd].Imposto.COFINS.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    //(*S08*)
                    NFe.det[nProd].Imposto.COFINS.pCOFINS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pCOFINS);
                    //(*S11*)
                    NFe.det[nProd].Imposto.COFINS.vCOFINS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vCOFINS);
                    break;

                case "S03":
                    ///
                    /// Grupo da TAG <det><imposto><COFINS>
                    /// 
                    //i := nProd;
                    //(*S06*)
                    NFe.det[nProd].Imposto.COFINS.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                    //(*S07*)
                    NFe.det[nProd].Imposto.COFINS.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    //(*S09*)
                    NFe.det[nProd].Imposto.COFINS.qBCProd = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.qBCProd);
                    //(*S10*)
                    NFe.det[nProd].Imposto.COFINS.vAliqProd = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.vAliqProd);
                    //(*S11*)
                    NFe.det[nProd].Imposto.COFINS.vCOFINS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vCOFINS);
                    break;

                case "S04":
                    ///
                    /// Grupo da TAG <det><imposto><COFINS>
                    /// 
                    //i := nProd;
                    //(*S06*)
                    NFe.det[nProd].Imposto.COFINS.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                    break;

                case "S05":
                    ///
                    /// Grupo da TAG <det><imposto><COFINS>
                    /// 
                    //i := nProd;
                    //(*S06*)
                    NFe.det[nProd].Imposto.COFINS.CST = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CST);
                    //(*S11*)
                    NFe.det[nProd].Imposto.COFINS.vCOFINS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vCOFINS);
                    break;

                case "S07":
                    ///
                    /// Grupo da TAG <det><imposto><COFINS>
                    /// 
                    //i := nProd;
                    //(*S07*)
                    NFe.det[nProd].Imposto.COFINS.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    //(*S08*)
                    NFe.det[nProd].Imposto.COFINS.pCOFINS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pCOFINS);
                    break;

                case "S09":
                    ///
                    /// Grupo da TAG <det><imposto><COFINS>
                    /// 
                    //i := nProd;
                    //(*S09*)
                    NFe.det[nProd].Imposto.COFINS.qBCProd = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.qBCProd);
                    //(*S10*)
                    NFe.det[nProd].Imposto.COFINS.vAliqProd = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.vAliqProd);
                    break;

                case "T":
                    ///
                    /// Grupo da TAG <det><imposto><COFINSST>
                    /// 
                    //i := nProd;
                    //(*T06*)
                    NFe.det[nProd].Imposto.COFINSST.vCOFINS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vCOFINS);
                    break;

                case "T02":
                    ///
                    /// Grupo da TAG <det><imposto><COFINSST>
                    /// 
                    //i := nProd;
                    //(*T02*)
                    NFe.det[nProd].Imposto.COFINSST.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    //(*T03*)
                    NFe.det[nProd].Imposto.COFINSST.pCOFINS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pCOFINS);
                    break;

                case "T04":
                    ///
                    /// Grupo da TAG <det><imposto><COFINSST>
                    /// 
                    //i := nProd;
                    //(*T04*)
                    NFe.det[nProd].Imposto.COFINSST.qBCProd = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.qBCProd);
                    //(*T05*)
                    NFe.det[nProd].Imposto.COFINSST.vAliqProd = (double)LerCampo(TpcnTipoCampo.tcDec4, Properties.Resources.vAliqProd);
                    break;

                case "U":
                    ///
                    /// Grupo da TAG <det><imposto><ISSQN>
                    /// 
                    //i := nProd;
                    //(*U02*)
                    NFe.det[nProd].Imposto.ISSQN.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    //(*U03*)
                    NFe.det[nProd].Imposto.ISSQN.vAliq = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vAliq);
                    //(*U04*)
                    NFe.det[nProd].Imposto.ISSQN.vISSQN = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vISSQN);
                    //(*U05*)
                    NFe.det[nProd].Imposto.ISSQN.cMunFG = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.cMunFG);
                    //(*U06*)
                    NFe.det[nProd].Imposto.ISSQN.cListServ = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.cListServ);
                    break;

                case "W02":
                    ///
                    /// Grupo da TAG <total><ICMSTot>
                    /// 
                    //(*W03*)
                    NFe.Total.ICMSTot.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    //(*W04*)
                    NFe.Total.ICMSTot.vICMS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vICMS);
                    //(*W05*)
                    NFe.Total.ICMSTot.vBCST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBCST);
                    //(*W06*)
                    NFe.Total.ICMSTot.vST = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vST);
                    //(*W07*)
                    NFe.Total.ICMSTot.vProd = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vProd);
                    //(*W08*)
                    NFe.Total.ICMSTot.vFrete = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vFrete);
                    //(*W09*)
                    NFe.Total.ICMSTot.vSeg = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vSeg);
                    //(*W10*)
                    NFe.Total.ICMSTot.vDesc = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vDesc);
                    //(*W11*)
                    NFe.Total.ICMSTot.vII = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vII);
                    //(*W12*)
                    NFe.Total.ICMSTot.vIPI = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vIPI);
                    //(*W13*)
                    NFe.Total.ICMSTot.vPIS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vPIS);
                    //(*W14*)
                    NFe.Total.ICMSTot.vCOFINS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vCOFINS);
                    //(*W15*)
                    NFe.Total.ICMSTot.vOutro = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vOutro);
                    //(*W16*)
                    NFe.Total.ICMSTot.vNF = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vNF);
                    break;

                case "W17":
                    ///
                    /// Grupo da TAG <total><ISSQNtot>
                    /// 
                    //(*W18*)
                    NFe.Total.ISSQNtot.vServ = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vServ);
                    //(*W19*)
                    NFe.Total.ISSQNtot.vBC = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    //(*W20*)
                    NFe.Total.ISSQNtot.vISS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vISS);
                    //(*W21*)
                    NFe.Total.ISSQNtot.vPIS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vPIS);
                    //(*W22*)
                    NFe.Total.ISSQNtot.vCOFINS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vCOFINS);
                    break;

                case "W23":
                    ///
                    /// Grupo da TAG <total><retTrib>
                    /// 
                    //(*W24*)
                    NFe.Total.retTrib.vRetPIS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vRetPIS);
                    //(*W25*)
                    NFe.Total.retTrib.vRetCOFINS = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vRetCOFINS);
                    //(*W26*)
                    NFe.Total.retTrib.vRetCSLL = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vRetCSLL);
                    //(*W27*)
                    NFe.Total.retTrib.vBCIRRF = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBCIRRF);
                    //(*W28*)
                    NFe.Total.retTrib.vIRRF = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vIRRF);
                    //(*W29*)
                    NFe.Total.retTrib.vBCRetPrev = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBCRetPrev);
                    //(*W30*)
                    NFe.Total.retTrib.vRetPrev = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vRetPrev);
                    break;

                case "X":
                    ///
                    /// Grupo da TAG <transp>
                    /// 
                    //(*X02*)
                    NFe.Transp.modFrete = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.modFrete);
                    break;

                case "X03":
                    ///
                    /// Grupo da TAG <transp><TRansportadora>
                    /// 
                    //(*X06*)
                    NFe.Transp.Transporta.xNome = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xNome);
                    //(*X07*)
                    NFe.Transp.Transporta.IE = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.IE);
                    //(*X08*)
                    NFe.Transp.Transporta.xEnder = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xEnder);
                    //(*X09*)
                    NFe.Transp.Transporta.xMun = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xMun);
                    //(*X10*)
                    NFe.Transp.Transporta.UF = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.UF);
                    break;

                case "X04":
                    //(*X04*)
                    NFe.Transp.Transporta.CNPJCPF = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CNPJ);
                    break;

                case "X05":
                    //(*X05*)
                    NFe.Transp.Transporta.CNPJCPF = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.CPF);
                    break;

                case "X11":
                    ///
                    /// Grupo da TAG <transp><retTransp>
                    /// 
                    //(*X12*)
                    NFe.Transp.retTransp.vServ = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vServ);
                    //(*X13*)
                    NFe.Transp.retTransp.vBCRet = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vBCRet);
                    //(*X14*)
                    NFe.Transp.retTransp.pICMSRet = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.pICMSRet);
                    //(*X15*)
                    NFe.Transp.retTransp.vICMSRet = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vICMSRet);
                    //(*X16*)
                    NFe.Transp.retTransp.CFOP = (string)LerCampo(TpcnTipoCampo.tcEsp, Properties.Resources.CFOP);
                    //(*X17*)
                    NFe.Transp.retTransp.cMunFG = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.cMunFG);
                    break;

                case "X18":
                    ///
                    /// Grupo da TAG <transp><veicTransp>
                    /// 
                    //(*X19*)
                    NFe.Transp.veicTransp.placa = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.placa);
                    //(*X20*)
                    NFe.Transp.veicTransp.UF = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.UF);
                    //(*X21*)
                    NFe.Transp.veicTransp.RNTC = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.RNTC);
                    break;

                case "X22":
                    ///
                    /// Grupo da TAG <transp><reboque>
                    /// 
                    //NFe.Transp.Reboque.add;
                    //i := NFe.Transp.Reboque.Count - 1;
                    NFe.Transp.Reboque.Add(new Reboque());
                    //(*X23*)
                    NFe.Transp.Reboque[NFe.Transp.Reboque.Count-1].placa = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.placa);
                    //(*X24*)
                    NFe.Transp.Reboque[NFe.Transp.Reboque.Count-1].UF = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.UF);
                    //(*X25*)
                    NFe.Transp.Reboque[NFe.Transp.Reboque.Count-1].RNTC = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.RNTC);
                    break;

                case "X26":
                    ///
                    /// Grupo da TAG <transp><vol>
                    /// 
                    //NFe.Transp.Vol.add;
                    //i := NFe.Transp.Vol.Count - 1;
                    NFe.Transp.Vol.Add(new Vol());
                    //(*X27*)
                    NFe.Transp.Vol[NFe.Transp.Vol.Count - 1].qVol = (int)LerCampo(TpcnTipoCampo.tcInt, Properties.Resources.qVol);
                    //(*X28*)
                    NFe.Transp.Vol[NFe.Transp.Vol.Count - 1].esp = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.esp);
                    //(*X29*)
                    NFe.Transp.Vol[NFe.Transp.Vol.Count - 1].marca = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.marca);
                    //(*X30*)
                    NFe.Transp.Vol[NFe.Transp.Vol.Count - 1].nVol = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.nVol);
                    //(*X31*)
                    NFe.Transp.Vol[NFe.Transp.Vol.Count - 1].pesoL = (double)LerCampo(TpcnTipoCampo.tcDec3, Properties.Resources.pesoL);
                    //(*X32*)
                    NFe.Transp.Vol[NFe.Transp.Vol.Count - 1].pesoB = (double)LerCampo(TpcnTipoCampo.tcDec3, Properties.Resources.pesoB);
                    break;

                case "X33":
                    ///
                    /// Grupo da TAG <transp><vol><lacres>
                    /// 
                    //i := NFe.Transp.Vol.Count - 1;
                    //NFe.transp.Vol[i].lacres.add;
                    //j := NFe.transp.Vol[i].lacres.Count - 1;

                    Lacres lacreItem = new Lacres();
                    //(*X34*)
                    lacreItem.nLacre = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.nLacre);

                    NFe.Transp.Vol[NFe.Transp.Vol.Count - 1].Lacres.Add(lacreItem);
                    break;

                case "Y02":
                    ///
                    /// Grupo da TAG <cobr>
                    /// 
                    //(*Y03*)
                    NFe.Cobr.Fat.nFat = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.nFat);
                    //(*Y04*)
                    NFe.Cobr.Fat.vOrig = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vOrig);
                    //(*Y05*)
                    NFe.Cobr.Fat.vDesc = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vDesc);
                    //(*Y06*)
                    NFe.Cobr.Fat.vLiq = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vLiq);
                    break;

                case "Y07":
                    ///
                    /// Grupo da TAG <cobr><dup>
                    /// 
                    //NFe.Cobr.Dup.Add;
                    //i := NFe.Cobr.Dup.Count - 1;
                    NFe.Cobr.Dup.Add(new Dup());

                    //(*Y08*)
                    NFe.Cobr.Dup[NFe.Cobr.Dup.Count - 1].nDup = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.nDup);
                    //(*Y09*)
                    NFe.Cobr.Dup[NFe.Cobr.Dup.Count - 1].dVenc = (DateTime)LerCampo(TpcnTipoCampo.tcDat, Properties.Resources.dVenc);
                    //(*Y10*)
                    NFe.Cobr.Dup[NFe.Cobr.Dup.Count - 1].vDup = (double)LerCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vDup);
                    break;

                case "Z":
                    ///
                    /// Grupo da TAG <InfAdic>
                    /// 
                    //(*Z02*)
                    NFe.InfAdic.infAdFisco = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.infAdFisco);
                    //(*Z03*)
                    NFe.InfAdic.infCpl = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.infCpl);
                    break;

                case "Z04":
                    ///
                    /// Grupo da TAG <infAdic><obsCont>
                    /// 
                    //NFe.InfAdic.obsCont.Add;
                    //i := NFe.InfAdic.obsCont.Count - 1;
                    NFe.InfAdic.obsCont.Add(new obsCont());

                    //(*Z05*)
                    NFe.InfAdic.obsCont[NFe.InfAdic.obsCont.Count - 1].xCampo = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xCampo);
                    //(*Z06*)
                    NFe.InfAdic.obsCont[NFe.InfAdic.obsCont.Count - 1].xTexto = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xTexto);
                    break;

                case "Z07":
                    ///
                    /// Grupo da TAG <infAdic><obsFisco>
                    /// 
                    //NFe.InfAdic.obsFisco.Add;
                    //i := NFe.InfAdic.obsFisco.Count - 1;
                    NFe.InfAdic.obsFisco.Add(new obsFisco());

                    //(*Z08*)
                    NFe.InfAdic.obsFisco[NFe.InfAdic.obsFisco.Count - 1].xCampo = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xCampo);
                    //(*Z09*)
                    NFe.InfAdic.obsFisco[NFe.InfAdic.obsFisco.Count - 1].xTexto = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xTexto);
                    break;

                case "Z10":
                    ///
                    /// Grupo da TAG <infAdic><procRef>
                    /// 
                    //NFe.InfAdic.procRef.Add;
                    //i := NFe.InfAdic.procRef.Count - 1;
                    NFe.InfAdic.procRef.Add(new procRef());

                    //(*Z11*)
                    NFe.InfAdic.procRef[NFe.InfAdic.procRef.Count - 1].nProc = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.nProc);
                    //(*Z12*)
                    NFe.InfAdic.procRef[NFe.InfAdic.procRef.Count - 1].indProc = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.indProc);
                    break;

                case "ZA":
                    ///
                    /// Grupo da TAG <exporta>
                    /// 
                    //(*ZA02*)
                    NFe.exporta.UFEmbarq = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.UFEmbarq);
                    //(*ZA03*)
                    NFe.exporta.xLocEmbarq = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xLocEmbarq);
                    break;

                case "ZB":
                    ///
                    /// Grupo da TAG <compra>
                    /// 
                    //(*ZB02*)
                    NFe.compra.xNEmp = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xNEmp);
                    //(*ZB03*)
                    NFe.compra.xPed = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xPed);
                    //(*ZB04*)
                    NFe.compra.xCont = (string)LerCampo(TpcnTipoCampo.tcStr, Properties.Resources.xCont);
                    break;
            }
        }

        internal class myConvert
        {
            public static double ToDouble(string val)
            {
                if (string.IsNullOrEmpty(val) || (val != null && val.Length == 0)) return 0.0;

                char charSeparator = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0];

                if (val.IndexOf(charSeparator, 0) < 0)
                    val += charSeparator + "00";

                DecimalConverter dc = new DecimalConverter();
                try
                {
                    decimal dd = (decimal)dc.ConvertFromString(val);
                    return decimal.ToDouble(dd);
                }
                catch
                {
                    val = GetRightStrDigit(val);
                }
                if (val.Length == 0) return 0.0;

                if (val[val.Length - 1] == charSeparator)
                    val = val.Remove(val.Length - 1, 1);
                return decimal.ToDouble((decimal)dc.ConvertFromString(val));
            }

            private static string GetRightStrDigit(string val)
            {
                int pt = val.IndexOf('.');
                int vg = val.IndexOf(',');
                if (vg != -1 && pt != -1)
                {
                    string v1;
                    if (pt > vg)
                    {
                        v1 = val.Substring(pt + 1, val.Length - pt - 1);
                        val = val.Remove(pt, val.Length - pt);
                        val = val.Replace(",", "") + '.' + v1;
                    }
                    else
                    {
                        v1 = val.Substring(vg + 1, val.Length - vg - 1);
                        val = val.Remove(vg, val.Length - vg);
                        val = val.Replace(".", "") + '.' + v1;
                    }
                }

                string rightVal = string.Empty;
                bool rightSymb = false;
                int sepCount = 0;
                for (int i = 0; i < val.Length; i++)
                {
                    rightSymb = false;
                    char ch = val[i];
                    rightSymb = char.IsDigit(ch);
                    if (rightVal.Length == 0 && ch == '-') rightSymb = true;
                    if (char.IsPunctuation(ch) && sepCount == 0)
                    {
                        if (ch == ',' || ch == '.')
                        {
                            ch = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0];
                            sepCount++;
                            rightSymb = true;
                        }
                    }
                    if (rightSymb) rightVal += ch;
                }
                return rightVal;
            }
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
}