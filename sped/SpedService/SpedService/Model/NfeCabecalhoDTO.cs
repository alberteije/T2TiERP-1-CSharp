using System;
using System.Text;
using System.Collections.Generic;


namespace SpedService.Model {
    
    public class NfeCabecalhoDTO {
        public NfeCabecalhoDTO() { }
        public int Id { get; set; }
        public TributOperacaoFiscalDTO TributOperacaoFiscal { get; set; }
        public EmpresaDTO Empresa { get; set; }
        public FornecedorDTO Fornecedor { get; set; }
        public ClienteDTO Cliente { get; set; }
        public int UfEmitente { get; set; }
        public string CodigoNumerico { get; set; }
        public string NaturezaOperacao { get; set; }
        public string IndicadorFormaPagamento { get; set; }
        public string CodigoModelo { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public System.DateTime DataEmissao { get; set; }
        public System.DateTime DataEntradaSaida { get; set; }
        public string HoraEntradaSaida { get; set; }
        public string TipoOperacao { get; set; }
        public int CodigoMunicipio { get; set; }
        public string FormatoImpressaoDanfe { get; set; }
        public string TipoEmissao { get; set; }
        public string ChaveAcesso { get; set; }
        public string DigitoChaveAcesso { get; set; }
        public string Ambiente { get; set; }
        public string FinalidadeEmissao { get; set; }
        public string ProcessoEmissao { get; set; }
        public string VersaoProcessoEmissao { get; set; }
        public System.DateTime DataEntradaContingencia { get; set; }
        public string JustificativaContingencia { get; set; }
        public decimal BaseCalculoIcms { get; set; }
        public decimal ValorIcms { get; set; }
        public decimal BaseCalculoIcmsSt { get; set; }
        public decimal ValorIcmsSt { get; set; }
        public decimal ValorTotalProdutos { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal ValorSeguro { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorImpostoImportacao { get; set; }
        public decimal ValorIpi { get; set; }
        public decimal ValorPis { get; set; }
        public decimal ValorCofins { get; set; }
        public decimal ValorDespesasAcessorias { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorServicos { get; set; }
        public decimal BaseCalculoIssqn { get; set; }
        public decimal ValorIssqn { get; set; }
        public decimal ValorPisIssqn { get; set; }
        public decimal ValorCofinsIssqn { get; set; }
        public decimal ValorRetidoPis { get; set; }
        public decimal ValorRetidoCofins { get; set; }
        public decimal ValorRetidoCsll { get; set; }
        public decimal BaseCalculoIrrf { get; set; }
        public decimal ValorRetidoIrrf { get; set; }
        public decimal BaseCalculoPrevidencia { get; set; }
        public decimal ValorRetidoPrevidencia { get; set; }
        public string ComexUfEmbarque { get; set; }
        public string ComexLocalEmbarque { get; set; }
        public string CompraNotaEmpenho { get; set; }
        public string CompraPedido { get; set; }
        public string CompraContrato { get; set; }
        public string InformacoesAddFisco { get; set; }
        public string InformacoesAddContribuinte { get; set; }
        public string StatusNota { get; set; }
    }
}
