using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using NFeService.Model;

namespace NFeService
{
    [ServiceContract]
    public interface INFe
    {
        [OperationContract]
        EmpresaDTO selectEmpresaId(int id);

        [OperationContract]
        IList<NFeCabecalhoDTO> selectNFeCabecalho(NFeCabecalhoDTO nfeCabecalho);

        [OperationContract]
        int salvarNFeCabecalho(NFeCabecalhoDTO nfeCabecalho);

        [OperationContract]
        NFeCabecalhoDTO selectNFeCabecalhoId(int id);

        [OperationContract]
        IList<ProdutoDTO> selectProduto(ProdutoDTO produto);


        #region Apenas Consultas

        #region Usuario
        [OperationContract]
        UsuarioDTO selectUsuario(String login, String senha);
        #endregion

        #region ControleAcesso
        [OperationContract]
        IList<ViewControleAcessoDTO> selectControleAcesso(ViewControleAcessoDTO viewControleAcesso);
        #endregion

        #region TributOperacaoFiscal
        [OperationContract]
        IList<TributOperacaoFiscalDTO> selectTributOperacaoFiscal(TributOperacaoFiscalDTO tributOperacaoFiscal);
        #endregion 

        #region ViewTributacaoPis
        [OperationContract]
        ViewTributacaoPisDTO selectViewTributacaoPis(ViewTributacaoPisDTO viewTributacaoPis);
        #endregion 

        #region ViewTributacaoCofins
        [OperationContract]
        ViewTributacaoCofinsDTO selectViewTributacaoCofins(ViewTributacaoCofinsDTO viewTributacaoCofins);
        #endregion 

        #region ViewTributacaoIcms
        [OperationContract]
        ViewTributacaoIcmsDTO selectViewTributacaoIcms(ViewTributacaoIcmsDTO viewTributacaoIcms);
        #endregion 

        #endregion

    }


}
