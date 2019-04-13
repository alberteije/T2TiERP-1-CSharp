using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SintegraService.Model;

namespace SintegraService
{
   [ServiceContract]
    public interface IServicoSintegra
    {
        [OperationContract]
        EmpresaDTO selectEmpresaId(int id);

        [OperationContract]
        IList<NFeCabecalhoDTO> selectNFeCabecalho(DateTime dataInicio, DateTime dataFim, NFeCabecalhoDTO nfeCabecalho);

        [OperationContract]
        NFeCabecalhoDTO selectNFeCabecalhoId(int id);

        #region ViewSpedNfeDetalhe
        [OperationContract]
        IList<ViewSpedNfeDetalheDTO> selectViewSpedNfeDetalhe(ViewSpedNfeDetalheDTO viewSpedNfeDetalhe);
        [OperationContract]
        IList<ViewSpedNfeDetalheDTO> selectViewSpedNfeDetalhePagina(int primeiroResultado, int quantidadeResultados, ViewSpedNfeDetalheDTO viewSpedNfeDetalhe);
        #endregion 


        #region Usuario
        [OperationContract]
        UsuarioDTO selectUsuario(String login, String senha);
        #endregion

        #region ControleAcesso
        [OperationContract]
        IList<ViewControleAcessoDTO> selectControleAcesso(ViewControleAcessoDTO viewControleAcesso);
        #endregion


    }

}
