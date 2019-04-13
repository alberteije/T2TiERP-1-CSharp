using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CadastrosBaseClient.Command;
using CadastrosBaseClient.CadastrosBaseReference;

namespace CadastrosBaseClient.ViewModel.CadastrosBase
{
    public class SefipCodigoMovimentacaoViewModel : ERPViewModelBase
    {
        public ObservableCollection<SefipCodigoMovimentacaoDTO> listaSefipCodigoMovimentacao { get; set; }
        private SefipCodigoMovimentacaoDTO _sefipCodigoMovimentacaoSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }

        public SefipCodigoMovimentacaoViewModel()
        {
            try
            {
                listaSefipCodigoMovimentacao = new ObservableCollection<SefipCodigoMovimentacaoDTO>();
                primeiroResultado = 0;
                this.atualizarListaSefipCodigoMovimentacao(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SefipCodigoMovimentacaoDTO sefipCodigoMovimentacaoSelected
        {
            get { return _sefipCodigoMovimentacaoSelected; }
            set
            {
                _sefipCodigoMovimentacaoSelected = value;
                notifyPropertyChanged("sefipCodigoMovimentacaoSelected");
            }
        }

        public ICommand paginaSeguinteCommand
        {
            get
            {
                if (seguinteCommand == null)
                {
                    seguinteCommand = new RelayCommand
                    (
                        param =>
                        {
                            this.atualizarListaSefipCodigoMovimentacao(1);
                        },
                            param =>
                            {
                                return true;
                            }
                    );
                }
                return seguinteCommand;
            }
        }

        public ICommand paginaAnteriorCommand
        {
            get
            {
                if (anteriorCommand == null)
                {
                    anteriorCommand = new RelayCommand
                    (
                        param =>
                        {
                            this.atualizarListaSefipCodigoMovimentacao(-1);
                        },
                            param =>
                            {
                                return true;
                            }
                    );
                }
                return anteriorCommand;
            }
        }

        public void salvarAtualizarSefipCodigoMovimentacao()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarSefipCodigoMovimentacao(sefipCodigoMovimentacaoSelected);
                    sefipCodigoMovimentacaoSelected = null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void atualizarListaSefipCodigoMovimentacao(int pagina)
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    if (pagina == 0)
                        primeiroResultado = 0;
                    else if (pagina > 0)
                        primeiroResultado += QUANTIDADE_PAGINA;
                    else if (pagina < 0)
                        primeiroResultado -= QUANTIDADE_PAGINA;
                    List<SefipCodigoMovimentacaoDTO> listaServ = serv.selectSefipCodigoMovimentacaoPagina(primeiroResultado, QUANTIDADE_PAGINA, new SefipCodigoMovimentacaoDTO());
                    listaSefipCodigoMovimentacao.Clear();
                    foreach (SefipCodigoMovimentacaoDTO objAdd in listaServ)
                    {
                        listaSefipCodigoMovimentacao.Add(objAdd);
                    }
                    sefipCodigoMovimentacaoSelected = null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void excluirSefipCodigoMovimentacao()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteSefipCodigoMovimentacao(sefipCodigoMovimentacaoSelected);
                    sefipCodigoMovimentacaoSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsEditar
        {
            get { return _isEditar; }
            set
            {
                _isEditar = value;
                notifyPropertyChanged("IsEditar");
                notifyPropertyChanged("IsListar");
            }
        }

        public bool IsListar
        {
            get { return !_isEditar; }
            set
            {
                _isEditar = !value;
                notifyPropertyChanged("IsEditar");
                notifyPropertyChanged("IsListar");
            }
        }
    }
}
