using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CadastrosBaseClient.CadastrosBaseReference;
using SearchWindow.Attributes;


namespace CadastrosBaseClient.Model
{
    public class ServicoCadastrosBase : ServicoCadastrosBaseClient
    {
        [SearchWindowDataSource(typeof(AtividadeForCliDTO))]
        public new List<AtividadeForCliDTO> selectAtividadeForCli(AtividadeForCliDTO atividadeforcli) 
        {
            return base.selectAtividadeForCli(atividadeforcli);
        }

        [SearchWindowDataSource(typeof(SituacaoForCliDTO))]
        public new List<SituacaoForCliDTO> selectSituacaoForCli(SituacaoForCliDTO situacaoforcli)
        {
            return base.selectSituacaoForCli(situacaoforcli);
        }

        [SearchWindowDataSource(typeof(EstadoCivilDTO))]
        public new List<EstadoCivilDTO> selectEstadoCivil(EstadoCivilDTO estadocivil)
        {
            return base.selectEstadoCivil(estadocivil);
        }

        [SearchWindowDataSource(typeof(ContabilContaDTO))]
        public new List<ContabilContaDTO> selectContabilConta(ContabilContaDTO ContabilConta)
        {
            return base.selectContabilConta(ContabilConta);
        }

        [SearchWindowDataSource(typeof(TributOperacaoFiscalDTO))]
        public new List<TributOperacaoFiscalDTO> selectTributOperacaoFiscal(TributOperacaoFiscalDTO TributOperacaoFiscal)
        {
            return base.selectTributOperacaoFiscal(TributOperacaoFiscal);
        }

        [SearchWindowDataSource(typeof(SetorDTO))]
        public new List<SetorDTO> selectSetor(SetorDTO Setor)
        {
            return base.selectSetor(Setor);
        }

        [SearchWindowDataSource(typeof(CargoDTO))]
        public new List<CargoDTO> selectCargo(CargoDTO Cargo)
        {
            return base.selectCargo(Cargo);
        }

        [SearchWindowDataSource(typeof(NivelFormacaoDTO))]
        public new List<NivelFormacaoDTO> selectNivelFormacao(NivelFormacaoDTO NivelFormacao)
        {
            return base.selectNivelFormacao(NivelFormacao);
        }


        [SearchWindowDataSource(typeof(TipoColaboradorDTO))]
        public new List<TipoColaboradorDTO> selectTipoColaborador(TipoColaboradorDTO TipoColaborador)
        {
            return base.selectTipoColaborador(TipoColaborador);
        }

        [SearchWindowDataSource(typeof(SituacaoColaboradorDTO))]
        public new List<SituacaoColaboradorDTO> selectSituacaoColaborador(SituacaoColaboradorDTO SituacaoColaborador)
        {
            return base.selectSituacaoColaborador(SituacaoColaborador);
        }

        [SearchWindowDataSource(typeof(SindicatoDTO))]
        public new List<SindicatoDTO> selectSindicato(SindicatoDTO Sindicato)
        {
            return base.selectSindicato(Sindicato);
        }

        [SearchWindowDataSource(typeof(ProdutoGrupoDTO))]
        public new List<ProdutoGrupoDTO> selectProdutoGrupo(ProdutoGrupoDTO ProdutoGrupo)
        {
            return base.selectProdutoGrupo(ProdutoGrupo);
        }

        [SearchWindowDataSource(typeof(ProdutoSubGrupoDTO))]
        public new List<ProdutoSubGrupoDTO> selectProdutoSubGrupo(ProdutoSubGrupoDTO ProdutoSubGrupo)
        {
            return base.selectProdutoSubGrupo(ProdutoSubGrupo);
        }

        [SearchWindowDataSource(typeof(ProdutoMarcaDTO))]
        public new List<ProdutoMarcaDTO> selectProdutoMarca(ProdutoMarcaDTO ProdutoMarca)
        {
            return base.selectProdutoMarca(ProdutoMarca);
        }

        [SearchWindowDataSource(typeof(TributGrupoTributarioDTO))]
        public new List<TributGrupoTributarioDTO> selectTributGrupoTributario(TributGrupoTributarioDTO TributGrupoTributario)
        {
            return base.selectTributGrupoTributario(TributGrupoTributario);
        }

        [SearchWindowDataSource(typeof(AlmoxarifadoDTO))]
        public new List<AlmoxarifadoDTO> selectAlmoxarifado(AlmoxarifadoDTO Almoxarifado)
        {
            return base.selectAlmoxarifado(Almoxarifado);
        }

        [SearchWindowDataSource(typeof(UnidadeProdutoDTO))]
        public new List<UnidadeProdutoDTO> selectUnidadeProduto(UnidadeProdutoDTO UnidadeProduto)
        {
            return base.selectUnidadeProduto(UnidadeProduto);
        }

        [SearchWindowDataSource(typeof(TributIcmsCustomCabDTO))]
        public new List<TributIcmsCustomCabDTO> selectTributIcmsCustomCab(TributIcmsCustomCabDTO TributIcmsCustomCab)
        {
            return base.selectTributIcmsCustomCab(TributIcmsCustomCab);
        }
    
    
    }



}
