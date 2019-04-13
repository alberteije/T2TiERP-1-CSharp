using System;
using System.Text;
using System.Collections.Generic;


namespace SpedService.Model {
    
    public class NfeCupomFiscalReferenciadoDTO {
        public NfeCupomFiscalReferenciadoDTO() { }
        public int Id { get; set; }
        public NfeCabecalhoDTO NfeCabecalho { get; set; }
        public string ModeloDocumentoFiscal { get; set; }
        public int NumeroOrdemEcf { get; set; }
        public int Coo { get; set; }
        public System.DateTime DataEmissaoCupom { get; set; }
        public int NumeroCaixa { get; set; }
        public string NumeroSerieEcf { get; set; }
    }
}
