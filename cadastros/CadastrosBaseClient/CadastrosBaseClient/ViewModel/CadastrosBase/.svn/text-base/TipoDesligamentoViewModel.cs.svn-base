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
    public class TipoDesligamentoViewModel : ERPViewModelBase
    {
        public ObservableCollection<TipoDesligamentoDTO> listaTipoDesligamento { get; set; }
        private TipoDesligamentoDTO _tipoDesligamentoSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }

        public TipoDesligamentoViewModel()
        {
            try
            {
                listaTipoDesligamento = new ObservableCollection<TipoDesligamentoDTO>();
                primeiroResultado = 0;
                this.atualizarListaTipoDesligamento(0);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public TipoDesligamentoDTO tipoDesligamentoSelected
        {
            get { return _tipoDesligamentoSelected; }
            set
            {
                _tipoDesligamentoSelected = value;
                notifyPropertyChanged("tipoDesligamentoSelected");
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
                            this.atualizarListaTipoDesligamento(1);
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
                            this.atualizarListaTipoDesligamento(-1);
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

        public void salvarAtualizarTipo_Desligamento()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarTipoDesligamento(tipoDesligamentoSelected);
                    tipoDesligamentoSelected = null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void atualizarListaTipoDesligamento(int pagina)
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
                    List<TipoDesligamentoDTO> listaServ = serv.selectTipoDesligamentoPagina(primeiroResultado, QUANTIDADE_PAGINA, new TipoDesligamentoDTO());
                    listaTipoDesligamento.Clear();
                    foreach (TipoDesligamentoDTO objAdd in listaServ)
                    {
                        listaTipoDesligamento.Add(objAdd);
                    }
                    tipoDesligamentoSelected = null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void excluirTipoDesligamento()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteTipoDesligamento(tipoDesligamentoSelected);
                    tipoDesligamentoSelected = null;
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
