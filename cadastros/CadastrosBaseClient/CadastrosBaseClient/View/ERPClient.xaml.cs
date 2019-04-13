using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CadastrosBaseClient.View.CadastrosBase;
using Microsoft.Windows.Controls.Ribbon;
using CadastrosBaseClient.ViewModel.CadastrosBase;
using CloseableTabItemDemo;

namespace CadastrosBaseClient.View
{
    /// <summary>
    /// Interaction logic for ERPClient.xaml
    /// </summary>
    public partial class ERPClient : RibbonWindow
    {
        CadastrosBaseViewModel viewModel = new CadastrosBaseViewModel();
        public static Window JanelaLogin;

        public ERPClient()
        {
            InitializeComponent();
            dockModulo.Children.Clear();
            dockModulo.Children.Add(new CadastrosBasePrincipal());
            doLogin();
        }

        private void MenuItem1_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Tem Certeza Que Deseja Sair do Sistema?", "Sair do Sistema", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        private void doLogin() 
        {
            Login janela = new Login();
            Window window = new Window()
            {
                Title = "Login",
                ShowInTaskbar = false,
                Topmost = false,
                ResizeMode = ResizeMode.NoResize,
                Width = 525,
                Height = 222,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen
            };
            window.Content = janela;
            JanelaLogin = window;
            window.ShowDialog();

            if (Login.logado == false)
            {
                MessageBox.Show("Aplicação será Encerrada.", "Informação do Sistema", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }

        private void BotaoEstadoCivil_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new EstadoCivilPrincipal(), "Estado Civil");
        }

        private void BotaoPessoa_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new PessoaPrincipal(), "Pessoa");
        }

        private void BotaoAtividade_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new AtividadeForCliPrincipal(), "Atividade Cliente/Fornecedor");
        }

        private void BotaoSituacao_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new SituacaoForCliPrincipal(), "Situação Cliente/Fornecedor");
        }

        private void BotaoCliente_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new ClientePrincipal(), "Cliente");
        }

        private void BotaoFornecedor_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new FornecedorPrincipal(), "Fornecedor");
        }

        private void BotaoTransportadora_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new TransportadoraPrincipal(), "Transportadora");
        }

        private void BotaoTipoAdmissao_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new TipoAdmissaoPrincipal(), "Tipo Admissão");
        }

        private void BotaoTipoRelacionamento_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new TipoRelacionamentoPrincipal(), "Tipo Relacionamento");
        }

        private void BotaoSituacaoColaborador_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new SituacaoColaboradorPrincipal(), "Situação Colaborador");
        }

        private void BotaoTipoDesligamento_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new TipoDesligamentoPrincipal(), "Tipo Desligamento");
        }

        private void BotaoTipoColaborador_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new TipoColaboradorPrincipal(), "Tipo Colaborador");
        }

        private void BotaoCargo_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new CargoPrincipal(), "Cargo");
        }

        private void BotaoNivelFormacao_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BotaoColaborador_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new ColaboradorPrincipal(), "Colaborador");
        }

        private void BotaoContador_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new ContadorPrincipal(), "Contador");
        }

        private void BotaoSindicato_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new SindicatoPrincipal(), "Sindicato");
        }

        private void BotaoConvenio_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new ConvenioPrincipal(), "Convênio");
        }

        private void BotaoSetor_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new SetorPrincipal(), "Setor");
        }

        private void BotaoAlmoxarifado_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new AlmoxarifadoPrincipal(), "Almoxarifado");
        }

        private void BotaoOperadoraPlanoSaude_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new OperadoraPlanoSaudePrincipal(), "Operadora Plano Saúde");
        }

        private void BotaoOperadoracartao_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new OperadoraCartaoPrincipal(), "Operadora Cartão");
        }

        private void BotaoPais_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BotaoEstado_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BotaoMunicipio_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BotaoCep_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new CepPrincipal(), "CEP");
        }

        private void BotaoMarca_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new ProdutoMarcaPrincipal(), "Produto Marca");
        }

        private void BotaoNcm_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new NcmPrincipal(), "NCM");
        }

        private void BotaoUnidade_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new UnidadeProdutoPrincipal(), "Produto Unidade");
        }

        private void BotaoGrupo_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new ProdutoGrupoPrincipal(), "Produto Grupo");
        }

        private void BotaoSubGrupo_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new ProdutoSubGrupoPrincipal(), "Produto Subgrupo");
        }

        private void BotaoProduto_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new ProdutoPrincipal(), "Produto");
        }

        private void BotaoBanco_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BotaoAgencia_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BotaoContaCaixa_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new ContaCaixaPrincipal(), "Conta/Caixa");
        }

        private void BotaoTalonario_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new TalonarioChequePrincipal(), "Talonário");
        }

        private void BotaoCheque_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new ChequePrincipal(), "Cheque");
        }

        private void BotaoTipoItemSped_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BotaoSpedPis439_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BotaoSpedPis4310_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BotaoSpedPis4313_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BotaoSpedPis4314_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BotaoSpedPis4315_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BotaoSpedPis4316_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BotaoCategoriaTrabalhoSefip_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BotaoCodigoMovimentacaoSefip_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new SefipCodigoMovimentacaoPrincipal(), "Sefip Código Movimentação");
        }

        private void BotaoCodigoRecolhimentoSefip_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BotaoTipoCreditoPis_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new TipoCreditoPisPrincipal(), "Tipo Crédito Pis");
        }

        private void BotaoBaseCreditoPis_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new BaseCreditoPisPrincipal(), "Base Crédito Pis");
        }

        private void BotaoCstCofins_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new CstCofinsPrincipal(), "CST Cofins");
        }

        private void BotaoCstIcmsA_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new CstIcmsAPrincipal(), "CST ICMS A");
        }

        private void BotaoCstIcmsB_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new CstIcmsBPrincipal(), "CST ICMS B");
        }

        private void BotaoCstIpi_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new CstIpiPrincipal(), "CST IPI");
        }

        private void BotaoCstPis_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new CstPisPrincipal(), "CST Pis");
        }

        private void BotaoCbo_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new CboPrincipal(), "CBO");
        }

        private void BotaoCfop_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new CfopPrincipal(), "CFOP");
        }

        private void BotaoGps_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new CodigoGpsPrincipal(), "Código GPS");
        }

        private void BotaoSalarioMinimo_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new SalarioMinimoPrincipal(), "Salário Mínimo");
        }

        private void BotaoSituacaoDocumento_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new SituacaoDocumentoPrincipal(), "Situação Documento");
        }

        private void BotaoCsosnA_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new CsosnAPrincipal(), "CSOSN A");
        }

        private void BotaoCsosnB_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new CsosnBPrincipal(), "CSOSN B");
        }

        private void BotaoFeriados_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new FeriadosPrincipal(), "Feriados");
        }

    }
}
