using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CadastrosBaseService.Model;

namespace CadastrosBaseService
{
    [ServiceContract]
    public interface IServicoCadastrosBase
    {
        #region  Cbo
        [OperationContract]
        int deleteCbo(CboDTO cbo);

        [OperationContract]
        int salvarAtualizarCbo(CboDTO cbo);

        [OperationContract]
        IList<CboDTO> selectCbo(CboDTO cbo);

        [OperationContract]
        IList<CboDTO> selectCboPagina(int primeiroResultado, int quantidadeResultados, CboDTO cbo);

        #endregion

        #region Cfop
        [OperationContract]
        int deleteCfop(CfopDTO cfop);
        [OperationContract]
        CfopDTO salvarAtualizarCfop(CfopDTO cfop);
        [OperationContract]
        IList<CfopDTO> selectCfop(CfopDTO cfop);
        [OperationContract]
        IList<CfopDTO> selectCfopPagina(int primeiroResultado, int quantidadeResultados, CfopDTO cfop);
        #endregion 

        #region  CsosnA
        [OperationContract]
        int deleteCsosnA(CsosnADTO dto);

        [OperationContract]
        int salvarAtualizarCsosnA(CsosnADTO dto);

        [OperationContract]
        IList<CsosnADTO> selectCsosnA(CsosnADTO dto);

        [OperationContract]
        IList<CsosnADTO> selectCsosnAPagina(int primeiroResultado, int quantidadeResultados, CsosnADTO dto);

        #endregion

        #region CsosnB
        [OperationContract]
        int deleteCsosnB(CsosnBDTO dto);

        [OperationContract]
        int salvarAtualizarCsosnB(CsosnBDTO dto);

        [OperationContract]
        IList<CsosnBDTO> selectCsosnB(CsosnBDTO dto);

        [OperationContract]
        IList<CsosnBDTO> selectCsosnBPagina(int primeiroResultado, int quantidadeResultados, CsosnBDTO dto);
        #endregion

        #region Situação Documento

        [OperationContract]
        int deleteSituacaoDocumento(SituacaoDocumentoDTO dto);

        [OperationContract]
        int salvarAtualizarSituacaoDocumento(SituacaoDocumentoDTO dto);

        [OperationContract]
        IList<SituacaoDocumentoDTO> selectSituacaoDocumento(SituacaoDocumentoDTO dto);

        [OperationContract]
        IList<SituacaoDocumentoDTO> selectSituacaoDocumentoPagina(int primeiroResultado, int quantidadeResultados, SituacaoDocumentoDTO dto);

        #endregion

        #region Tipo Credio Pis
        [OperationContract]
        int deleteTipoCredioPis(TipoCreditoPisDTO dto);

        [OperationContract]
        int salvarAtualizarTipoCredioPis(TipoCreditoPisDTO dto);

        [OperationContract]
        IList<TipoCreditoPisDTO> selectTipoCredioPis(TipoCreditoPisDTO dto);

        [OperationContract]
        IList<TipoCreditoPisDTO> selectTipoCredioPisPagina(int primeiroResultado, int quantidadeResultados, TipoCreditoPisDTO dto);

        #endregion

        #region CepDTO Contract
        [OperationContract]
        int deleteCep(CepDTO cep);
        [OperationContract]
        int salvarAtualizarCep(CepDTO cep);
        [OperationContract]
        IList<CepDTO> selectCep(CepDTO cep);
        [OperationContract]
        IList<CepDTO> selectCepPagina(int primeiroResultado, int quantidadeResultados, CepDTO cep);
        #endregion

        #region CodigoGpsDTO Contract
        [OperationContract]
        int deleteCodigoGps(CodigoGpsDTO codigo_gps);
        [OperationContract]
        int salvarAtualizarCodigoGps(CodigoGpsDTO codigo_gps);
        [OperationContract]
        IList<CodigoGpsDTO> selectCodigoGps(CodigoGpsDTO codigo_gps);
        [OperationContract]
        IList<CodigoGpsDTO> selectCodigoGpsPagina(int primeiroResultado, int quantidadeResultados, CodigoGpsDTO codigo_gps);
        #endregion

        #region Salario_MinimoDTO Contract
        [OperationContract]
        int deleteSalarioMinimo(SalarioMinimoDTO salario_minimo);
        [OperationContract]
        int salvarAtualizarSalarioMinimo(SalarioMinimoDTO salario_minimo);
        [OperationContract]
        IList<SalarioMinimoDTO> selectSalarioMinimo(SalarioMinimoDTO salario_minimo);
        [OperationContract]
        IList<SalarioMinimoDTO> selectSalarioMinimoPagina(int primeiroResultado, int quantidadeResultados, SalarioMinimoDTO salario_minimo);
        #endregion

        #region TipoDesligamento
        [OperationContract]
        int deleteTipoDesligamento(TipoDesligamentoDTO tipoDesligamento);
        [OperationContract]
        TipoDesligamentoDTO salvarAtualizarTipoDesligamento(TipoDesligamentoDTO tipoDesligamento);
        [OperationContract]
        IList<TipoDesligamentoDTO> selectTipoDesligamento(TipoDesligamentoDTO tipoDesligamento);
        [OperationContract]
        IList<TipoDesligamentoDTO> selectTipoDesligamentoPagina(int primeiroResultado, int quantidadeResultados, TipoDesligamentoDTO tipoDesligamento);
        #endregion 

        #region Sefip_Codigo_MovimentacaoDTO Contract
        [OperationContract]
        int deleteSefipCodigoMovimentacao(SefipCodigoMovimentacaoDTO sefip_codigo_movimentacao);
        [OperationContract]
        int salvarAtualizarSefipCodigoMovimentacao(SefipCodigoMovimentacaoDTO sefip_codigo_movimentacao);
        [OperationContract]
        IList<SefipCodigoMovimentacaoDTO> selectSefipCodigoMovimentacao(SefipCodigoMovimentacaoDTO sefip_codigo_movimentacao);
        [OperationContract]
        IList<SefipCodigoMovimentacaoDTO> selectSefipCodigoMovimentacaoPagina(int primeiroResultado, int quantidadeResultados, SefipCodigoMovimentacaoDTO sefip_codigo_movimentacao);
        #endregion

        #region BaseCreditoPis
        [OperationContract]
        int deleteBaseCreditoPis(BaseCreditoPisDTO baseCreditoPis);
        [OperationContract]
        int salvarAtualizarBaseCreditoPis(BaseCreditoPisDTO baseCreditoPis);
        [OperationContract]
        IList<BaseCreditoPisDTO> selectBaseCreditoPis(BaseCreditoPisDTO baseCreditoPis);
        [OperationContract]
        IList<BaseCreditoPisDTO> selectBaseCreditoPisPagina(int primeiroResultado, int quantidadeResultados, BaseCreditoPisDTO baseCreditoPis);
        #endregion

        #region CstIpi
        [OperationContract]
        int deleteCstIpi(CstIpiDTO cstIpi);
        [OperationContract]
        int salvarAtualizarCstIpi(CstIpiDTO cstIpi);
        [OperationContract]
        IList<CstIpiDTO> selectCstIpi(CstIpiDTO cstIpi);
        [OperationContract]
        IList<CstIpiDTO> selectCstIpiPagina(int primeiroResultado, int quantidadeResultados, CstIpiDTO cstIpi);
        #endregion

        #region Ncm
        [OperationContract]
        int deleteNcm(NcmDTO ncm);
        [OperationContract]
        int salvarAtualizarNcm(NcmDTO ncm);
        [OperationContract]
        IList<NcmDTO> selectNcm(NcmDTO ncm);
        [OperationContract]
        IList<NcmDTO> selectNcmPagina(int primeiroResultado, int quantidadeResultados, NcmDTO ncm);
        #endregion

        #region Feriados
        [OperationContract]
        int deleteFeriados(FeriadosDTO feriados);
        [OperationContract]
        int salvarAtualizarFeriados(FeriadosDTO feriados);
        [OperationContract]
        IList<FeriadosDTO> selectFeriados(FeriadosDTO feriados);
        [OperationContract]
        IList<FeriadosDTO> selectFeriadosPagina(int primeiroResultado, int quantidadeResultados, FeriadosDTO feriados);
        #endregion   

        #region CstCofins
        [OperationContract]
        int deleteCstCofins(CstCofinsDTO cstcofins);
        [OperationContract]
        int salvarAtualizarCstCofins(CstCofinsDTO cstcofins);
        [OperationContract]
        IList<CstCofinsDTO> selectCstCofins(CstCofinsDTO cstcofins);
        [OperationContract]
        IList<CstCofinsDTO> selectCstCofinsPagina(int primeiroResultado, int quantidadeResultados, CstCofinsDTO cstcofins);
        #endregion

        #region CstIcmsA
        [OperationContract]
        int deleteCstIcmsA(CstIcmsADTO csticmsa);
        [OperationContract]
        int salvarAtualizarCstIcmsA(CstIcmsADTO csticmsa);
        [OperationContract]
        IList<CstIcmsADTO> selectCstIcmsA(CstIcmsADTO csticmsa);
        [OperationContract]
        IList<CstIcmsADTO> selectCstIcmsAPagina(int primeiroResultado, int quantidadeResultados, CstIcmsADTO csticmsa);
        #endregion

        #region CstIcmsB
        [OperationContract]
        int deleteCstIcmsB(CstIcmsBDTO csticmsb);
        [OperationContract]
        int salvarAtualizarCstIcmsB(CstIcmsBDTO csticmsb);
        [OperationContract]
        IList<CstIcmsBDTO> selectCstIcmsB(CstIcmsBDTO csticmsb);
        [OperationContract]
        IList<CstIcmsBDTO> selectCstIcmsBPagina(int primeiroResultado, int quantidadeResultados, CstIcmsBDTO csticmsb);
        #endregion

        #region CstPis
        [OperationContract]
        int deleteCstPis(CstPisDTO cstpis);
        [OperationContract]
        int salvarAtualizarCstPis(CstPisDTO cstpis);
        [OperationContract]
        IList<CstPisDTO> selectCstPis(CstPisDTO cstpis);
        [OperationContract]
        IList<CstPisDTO> selectCstPisPagina(int primeiroResultados, int quantidadeResultados, CstPisDTO cstpis);
        #endregion

        #region SituacaoForCli
        [OperationContract]
        int deleteSituacaoForCli(SituacaoForCliDTO situacaoforcli);
        [OperationContract]
        int salvarAtualizarSituacaoForCli(SituacaoForCliDTO situacaoforcli);
        [OperationContract]
        IList<SituacaoForCliDTO> selectSituacaoForCli(SituacaoForCliDTO situacaoforcli);
        [OperationContract]
        IList<SituacaoForCliDTO> selectSituacaoForCliPagina(int primeiroResultado, int quantidadeResultados, SituacaoForCliDTO situacaoforcli);
        #endregion    

        #region AtividadeForCli
        [OperationContract]
        int deleteAtividadeForCli(AtividadeForCliDTO atividadeforcli);
        [OperationContract]
        int salvarAtualizarAtividadeForCli(AtividadeForCliDTO atividadeforcli);
        [OperationContract]
        IList<AtividadeForCliDTO> selectAtividadeForCli(AtividadeForCliDTO atividadeforcli);
        [OperationContract]
        IList<AtividadeForCliDTO> selectAtividadeForCliPagina(int primeiroResultado, int quantidadeResultados, AtividadeForCliDTO atividadeforcli);
        #endregion

        #region Cheque
        [OperationContract]
        int deleteCheque(ChequeDTO cheque);
        [OperationContract]
        int salvarAtualizarCheque(ChequeDTO cheque);
        [OperationContract]
        IList<ChequeDTO> selectCheque(ChequeDTO cheque);
        [OperationContract]
        IList<ChequeDTO> selectChequePagina(int primeiroResultado, int quantidadeResultados, ChequeDTO cheque);
        #endregion

        #region TalonarioCheque
        [OperationContract]
        int deleteTalonarioCheque(TalonarioChequeDTO talonariocheque);
        [OperationContract]
        int salvarAtualizarTalonarioCheque(TalonarioChequeDTO talonariocheque);
        [OperationContract]
        IList<TalonarioChequeDTO> selectTalonarioCheque(TalonarioChequeDTO talonariocheque);
        [OperationContract]
        IList<TalonarioChequeDTO> selectTalonarioChequePagina(int primeiroResultato, int quantidadeResultados, TalonarioChequeDTO talonariocheque);
        #endregion

        #region ContaCaixa
        [OperationContract]
        int deleteContaCaixa(ContaCaixaDTO contacaixa);
        [OperationContract]
        int salvarAtualizarContaCaixa(ContaCaixaDTO contacaixa);
        [OperationContract]
        IList<ContaCaixaDTO> selectContaCaixa(ContaCaixaDTO contacaixa);
        [OperationContract]
        IList<ContaCaixaDTO> selectContaCaixaPagina(int primeiroResultado, int quantidadeResultados, ContaCaixaDTO contacaixa);
        #endregion

        #region Convenio
        [OperationContract]
        int deleteConvenio(ConvenioDTO convenio);
        [OperationContract]
        int salvarAtualizarConvenio(ConvenioDTO convenio);
        [OperationContract]
        IList<ConvenioDTO> selectConvenio(ConvenioDTO convenio);
        [OperationContract]
        IList<ConvenioDTO> selectConvenioPagina(int primeiroResultado, int quantidadeResultados, ConvenioDTO convenio);
        #endregion

        #region OperadoraCartao
        [OperationContract]
        int deleteOperadoraCartao(OperadoraCartaoDTO operadorcartao);
        [OperationContract]
        int salvarAtualizarOperadoraCartao(OperadoraCartaoDTO operadoracartao);
        [OperationContract]
        IList<OperadoraCartaoDTO> selectOperadoraCartao(OperadoraCartaoDTO operadoracartao);
        [OperationContract]
        IList<OperadoraCartaoDTO> selectOperadoraCartaoPagina(int primeiroResultado, int quantidadeResultados, OperadoraCartaoDTO operadoracartao);
        #endregion

        #region OperadoraPlanoSaude
        [OperationContract]
        int deleteOperadoraPlanoSaude(OperadoraPlanoSaudeDTO operadoraplanosaude);
        [OperationContract]
        int salvarAtualizarOperadoraPlanoSaude(OperadoraPlanoSaudeDTO operadoraplanosaude);
        [OperationContract]
        IList<OperadoraPlanoSaudeDTO> selectOperadoraPlanoSaude(OperadoraPlanoSaudeDTO operadoraplanosaude);
        [OperationContract]
        IList<OperadoraPlanoSaudeDTO> selectOperadoraPlanoSaudePagina(int primeiroResultado, int quantidadeResultados, OperadoraPlanoSaudeDTO operadoraplanosaude);
        #endregion

        #region Sindicato
        [OperationContract]
        int deleteSindicato(SindicatoDTO sindicato);
        [OperationContract]
        SindicatoDTO salvarAtualizarSindicato(SindicatoDTO sindicato);
        [OperationContract]
        IList<SindicatoDTO> selectSindicato(SindicatoDTO sindicato);
        [OperationContract]
        IList<SindicatoDTO> selectSindicatoPagina(int primeiroResultado, int quantidadeResultados, SindicatoDTO sindicato);
        #endregion 

        #region SituacaoColaborador
        [OperationContract]
        int deleteSituacaoColaborador(SituacaoColaboradorDTO situacaoColaborador);
        [OperationContract]
        SituacaoColaboradorDTO salvarAtualizarSituacaoColaborador(SituacaoColaboradorDTO situacaoColaborador);
        [OperationContract]
        IList<SituacaoColaboradorDTO> selectSituacaoColaborador(SituacaoColaboradorDTO situacaoColaborador);
        [OperationContract]
        IList<SituacaoColaboradorDTO> selectSituacaoColaboradorPagina(int primeiroResultado, int quantidadeResultados, SituacaoColaboradorDTO situacaoColaborador);
        #endregion 

        #region EstadoCivil
        [OperationContract]
        int deleteEstadoCivil(EstadoCivilDTO estadoCivil);
        [OperationContract]
        int salvarAtualizarEstadoCivil(EstadoCivilDTO estadoCivil);
        [OperationContract]
        IList<EstadoCivilDTO> selectEstadoCivil(EstadoCivilDTO estadoCivil);
        [OperationContract]
        IList<EstadoCivilDTO> selectEstadoCivilPagina(int primeiroResultado, int quantidadeResultados, EstadoCivilDTO estadoCivil);
        #endregion

        #region TipoAdmissao
        [OperationContract]
        int deleteTipoAdmissao(TipoAdmissaoDTO tipoAdmissao);
        [OperationContract]
        TipoAdmissaoDTO salvarAtualizarTipoAdmissao(TipoAdmissaoDTO tipoAdmissao);
        [OperationContract]
        IList<TipoAdmissaoDTO> selectTipoAdmissao(TipoAdmissaoDTO tipoAdmissao);
        [OperationContract]
        IList<TipoAdmissaoDTO> selectTipoAdmissaoPagina(int primeiroResultado, int quantidadeResultados, TipoAdmissaoDTO tipoAdmissao);
        #endregion 

        #region TipoRelacionamento
        [OperationContract]
        int deleteTipoRelacionamento(TipoRelacionamentoDTO tipoRelacionamento);
        [OperationContract]
        TipoRelacionamentoDTO salvarAtualizarTipoRelacionamento(TipoRelacionamentoDTO tipoRelacionamento);
        [OperationContract]
        IList<TipoRelacionamentoDTO> selectTipoRelacionamento(TipoRelacionamentoDTO tipoRelacionamento);
        [OperationContract]
        IList<TipoRelacionamentoDTO> selectTipoRelacionamentoPagina(int primeiroResultado, int quantidadeResultados, TipoRelacionamentoDTO tipoRelacionamento);
        #endregion 

        #region TipoColaborador
        [OperationContract]
        int deleteTipoColaborador(TipoColaboradorDTO tipoColaborador);
        [OperationContract]
        TipoColaboradorDTO salvarAtualizarTipoColaborador(TipoColaboradorDTO tipoColaborador);
        [OperationContract]
        IList<TipoColaboradorDTO> selectTipoColaborador(TipoColaboradorDTO tipoColaborador);
        [OperationContract]
        IList<TipoColaboradorDTO> selectTipoColaboradorPagina(int primeiroResultado, int quantidadeResultados, TipoColaboradorDTO tipoColaborador);
        #endregion 

        #region Cargo
        [OperationContract]
        int deleteCargo(CargoDTO cargo);
        [OperationContract]
        CargoDTO salvarAtualizarCargo(CargoDTO cargo);
        [OperationContract]
        IList<CargoDTO> selectCargo(CargoDTO cargo);
        [OperationContract]
        IList<CargoDTO> selectCargoPagina(int primeiroResultado, int quantidadeResultados, CargoDTO cargo);
        #endregion 

        #region Setor
        [OperationContract]
        int deleteSetor(SetorDTO setor);
        [OperationContract]
        SetorDTO salvarAtualizarSetor(SetorDTO setor);
        [OperationContract]
        IList<SetorDTO> selectSetor(SetorDTO setor);
        [OperationContract]
        IList<SetorDTO> selectSetorPagina(int primeiroResultado, int quantidadeResultados, SetorDTO setor);
        #endregion 

        #region Colaborador
        [OperationContract]
        int deleteColaborador(ColaboradorDTO colaborador);
        [OperationContract]
        ColaboradorDTO salvarAtualizarColaborador(ColaboradorDTO colaborador);
        [OperationContract]
        IList<ColaboradorDTO> selectColaborador(ColaboradorDTO colaborador);
        [OperationContract]
        IList<ColaboradorDTO> selectColaboradorPagina(int primeiroResultado, int quantidadeResultados, ColaboradorDTO colaborador);
        #endregion 

        #region Contador
        [OperationContract]
        int deleteContador(ContadorDTO contador);
        [OperationContract]
        ContadorDTO salvarAtualizarContador(ContadorDTO contador);
        [OperationContract]
        IList<ContadorDTO> selectContador(ContadorDTO contador);
        [OperationContract]
        IList<ContadorDTO> selectContadorPagina(int primeiroResultado, int quantidadeResultados, ContadorDTO contador);
        #endregion 

        #region ProdutoMarca
        [OperationContract]
        int deleteProdutoMarca(ProdutoMarcaDTO produtoMarca);
        [OperationContract]
        ProdutoMarcaDTO salvarAtualizarProdutoMarca(ProdutoMarcaDTO produtoMarca);
        [OperationContract]
        IList<ProdutoMarcaDTO> selectProdutoMarca(ProdutoMarcaDTO produtoMarca);
        [OperationContract]
        IList<ProdutoMarcaDTO> selectProdutoMarcaPagina(int primeiroResultado, int quantidadeResultados, ProdutoMarcaDTO produtoMarca);
        #endregion 

        #region UnidadeProduto
        [OperationContract]
        int deleteUnidadeProduto(UnidadeProdutoDTO unidadeProduto);
        [OperationContract]
        UnidadeProdutoDTO salvarAtualizarUnidadeProduto(UnidadeProdutoDTO unidadeProduto);
        [OperationContract]
        IList<UnidadeProdutoDTO> selectUnidadeProduto(UnidadeProdutoDTO unidadeProduto);
        [OperationContract]
        IList<UnidadeProdutoDTO> selectUnidadeProdutoPagina(int primeiroResultado, int quantidadeResultados, UnidadeProdutoDTO unidadeProduto);
        #endregion 

        #region ProdutoGrupo
        [OperationContract]
        int deleteProdutoGrupo(ProdutoGrupoDTO produtoGrupo);
        [OperationContract]
        ProdutoGrupoDTO salvarAtualizarProdutoGrupo(ProdutoGrupoDTO produtoGrupo);
        [OperationContract]
        IList<ProdutoGrupoDTO> selectProdutoGrupo(ProdutoGrupoDTO produtoGrupo);
        [OperationContract]
        IList<ProdutoGrupoDTO> selectProdutoGrupoPagina(int primeiroResultado, int quantidadeResultados, ProdutoGrupoDTO produtoGrupo);
        #endregion 

        #region ProdutoSubGrupo
        [OperationContract]
        int deleteProdutoSubGrupo(ProdutoSubGrupoDTO produtoSubGrupo);
        [OperationContract]
        ProdutoSubGrupoDTO salvarAtualizarProdutoSubGrupo(ProdutoSubGrupoDTO produtoSubGrupo);
        [OperationContract]
        IList<ProdutoSubGrupoDTO> selectProdutoSubGrupo(ProdutoSubGrupoDTO produtoSubGrupo);
        [OperationContract]
        IList<ProdutoSubGrupoDTO> selectProdutoSubGrupoPagina(int primeiroResultado, int quantidadeResultados, ProdutoSubGrupoDTO produtoSubGrupo);
        #endregion 

        #region Almoxarifado
        [OperationContract]
        int deleteAlmoxarifado(AlmoxarifadoDTO almoxarifado);
        [OperationContract]
        AlmoxarifadoDTO salvarAtualizarAlmoxarifado(AlmoxarifadoDTO almoxarifado);
        [OperationContract]
        IList<AlmoxarifadoDTO> selectAlmoxarifado(AlmoxarifadoDTO almoxarifado);
        [OperationContract]
        IList<AlmoxarifadoDTO> selectAlmoxarifadoPagina(int primeiroResultado, int quantidadeResultados, AlmoxarifadoDTO almoxarifado);
        #endregion 

        #region Produto
        [OperationContract]
        int deleteProduto(ProdutoDTO produto);
        [OperationContract]
        ProdutoDTO salvarAtualizarProduto(ProdutoDTO produto);
        [OperationContract]
        IList<ProdutoDTO> selectProduto(ProdutoDTO produto);
        [OperationContract]
        IList<ProdutoDTO> selectProdutoPagina(int primeiroResultado, int quantidadeResultados, ProdutoDTO produto);
        #endregion 


        #region Apenas Consultas

        #region Empresa
        [OperationContract]
        IList<EmpresaDTO> selectEmpresa(EmpresaDTO empresa);
        #endregion

        #region ContabilConta
        [OperationContract]
        IList<ContabilContaDTO> selectContabilConta(ContabilContaDTO contabilConta);
        [OperationContract]
        IList<ContabilContaDTO> selectContabilContaPagina(int primeiroResultado, int quantidadeResultados, ContabilContaDTO contabilConta);
        #endregion 

        #region TributOperacaoFiscal
        [OperationContract]
        IList<TributOperacaoFiscalDTO> selectTributOperacaoFiscal(TributOperacaoFiscalDTO tributOperacaoFiscal);
        [OperationContract]
        IList<TributOperacaoFiscalDTO> selectTributOperacaoFiscalPagina(int primeiroResultado, int quantidadeResultados, TributOperacaoFiscalDTO tributOperacaoFiscal);
        #endregion 

        #region NivelFormacao
        [OperationContract]
        IList<NivelFormacaoDTO> selectNivelFormacao(NivelFormacaoDTO nivelFormacao);
        [OperationContract]
        IList<NivelFormacaoDTO> selectNivelFormacaoPagina(int primeiroResultado, int quantidadeResultados, NivelFormacaoDTO nivelFormacao);
        #endregion 

        #region TributGrupoTributario
        [OperationContract]
        IList<TributGrupoTributarioDTO> selectTributGrupoTributario(TributGrupoTributarioDTO tributGrupoTributario);
        [OperationContract]
        IList<TributGrupoTributarioDTO> selectTributGrupoTributarioPagina(int primeiroResultado, int quantidadeResultados, TributGrupoTributarioDTO tributGrupoTributario);
        #endregion 

        #region TributIcmsCustomCab
        [OperationContract]
        IList<TributIcmsCustomCabDTO> selectTributIcmsCustomCab(TributIcmsCustomCabDTO tributIcmsCustomCab);
        [OperationContract]
        IList<TributIcmsCustomCabDTO> selectTributIcmsCustomCabPagina(int primeiroResultado, int quantidadeResultados, TributIcmsCustomCabDTO tributIcmsCustomCab);
        #endregion 
 
        #region Usuario
        [OperationContract]
        UsuarioDTO selectUsuario(String login, String senha);
        #endregion

        #region ControleAcesso
        [OperationContract]
        IList<ViewControleAcessoDTO> selectControleAcesso(ViewControleAcessoDTO viewControleAcesso);
        #endregion

        #endregion

    }
}
