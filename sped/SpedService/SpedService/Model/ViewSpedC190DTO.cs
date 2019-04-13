using System;
using System.Text;
using System.Collections.Generic;


namespace SpedService.Model {
    
    public class ViewSpedC190DTO {
        public ViewSpedC190DTO() { }
        public int Id { get; set; }
        public string CstIcms { get; set; }
        public int Cfop { get; set; }
        public decimal AliquotaIcms { get; set; }
        public System.DateTime DataEmissao { get; set; }
        public decimal SomaValorOperacao { get; set; }
        public decimal SomaBaseCalculoIcms { get; set; }
        public decimal SomaValorIcms { get; set; }
        public decimal SomaBaseCalculoIcmsSt { get; set; }
        public decimal SomaValorIcmsSt { get; set; }
        public decimal SomaVlRedBc { get; set; }
        public decimal SomaValorIpi { get; set; }
    }
}
