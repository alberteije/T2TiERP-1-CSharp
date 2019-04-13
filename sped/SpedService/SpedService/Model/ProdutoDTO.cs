using System;
using System.Text;
using System.Collections.Generic;


namespace SpedService.Model {
    
    public class ProdutoDTO {
        public ProdutoDTO() { }
        public int Id { get; set; }
        public UnidadeProdutoDTO UnidadeProduto { get; set; }
        public string Gtin { get; set; }
        public string CodigoInterno { get; set; }
        public string Ncm { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string DescricaoPdv { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal PrecoVendaMinimo { get; set; }
        public decimal PrecoSugerido { get; set; }
        public decimal CustoMedioLiquido { get; set; }
        public decimal PrecoLucroZero { get; set; }
        public decimal PrecoLucroMinimo { get; set; }
        public decimal PrecoLucroMaximo { get; set; }
        public decimal Markup { get; set; }
        public decimal QuantidadeEstoque { get; set; }
        public decimal QuantidadeEstoqueAnterior { get; set; }
        public decimal EstoqueMinimo { get; set; }
        public decimal EstoqueMaximo { get; set; }
        public decimal EstoqueIdeal { get; set; }
        public string Excluido { get; set; }
        public string Inativo { get; set; }
        public System.DateTime DataCadastro { get; set; }
        public string FotoProduto { get; set; }
        public string ExTipi { get; set; }
        public string CodigoLst { get; set; }
        public string ClasseAbc { get; set; }
        public string Iat { get; set; }
        public string Ippt { get; set; }
        public string TipoItemSped { get; set; }
        public decimal Peso { get; set; }
        public decimal PorcentoComissao { get; set; }
        public decimal PontoPedido { get; set; }
        public decimal LoteEconomicoCompra { get; set; }
        public decimal AliquotaIcmsPaf { get; set; }
        public decimal AliquotaIssqnPaf { get; set; }
        public string TotalizadorParcial { get; set; }
        public int CodigoBalanca { get; set; }
        public System.DateTime DataAlteracao { get; set; }
        public string Tipo { get; set; }
    }
}
