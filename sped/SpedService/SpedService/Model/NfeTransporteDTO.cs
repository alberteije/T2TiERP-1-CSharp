using System;
using System.Text;
using System.Collections.Generic;


namespace SpedService.Model {
    
    public class NfeTransporteDTO {
        public NfeTransporteDTO() { }
        public int Id { get; set; }
        public TransportadoraDTO Transportadora { get; set; }
        public NfeCabecalhoDTO NfeCabecalho { get; set; }
        public string ModalidadeFrete { get; set; }
        public string CpfCnpj { get; set; }
        public string Nome { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Endereco { get; set; }
        public string NomeMunicipio { get; set; }
        public string Uf { get; set; }
        public decimal ValorServico { get; set; }
        public decimal ValorBcRetencaoIcms { get; set; }
        public decimal AliquotaRetencaoIcms { get; set; }
        public decimal ValorIcmsRetido { get; set; }
        public int Cfop { get; set; }
        public int Municipio { get; set; }
        public string PlacaVeiculo { get; set; }
        public string UfVeiculo { get; set; }
        public string RntcVeiculo { get; set; }
        public string Vagao { get; set; }
        public string Balsa { get; set; }
    }
}
