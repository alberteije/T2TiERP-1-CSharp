using System;
using System.Text;
using System.Collections.Generic;


namespace SpedService.Model {
    
    public class EcfVendaDetalheDTO {
        public EcfVendaDetalheDTO() { }
        public int Id { get; set; }
        public string NomeCaixa { get; set; }
        public int IdGeradoCaixa { get; set; }
        public int IdEmpresa { get; set; }
        public int IdEcfProduto { get; set; }
        public int IdEcfVendaCabecalho { get; set; }
        public int Cfop { get; set; }
        public string Gtin { get; set; }
        public int Ccf { get; set; }
        public int Coo { get; set; }
        public string SerieEcf { get; set; }
        public int Item { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TotalItem { get; set; }
        public decimal BaseIcms { get; set; }
        public decimal TaxaIcms { get; set; }
        public decimal Icms { get; set; }
        public decimal TaxaDesconto { get; set; }
        public decimal Desconto { get; set; }
        public decimal TaxaIssqn { get; set; }
        public decimal Issqn { get; set; }
        public decimal TaxaPis { get; set; }
        public decimal Pis { get; set; }
        public decimal TaxaCofins { get; set; }
        public decimal Cofins { get; set; }
        public decimal TaxaAcrescimo { get; set; }
        public decimal Acrescimo { get; set; }
        public decimal AcrescimoRateio { get; set; }
        public decimal DescontoRateio { get; set; }
        public string TotalizadorParcial { get; set; }
        public string Cst { get; set; }
        public string Cancelado { get; set; }
        public string MovimentaEstoque { get; set; }
        public string EcfIcmsSt { get; set; }
        public string HashTripa { get; set; }
        public int HashIncremento { get; set; }
        public System.DateTime DataSincronizacao { get; set; }
        public string HoraSincronizacao { get; set; }
    }
}
