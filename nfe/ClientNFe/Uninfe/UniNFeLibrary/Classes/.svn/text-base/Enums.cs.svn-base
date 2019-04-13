using System;
using System.Collections.Generic;
using System.Text;

namespace UniNFeLibrary.Enums
{
    #region SubPastas da pasta de enviados
    /// <summary>
    /// SubPastas da pasta de XML´s enviados para os webservices
    /// </summary>
    public enum PastaEnviados
    {
        EmProcessamento,
        Autorizados,
        Denegados
    }
    #endregion

    #region Servicos
    /// <summary>
    /// Serviços executados pelo Aplicativo
    /// </summary>
    public enum Servicos
    {
        /// <summary>
        /// Assina, valida e envia o XML de cancelamento de NFe para o webservice
        /// </summary>
        CancelarNFe,
        /// <summary>
        /// Assina, valida e envia o XML de Inutilização de números de NFe para o webservice
        /// </summary>
        InutilizarNumerosNFe,
        /// <summary>
        /// Valida e envia o XML de pedido de Consulta da Situação da NFe para o webservice
        /// </summary>
        PedidoConsultaSituacaoNFe,
        /// <summary>
        /// Valida e envia o XML de pedido de Consulta Status dos Serviços da NFe para o webservice
        /// </summary>
        PedidoConsultaStatusServicoNFe,
        /// <summary>
        /// Valida e envia o XML de pedido de Consulta da Situação do Lote da NFe para o webservice
        /// </summary>
        PedidoSituacaoLoteNFe,
        /// <summary>
        /// Valida e envia o XML de pedido de Consulta do Cadastro do Contribuinte para o webservice
        /// </summary>
        ConsultaCadastroContribuinte,
        /// <summary>
        /// Consultar Informações Gerais do UniNFe
        /// </summary>
        ConsultaInformacoesUniNFe,
        /// <summary>
        /// Solicitar ao UniNFe que altere suas configurações
        /// </summary>
        AlterarConfiguracoesUniNFe,
        /// <summary>
        /// Assinar e valida os XML´s de Notas Fiscais da Pasta de Envio
        /// </summary>
        AssinarNFePastaEnvio,
        /// <summary>
        /// Assinar e valida os XML´s de Notas Fiscais da Pasta de Envio em Lote
        /// </summary>
        AssinarNFePastaEnvioEmLote,
        /// <summary>
        /// Montar lote de notas com apenas uma nota fiscal
        /// </summary>
        MontarLoteUmaNFe,
        /// <summary>
        /// Montar lote de notas com várias notas fiscais
        /// </summary>
        MontarLoteVariasNFe,
        /// <summary>
        /// Envia os lotes de notas fiscais eletrônicas para os webservices
        /// </summary>
        EnviarLoteNfe,
        /// <summary>
        /// Somente validar e assinar o XML
        /// </summary>
        ValidarAssinar,
        /// <summary>
        /// Somente converter TXT da NFe para XML de NFe
        /// </summary>
        ConverterTXTparaXML,
        /// <summary>
        /// Monta chave de acesso
        /// </summary>
        GerarChaveNFe,
        /// <summary>
        /// Efetua verificações nas notas em processamento para evitar algumas falhas e perder retornos de autorização de notas
        /// </summary>
        EmProcessamento,
        /// <summary>
        /// Efetua uma limpeza das pastas que recebem arquivos temporários
        /// </summary>
        LimpezaTemporario,
        /// <summary>
        /// Enviar o XML do DPEC para o SCE - Sistema de Contingência Eletrônica
        /// </summary>
        EnviarDPEC,
        /// <summary>
        /// Consultar o registro do DPEC no SCE - Sistema de Contingência Eletrônica        
        /// </summary>
        ConsultarDPEC,
        /// <summary>
        /// Thread de execução de serviços diversos do UniNFe
        /// </summary>
    }
    #endregion

    #region TipoAplicativo
    public enum TipoAplicativo
    {
        /// <summary>
        /// Aplicativo de conhecimento de transporte eletrônico
        /// </summary>
        Cte,
        /// <summary>
        /// Aplicativo de nota fiscal eletrônica
        /// </summary>
        Nfe
    }
    #endregion

}
