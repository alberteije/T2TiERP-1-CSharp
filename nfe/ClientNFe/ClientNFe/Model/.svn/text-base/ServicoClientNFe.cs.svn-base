using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientNFe.NFeServiceReference;
using SearchWindow.Attributes;

namespace ClientNFe.Model
{
    public class ServicoClientNFe : NFeClient
    {
        [SearchWindowDataSource(typeof(ProdutoDTO))]
        public new List<ProdutoDTO> selectProduto(ProdutoDTO produto) 
        {
            try
            {
                return base.selectProduto(produto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [SearchWindowDataSource(typeof(TributOperacaoFiscalDTO))]
        public new List<TributOperacaoFiscalDTO> selectOperacaoFiscal(TributOperacaoFiscalDTO operacaoFiscal)
        {
            try
            {
                return base.selectTributOperacaoFiscal(operacaoFiscal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
