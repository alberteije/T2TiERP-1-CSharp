using System;
using System.Text;
using System.Collections.Generic;


namespace SpedService.Model {
    
    public class EcfR02DTO {
        public EcfR02DTO() { }
        public int Id { get; set; }
        public string NomeCaixa { get; set; }
        public int IdGeradoCaixa { get; set; }
        public int IdEmpresa { get; set; }
        public int IdOperador { get; set; }
        public int IdImpressora { get; set; }
        public int IdEcfCaixa { get; set; }
        public string SerieEcf { get; set; }
        public int Crz { get; set; }
        public int Coo { get; set; }
        public int Cro { get; set; }
        public System.DateTime DataMovimento { get; set; }
        public System.DateTime DataEmissao { get; set; }
        public string HoraEmissao { get; set; }
        public decimal VendaBruta { get; set; }
        public decimal GrandeTotal { get; set; }
        public string HashTripa { get; set; }
        public int HashIncremento { get; set; }
        public System.DateTime DataSincronizacao { get; set; }
        public string HoraSincronizacao { get; set; }
    }
}
