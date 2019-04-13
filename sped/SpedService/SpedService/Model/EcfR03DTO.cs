using System;
using System.Text;
using System.Collections.Generic;


namespace SpedService.Model {
    
    public class EcfR03DTO {
        public EcfR03DTO() { }
        public int Id { get; set; }
        public string NomeCaixa { get; set; }
        public int IdGeradoCaixa { get; set; }
        public int IdEmpresa { get; set; }
        public int IdR02 { get; set; }
        public string SerieEcf { get; set; }
        public string TotalizadorParcial { get; set; }
        public decimal ValorAcumulado { get; set; }
        public int Crz { get; set; }
        public string HashTripa { get; set; }
        public int HashIncremento { get; set; }
        public System.DateTime DataSincronizacao { get; set; }
        public string HoraSincronizacao { get; set; }
    }
}
