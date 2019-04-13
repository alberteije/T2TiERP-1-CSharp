using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SpedService.Model;

namespace SpedService
{
    [ServiceContract]
    public interface IServicoSped
    {

        #region Geração do Arquivo
        [OperationContract]
        ArquivoDTO gerarSped(string pDataIni, string pDataFim, int pVersao, int pFinalidade, int pPerfil, int pIdEmpresa, int pInventario, int pIdContador);
        #endregion

        #region Empresa
        [OperationContract]
        IList<EmpresaDTO> selectEmpresa(EmpresaDTO empresa);
        [OperationContract]
        IList<EmpresaDTO> selectEmpresaPagina(int primeiroResultado, int quantidadeResultados, EmpresaDTO empresa);
        #endregion 

        #region Contador
        [OperationContract]
        IList<ContadorDTO> selectContador(ContadorDTO contador);
        [OperationContract]
        IList<ContadorDTO> selectContadorPagina(int primeiroResultado, int quantidadeResultados, ContadorDTO contador);
        #endregion 

        #region UnidadeProduto
        [OperationContract]
        IList<UnidadeProdutoDTO> selectUnidadeProduto(UnidadeProdutoDTO unidadeProduto);
        [OperationContract]
        IList<UnidadeProdutoDTO> selectUnidadeProdutoPagina(int primeiroResultado, int quantidadeResultados, UnidadeProdutoDTO unidadeProduto);
        #endregion 

        #region Produto
        [OperationContract]
        IList<ProdutoDTO> selectProduto(ProdutoDTO produto);
        [OperationContract]
        IList<ProdutoDTO> selectProdutoPagina(int primeiroResultado, int quantidadeResultados, ProdutoDTO produto);
        #endregion 

        #region Cliente
        [OperationContract]
        IList<ClienteDTO> selectCliente(ClienteDTO cliente);
        [OperationContract]
        IList<ClienteDTO> selectClientePagina(int primeiroResultado, int quantidadeResultados, ClienteDTO cliente);
        #endregion 

        #region EcfImpressora
        [OperationContract]
        IList<EcfImpressoraDTO> selectEcfImpressora(EcfImpressoraDTO ecfImpressora);
        [OperationContract]
        IList<EcfImpressoraDTO> selectEcfImpressoraPagina(int primeiroResultado, int quantidadeResultados, EcfImpressoraDTO ecfImpressora);
        #endregion 

        #region EcfNotaFiscalCabecalho
        [OperationContract]
        IList<EcfNotaFiscalCabecalhoDTO> selectEcfNotaFiscalCabecalho(EcfNotaFiscalCabecalhoDTO ecfNotaFiscalCabecalho);
        [OperationContract]
        IList<EcfNotaFiscalCabecalhoDTO> selectEcfNotaFiscalCabecalhoPagina(int primeiroResultado, int quantidadeResultados, EcfNotaFiscalCabecalhoDTO ecfNotaFiscalCabecalho);
        #endregion 

        #region NfeCabecalho
        [OperationContract]
        IList<NfeCabecalhoDTO> selectNfeCabecalho(NfeCabecalhoDTO nfeCabecalho);
        [OperationContract]
        IList<NfeCabecalhoDTO> selectNfeCabecalhoPagina(int primeiroResultado, int quantidadeResultados, NfeCabecalhoDTO nfeCabecalho);
        #endregion 

        #region NfeCupomFiscalReferenciado
        [OperationContract]
        IList<NfeCupomFiscalReferenciadoDTO> selectNfeCupomFiscalReferenciado(NfeCupomFiscalReferenciadoDTO nfeCupomFiscalReferenciado);
        [OperationContract]
        IList<NfeCupomFiscalReferenciadoDTO> selectNfeCupomFiscalReferenciadoPagina(int primeiroResultado, int quantidadeResultados, NfeCupomFiscalReferenciadoDTO nfeCupomFiscalReferenciado);
        #endregion 

        #region ViewSpedC190
        [OperationContract]
        IList<ViewSpedC190DTO> selectViewSpedC190(ViewSpedC190DTO viewSpedC190);
        [OperationContract]
        IList<ViewSpedC190DTO> selectViewSpedC190Pagina(int primeiroResultado, int quantidadeResultados, ViewSpedC190DTO viewSpedC190);
        #endregion 

        #region ViewSpedC300
        [OperationContract]
        IList<ViewSpedC300DTO> selectViewSpedC300(ViewSpedC300DTO viewSpedC300);
        [OperationContract]
        IList<ViewSpedC300DTO> selectViewSpedC300Pagina(int primeiroResultado, int quantidadeResultados, ViewSpedC300DTO viewSpedC300);
        #endregion 

        #region ViewSpedC321
        [OperationContract]
        IList<ViewSpedC321DTO> selectViewSpedC321(ViewSpedC321DTO viewSpedC321);
        [OperationContract]
        IList<ViewSpedC321DTO> selectViewSpedC321Pagina(int primeiroResultado, int quantidadeResultados, ViewSpedC321DTO viewSpedC321);
        #endregion 

        #region ViewSpedC370
        [OperationContract]
        IList<ViewSpedC370DTO> selectViewSpedC370(ViewSpedC370DTO viewSpedC370);
        [OperationContract]
        IList<ViewSpedC370DTO> selectViewSpedC370Pagina(int primeiroResultado, int quantidadeResultados, ViewSpedC370DTO viewSpedC370);
        #endregion 

        #region ViewSpedC390
        [OperationContract]
        IList<ViewSpedC390DTO> selectViewSpedC390(ViewSpedC390DTO viewSpedC390);
        [OperationContract]
        IList<ViewSpedC390DTO> selectViewSpedC390Pagina(int primeiroResultado, int quantidadeResultados, ViewSpedC390DTO viewSpedC390);
        #endregion 

        #region ViewSpedC425
        [OperationContract]
        IList<ViewSpedC425DTO> selectViewSpedC425(ViewSpedC425DTO viewSpedC425);
        [OperationContract]
        IList<ViewSpedC425DTO> selectViewSpedC425Pagina(int primeiroResultado, int quantidadeResultados, ViewSpedC425DTO viewSpedC425);
        #endregion 

        #region ViewSpedC490
        [OperationContract]
        IList<ViewSpedC490DTO> selectViewSpedC490(ViewSpedC490DTO viewSpedC490);
        [OperationContract]
        IList<ViewSpedC490DTO> selectViewSpedC490Pagina(int primeiroResultado, int quantidadeResultados, ViewSpedC490DTO viewSpedC490);
        #endregion 

        #region EcfR02
        [OperationContract]
        IList<EcfR02DTO> selectEcfR02(EcfR02DTO ecfR02);
        [OperationContract]
        IList<EcfR02DTO> selectEcfR02Pagina(int primeiroResultado, int quantidadeResultados, EcfR02DTO ecfR02);
        #endregion 

        #region EcfR03
        [OperationContract]
        IList<EcfR03DTO> selectEcfR03(EcfR03DTO ecfR03);
        [OperationContract]
        IList<EcfR03DTO> selectEcfR03Pagina(int primeiroResultado, int quantidadeResultados, EcfR03DTO ecfR03);
        #endregion 

        #region EcfVendaCabecalho
        [OperationContract]
        IList<EcfVendaCabecalhoDTO> selectEcfVendaCabecalho(EcfVendaCabecalhoDTO ecfVendaCabecalho);
        [OperationContract]
        IList<EcfVendaCabecalhoDTO> selectEcfVendaCabecalhoPagina(int primeiroResultado, int quantidadeResultados, EcfVendaCabecalhoDTO ecfVendaCabecalho);
        #endregion 

        #region EcfVendaDetalhe
        [OperationContract]
        IList<EcfVendaDetalheDTO> selectEcfVendaDetalhe(EcfVendaDetalheDTO ecfVendaDetalhe);
        [OperationContract]
        IList<EcfVendaDetalheDTO> selectEcfVendaDetalhePagina(int primeiroResultado, int quantidadeResultados, EcfVendaDetalheDTO ecfVendaDetalhe);
        #endregion 

        #region FiscalApuracaoIcms
        [OperationContract]
        IList<FiscalApuracaoIcmsDTO> selectFiscalApuracaoIcms(FiscalApuracaoIcmsDTO fiscalApuracaoIcms);
        [OperationContract]
        IList<FiscalApuracaoIcmsDTO> selectFiscalApuracaoIcmsPagina(int primeiroResultado, int quantidadeResultados, FiscalApuracaoIcmsDTO fiscalApuracaoIcms);
        #endregion 

        #region ProdutoAlteracaoItem
        [OperationContract]
        IList<ProdutoAlteracaoItemDTO> selectProdutoAlteracaoItem(ProdutoAlteracaoItemDTO produtoAlteracaoItem);
        [OperationContract]
        IList<ProdutoAlteracaoItemDTO> selectProdutoAlteracaoItemPagina(int primeiroResultado, int quantidadeResultados, ProdutoAlteracaoItemDTO produtoAlteracaoItem);
        #endregion 

        #region ViewSpedNfeEmitente
        [OperationContract]
        IList<ViewSpedNfeEmitenteDTO> selectViewSpedNfeEmitente(ViewSpedNfeEmitenteDTO viewSpedNfeEmitente);
        [OperationContract]
        IList<ViewSpedNfeEmitenteDTO> selectViewSpedNfeEmitentePagina(int primeiroResultado, int quantidadeResultados, ViewSpedNfeEmitenteDTO viewSpedNfeEmitente);
        #endregion 

        #region ViewSpedNfeDestinatario
        [OperationContract]
        IList<ViewSpedNfeDestinatarioDTO> selectViewSpedNfeDestinatario(ViewSpedNfeDestinatarioDTO viewSpedNfeDestinatario);
        [OperationContract]
        IList<ViewSpedNfeDestinatarioDTO> selectViewSpedNfeDestinatarioPagina(int primeiroResultado, int quantidadeResultados, ViewSpedNfeDestinatarioDTO viewSpedNfeDestinatario);
        #endregion 

        #region ViewSpedNfeItem
        [OperationContract]
        IList<ViewSpedNfeItemDTO> selectViewSpedNfeItem(ViewSpedNfeItemDTO viewSpedNfeItem);
        [OperationContract]
        IList<ViewSpedNfeItemDTO> selectViewSpedNfeItemPagina(int primeiroResultado, int quantidadeResultados, ViewSpedNfeItemDTO viewSpedNfeItem);
        #endregion 

        #region UnidadeConversao
        [OperationContract]
        IList<UnidadeConversaoDTO> selectUnidadeConversao(UnidadeConversaoDTO unidadeConversao);
        [OperationContract]
        IList<UnidadeConversaoDTO> selectUnidadeConversaoPagina(int primeiroResultado, int quantidadeResultados, UnidadeConversaoDTO unidadeConversao);
        #endregion 

        #region TributOperacaoFiscal
        [OperationContract]
        IList<TributOperacaoFiscalDTO> selectTributOperacaoFiscal(TributOperacaoFiscalDTO tributOperacaoFiscal);
        [OperationContract]
        IList<TributOperacaoFiscalDTO> selectTributOperacaoFiscalPagina(int primeiroResultado, int quantidadeResultados, TributOperacaoFiscalDTO tributOperacaoFiscal);
        #endregion 

        #region NfeTransporte
        [OperationContract]
        IList<NfeTransporteDTO> selectNfeTransporte(NfeTransporteDTO nfeTransporte);
        [OperationContract]
        IList<NfeTransporteDTO> selectNfeTransportePagina(int primeiroResultado, int quantidadeResultados, NfeTransporteDTO nfeTransporte);
        #endregion 

        #region ViewSpedNfeDetalhe
        [OperationContract]
        IList<ViewSpedNfeDetalheDTO> selectViewSpedNfeDetalhe(ViewSpedNfeDetalheDTO viewSpedNfeDetalhe);
        [OperationContract]
        IList<ViewSpedNfeDetalheDTO> selectViewSpedNfeDetalhePagina(int primeiroResultado, int quantidadeResultados, ViewSpedNfeDetalheDTO viewSpedNfeDetalhe);
        #endregion 

        #region ViewPessoaContador
        [OperationContract]
        IList<ViewPessoaContadorDTO> selectViewPessoaContador(ViewPessoaContadorDTO viewPessoaContador);
        [OperationContract]
        IList<ViewPessoaContadorDTO> selectViewPessoaContadorPagina(int primeiroResultado, int quantidadeResultados, ViewPessoaContadorDTO viewPessoaContador);
        #endregion 



        #region Usuario
        [OperationContract]
        UsuarioDTO selectUsuario(String login, String senha);
        #endregion

        #region ControleAcesso
        [OperationContract]
        IList<ViewControleAcessoDTO> selectControleAcesso(ViewControleAcessoDTO viewControleAcesso);
        #endregion

    }


}
