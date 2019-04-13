using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using CadastrosBaseClient.Command;
using System.Collections.ObjectModel;
using CadastrosBaseClient.CadastrosBaseReference;

namespace CadastrosBaseClient.ViewModel.CadastrosBase
{
    public class SituacaoForCliViewModel : ERPViewModelBase
    {
        public ObservableCollection<SituacaoForCliDTO> listaSituacaoForCli{ get; set; }
        private SituacaoForCliDTO _situacaoforcliSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }


        public SituacaoForCliViewModel()
        {
            try
            {
                listaSituacaoForCli = new ObservableCollection<SituacaoForCliDTO>();
                primeiroResultado = 0;
                this.atualizarListaSituacaoForCli(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SituacaoForCliDTO situacaoforcliSelected
        {
            get { return _situacaoforcliSelected; }
            set
            {
                _situacaoforcliSelected = value;
                notifyPropertyChanged("situacaoforcliSelected");
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
                            this.atualizarListaSituacaoForCli(1);
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
                            this.atualizarListaSituacaoForCli(-1);
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

        public void salvarAtualizarSituacaoForCli()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarSituacaoForCli(situacaoforcliSelected);
                    situacaoforcliSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaSituacaoForCli(int pagina)
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

                    List<SituacaoForCliDTO> listaServ = serv.selectSituacaoForCliPagina(primeiroResultado, QUANTIDADE_PAGINA, new SituacaoForCliDTO());

                    listaSituacaoForCli.Clear();

                    foreach (SituacaoForCliDTO objAdd in listaServ)
                    {
                        listaSituacaoForCli.Add(objAdd);
                    }
                    situacaoforcliSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirSituacaoForCli()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteSituacaoForCli(situacaoforcliSelected);
                    situacaoforcliSelected = null;
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
