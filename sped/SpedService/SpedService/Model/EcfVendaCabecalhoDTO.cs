using System;
using System.Text;
using System.Collections.Generic;


namespace SpedService.Model {
    
    public class EcfVendaCabecalhoDTO {
        public EcfVendaCabecalhoDTO() { }
        public int Id { get; set; }
        public string NomeCaixa { get; set; }
        public int IdGeradoCaixa { get; set; }
        public int IdEmpresa { get; set; }
        public int IdCliente { get; set; }
        public int IdEcfFuncionario { get; set; }
        public int IdEcfMovimento { get; set; }
        public int IdEcfDav { get; set; }
        public int IdEcfPreVendaCabecalho { get; set; }
        public string SerieEcf { get; set; }
        public int Cfop { get; set; }
        public int Coo { get; set; }
        public int Ccf { get; set; }
        public System.DateTime DataVenda { get; set; }
        public string HoraVenda { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal TaxaDesconto { get; set; }
        public decimal Desconto { get; set; }
        public decimal TaxaAcrescimo { get; set; }
        public decimal Acrescimo { get; set; }
        public decimal ValorFinal { get; set; }
        public decimal ValorRecebido { get; set; }
        public decimal Troco { get; set; }
        public decimal ValorCancelado { get; set; }
        public decimal TotalProdutos { get; set; }
        public decimal TotalDocumento { get; set; }
        public decimal BaseIcms { get; set; }
        public decimal Icms { get; set; }
        public decimal IcmsOutras { get; set; }
        public decimal Issqn { get; set; }
        public decimal Pis { get; set; }
        public decimal Cofins { get; set; }
        public decimal AcrescimoItens { get; set; }
        public decimal DescontoItens { get; set; }
        public string StatusVenda { get; set; }
        public string NomeCliente { get; set; }
        public string CpfCnpjCliente { get; set; }
        public string CupomCancelado { get; set; }
        public string HashTripa { get; set; }
        public int HashIncremento { get; set; }
        public System.DateTime DataSincronizacao { get; set; }
        public string HoraSincronizacao { get; set; }
    }
}
