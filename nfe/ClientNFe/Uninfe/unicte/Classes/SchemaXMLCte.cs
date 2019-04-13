using System;
using System.Collections.Generic;
using System.Text;
using UniNFeLibrary;

namespace unicte
{
    class SchemaXMLCte
    {
        public static void CriarListaIDXML()
        {
            #region Limpar listas
            SchemaXML.lstXMLID.Clear();
            SchemaXML.lstXMLSchema.Clear();
            SchemaXML.lstXMLTag.Clear();
            SchemaXML.lstXMLTagAssinar.Clear();
            SchemaXML.lstXMLTextoID.Clear();
            #endregion

            #region XML Distribuição NFe
            SchemaXML.lstXMLTag.Add("cteProc");
            SchemaXML.lstXMLID.Add(9);
            SchemaXML.lstXMLTextoID.Add("XML de distribuição do CTe com protocolo de autorização anexado");
            SchemaXML.lstXMLSchema.Add("procCTe_v1.03.xsd");
            SchemaXML.lstXMLTagAssinar.Add(string.Empty);
            #endregion

            #region XML Distribuição Cancelamento
            SchemaXML.lstXMLTag.Add("procCancCTe");
            SchemaXML.lstXMLID.Add(10);
            SchemaXML.lstXMLTextoID.Add("XML de distribuição do Cancelamento do CTe com protocolo de autorização anexado");
            SchemaXML.lstXMLSchema.Add("procCancCTe_v1.03.xsd");
            SchemaXML.lstXMLTagAssinar.Add(string.Empty);
            #endregion

            #region XML Distribuição Inutilização
            SchemaXML.lstXMLTag.Add("procInutCTe");
            SchemaXML.lstXMLID.Add(11);
            SchemaXML.lstXMLTextoID.Add("XML de distribuição de Inutilização de Números do CTe com protocolo de autorização anexado");
            SchemaXML.lstXMLSchema.Add("procInutCTe_v1.03.xsd");
            SchemaXML.lstXMLTagAssinar.Add(string.Empty);
            #endregion

            #region XML NFe
            SchemaXML.lstXMLTag.Add("CTe");
            SchemaXML.lstXMLID.Add(1);
            SchemaXML.lstXMLTextoID.Add("XML de Conhecimento de Transporte Eletrônico");
            SchemaXML.lstXMLSchema.Add("cte_v1.03.xsd");
            SchemaXML.lstXMLTagAssinar.Add("infCte");
            #endregion

            #region XML Envio Lote
            SchemaXML.lstXMLTag.Add("enviCTe");
            SchemaXML.lstXMLID.Add(2);
            SchemaXML.lstXMLTextoID.Add("XML de Envio de Lote dos Conhecimentos de Transportes Eletrônicos");
            SchemaXML.lstXMLSchema.Add("enviCte_v1.03.xsd");
            SchemaXML.lstXMLTagAssinar.Add(string.Empty);
            #endregion

            #region XML Cancelamento
            SchemaXML.lstXMLTag.Add("cancCTe");
            SchemaXML.lstXMLID.Add(3);
            SchemaXML.lstXMLTextoID.Add("XML de Cancelamento do Conhecimento de Transporte Eletrônico");
            SchemaXML.lstXMLSchema.Add("cancCte_v1.03.xsd");
            SchemaXML.lstXMLTagAssinar.Add("infCanc");
            #endregion

            #region XML Inutilização
            SchemaXML.lstXMLTag.Add("inutCTe");
            SchemaXML.lstXMLID.Add(4);
            SchemaXML.lstXMLTextoID.Add("XML de Inutilização de Numerações do Conhecimento de Transporte Eletrônico");
            SchemaXML.lstXMLSchema.Add("inutCte_v1.03.xsd");
            SchemaXML.lstXMLTagAssinar.Add("infInut");
            #endregion

            #region XML Consulta Situação NFe
            SchemaXML.lstXMLTag.Add("consSitCTe");
            SchemaXML.lstXMLID.Add(5);
            SchemaXML.lstXMLTextoID.Add("XML de Consulta da Situação do Conhecimento de Transporte Eletrônico");
            SchemaXML.lstXMLSchema.Add("consSitCte_v1.03.xsd");
            SchemaXML.lstXMLTagAssinar.Add(string.Empty);
            #endregion

            #region XML Consulta Recibo Lote
            SchemaXML.lstXMLTag.Add("consReciCTe");
            SchemaXML.lstXMLID.Add(6);
            SchemaXML.lstXMLTextoID.Add("XML de Consulta do Recibo do Lote de Conhecimentos de Transportes Eletrônicos");
            SchemaXML.lstXMLSchema.Add("consReciCte_v1.03.xsd");
            SchemaXML.lstXMLTagAssinar.Add(string.Empty);
            #endregion

            #region XML Consulta Situação Serviço NFe
            SchemaXML.lstXMLTag.Add("consStatServCte");
            SchemaXML.lstXMLID.Add(7);
            SchemaXML.lstXMLTextoID.Add("XML de Consulta da Situação do Serviço do Conhecimento de Transporte Eletrônico");
            SchemaXML.lstXMLSchema.Add("consStatServCte_v1.03.xsd");
            SchemaXML.lstXMLTagAssinar.Add(string.Empty);
            #endregion

            #region XML Consulta Cadastro Contribuinte
            SchemaXML.lstXMLTag.Add("ConsCad");
            SchemaXML.lstXMLID.Add(8);
            SchemaXML.lstXMLTextoID.Add("XML de Consulta do Cadastro do Contribuinte");
            SchemaXML.lstXMLSchema.Add("consCad_v1.03.xsd");
            SchemaXML.lstXMLTagAssinar.Add(string.Empty);
            #endregion

            SchemaXML.MaxID = 0;
            for (int i = 0; i < SchemaXML.lstXMLID.Count; i++)
			{
                if (SchemaXML.lstXMLID[i] > SchemaXML.MaxID)
                    SchemaXML.MaxID = SchemaXML.lstXMLID[i];			 
			}
        }
    }
}
