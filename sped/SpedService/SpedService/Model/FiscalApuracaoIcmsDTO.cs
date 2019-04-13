using System;
using System.Text;
using System.Collections.Generic;


namespace SpedService.Model {
    
    public class FiscalApuracaoIcmsDTO {
        public FiscalApuracaoIcmsDTO() { }
        public int Id { get; set; }
        public EmpresaDTO Empresa { get; set; }
        public string Competencia { get; set; }
        public decimal ValorTotalDebito { get; set; }
        public decimal ValorAjusteDebito { get; set; }
        public decimal ValorTotalAjusteDebito { get; set; }
        public decimal ValorEstornoCredito { get; set; }
        public decimal ValorTotalCredito { get; set; }
        public decimal ValorAjusteCredito { get; set; }
        public decimal ValorTotalAjusteCredito { get; set; }
        public decimal ValorEstornoDebito { get; set; }
        public decimal ValorSaldoCredorAnterior { get; set; }
        public decimal ValorSaldoApurado { get; set; }
        public decimal ValorTotalDeducao { get; set; }
        public decimal ValorIcmsRecolher { get; set; }
        public decimal ValorSaldoCredorTransp { get; set; }
        public decimal ValorDebitoEspecial { get; set; }
    }
}
