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
    public class AtividadeForCliViewModel : ERPViewModelBase
    {
        public ObservableCollection<AtividadeForCliDTO> listaAtividadeForCli { get; set; }
        private AtividadeForCliDTO _atividadeforcliSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }


        public AtividadeForCliViewModel()
        {
            try
            {
                listaAtividadeForCli = new ObservableCollection<AtividadeForCliDTO>();
                primeiroResultado = 0;
                this.atualizarListaAtividadeForCli(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AtividadeForCliDTO atividadeforcliSelected
        {
            get { return _atividadeforcliSelected; }
            set
            {
                _atividadeforcliSelected = value;
                notifyPropertyChanged("atividadeforcliSelected");
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
                            this.atualizarListaAtividadeForCli(1);
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
                            this.atualizarListaAtividadeForCli(-1);
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

        public void salvarAtualizarAtividadeForCli()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarAtividadeForCli(atividadeforcliSelected);
                    atividadeforcliSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaAtividadeForCli(int pagina)
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

                    List<AtividadeForCliDTO> listaServ = serv.selectAtividadeForCliPagina(primeiroResultado, QUANTIDADE_PAGINA, new AtividadeForCliDTO());

                    listaAtividadeForCli.Clear();

                    foreach (AtividadeForCliDTO objAdd in listaServ)
                    {
                        listaAtividadeForCli.Add(objAdd);
                    }
                    atividadeforcliSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirAtividadeForCli()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteAtividadeForCli(atividadeforcliSelected);
                    atividadeforcliSelected = null;
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
