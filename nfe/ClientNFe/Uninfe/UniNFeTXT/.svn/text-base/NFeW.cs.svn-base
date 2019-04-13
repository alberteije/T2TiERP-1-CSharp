using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Globalization;

namespace UniNFeTXT
{
    public class NFeW
    {
        public string cMensagemErro { get; set; }
        public string cFileName { get; private set; }

        private XmlDocument doc;
        private XmlNode nodePai = null;

        /// <summary>
        /// GerarXml
        /// </summary>
        /// <param name="NFe"></param>
        public void GerarXml(NFe NFe, string cDestino)
        {
            doc = new XmlDocument();
            XmlDeclaration node = doc.CreateXmlDeclaration("1.0", "utf-8", "");
            doc.InsertBefore(node, doc.DocumentElement);
            ///
            /// NFe
            /// 
            XmlNode xmlInf = doc.CreateElement("NFe");
            if (NFe.ide.cUF != 29)  //Bahia
            {
                XmlAttribute xmlVersion = doc.CreateAttribute("xmlns:xsi");
                xmlVersion.Value = "http://www.w3.org/2001/XMLSchema-instance";
                xmlInf.Attributes.Append(xmlVersion);
            }
            XmlAttribute xmlVersion1 = doc.CreateAttribute("xmlns");
            xmlVersion1.Value = "http://www.portalfiscal.inf.br/nfe";
            xmlInf.Attributes.Append(xmlVersion1);
            doc.AppendChild(xmlInf);
            ///
            /// infNFe
            /// 
            XmlElement infNfe = doc.CreateElement("infNFe");
            XmlAttribute infNfeAttr1 = doc.CreateAttribute("versao");
            infNfeAttr1.Value = "1.10";
            infNfe.Attributes.Append(infNfeAttr1);
            XmlAttribute infNfeAttrId = doc.CreateAttribute("Id");

            string cChave = NFe.ide.cUF.ToString() + 
                            NFe.ide.dEmi.Year.ToString("0000").Substring(2) +
                            NFe.ide.dEmi.Month.ToString("00"); //data AAMM

            Int64 iTmp = Convert.ToInt64("0" + NFe.emit.CNPJCPF);
            cChave += iTmp.ToString("00000000000000") + "55";

            if (NFe.ide.cNF == 0)
            {
                ///
                /// gera codigo aleatorio
                /// 
                NFe.ide.cNF = this.GerarCodigoNumerico(NFe.ide.nNF);
            }
            if (NFe.ide.cDV == 0)
            {
                ///
                /// calcula do digito verificador
                /// 
                string ccChave = cChave + NFe.ide.serie.ToString("000") + NFe.ide.nNF.ToString("000000000") + NFe.ide.cNF.ToString("000000000");

                NFe.ide.cDV = this.GerarDigito(ccChave);
            }
            cChave += NFe.ide.serie.ToString("000") + 
                        NFe.ide.nNF.ToString("000000000") + 
                        NFe.ide.cNF.ToString("000000000") + 
                        NFe.ide.cDV.ToString("0");
            NFe.infNFe.ID = cChave;

            infNfeAttrId.Value = "NFe" + NFe.infNFe.ID;
            infNfe.Attributes.Append(infNfeAttrId);
            xmlInf.AppendChild(infNfe);

            infNfe.AppendChild(GerarInfNFe(NFe));
            infNfe.AppendChild(GerarEmit(NFe));
            GerarAvulsa(NFe, infNfe);
            infNfe.AppendChild(GerarDest(NFe));
            GerarRetirada(NFe, infNfe);
            GerarEntrega(NFe, infNfe);
            GerarDet(NFe, infNfe);
            GerarTotal(NFe, infNfe);
            GerarTransp(NFe.Transp, infNfe);
            GerarCobr(NFe.Cobr, infNfe);
            GerarInfAdic(NFe.InfAdic, infNfe);
            GerarExporta(NFe.exporta, infNfe);
            GerarCompra(NFe.compra, infNfe);

            this.cFileName = NFe.infNFe.ID + "-nfe.xml";

            if (string.IsNullOrEmpty(cDestino))
                doc.Save(this.cFileName);
            else
            {
                if (cDestino.Substring(cDestino.Length - 1, 1) == @"\")
                    cDestino = cDestino.Substring(0, cDestino.Length - 1);

                if (!Directory.Exists(cDestino + "\\convertidos"))
                {
                    ///
                    /// cria uma pasta temporária para armazenar o XML convertido
                    /// 
                    System.IO.Directory.CreateDirectory(cDestino + "\\convertidos");
                }
                doc.Save(cDestino + "\\convertidos\\" + this.cFileName);
            }
        }

        private void GerarAvulsa(NFe NFe, XmlElement root)
        {
            if (!string.IsNullOrEmpty(NFe.avulsa.CNPJ))
            {
                XmlElement ELav = doc.CreateElement("avulsa");
                nodePai = ELav;
                root.AppendChild(ELav);

                wCampo(NFe.avulsa.CNPJ, TpcnTipoCampo.tcStr, Properties.Resources.CNPJ);
                wCampo(NFe.avulsa.xOrgao, TpcnTipoCampo.tcStr, Properties.Resources.xOrgao);
                wCampo(NFe.avulsa.matr, TpcnTipoCampo.tcStr, Properties.Resources.matr);
                wCampo(NFe.avulsa.xAgente, TpcnTipoCampo.tcStr, Properties.Resources.xAgente);
                wCampo(NFe.avulsa.fone, TpcnTipoCampo.tcStr, Properties.Resources.fone);
                wCampo(NFe.avulsa.UF, TpcnTipoCampo.tcStr, Properties.Resources.UF);
                //if not ValidarUF(nfe.Avulsa.UF) then
                //  wAlerta("D07", Properties.Resources.UF, Properties.Resources.UF, ERR_MSG_INVALIDO);
                wCampo(NFe.avulsa.nDAR, TpcnTipoCampo.tcStr, Properties.Resources.nDAR);
                wCampo(NFe.avulsa.dEmi, TpcnTipoCampo.tcDat, Properties.Resources.dEmi);
                wCampo(NFe.avulsa.vDAR, TpcnTipoCampo.tcDec2, Properties.Resources.vDAR);
                wCampo(NFe.avulsa.repEmi, TpcnTipoCampo.tcStr, Properties.Resources.repEmi);
                wCampo(NFe.avulsa.dPag, TpcnTipoCampo.tcDat, Properties.Resources.dPag);
            }
        }

        private void GerarCobr(Cobr Cobr, XmlElement root)
        {
            if (!string.IsNullOrEmpty(Cobr.Fat.nFat) ||
                (Cobr.Fat.vOrig > 0) ||
                (Cobr.Fat.vDesc > 0) ||
                (Cobr.Fat.vLiq > 0) ||
                (Cobr.Dup.Count > 0))
            {
                XmlElement nodeCobr = doc.CreateElement("cobr");
                nodePai = nodeCobr;
                root.AppendChild(nodeCobr);
                //
                //(**)GerarCobrFat;
                //
                if ((!string.IsNullOrEmpty(Cobr.Fat.nFat) ||
                    (Cobr.Fat.vOrig > 0) ||
                    (Cobr.Fat.vDesc > 0) ||
                    (Cobr.Fat.vLiq > 0)))
                {
                    XmlElement nodeFat = doc.CreateElement("fat");
                    nodeCobr.AppendChild(nodeFat);
                    nodePai = nodeFat;
                    
                    wCampo(TpcnTipoCampo.tcStr,  Properties.Resources.nFat, 01, 60, 0, Cobr.Fat.nFat);
                    wCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vOrig, 01, 15, 0, Cobr.Fat.vOrig);
                    wCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vDesc, 01, 15, 0, Cobr.Fat.vDesc);
                    wCampo(TpcnTipoCampo.tcDec2, Properties.Resources.vLiq, 01, 15, 0, Cobr.Fat.vLiq);
                }
                //
                //(**)GerarCobrDup;
                //
                foreach (Dup Dup in Cobr.Dup)
                {
                    XmlElement nodeDup = doc.CreateElement("dup");
                    nodeCobr.AppendChild(nodeDup);
                    nodePai = nodeDup;

                    wCampo(TpcnTipoCampo.tcStr, Properties.Resources.nDup, 01, 60, 0, Dup.nDup);
                    wCampo(TpcnTipoCampo.tcDat, Properties.Resources.dVenc, 10, 10, 0, Dup.dVenc);
                    wCampo(TpcnTipoCampo.tcDec2,Properties.Resources.vDup, 01, 15, 0, Dup.vDup);
                }
            }
        }

        public Int32 GerarCodigoNumerico(Int32 numeroNF)
        {
            string s;
            Int32 i, j, k;

            // Essa função gera um código numerico atravéz de calculos realizados sobre o parametro numero
            s = numeroNF.ToString("000000000");
            for (i = 0; i < 9; ++i)
            {
                k = 0;
                for (j = 0; j < 9; ++j)
                    k += Convert.ToInt32(s[j]) * (j + 1);
                s = (k % 11).ToString().Trim() + s;
            }
            return Convert.ToInt32(s.Substring(0, 9));
        }

        private void GerarCompra(Compra compra, XmlElement root)
        {
            if (!string.IsNullOrEmpty(compra.xNEmp) || !string.IsNullOrEmpty(compra.xPed) || !string.IsNullOrEmpty(compra.xCont))
            {
                nodePai = doc.CreateElement("compra");
                root.AppendChild(nodePai);

                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.xNEmp, 01, 17, 0, compra.xNEmp);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.xPed, 01, 60, 0, compra.xPed);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.xCont, 01, 60, 0, compra.xCont);
            }
        }

        private XmlElement GerarDest(NFe NFe)
        {
            XmlElement e0 = doc.CreateElement("dest");
            nodePai = e0;

            if (NFe.dest.enderDest.UF != "EX")
                wCampo(NFe.dest.CNPJCPF, TpcnTipoCampo.tcStr, (NFe.dest.CNPJCPF.Length == 11 ? Properties.Resources.CPF : Properties.Resources.CNPJ));
            else
                wCampo("", TpcnTipoCampo.tcStr, Properties.Resources.CNPJ);
            
            wCampo(NFe.dest.xNome, TpcnTipoCampo.tcStr, Properties.Resources.xNome);
            ///
            /// (**)GerarDestEnderDest(UF);
            /// 
            //AjustarMunicipioUF(xUF, xMun, cMun, nfe.Dest.enderDest.cPais, nfe.Dest.enderDest.UF, nfe.Dest.enderDest.xMun, nfe.Dest.enderDest.cMun);
            //UF = xUF;
            XmlElement e1 = doc.CreateElement("enderDest");
            e0.AppendChild(e1);
            nodePai = e1;
            
            wCampo(NFe.dest.enderDest.xLgr, TpcnTipoCampo.tcStr, Properties.Resources.xLgr);
            wCampo(NFe.dest.enderDest.nro, TpcnTipoCampo.tcStr, Properties.Resources.nro);
            wCampo(NFe.dest.enderDest.xCpl, TpcnTipoCampo.tcStr, Properties.Resources.xCpl, false);
            wCampo(NFe.dest.enderDest.xBairro, TpcnTipoCampo.tcStr, Properties.Resources.xBairro);
            wCampo(NFe.dest.enderDest.cMun, TpcnTipoCampo.tcInt, Properties.Resources.cMun, true, 7);
            //if not ValidarMunicipio(cMun) then
            //  wAlerta("E10", Properties.Resources.cMun, Properties.Resources.cMun, ERR_MSG_INVALIDO);
            wCampo(NFe.dest.enderDest.xMun, TpcnTipoCampo.tcStr, Properties.Resources.xMun);
            wCampo(NFe.dest.enderDest.UF, TpcnTipoCampo.tcStr, Properties.Resources.UF);
            //if not ValidarUF(xUF) then
            //  wAlerta("E12", Properties.Resources.UF, Properties.Resources.UF, ERR_MSG_INVALIDO);
            wCampo(NFe.dest.enderDest.CEP, TpcnTipoCampo.tcInt, Properties.Resources.CEP, false, 8);
            wCampo(NFe.dest.enderDest.cPais, TpcnTipoCampo.tcInt, Properties.Resources.cPais, false, 4);
            //if not ValidarCodigoPais(nfe.Dest.enderDest.cPais) = -1 then
            //  wAlerta("E14", Properties.Resources.cPais, Properties.Resources.cPais, ERR_MSG_INVALIDO);
            wCampo(NFe.dest.enderDest.xPais, TpcnTipoCampo.tcStr, Properties.Resources.xPais, false);
            wCampo(NFe.dest.enderDest.fone, TpcnTipoCampo.tcStr, Properties.Resources.fone, false);
            ///
            /// </enderDest">
            /// 
            nodePai = e0;
            if (NFe.dest.enderDest.UF != "EX")
            {
                // Inscrição Estadual
                wCampo(NFe.dest.IE, TpcnTipoCampo.tcStr, Properties.Resources.IE);
                //if (NFe.dest.CNPJCPF.Length == 11 && !string.IsNullOrEmpty(NFe.dest.IE))
                //  wAlerta("E17", Properties.Resources.IE, DSC_IE, ERR_MSG_INVALIDO);
                //if (FOpcoes.ValidarInscricoes) and (nfe.Dest.IE <> "") then
                //  if not ValidarIE(nfe.Dest.IE, UF) then
                //      wAlerta("E17", Properties.Resources.IE, DSC_IE, ERR_MSG_INVALIDO);
                //
                wCampo(NFe.dest.ISUF, TpcnTipoCampo.tcStr, Properties.Resources.ISUF, false);
                //if (FOpcoes.ValidarInscricoes) and (nfe.Dest.ISUF <> "") then
                //  if not ValidarISUF(nfe.Dest.ISUF) then
                //      wAlerta("E18", Properties.Resources.ISUF, DSC_ISUF, ERR_MSG_INVALIDO);
                //
            }
            else
                wCampo("", TpcnTipoCampo.tcStr, Properties.Resources.IE);

            return e0;
        }

        private void GerarDet(NFe NFe, XmlElement root)
        {
            if (NFe.det.Count > 990)
                this.cMensagemErro += "Número máximo de itens excedeu o máximo permitido" + Environment.NewLine;

            foreach (Det det in NFe.det)
            {
                XmlElement rootDet = doc.CreateElement("det");
                XmlAttribute xmlItem = doc.CreateAttribute("nItem");
                xmlItem.Value = det.Prod.nItem.ToString();
                rootDet.Attributes.Append(xmlItem);
                root.AppendChild(rootDet);

                ///
                /// Linha do produto
                /// 
                XmlElement nodeProd = doc.CreateElement("prod");
                rootDet.AppendChild(nodeProd);
                nodePai = nodeProd;

                wCampo(det.Prod.cProd, TpcnTipoCampo.tcStr, Properties.Resources.cProd);
                wCampo(det.Prod.cEAN, TpcnTipoCampo.tcStr, Properties.Resources.cEAN);
                wCampo(det.Prod.xProd, TpcnTipoCampo.tcStr, Properties.Resources.xProd);
                wCampo(det.Prod.NCM, TpcnTipoCampo.tcStr, Properties.Resources.NCM, false);
                wCampo(det.Prod.EXTIPI, TpcnTipoCampo.tcStr, Properties.Resources.EXTIPI, false);
                wCampo(det.Prod.genero, TpcnTipoCampo.tcInt, Properties.Resources.genero, false, 2);
                wCampo(det.Prod.CFOP, TpcnTipoCampo.tcEsp, Properties.Resources.CFOP);
                wCampo(det.Prod.uCom, TpcnTipoCampo.tcStr, Properties.Resources.uCom);
                wCampo(det.Prod.qCom, TpcnTipoCampo.tcDec4, Properties.Resources.qCom);
                wCampo(det.Prod.vUnCom, TpcnTipoCampo.tcDec4, Properties.Resources.vUnCom);
                wCampo(det.Prod.vProd, TpcnTipoCampo.tcDec2, Properties.Resources.vProd);
                wCampo(det.Prod.cEANTrib, TpcnTipoCampo.tcStr, Properties.Resources.cEANTrib);
                wCampo(det.Prod.uTrib, TpcnTipoCampo.tcStr, Properties.Resources.uTrib);
                wCampo(det.Prod.qTrib, TpcnTipoCampo.tcDec4, Properties.Resources.qTrib);
                wCampo(det.Prod.vUnTrib, TpcnTipoCampo.tcDec4, Properties.Resources.vUnTrib);
                wCampo(det.Prod.vFrete, TpcnTipoCampo.tcDec2, Properties.Resources.vFrete, false);
                wCampo(det.Prod.vSeg, TpcnTipoCampo.tcDec2, Properties.Resources.vSeg, false);
                wCampo(det.Prod.vDesc, TpcnTipoCampo.tcDec2, Properties.Resources.vDesc, false);
                ///
                GerarDetProdDI(det.Prod.DI, nodeProd);
                GerarDetProdVeicProd(det.Prod.veicProd, nodeProd);
                GerarDetProdMed(det.Prod.med, nodeProd);
                GerarDetProdArma(det.Prod.arma, nodeProd);
                GerarDetProdComb(det.Prod.comb, nodeProd);
                GerarDetImposto(det.Imposto, rootDet);
                //
                nodePai = rootDet;
                wCampo(det.infAdProd, TpcnTipoCampo.tcStr, Properties.Resources.infAdProd, false);
            }
        }

        private void GerarDetProdArma(List<Arma> armaList, XmlElement root)
        {
                foreach(Arma arma in armaList)
                {
                    XmlElement e0 = doc.CreateElement("arma");
                    root.AppendChild(e0);
                    nodePai=e0;

                    wCampo(arma.tpArma, TpcnTipoCampo.tcStr, Properties.Resources.tpArma);
                    wCampo(arma.nSerie, TpcnTipoCampo.tcInt, Properties.Resources.nSerie);
                    wCampo(arma.nCano, TpcnTipoCampo.tcInt, Properties.Resources.nCano);
                    wCampo(arma.descr, TpcnTipoCampo.tcStr, Properties.Resources.descr);
                }
        }

        private void GerarDetProdComb(Comb comb, XmlElement root)
        {
                if ((comb.cProdANP > 0) ||
                    (!string.IsNullOrEmpty(comb.CODIF)) ||
                    (comb.qTemp > 0) ||
                    (comb.CIDE.qBCprod > 0) ||
                    (comb.CIDE.vAliqProd > 0) ||
                    (comb.CIDE.vCIDE > 0) ||
                    (comb.ICMS.vBCICMS > 0) ||
                    (comb.ICMS.vICMS > 0) ||
                    (comb.ICMS.vBCICMSST > 0) ||
                    (comb.ICMS.vICMSST > 0) ||
                    (comb.ICMSInter.vBCICMSSTDest > 0) ||
                    (comb.ICMSInter.vICMSSTDest > 0) ||
                    (comb.ICMSCons.vBCICMSSTCons > 0) ||
                    (comb.ICMSCons.vICMSSTCons > 0) ||
                    (!string.IsNullOrEmpty(comb.ICMSCons.UFcons)))
                {
                    XmlElement e0 = doc.CreateElement("comb");
                    root.AppendChild(e0);
                    nodePai = e0;

                    wCampo(comb.cProdANP, TpcnTipoCampo.tcInt, Properties.Resources.cProdANP);
                    wCampo(comb.CODIF, TpcnTipoCampo.tcEsp, Properties.Resources.CODIF);
                    wCampo(comb.qTemp, TpcnTipoCampo.tcDec4, Properties.Resources.qTemp);
                    //
                    //(**)GerarDetProdCombCIDE(i);
                    //
                    if ((comb.CIDE.qBCprod > 0) ||
                        (comb.CIDE.vAliqProd > 0) ||
                        (comb.CIDE.vCIDE > 0))
                    {
                        XmlElement e1 = doc.CreateElement("CIDE");
                        e0.AppendChild(e1);
                        nodePai = e1;
        
                        wCampo(comb.CIDE.qBCprod, TpcnTipoCampo.tcDec4, Properties.Resources.qBCProd);
                        wCampo(comb.CIDE.vAliqProd, TpcnTipoCampo.tcDec4, Properties.Resources.vAliqProd);
                        wCampo(comb.CIDE.vCIDE, TpcnTipoCampo.tcDec2, Properties.Resources.vCIDE);

                        //nodePai = e0;
                    }
                    //
                    //(**)GerarDetProdCombICMS(i);
                    //
                    if ((comb.ICMS.vBCICMS>0) ||
                        (comb.ICMS.vICMS>0) ||
                        (comb.ICMS.vBCICMSST>0)||
                        (comb.ICMS.vICMSST>0))
                    {
                        XmlElement e2 = doc.CreateElement("ICMSComb");
                        e0.AppendChild(e2);
                        nodePai = e2;
        
                        wCampo(comb.ICMS.vBCICMS, TpcnTipoCampo.tcDec2, Properties.Resources.vBCICMS);
                        wCampo(comb.ICMS.vICMS, TpcnTipoCampo.tcDec2, Properties.Resources.vICMS);
                        wCampo(comb.ICMS.vBCICMSST, TpcnTipoCampo.tcDec2, Properties.Resources.vBCICMSST);
                        wCampo(comb.ICMS.vICMSST, TpcnTipoCampo.tcDec2, Properties.Resources.vICMSST);
                    }
                    //
                    //(**)GerarDetProdCombICMSInter(i);
                    //
                    if ((comb.ICMSInter.vBCICMSSTDest > 0) || (comb.ICMSInter.vICMSSTDest > 0))
                    {
                        XmlElement e2 = doc.CreateElement("ICMSInter");
                        e0.AppendChild(e2);
                        nodePai = e2;
        
                        wCampo(comb.ICMSInter.vBCICMSSTDest, TpcnTipoCampo.tcDec2, Properties.Resources.vBCICMSSTDest);
                        wCampo(comb.ICMSInter.vICMSSTDest, TpcnTipoCampo.tcDec2, Properties.Resources.vICMSSTDest);
                    }
                    //
                    //(**)GerarDetProdCombICMSCons(i);
                    //
                    if ((comb.ICMSCons.vBCICMSSTCons > 0) ||
                        (comb.ICMSCons.vICMSSTCons > 0) ||
                        (!string.IsNullOrEmpty(comb.ICMSCons.UFcons)))
                    {
                        XmlElement e2 = doc.CreateElement("ICMSCons");
                        e0.AppendChild(e2);
                        nodePai = e2;
        
                        wCampo(comb.ICMSCons.vBCICMSSTCons, TpcnTipoCampo.tcDec2, Properties.Resources.vBCICMSSTCons);
                        wCampo(comb.ICMSCons.vICMSSTCons, TpcnTipoCampo.tcDec2, Properties.Resources.vICMSSTCons);
                        wCampo(comb.ICMSCons.UFcons, TpcnTipoCampo.tcStr, Properties.Resources.UFcons);
                        //if not ValidarUF(nfe.Det[i].Prod.comb.ICMSCons.UFcons) then
                        //  wAlerta("L120", Properties.Resources.UFcons, DSC_UFCONS, ERR_MSG_INVALIDO);
                    }
                }
        }

        private void GerarDetProdDI(List<DI> diList, XmlElement root)
        {
            XmlNode oldNode = nodePai;

            foreach (DI di in diList)
            {
                XmlElement e0 = doc.CreateElement("DI");
                root.AppendChild(e0);
                nodePai = e0;

                wCampo(di.nDi, TpcnTipoCampo.tcStr, Properties.Resources.nDI);
                wCampo(di.dDi, TpcnTipoCampo.tcDat, Properties.Resources.dDI);
                wCampo(di.xLocDesemb, TpcnTipoCampo.tcStr, Properties.Resources.xLocDesemb);
                wCampo(di.UFDesemb, TpcnTipoCampo.tcStr, Properties.Resources.UFDesemb);
                //if not ValidarUF(nfe.Det[i].Prod.DI[j].UFDesemb) then
                //  wAlerta("I22", Properties.Resources.UFDesemb, DSC_UFDESEMB, ERR_MSG_INVALIDO);
                wCampo(di.dDesemb, TpcnTipoCampo.tcDat, Properties.Resources.dDesemb);
                wCampo(di.cExportador, TpcnTipoCampo.tcStr, Properties.Resources.cExportador);
                //
                //GerarDetProdDIadi
                //
                foreach (Adi adi in di.adi)
                {
                    XmlElement e1 = doc.CreateElement("adi");
                    e0.AppendChild(e1);
                    nodePai = e1;

                    wCampo(adi.nAdicao, TpcnTipoCampo.tcInt, Properties.Resources.nAdicao);
                    wCampo(adi.nSeqAdi, TpcnTipoCampo.tcInt, Properties.Resources.nSeqAdic);
                    wCampo(adi.cFabricante, TpcnTipoCampo.tcStr, Properties.Resources.cFabricante);
                    wCampo(adi.vDescDI, TpcnTipoCampo.tcDec2, Properties.Resources.vDescDI, false);
                }
                nodePai = oldNode;
            }
        }

        private void GerarDetProdMed(List<Med> medList, XmlElement root)
        {
            foreach(Med med in medList)
            {
                XmlElement e0 = doc.CreateElement("med");
                root.AppendChild(e0);
                nodePai = e0;

                wCampo(med.nLote, TpcnTipoCampo.tcStr, Properties.Resources.nLote);
                wCampo(med.qLote, TpcnTipoCampo.tcDec3, Properties.Resources.qLote);
                wCampo(med.dFab, TpcnTipoCampo.tcDat, Properties.Resources.dFab);
                wCampo(med.dVal, TpcnTipoCampo.tcDat, Properties.Resources.dVal);
                wCampo(med.vPMC, TpcnTipoCampo.tcDec2, Properties.Resources.vPMC);
            }
        }

        private void GerarDetProdVeicProd(veicProd veicProd, XmlElement root)
        {
            if (!string.IsNullOrEmpty(veicProd.chassi))
            {
                XmlNode oldNode = nodePai;
                try
                {
                    XmlElement e0 = doc.CreateElement("veicProd");
                    root.AppendChild(e0);
                    nodePai = e0;

                    wCampo(veicProd.tpOP, TpcnTipoCampo.tcStr, Properties.Resources.tpOp);
                    wCampo(veicProd.chassi, TpcnTipoCampo.tcStr, Properties.Resources.chassi);
                    wCampo(veicProd.cCor, TpcnTipoCampo.tcStr, Properties.Resources.cCor);
                    wCampo(veicProd.xCor, TpcnTipoCampo.tcStr, Properties.Resources.xCor);
                    wCampo(veicProd.pot, TpcnTipoCampo.tcStr, Properties.Resources.pot);
                    wCampo(veicProd.CM3, TpcnTipoCampo.tcStr, Properties.Resources.CM3);
                    wCampo(veicProd.pesoL, TpcnTipoCampo.tcStr, Properties.Resources.pesoL);
                    wCampo(veicProd.pesoB, TpcnTipoCampo.tcStr, Properties.Resources.pesoB);
                    wCampo(veicProd.nSerie, TpcnTipoCampo.tcStr, Properties.Resources.nSerie);
                    wCampo(veicProd.tpComb, TpcnTipoCampo.tcStr, Properties.Resources.tpComb);
                    wCampo(veicProd.nMotor, TpcnTipoCampo.tcStr, Properties.Resources.nMotor);
                    wCampo(veicProd.CMKG, TpcnTipoCampo.tcStr, Properties.Resources.CMKG);
                    wCampo(veicProd.dist, TpcnTipoCampo.tcStr, Properties.Resources.dist);
                    wCampo(veicProd.RENAVAM, TpcnTipoCampo.tcStr, Properties.Resources.RENAVAM, false);
                    wCampo(veicProd.anoMod, TpcnTipoCampo.tcInt, Properties.Resources.anoMod, true, 4);
                    wCampo(veicProd.anoFab, TpcnTipoCampo.tcInt, Properties.Resources.anoFab, true, 4);
                    wCampo(veicProd.tpPint, TpcnTipoCampo.tcStr, Properties.Resources.tpPint);
                    wCampo(veicProd.tpVeic, TpcnTipoCampo.tcInt, Properties.Resources.tpVeic);
                    wCampo(veicProd.espVeic, TpcnTipoCampo.tcInt, Properties.Resources.espVeic);
                    wCampo(veicProd.VIN, TpcnTipoCampo.tcStr, Properties.Resources.VIN);
                    wCampo(veicProd.condVeic, TpcnTipoCampo.tcStr, Properties.Resources.condVeic);
                    wCampo(veicProd.cMod, TpcnTipoCampo.tcStr, Properties.Resources.cMod);
                }
                finally
                {
                    nodePai = oldNode;
                }
            }
        }

        public Int32 GerarDigito(string chave)
        {
            int i, j, Digito;
            const string PESO = "4329876543298765432987654329876543298765432";

            chave = chave.Replace("NFe", "");
            if (chave.Length != 43)
            {
                this.cMensagemErro += string.Format("Erro na composição da chave [{0}] para obter o DV", chave) + Environment.NewLine;
                return 0;
            }
            else
            {
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
                    this.cMensagemErro += string.Format("Erro no cálculo do DV da chave [{0}]", chave) + Environment.NewLine;
                return Digito;
            }
        }

        private XmlElement GerarEmit(NFe NFe)
        {
            XmlElement ELemit = doc.CreateElement("emit");
            nodePai = ELemit;

            wCampo(NFe.emit.CNPJCPF, TpcnTipoCampo.tcStr, (NFe.emit.CNPJCPF.Length==11 ? Properties.Resources.CPF : Properties.Resources.CNPJ));
            wCampo(NFe.emit.xNome, TpcnTipoCampo.tcStr, Properties.Resources.xNome);
            wCampo(NFe.emit.xFant, TpcnTipoCampo.tcStr, Properties.Resources.xFant, false);
            ///
            /// <enderEmit>
            /// 
            XmlElement el = doc.CreateElement("enderEmit");
            nodePai.AppendChild(el);
            nodePai = el;
            wCampo(NFe.emit.enderEmit.xLgr, TpcnTipoCampo.tcStr, Properties.Resources.xLgr);
            wCampo(NFe.emit.enderEmit.nro, TpcnTipoCampo.tcStr, Properties.Resources.nro);
            wCampo(NFe.emit.enderEmit.xCpl, TpcnTipoCampo.tcStr, Properties.Resources.xCpl, false);
            wCampo(NFe.emit.enderEmit.xBairro, TpcnTipoCampo.tcStr, Properties.Resources.xBairro);
            wCampo(NFe.emit.enderEmit.cMun, TpcnTipoCampo.tcInt, Properties.Resources.cMun, true,7);
            //if not ValidarMunicipio(nfe.Emit.EnderEmit.cMun) then
            //  wAlerta("C10", Properties.Resources.cMun, Properties.Resources.cMun, ERR_MSG_INVALIDO);
            wCampo(NFe.emit.enderEmit.xMun, TpcnTipoCampo.tcStr, Properties.Resources.xMun);
            wCampo(NFe.emit.enderEmit.UF, TpcnTipoCampo.tcStr, Properties.Resources.UF);
            //if not ValidarUF(xUF) then
            //  wAlerta("C12", Properties.Resources.UF, Properties.Resources.UF, ERR_MSG_INVALIDO);
            wCampo(NFe.emit.enderEmit.CEP, TpcnTipoCampo.tcInt, Properties.Resources.CEP, false, 8);
            wCampo(NFe.emit.enderEmit.cPais, TpcnTipoCampo.tcInt, Properties.Resources.cPais, false);
            wCampo(NFe.emit.enderEmit.xPais, TpcnTipoCampo.tcStr, Properties.Resources.xPais, false);
            wCampo(NFe.emit.enderEmit.fone, TpcnTipoCampo.tcStr, Properties.Resources.fone, false);
            ///
            /// </enderEmit>
            /// 
            nodePai = ELemit;
            wCampo(NFe.emit.IE, TpcnTipoCampo.tcStr, Properties.Resources.IE);
            //if (FOpcoes.ValidarInscricoes) and (nfe.Ide.procEmi <> peAvulsaFisco) then
            //{
            //    if (NFe.emit.IE.Length == 0)
            //        wAlerta("C17", Properties.Resources.IE, DSC_IE, ERR_MSG_VAZIO)
            //    else
            //    {
            //        if not ValidarIE(nfe.Emit.IE, CodigoParaUF(nfe.Ide.cUF)) then
            //            wAlerta("C17", Properties.Resources.IE, DSC_IE, ERR_MSG_INVALIDO);
            //    }
            //}
            wCampo(NFe.emit.IEST, TpcnTipoCampo.tcStr, Properties.Resources.IEST, false);
            //wCampo(NFe.emit.IM, TpcnTipoCampo.tcStr, Properties.Resources.IM, false);
            //if (NFe.emit.IM.Length > 0)
              //  wCampo(NFe.emit.CNAE, TpcnTipoCampo.tcStr, Properties.Resources.CNAE);

            return ELemit;
        }

        private void GerarEntrega(NFe NFe, XmlElement root)
        {
            if (!string.IsNullOrEmpty(NFe.entrega.xLgr))
            {
                XmlElement e0 = doc.CreateElement("entrega");
                root.AppendChild(e0);
                nodePai = e0;

                //AjustarMunicipioUF(xUF, xMun, cMun, nfe.Dest.enderDest.cPais, nfe.Entrega.UF, nfe.Entrega.xMun, nfe.Entrega.cMun);
                wCampo(NFe.entrega.CNPJ, TpcnTipoCampo.tcStr, (NFe.entrega.CNPJ.Length == 11 ? Properties.Resources.CPF : Properties.Resources.CNPJ));
                wCampo(NFe.entrega.xLgr, TpcnTipoCampo.tcStr, Properties.Resources.xLgr);
                wCampo(NFe.entrega.nro, TpcnTipoCampo.tcStr, Properties.Resources.nro);
                wCampo(NFe.entrega.xCpl, TpcnTipoCampo.tcStr, Properties.Resources.xCpl, false);
                wCampo(NFe.entrega.xBairro, TpcnTipoCampo.tcStr, Properties.Resources.xBairro);
                wCampo(NFe.entrega.cMun, TpcnTipoCampo.tcInt, Properties.Resources.cMun, true, 7);
                //if not ValidarMunicipio(cMun) then
                //  wAlerta("F07", Properties.Resources.cMun, Properties.Resources.cMun, ERR_MSG_INVALIDO);
                wCampo(NFe.entrega.xMun, TpcnTipoCampo.tcStr, Properties.Resources.xMun);
                wCampo(NFe.entrega.UF, TpcnTipoCampo.tcStr, Properties.Resources.UF);
                //if not ValidarUF(xUF) then
                //  wAlerta("G09", Properties.Resources.UF, Properties.Resources.UF, ERR_MSG_INVALIDO);
            }
        }

        private void GerarExporta(Exporta exporta, XmlElement root)
        {
            if (!string.IsNullOrEmpty(exporta.UFEmbarq) || !string.IsNullOrEmpty(exporta.xLocEmbarq))
            {
                nodePai  = doc.CreateElement("exporta");
                root.AppendChild(nodePai);

                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.UFEmbarq, 02, 02, 1, exporta.UFEmbarq);
                //if not ValidarUF(nfe.exporta.UFembarq) then
                //  wAlerta("ZA02", Properties.Resources.UFembarq, DSC_UFEMBARQ, ERR_MSG_INVALIDO);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.xLocEmbarq, 01, 60, 1, exporta.xLocEmbarq);
            }
        }

        private void GerarInfAdic(InfAdic InfAdic, XmlElement root)
        {
            if ((!string.IsNullOrEmpty(InfAdic.infAdFisco)) ||
                (!string.IsNullOrEmpty(InfAdic.infCpl)) ||
                (InfAdic.obsCont.Count > 1) ||
                (InfAdic.obsFisco.Count > 1) ||
                (InfAdic.procRef.Count > 1))
            {
                XmlElement nodeinfAdic = doc.CreateElement("infAdic");
                root.AppendChild(nodeinfAdic);
                nodePai = nodeinfAdic;

                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.infAdFisco, 01, 0256, 0, InfAdic.infAdFisco);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.infCpl, 01, 5000, 0, InfAdic.infCpl);
                //
                //(**)GerarInfAdicObsCont;
                //
                if (InfAdic.obsCont.Count > 10)
                    this.cMensagemErro += "obsCont: Excedeu o máximo permitido de 10" + Environment.NewLine;

                foreach(obsCont obsCont in InfAdic.obsCont)
                {
                    XmlElement nodeobsCont = doc.CreateElement("obsCont");
                    XmlAttribute xmlItem = doc.CreateAttribute(Properties.Resources.xCampo);
                    xmlItem.Value = obsCont.xCampo;
                    nodeobsCont.Attributes.Append(xmlItem);
                    nodeinfAdic.AppendChild(nodeobsCont);
                    nodePai = nodeobsCont;

                    //if length(trim(nfe.InfAdic.obsCont[i].xCampo)) > 20 then
                    //  wAlerta("ZO5", Properties.Resources.xCampo, DSC_XCAMPO, ERR_MSG_MAIOR);
                    //if length(trim(nfe.InfAdic.obsCont[i].xCampo)) = 0 then
                    //  wAlerta("ZO5", Properties.Resources.xCampo, DSC_XCAMPO, ERR_MSG_VAZIO);
                    wCampo(TpcnTipoCampo.tcStr, Properties.Resources.xTexto, 01, 60, 1, obsCont.xTexto);
                }
                //
                //(**)GerarInfAdicObsFisco;
                //
                if (InfAdic.obsFisco.Count > 10)
                    this.cMensagemErro += "obsFisco: Excedeu o máximo permitido de 10" + Environment.NewLine;

                foreach(obsFisco obsFisco in InfAdic.obsFisco)
                {
                    XmlElement nodeobsFisco = doc.CreateElement("obsFisco");
                    XmlAttribute xmlItem = doc.CreateAttribute(Properties.Resources.xCampo);
                    xmlItem.Value = obsFisco.xCampo;
                    nodeobsFisco.Attributes.Append(xmlItem);
                    nodeinfAdic.AppendChild(nodeobsFisco);
                    nodePai = nodeobsFisco;

                    //if length(trim(nfe.InfAdic.obsFisco[i].xCampo)) > 20 then
                    //  wAlerta("ZO8", Properties.Resources.xCampo, DSC_XCAMPO, ERR_MSG_MAIOR);
                    //if length(trim(nfe.InfAdic.obsFisco[i].xCampo)) = 0 then
                    //  wAlerta("ZO8", Properties.Resources.xCampo, DSC_XCAMPO, ERR_MSG_VAZIO);
                    wCampo(TpcnTipoCampo.tcStr, Properties.Resources.xTexto, 01, 60, 1, obsFisco.xTexto);
                }
                //
                //(**)GerarInfAdicProcRef;
                //
                foreach(procRef procRef in InfAdic.procRef)
                {
                    XmlElement nodeprocRef = doc.CreateElement("procRef");
                    nodeinfAdic.AppendChild(nodeprocRef);
                    nodePai = nodeprocRef;

                    wCampo(TpcnTipoCampo.tcStr, Properties.Resources.nProc, 01, 60, 1, procRef.nProc);
                    wCampo(TpcnTipoCampo.tcStr, Properties.Resources.indProc, 01, 01, 1, procRef.indProc);
                }
            }
        }

        private XmlElement GerarInfNFe(NFe Nfe)
        {
            XmlElement ELide = doc.CreateElement("ide");

            nodePai = ELide;
            wCampo(Nfe.ide.cUF, TpcnTipoCampo.tcInt, Properties.Resources.cUF);
            wCampo(Nfe.ide.cNF, TpcnTipoCampo.tcInt, Properties.Resources.cNF, true, 9);
            wCampo(Nfe.ide.natOp, TpcnTipoCampo.tcStr, Properties.Resources.natOp);
            wCampo(Nfe.ide.indPag, TpcnTipoCampo.tcInt, Properties.Resources.indPag);
            wCampo(Nfe.ide.mod, TpcnTipoCampo.tcInt, Properties.Resources.mod);
            wCampo(Nfe.ide.serie, TpcnTipoCampo.tcInt, Properties.Resources.serie);
            wCampo(Nfe.ide.nNF, TpcnTipoCampo.tcInt, Properties.Resources.nNF);
            wCampo(Nfe.ide.dEmi, TpcnTipoCampo.tcDat, Properties.Resources.dEmi);
            wCampo(Nfe.ide.dSaiEnt, TpcnTipoCampo.tcDat, Properties.Resources.dSaiEnt, false);
            wCampo(Nfe.ide.tpNF, TpcnTipoCampo.tcInt, Properties.Resources.tpNF);
            wCampo(Nfe.ide.cMunFG, TpcnTipoCampo.tcInt, Properties.Resources.cMunFG);

            // Gera TAGs referentes a NFe referência
            foreach(NFref refNFe in Nfe.ide.NFref)
            {
                if (!string.IsNullOrEmpty(refNFe.refNFe))
                {
                    XmlElement ep = doc.CreateElement(Properties.Resources.NFref);
                    XmlElement el = doc.CreateElement(Properties.Resources.refNFe);
                    el.InnerText = refNFe.refNFe;
                    ep.AppendChild(el);
                    ELide.AppendChild(ep);
                }
            }
            // Gera TAGs se NÃO for uma NFe referência
            foreach (NFref refNFe in Nfe.ide.NFref)
            {
                if (string.IsNullOrEmpty(refNFe.refNFe))
                {
                    XmlElement ep = doc.CreateElement(Properties.Resources.NFref);
                    XmlElement el = doc.CreateElement("refNF");
                    ep.AppendChild(el);
                    ELide.AppendChild(ep);
                    nodePai = el;

                    wCampo(refNFe.RefNF.cUF, TpcnTipoCampo.tcInt, Properties.Resources.cUF);
                    //if not ValidarCodigoUF(nfe.Ide.NFref[i].RefNF.cUF) then
                    //  wAlerta("B15", Properties.Resources.cUF, DSC_CUF, ERR_MSG_INVALIDO);
                    wCampo(refNFe.RefNF.AAMM, TpcnTipoCampo.tcEsp, Properties.Resources.AAMM);
                    //if not ValidarAAMM(nfe.Ide.NFref[i].RefNF.AAMM) then
                    //  wAlerta("B16", Properties.Resources.AAMM, DSC_AAMM, "Periodo inválido");
                    wCampo(refNFe.RefNF.CNPJ, TpcnTipoCampo.tcStr, Properties.Resources.CNPJ);
                    wCampo(refNFe.RefNF.mod, TpcnTipoCampo.tcStr, Properties.Resources.mod);
                    //if not ValidarMod(nfe.Ide.NFref[i].RefNF.Modelo) then
                    //  wAlerta("B18", Properties.Resources.mod, DSC_MOD, "Modelo de documento inválido");
                    wCampo(refNFe.RefNF.serie, TpcnTipoCampo.tcInt, Properties.Resources.serie);
                    wCampo(refNFe.RefNF.nNF, TpcnTipoCampo.tcInt, Properties.Resources.nNF);
                }
            }
            nodePai = ELide;
            wCampo(Nfe.ide.tpImp, TpcnTipoCampo.tcInt, Properties.Resources.tpImp);
            wCampo(Nfe.ide.tpEmis, TpcnTipoCampo.tcInt, Properties.Resources.tpEmis);
            wCampo(Nfe.ide.cDV, TpcnTipoCampo.tcInt, Properties.Resources.cDV);
            wCampo(Nfe.ide.tpAmb, TpcnTipoCampo.tcInt, Properties.Resources.tpAmb);
            wCampo(Nfe.ide.finNFe, TpcnTipoCampo.tcInt, Properties.Resources.finNFe);
            wCampo(Nfe.ide.procEmi, TpcnTipoCampo.tcStr, Properties.Resources.procEmi);
            wCampo(Nfe.ide.verProc, TpcnTipoCampo.tcStr, Properties.Resources.verProc);

            return ELide;
        }

        private void GerarDetImposto(Imposto imposto, XmlElement root)
        {
            XmlElement nodeImposto = doc.CreateElement("imposto");
            root.AppendChild(nodeImposto);

            GerarDetImpostoICMS(imposto.ICMS, nodeImposto);
            GerarDetImpostoIPI(imposto.IPI, nodeImposto);
            GerarDetImpostoII(imposto.II, nodeImposto);
            GerarDetImpostoPIS(imposto.PIS, nodeImposto);
            GerarDetImpostoPISST(imposto.PISST, nodeImposto);
            GerarDetImpostoCOFINS(imposto.COFINS, nodeImposto);
            GerarDetImpostoCOFINSST(imposto.COFINSST, nodeImposto);
            GerarDetImpostoISSQN(imposto.ISSQN, nodeImposto);
        }

        private void GerarDetImpostoCOFINS(COFINS COFINS, XmlElement nodeImposto)
        {
            if (!string.IsNullOrEmpty(COFINS.CST))
            {
                XmlElement node0 = doc.CreateElement("COFINS");

                switch (COFINS.CST)
                {
                    case "01":
                    case "02":
                        {
                            nodePai = doc.CreateElement("COFINSAliq");
                            node0.AppendChild(nodePai);
                            nodeImposto.AppendChild(node0);

                            wCampo(COFINS.CST, TpcnTipoCampo.tcStr, Properties.Resources.CST);
                            wCampo(COFINS.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                            wCampo(COFINS.pCOFINS, TpcnTipoCampo.tcDec2, Properties.Resources.pCOFINS);
                            wCampo(COFINS.vCOFINS, TpcnTipoCampo.tcDec2, Properties.Resources.vCOFINS);
                        }
                        break;

                    case "03":
                        {
                            nodePai = doc.CreateElement("COFINSQtde");
                            node0.AppendChild(nodePai);
                            nodeImposto.AppendChild(node0);

                            wCampo(COFINS.CST, TpcnTipoCampo.tcStr, Properties.Resources.CST);
                            wCampo(COFINS.qBCProd, TpcnTipoCampo.tcDec4, Properties.Resources.qBCProd);
                            wCampo(COFINS.vAliqProd, TpcnTipoCampo.tcDec4, Properties.Resources.vAliqProd);
                            wCampo(COFINS.vCOFINS, TpcnTipoCampo.tcDec2, Properties.Resources.vCOFINS);
                        }
                        break;

                    case "04":
                    case "06":
                    case "07":
                    case "08":
                    case "09":
                        {
                            nodePai = doc.CreateElement("COFINSNT");
                            node0.AppendChild(nodePai);
                            nodeImposto.AppendChild(node0);

                            wCampo(COFINS.CST, TpcnTipoCampo.tcStr, Properties.Resources.CST);
                        }
                        break;

                    case "99":
                        {
                            if ((COFINS.vBC + COFINS.pCOFINS > 0) && (COFINS.qBCProd + COFINS.vAliqProd > 0))
                                this.cMensagemErro += "COFINSOutr: As TAG <vBC> e <pCOFINS> não podem ser informadas em conjunto com as TAG <qBCProd> e <vAliqProd>" + Environment.NewLine;

                            nodePai = doc.CreateElement("COFINSOutr");
                            node0.AppendChild(nodePai);
                            nodeImposto.AppendChild(node0);

                            wCampo(COFINS.CST, TpcnTipoCampo.tcStr, Properties.Resources.CST);

                            if (COFINS.qBCProd + COFINS.vAliqProd > 0)
                            {
                                wCampo(COFINS.qBCProd, TpcnTipoCampo.tcDec4, Properties.Resources.qBCProd);
                                wCampo(COFINS.vAliqProd, TpcnTipoCampo.tcDec4, Properties.Resources.vAliqProd);
                            }
                            else
                            {
                                wCampo(COFINS.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                                wCampo(COFINS.pCOFINS, TpcnTipoCampo.tcDec2, Properties.Resources.pCOFINS);
                            }
                            wCampo(COFINS.vCOFINS, TpcnTipoCampo.tcDec2, Properties.Resources.vCOFINS);
                        }
                        break;
                }
            }
            COFINS.CST = "";
            COFINS.pCOFINS =
                COFINS.qBCProd =
                COFINS.vAliqProd =
                COFINS.vBC =
                COFINS.vBCProd =
                COFINS.vCOFINS = 0;
        }

        private void GerarDetImpostoCOFINSST(COFINSST COFINSST, XmlElement nodeImposto)
        {
            if ((COFINSST.vBC > 0) ||
                (COFINSST.pCOFINS > 0) ||
                (COFINSST.qBCProd > 0) ||
                (COFINSST.vAliqProd > 0) ||
                (COFINSST.vCOFINS > 0))
            {
                if ((COFINSST.vBC + COFINSST.pCOFINS > 0) && (COFINSST.qBCProd + COFINSST.vAliqProd > 0))
                    this.cMensagemErro += "COFINSST: As TAG <vBC> e <pCOFINS> não podem ser informadas em conjunto com as TAG <qBCProd> e <vAliqProd>" + Environment.NewLine;

                XmlElement node0 = doc.CreateElement("COFINSST");

                if (COFINSST.vBC + COFINSST.pCOFINS > 0)
                {
                    nodePai = node0;
                    nodeImposto.AppendChild(node0);

                    wCampo(COFINSST.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    wCampo(COFINSST.pCOFINS, TpcnTipoCampo.tcDec2, Properties.Resources.pCOFINS);
                    wCampo(COFINSST.vCOFINS, TpcnTipoCampo.tcDec2, Properties.Resources.vCOFINS);
                }
                if (COFINSST.qBCProd + COFINSST.vAliqProd > 0)
                {
                    nodePai = node0;
                    nodeImposto.AppendChild(node0);

                    wCampo(COFINSST.qBCProd, TpcnTipoCampo.tcDec4, Properties.Resources.qBCProd);
                    wCampo(COFINSST.vAliqProd, TpcnTipoCampo.tcDec4, Properties.Resources.vAliqProd);
                    wCampo(COFINSST.vCOFINS, TpcnTipoCampo.tcDec2, Properties.Resources.vCOFINS);
                }
            }
            COFINSST.vBC =
                COFINSST.pCOFINS =
                COFINSST.qBCProd =
                COFINSST.vAliqProd =
                COFINSST.vCOFINS = 0;
        }

        private void GerarDetImpostoICMS(ICMS ICMS, XmlElement nodeImposto)
        {
            if (!string.IsNullOrEmpty(ICMS.CST))
            {
                if ((ICMS.CST == "41") || (ICMS.CST == "50"))
                    ICMS.CST = "40";

                XmlElement e0 = doc.CreateElement("ICMS");
                nodePai = doc.CreateElement("ICMS" + ICMS.CST);
                e0.AppendChild(nodePai);
                nodeImposto.AppendChild(e0);

                wCampo(ICMS.orig, TpcnTipoCampo.tcInt, Properties.Resources.orig);
                wCampo(ICMS.CST, TpcnTipoCampo.tcStr, Properties.Resources.CST);
                //
                switch (ICMS.CST)
                {
                    case "00":
                        wCampo(ICMS.modBC, TpcnTipoCampo.tcStr, Properties.Resources.modBC);
                        wCampo(ICMS.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                        wCampo(ICMS.pICMS, TpcnTipoCampo.tcDec2, Properties.Resources.pICMS);
                        wCampo(ICMS.vICMS, TpcnTipoCampo.tcDec2, Properties.Resources.vICMS);
                        break;

                    case "10":
                        wCampo(ICMS.modBC, TpcnTipoCampo.tcStr, Properties.Resources.modBC);
                        wCampo(ICMS.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                        wCampo(ICMS.pICMS, TpcnTipoCampo.tcDec2, Properties.Resources.pICMS);
                        wCampo(ICMS.vICMS, TpcnTipoCampo.tcDec2, Properties.Resources.vICMS);
                        wCampo(ICMS.modBCST, TpcnTipoCampo.tcStr, Properties.Resources.modBCST);
                        wCampo(ICMS.pMVAST, TpcnTipoCampo.tcDec2, Properties.Resources.pMVAST, false);
                        wCampo(ICMS.pRedBCST, TpcnTipoCampo.tcDec2, Properties.Resources.pRedBCST, false);
                        wCampo(ICMS.vBCST, TpcnTipoCampo.tcDec2, Properties.Resources.vBCST);
                        wCampo(ICMS.pICMSST, TpcnTipoCampo.tcDec2, Properties.Resources.pICMSST);
                        wCampo(ICMS.vICMSST, TpcnTipoCampo.tcDec2, Properties.Resources.vICMSST);
                        break;

                    case "20":
                        wCampo(ICMS.modBC, TpcnTipoCampo.tcStr, Properties.Resources.modBC);
                        wCampo(ICMS.pRedBC, TpcnTipoCampo.tcDec2, Properties.Resources.pRedBC);
                        wCampo(ICMS.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                        wCampo(ICMS.pICMS, TpcnTipoCampo.tcDec2, Properties.Resources.pICMS);
                        wCampo(ICMS.vICMS, TpcnTipoCampo.tcDec2, Properties.Resources.vICMS);
                        break;

                    case "30":
                        wCampo(ICMS.modBCST, TpcnTipoCampo.tcStr, Properties.Resources.modBCST);
                        wCampo(ICMS.pMVAST, TpcnTipoCampo.tcDec2, Properties.Resources.pMVAST, false);
                        wCampo(ICMS.pRedBCST, TpcnTipoCampo.tcDec2, Properties.Resources.pRedBCST, false);
                        wCampo(ICMS.vBCST, TpcnTipoCampo.tcDec2, Properties.Resources.vBCST);
                        wCampo(ICMS.pICMSST, TpcnTipoCampo.tcDec2, Properties.Resources.pICMSST);
                        wCampo(ICMS.vICMSST, TpcnTipoCampo.tcDec2, Properties.Resources.vICMSST);
                        break;

                    case "51":
                        //Esse bloco fica a critério de cada UF a obrigação das informações, conforme o manual
                        wCampo(ICMS.modBC, TpcnTipoCampo.tcStr, Properties.Resources.modBC, false);
                        wCampo(ICMS.pRedBC, TpcnTipoCampo.tcDec2, Properties.Resources.pRedBC, false);
                        wCampo(ICMS.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC, false);
                        wCampo(ICMS.pICMS, TpcnTipoCampo.tcDec2, Properties.Resources.pICMS, false);
                        wCampo(ICMS.vICMS, TpcnTipoCampo.tcDec2, Properties.Resources.vICMS, false);
                        break;

                    case "60":
                        wCampo(ICMS.vBCST, TpcnTipoCampo.tcDec2, Properties.Resources.vBCST);
                        wCampo(ICMS.vICMSST, TpcnTipoCampo.tcDec2, Properties.Resources.vICMSST);
                        break;

                    case "70":
                        wCampo(ICMS.modBC, TpcnTipoCampo.tcStr, Properties.Resources.modBC);
                        wCampo(ICMS.pRedBC, TpcnTipoCampo.tcDec2, Properties.Resources.pRedBC);
                        wCampo(ICMS.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                        wCampo(ICMS.pICMS, TpcnTipoCampo.tcDec2, Properties.Resources.pICMS);
                        wCampo(ICMS.vICMS, TpcnTipoCampo.tcDec2, Properties.Resources.vICMS);
                        wCampo(ICMS.modBCST, TpcnTipoCampo.tcStr, Properties.Resources.modBCST);
                        wCampo(ICMS.pMVAST, TpcnTipoCampo.tcDec2, Properties.Resources.pMVAST, false);
                        wCampo(ICMS.pRedBCST, TpcnTipoCampo.tcDec2, Properties.Resources.pRedBCST, false);
                        wCampo(ICMS.vBCST, TpcnTipoCampo.tcDec2, Properties.Resources.vBCST);
                        wCampo(ICMS.pICMSST, TpcnTipoCampo.tcDec2, Properties.Resources.pICMSST);
                        wCampo(ICMS.vICMSST, TpcnTipoCampo.tcDec2, Properties.Resources.vICMSST);
                        break;

                    case "90":
                        wCampo(ICMS.modBC, TpcnTipoCampo.tcStr, Properties.Resources.modBC);
                        wCampo(ICMS.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                        wCampo(ICMS.pRedBC, TpcnTipoCampo.tcDec2, Properties.Resources.pRedBC, false);
                        wCampo(ICMS.pICMS, TpcnTipoCampo.tcDec2, Properties.Resources.pICMS);
                        wCampo(ICMS.vICMS, TpcnTipoCampo.tcDec2, Properties.Resources.vICMS);
                        wCampo(ICMS.modBCST, TpcnTipoCampo.tcStr, Properties.Resources.modBCST);
                        wCampo(ICMS.pMVAST, TpcnTipoCampo.tcDec2, Properties.Resources.pMVAST, false);
                        wCampo(ICMS.pRedBCST, TpcnTipoCampo.tcDec2, Properties.Resources.pRedBCST, false);
                        wCampo(ICMS.vBCST, TpcnTipoCampo.tcDec2, Properties.Resources.vBCST);
                        wCampo(ICMS.pICMSST, TpcnTipoCampo.tcDec2, Properties.Resources.pICMSST);
                        wCampo(ICMS.vICMSST, TpcnTipoCampo.tcDec2, Properties.Resources.vICMSST);
                        break;
                }
            }
            ICMS.CST =
                ICMS.modBC =
                ICMS.modBCST = "";
            ICMS.pICMS =
                ICMS.pICMSST =
                ICMS.pMVAST =
                ICMS.pRedBC =
                ICMS.pRedBCST =
                ICMS.vBC =
                ICMS.vBCST =
                ICMS.vICMS =
                ICMS.vICMSST = 0;
        }

        private void GerarDetImpostoII(II II, XmlElement nodeImposto)
        {
            if (II.vII > 0)
            {
                nodePai = doc.CreateElement("II");
                nodeImposto.AppendChild(nodePai);

                wCampo(II.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                wCampo(II.vDespAdu, TpcnTipoCampo.tcDec2, Properties.Resources.vDespAdu);
                wCampo(II.vII, TpcnTipoCampo.tcDec2, Properties.Resources.vII);
                wCampo(II.vIOF, TpcnTipoCampo.tcDec2, Properties.Resources.vIOF);
            }
            II.vBC = II.vDespAdu = II.vIOF = II.vII = 0;
        }

        private void GerarDetImpostoIPI(IPI IPI, XmlElement nodeImposto)
        {
            if (!string.IsNullOrEmpty(IPI.CST))
            {
                bool CST00495099;

                // variavel CST00495099 usada para Ignorar Tag <IPI>
                // se GerarTagIPIparaNaoTributado = False e CST00495099 = False

                CST00495099 = (IPI.CST == "00" || IPI.CST == "49" || IPI.CST == "50" || IPI.CST == "99");

                //if (not FOpcoes.FGerarTagIPIparaNaoTributado) and (not CST00495099) then exit;

                XmlElement e0 = doc.CreateElement("IPI");
                nodeImposto.AppendChild(e0);
                nodePai = e0;

                wCampo(IPI.clEnq, TpcnTipoCampo.tcStr, Properties.Resources.clEnq, false);
                wCampo(IPI.CNPJProd, TpcnTipoCampo.tcStr, Properties.Resources.CNPJProd, false);
                wCampo(IPI.cSelo, TpcnTipoCampo.tcStr, Properties.Resources.cSelo, false);
                wCampo(IPI.qSelo, TpcnTipoCampo.tcInt, Properties.Resources.qSelo, false);
                if (IPI.cEnq.Trim() == "")
                    IPI.cEnq = "999";
                wCampo(IPI.cEnq, TpcnTipoCampo.tcStr, Properties.Resources.cEnq);

                if (CST00495099)
                {
                    if ((IPI.vBC + IPI.pIPI > 0) && (IPI.qUnid + IPI.vUnid > 0))
                    {
                        this.cMensagemErro += "IPITrib: As TAG <vBC> e <pIPI> não podem ser informadas em conjunto com as TAG <qUnid> e <vUnid>" + Environment.NewLine;
                    }

                    nodePai = doc.CreateElement("IPITrib");
                    e0.AppendChild(nodePai);

                    wCampo(IPI.CST, TpcnTipoCampo.tcStr, Properties.Resources.CST);
                    if (IPI.qUnid + IPI.vUnid > 0)
                    {
                        wCampo(IPI.qUnid, TpcnTipoCampo.tcDec4, Properties.Resources.qUnid);
                        wCampo(IPI.vUnid, TpcnTipoCampo.tcDec4, Properties.Resources.vUnid);
                    }
                    else
                    {
                        wCampo(IPI.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                        wCampo(IPI.pIPI, TpcnTipoCampo.tcDec2, Properties.Resources.pIPI);
                    }
                    wCampo(IPI.vIPI, TpcnTipoCampo.tcDec2, Properties.Resources.vIPI);
                }
                else //(* Quando CST/IPI for 01,02,03,04,51,52,53,54 ou 55 *)
                {
                    nodePai = doc.CreateElement("IPINT");
                    e0.AppendChild(nodePai);
                    wCampo(IPI.CST, TpcnTipoCampo.tcStr, Properties.Resources.CST);
                }
            }
            IPI.CST =
                IPI.cEnq =
                IPI.clEnq =
                IPI.CNPJProd =
                IPI.cSelo = "";
            IPI.qSelo = 0;
            IPI.pIPI =
                IPI.qUnid =
                IPI.vBC =
                IPI.vIPI =
                IPI.vUnid = 0;
        }

        private void GerarDetImpostoISSQN(ISSQN ISSQN, XmlElement nodeImposto)
        {
            if ((ISSQN.vBC > 0) ||
                (ISSQN.vAliq > 0) ||
                (ISSQN.vISSQN > 0) ||
                (ISSQN.cMunFG > 0) |
                (ISSQN.cListServ > 0))
            {
                nodePai = /*xml.xml*/doc.CreateElement("ISSQN");
                nodeImposto.AppendChild(nodePai);

                wCampo(ISSQN.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                wCampo(ISSQN.vAliq, TpcnTipoCampo.tcDec2, Properties.Resources.vAliq);
                wCampo(ISSQN.vISSQN, TpcnTipoCampo.tcDec2, Properties.Resources.vISSQN);
                wCampo(ISSQN.cMunFG, TpcnTipoCampo.tcInt, Properties.Resources.cMunFG, true, 7);
                //if not ValidarMunicipio(ISSQN.cMunFG) then
                //    wAlerta("U05", Properties.Resources.cMunFG, Properties.Resources.cMunFG, ERR_MSG_INVALIDO);
                wCampo(ISSQN.cListServ, TpcnTipoCampo.tcInt, Properties.Resources.cListServ);
                //if (FOpcoes.ValidarListaServicos) and (ISSQN.cListServ <> 0) then
                //    if not ValidarCListServ(ISSQN.cListServ) then
                //        wAlerta("U06", Properties.Resources.cListServ, Properties.Resources.cListServ, ERR_MSG_INVALIDO);
            }
            ISSQN.vBC =
                ISSQN.vAliq =
                ISSQN.vISSQN =
                ISSQN.cMunFG =
                ISSQN.cListServ = 0;
        }

        private void GerarDetImpostoPIS(PIS PIS, XmlElement nodeImposto)
        {
            if (!string.IsNullOrEmpty(PIS.CST))
            {
                XmlElement e0 = doc.CreateElement("PIS");

                switch (PIS.CST)
                {
                    case "01":
                    case "02":
                        nodePai = /*xml.xml*/doc.CreateElement("PISAliq");
                        e0.AppendChild(nodePai);
                        nodeImposto.AppendChild(e0);
                        wCampo(PIS.CST, TpcnTipoCampo.tcStr, Properties.Resources.CST);
                        wCampo(PIS.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                        wCampo(PIS.pPIS, TpcnTipoCampo.tcDec2, Properties.Resources.pPIS);
                        wCampo(PIS.vPIS, TpcnTipoCampo.tcDec2, Properties.Resources.vPIS);
                        break;

                    case "03":
                        nodePai = doc.CreateElement("PISQtde");
                        e0.AppendChild(nodePai);
                        nodeImposto.AppendChild(e0);
                        wCampo(PIS.CST, TpcnTipoCampo.tcStr, Properties.Resources.CST);
                        wCampo(PIS.qBCProd, TpcnTipoCampo.tcDec4, Properties.Resources.qBCProd);
                        wCampo(PIS.vAliqProd, TpcnTipoCampo.tcDec4, Properties.Resources.vAliqProd);
                        wCampo(PIS.vPIS, TpcnTipoCampo.tcDec2, Properties.Resources.vPIS);
                        break;

                    case "04":
                    case "06":
                    case "07":
                    case "08":
                    case "09":
                        nodePai = doc.CreateElement("PISNT");
                        e0.AppendChild(nodePai);
                        nodeImposto.AppendChild(e0);
                        wCampo(PIS.CST, TpcnTipoCampo.tcStr, Properties.Resources.CST);
                        break;

                    case "99":
                        if  ((PIS.vBC + PIS.pPIS > 0) && 
                            (PIS.qBCProd + PIS.vAliqProd > 0))
                        {
                            this.cMensagemErro += "PIS: As TAG <vBC> e <pPIS> não podem ser informadas em conjunto com as TAG <qBCProd> e <vAliqProd>" + Environment.NewLine;
                        }

                        nodePai = doc.CreateElement(Properties.Resources.PISOutr);
                        e0.AppendChild(nodePai);
                        nodeImposto.AppendChild(e0);

                        wCampo(PIS.CST, TpcnTipoCampo.tcStr, Properties.Resources.CST);
                        if (PIS.qBCProd + PIS.vAliqProd > 0)
                        {
                            wCampo(PIS.qBCProd, TpcnTipoCampo.tcDec4, Properties.Resources.qBCProd);
                            wCampo(PIS.vAliqProd, TpcnTipoCampo.tcDec4, Properties.Resources.vAliqProd);
                        }
                        else
                        {
                            wCampo(PIS.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                            wCampo(PIS.pPIS, TpcnTipoCampo.tcDec2, Properties.Resources.pPIS);
                        }
                        wCampo(PIS.vPIS, TpcnTipoCampo.tcDec2, Properties.Resources.vPIS);
                        break;
                }
            }
            PIS.CST = "";
            PIS.pPIS =
                PIS.qBCProd =
                PIS.vAliqProd =
                PIS.vBC =
                PIS.vPIS = 0;
        }

        private void GerarDetImpostoPISST(PISST PISST, XmlElement nodeImposto)
        {
            if ((PISST.vBC > 0) ||
              (PISST.pPis > 0) ||
              (PISST.qBCProd > 0) ||
              (PISST.vAliqProd > 0) ||
              (PISST.vPIS > 0))
            {
                if ((PISST.vBC + PISST.pPis > 0) && (PISST.qBCProd + PISST.vAliqProd > 0))
                    this.cMensagemErro += "PISST:  As TAG <vBC> e <pPIS> não podem ser informadas em conjunto com as TAG <qBCProd> e <vAliqProd>)" + Environment.NewLine;

                if (PISST.vBC + PISST.pPis > 0)
                {
                    nodePai = doc.CreateElement("PISST");
                    nodeImposto.AppendChild(nodePai);

                    wCampo(PISST.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
                    wCampo(PISST.pPis, TpcnTipoCampo.tcDec2, Properties.Resources.pPIS);
                    wCampo(PISST.vPIS, TpcnTipoCampo.tcDec2, Properties.Resources.vPIS);
                }
                if (PISST.qBCProd + PISST.vAliqProd > 0)
                {
                    nodePai = doc.CreateElement("PISST");
                    nodeImposto.AppendChild(nodePai);
                    wCampo(PISST.qBCProd, TpcnTipoCampo.tcDec4, Properties.Resources.qBCProd);
                    wCampo(PISST.vAliqProd, TpcnTipoCampo.tcDec4, Properties.Resources.vAliqProd);
                    wCampo(PISST.vPIS, TpcnTipoCampo.tcDec2, Properties.Resources.vPIS);
                }
            }
            PISST.vBC =
                PISST.pPis =
                PISST.qBCProd =
                PISST.vAliqProd =
                PISST.vPIS = 0;
        }

        private void GerarRetirada(NFe NFe, XmlElement root)
        {
            if (!string.IsNullOrEmpty(NFe.retirada.xLgr))
            {
                XmlElement el = doc.CreateElement("retirada");
                root.AppendChild(el);
                nodePai = el;

                //AjustarMunicipioUF(xUF, xMun, cMun, nfe.Emit.EnderEmit.cPais, nfe.Retirada.UF, nfe.Retirada.xMun, nfe.Retirada.cMun);
                wCampo(NFe.retirada.CNPJ, TpcnTipoCampo.tcStr, (NFe.retirada.CNPJ.Length == 11 ? Properties.Resources.CPF : Properties.Resources.CNPJ));
                wCampo(NFe.retirada.xLgr, TpcnTipoCampo.tcStr, Properties.Resources.xLgr);
                wCampo(NFe.retirada.nro, TpcnTipoCampo.tcStr, Properties.Resources.nro);
                wCampo(NFe.retirada.xCpl, TpcnTipoCampo.tcStr, Properties.Resources.xCpl, false);
                wCampo(NFe.retirada.xBairro, TpcnTipoCampo.tcStr, Properties.Resources.xBairro);
                wCampo(NFe.retirada.cMun, TpcnTipoCampo.tcInt, Properties.Resources.cMun, true, 7);
                //if not ValidarMunicipio(cMun) then
                //  wAlerta("F07", Properties.Resources.cMun, Properties.Resources.cMun, ERR_MSG_INVALIDO);
                wCampo(NFe.retirada.xMun, TpcnTipoCampo.tcStr, Properties.Resources.xMun);
                wCampo(NFe.retirada.UF, TpcnTipoCampo.tcStr, Properties.Resources.UF);
                //if not ValidarUF(xUF) then
                //  wAlerta("F09", Properties.Resources.UF, Properties.Resources.UF, ERR_MSG_INVALIDO);
            }
        }

        private void GerarTransp(Transp Transp, XmlElement root)
        {
            XmlElement nodeTransp = doc.CreateElement("transp");
            root.AppendChild(nodeTransp);
            nodePai = nodeTransp;

            wCampo(TpcnTipoCampo.tcStr, Properties.Resources.modFrete, 1, 1, 1, Transp.modFrete);
            //
            //  (**)GerarTranspTransporta;
            //
            if (!string.IsNullOrEmpty(Transp.Transporta.CNPJCPF) ||
                !string.IsNullOrEmpty(Transp.Transporta.xNome) ||
                !string.IsNullOrEmpty(Transp.Transporta.IE) ||
                !string.IsNullOrEmpty(Transp.Transporta.xEnder) ||
                !string.IsNullOrEmpty(Transp.Transporta.xMun) ||
                !string.IsNullOrEmpty(Transp.Transporta.UF))
            {
                XmlElement nodeTransporta = doc.CreateElement("transporta");
                nodeTransp.AppendChild(nodeTransporta);
                nodePai = nodeTransporta;

                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.CNPJ, 1, 14, 1, Transp.Transporta.CNPJCPF);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.xNome, 01, 60, 0, Transp.Transporta.xNome);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.IE, 02, 14, 0, Transp.Transporta.IE);
                //if (FOpcoes.ValidarInscricoes) and (nfe.Transp.Transporta.IE <> "") then
                //  if not ValidarIE(nfe.Transp.Transporta.IE, nfe.Transp.Transporta.UF) then
                //      wAlerta("X07", Properties.Resources.IE, DSC_IE, ERR_MSG_INVALIDO);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.xEnder, 01, 60, 0, Transp.Transporta.xEnder);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.xMun, 01, 60, 0, Transp.Transporta.xMun);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.UF, 01, 02, 0, Transp.Transporta.UF);
                //if not ValidarUF(nfe.Transp.Transporta.UF) then
                //  wAlerta("X10", Properties.Resources.UF, Properties.Resources.UF, ERR_MSG_INVALIDO);
            }
            //
            //  (**)GerarTranspRetTransp;
            //
            if ((Transp.retTransp.vServ > 0) ||
              (Transp.retTransp.vBCRet > 0) ||
              (Transp.retTransp.pICMSRet > 0) ||
              (Transp.retTransp.vICMSRet > 0) ||
              (!string.IsNullOrEmpty(Transp.retTransp.CFOP)) ||
              (Transp.retTransp.cMunFG > 0))
            {
                XmlElement nodeRetTransporta = doc.CreateElement("retTransp");
                nodeTransp.AppendChild(nodeRetTransporta);
                nodePai = nodeRetTransporta;

                wCampo(TpcnTipoCampo.tcDec2,Properties.Resources.vServ, 01, 15, 1, Transp.retTransp.vServ);
                wCampo(TpcnTipoCampo.tcDec2,Properties.Resources.vBCRet, 01, 15, 1, Transp.retTransp.vBCRet);
                wCampo(TpcnTipoCampo.tcDec2,Properties.Resources.pICMSRet, 01, 05, 1, Transp.retTransp.pICMSRet);
                wCampo(TpcnTipoCampo.tcDec2,Properties.Resources.vICMSRet, 01, 15, 1, Transp.retTransp.vICMSRet);
                wCampo(TpcnTipoCampo.tcEsp, Properties.Resources.CFOP, 04, 04, 1, Transp.retTransp.CFOP);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.cMunFG, 07, 07, 1, Transp.retTransp.cMunFG);
                //if not ValidarMunicipio(nfe.Transp.retTransp.cMunFG) then
                //  wAlerta("X17", Properties.Resources.cMunFG, Properties.Resources.cMunFG, ERR_MSG_INVALIDO);
            }
            //
            //  (**)GerarTranspVeicTransp;
            //
            if (!string.IsNullOrEmpty(Transp.veicTransp.placa) ||
              !string.IsNullOrEmpty(Transp.veicTransp.UF) ||
              !string.IsNullOrEmpty(Transp.veicTransp.RNTC))
            {
                XmlElement nodeveicTransp = doc.CreateElement("veicTransp");
                nodeTransp.AppendChild(nodeveicTransp);
                nodePai = nodeveicTransp;

                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.placa, 01, 08, 1, Transp.veicTransp.placa);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.UF, 02, 02, 1, Transp.veicTransp.UF);
                //if not ValidarUF(nfe.Transp.veicTransp.UF) then
                //  wAlerta("X20", Properties.Resources.UF, Properties.Resources.UF, ERR_MSG_INVALIDO);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.RNTC, 01, 20, 0, Transp.veicTransp.RNTC);
            }
            //
            //(**)GerarTranspReboque;
            //
            if (Transp.Reboque.Count > 2)
                this.cMensagemErro += "Transp.reboque: Excedeu o máximo permitido de 2" + Environment.NewLine;

            foreach (Reboque Reboque in Transp.Reboque)
            {
                XmlElement nodeReboque = doc.CreateElement("reboque");
                nodeTransp.AppendChild(nodeReboque);
                nodePai = nodeReboque;

                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.placa, 01, 08, 1, Reboque.placa);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.UF, 02, 02, 1, Reboque.UF);
                //if not ValidarUF(nfe.Transp.Reboque[i].UF) then
                //  wAlerta("X24", Properties.Resources.UF, Properties.Resources.UF, ERR_MSG_INVALIDO);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.RNTC, 01, 20, 0, Reboque.RNTC);
            }

            //
            //(**)GerarTranspVol;
            //
            foreach (Vol Vol in Transp.Vol)
            {
                XmlElement nodeVol = doc.CreateElement("vol");
                nodeTransp.AppendChild(nodeVol);
                nodePai = nodeVol;

                wCampo(TpcnTipoCampo.tcInt, Properties.Resources.qVol, 01, 15, 0, Vol.qVol);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.esp, 01, 60, 0, Vol.esp);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.marca, 01, 60, 0, Vol.marca);
                wCampo(TpcnTipoCampo.tcStr, Properties.Resources.nVol, 01, 60, 0, Vol.nVol);
                wCampo(TpcnTipoCampo.tcDec3, Properties.Resources.pesoL, 01, 15, 0, Vol.pesoL);
                wCampo(TpcnTipoCampo.tcDec3, Properties.Resources.pesoB, 01, 15, 0, Vol.pesoB);
                //(**)GerarTranspVolLacres(i);
                foreach (Lacres lacres in Vol.Lacres)
                {
                    XmlElement nodeVolLacres = doc.CreateElement("lacres");
                    nodeVol.AppendChild(nodeVolLacres);
                    nodePai = nodeVolLacres;

                    wCampo(TpcnTipoCampo.tcStr, Properties.Resources.nLacre, 01, 60, 1, lacres.nLacre);
                }
            }
        }

        private void GerarTotal(NFe NFe, XmlElement root)
        {
            XmlElement nodeTotal = doc.CreateElement("total");
            root.AppendChild(nodeTotal);

            GerarTotalICMSTotal(NFe.Total.ICMSTot, nodeTotal);
            GerarTotalISSQNtot(NFe.Total.ISSQNtot, nodeTotal);
            GerarTotalretTrib(NFe.Total.retTrib, nodeTotal);
        }

        private void GerarTotalICMSTotal(ICMSTot ICMSTot, XmlElement nodeTotal)
        {
            nodePai = doc.CreateElement("ICMSTot");
            nodeTotal.AppendChild(nodePai);

            wCampo(ICMSTot.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC);
            wCampo(ICMSTot.vICMS, TpcnTipoCampo.tcDec2, Properties.Resources.vICMS);
            wCampo(ICMSTot.vBCST, TpcnTipoCampo.tcDec2, Properties.Resources.vBCST);
            wCampo(ICMSTot.vST, TpcnTipoCampo.tcDec2, Properties.Resources.vST);
            wCampo(ICMSTot.vProd, TpcnTipoCampo.tcDec2, Properties.Resources.vProd);
            wCampo(ICMSTot.vFrete, TpcnTipoCampo.tcDec2, Properties.Resources.vFrete);
            wCampo(ICMSTot.vSeg, TpcnTipoCampo.tcDec2, Properties.Resources.vSeg);
            wCampo(ICMSTot.vDesc, TpcnTipoCampo.tcDec2, Properties.Resources.vDesc);
            wCampo(ICMSTot.vII, TpcnTipoCampo.tcDec2, Properties.Resources.vII);
            wCampo(ICMSTot.vIPI, TpcnTipoCampo.tcDec2, Properties.Resources.vIPI);
            wCampo(ICMSTot.vPIS, TpcnTipoCampo.tcDec2, Properties.Resources.vPIS);
            wCampo(ICMSTot.vCOFINS, TpcnTipoCampo.tcDec2, Properties.Resources.vCOFINS);
            wCampo(ICMSTot.vOutro, TpcnTipoCampo.tcDec2, Properties.Resources.vOutro);
            wCampo(ICMSTot.vNF, TpcnTipoCampo.tcDec2, Properties.Resources.vNF);
        }

        private void GerarTotalISSQNtot(ISSQNtot ISSQNtot, XmlElement nodeTotal)
        {
            if ((ISSQNtot.vServ > 0) ||
                (ISSQNtot.vBC > 0) ||
                (ISSQNtot.vISS > 0) ||
                (ISSQNtot.vPIS > 0) ||
                (ISSQNtot.vCOFINS > 0))
            {
                nodePai = doc.CreateElement("ISSQNtot");
                nodeTotal.AppendChild(nodePai);

                wCampo(ISSQNtot.vServ, TpcnTipoCampo.tcDec2, Properties.Resources.vServ, false);
                wCampo(ISSQNtot.vBC, TpcnTipoCampo.tcDec2, Properties.Resources.vBC, false);
                wCampo(ISSQNtot.vISS, TpcnTipoCampo.tcDec2, Properties.Resources.vISS, false);
                wCampo(ISSQNtot.vPIS, TpcnTipoCampo.tcDec2, Properties.Resources.vPIS, false);
                wCampo(ISSQNtot.vCOFINS, TpcnTipoCampo.tcDec2, Properties.Resources.vCOFINS, false);
            }
            ISSQNtot.vServ =
                ISSQNtot.vBC =
                ISSQNtot.vISS =
                ISSQNtot.vPIS =
                ISSQNtot.vCOFINS = 0;
        }

        private void GerarTotalretTrib(retTrib retTrib, XmlElement nodeTotal)
        {
            if ((retTrib.vRetPIS > 0) ||
                (retTrib.vRetCOFINS > 0) ||
                (retTrib.vRetCSLL > 0) ||
                (retTrib.vBCIRRF > 0) ||
                (retTrib.vIRRF > 0) ||
                (retTrib.vBCRetPrev > 0) ||
                (retTrib.vRetPrev > 0))
            {
                nodePai = doc.CreateElement("retTrib");
                nodeTotal.AppendChild(nodePai);

                wCampo(retTrib.vRetPIS, TpcnTipoCampo.tcDec2, Properties.Resources.vRetPIS, false);
                wCampo(retTrib.vRetCOFINS, TpcnTipoCampo.tcDec2, Properties.Resources.vRetCOFINS, false);
                wCampo(retTrib.vRetCSLL, TpcnTipoCampo.tcDec2, Properties.Resources.vRetCSLL, false);
                wCampo(retTrib.vBCIRRF, TpcnTipoCampo.tcDec2, Properties.Resources.vBCIRRF, false);
                wCampo(retTrib.vIRRF, TpcnTipoCampo.tcDec2, Properties.Resources.vIRRF, false);
                wCampo(retTrib.vBCRetPrev, TpcnTipoCampo.tcDec2, Properties.Resources.vBCRetPrev, false);
                wCampo(retTrib.vRetPrev, TpcnTipoCampo.tcDec2, Properties.Resources.vRetPrev, false);
            }
            retTrib.vRetPIS =
                retTrib.vRetCOFINS =
                retTrib.vRetCSLL =
                retTrib.vBCIRRF =
                retTrib.vIRRF =
                retTrib.vBCRetPrev =
                retTrib.vRetPrev = 0;
        }

        private void wCampo(object obj, TpcnTipoCampo Tipo, string TAG)
        {
            wCampo(obj, Tipo, TAG, true, 0);
        }

        private void wCampo(object obj, TpcnTipoCampo Tipo, string TAG, bool Obrigatorio)
        {
            wCampo(obj, Tipo, TAG, Obrigatorio, 0);
        }

        private void wCampo(object obj, TpcnTipoCampo Tipo, string TAG, bool Obrigatorio, int nAlign)
        {
            TAG = TAG.Trim();

            if (obj == null)
                return;

            if (Tipo == TpcnTipoCampo.tcDat || Tipo == TpcnTipoCampo.tcDatHor)
                if (((DateTime)obj).Year == 1)
                    if (!Obrigatorio)
                        return;

            if (!Obrigatorio)
            {
                if (Tipo == TpcnTipoCampo.tcInt)
                    if ((int)obj == 0)
                        return;

                if (Tipo == TpcnTipoCampo.tcDec2 || Tipo == TpcnTipoCampo.tcDec3 || Tipo == TpcnTipoCampo.tcDec4)
                    if ((double)obj == 0)
                        return;

                if (obj.ToString() == "")
                    return;
            }
            XmlElement valueEl1 = doc.CreateElement(TAG);

            switch (Tipo)
            {
                case TpcnTipoCampo.tcDec2:
                    if (((double)obj) > 0 || Obrigatorio)
                        valueEl1.InnerText = ((double)obj).ToString("0.00").Replace(",", ".");
                    break;

                case TpcnTipoCampo.tcDec3:
                    if (((double)obj) > 0 || Obrigatorio)
                        valueEl1.InnerText = ((double)obj).ToString("0.000").Replace(",", ".");
                    break;

                case TpcnTipoCampo.tcDec4:
                    if (((double)obj) > 0 || Obrigatorio)
                        valueEl1.InnerText = ((double)obj).ToString("0.0000").Replace(",", ".");
                    break;

                case TpcnTipoCampo.tcDatHor:
                    if (((DateTime)obj).Year > 1)
                        valueEl1.InnerText = ((DateTime)obj).ToString("yyyy-MM-ddThh:mm:ss");
                    break;

                case TpcnTipoCampo.tcDat:
                    if (((DateTime)obj).Year > 1)
                        valueEl1.InnerText = ((DateTime)obj).ToString("yyyy-MM-dd");
                    break;

                default:
                    if (nAlign > 0)
                    {
                        valueEl1.InnerText = obj.ToString().PadLeft(nAlign, '0');
                    }
                    else
                        if (Tipo == TpcnTipoCampo.tcInt)
                        {
                            if (((int)obj) != 0 || Obrigatorio)
                                valueEl1.InnerText = ((int)obj).ToString();
                        }
                        else
                            if (obj.ToString() != "")//|| Obrigatorio)
                                valueEl1.InnerText = ConvertToOEM(obj.ToString().Trim());
                    break;
            }
            nodePai.AppendChild(valueEl1);
        }

        private void wCampo(TpcnTipoCampo Tipo, string TAG, int min, int max, int ocorrencias, object valor)
        {
            wCampo(valor, Tipo, TAG, ocorrencias==1, 0);
        }

        #region ConverToOEM
        private string ConvertToOEM(string FBuffer)
        {
            const string FAnsi = (" áéíóúÁÉÍÓÚçÇàèìòùÀÈÌÒÙãõÃÕºª§ÑâäåêëïîÄÅôûÿÖÜñüÂ");
            const string FOEM = (" aeiouAEIOUcCaeiouAEIOUaoAOoa.NaaaeeiiAAouyOUnuA");
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
            //result = result.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&#39;");
            //result = result.Replace("\r\n", " ");
            return result;
        }
        #endregion

    }
}
