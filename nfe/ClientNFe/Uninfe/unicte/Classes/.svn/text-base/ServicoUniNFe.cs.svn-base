using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Reflection;
using UniNFeLibrary;
using UniNFeLibrary.Enums;
using System.Xml;
using System.Windows.Forms;

namespace uninfe
{
    #region Classe ServicoUniNFe
    /// <summary>
    /// Classe responsável pela execução dos serviços do UniNFe
    /// </summary>
    public class ServicoUniNFe : absServicoApp
    {
        #region BuscaXML()
        /// <summary>
        /// Procurar os arquivos XML´s a serem enviados aos web-services ou para ser executado alguma rotina
        /// </summary>
        /// <param name="pTipoArq">Mascara dos arquivos as serem pesquisados. Ex: *.xml   *-nfe.xml</param>
        public override void BuscaXML(object parametroThread)
        {
            ParametroThread param = (ParametroThread)parametroThread;

            ServicoNFe oNfe = new ServicoNFe();

            //Criar XML de controle de fluxo de envios de Notas Fiscais
            FluxoNfe oFluxoNfe = new FluxoNfe();
            try
            {
                oFluxoNfe.CriarXml(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar criar o XML para o controle do fuxo do envio dos documentos eletrônicos.\r\n\r\nErro:" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            while (true)
            {
                this.ProcessaXML(oNfe, param.Servico);

                Thread.Sleep(1000); //Pausa na Thread de 1000 milissegundos ou 1 segundo
            }
        }
        #endregion
    }
    #endregion
}