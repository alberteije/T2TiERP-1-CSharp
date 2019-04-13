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
    public class ContaCaixaViewModel : ERPViewModelBase
    {
        public ObservableCollection<ContaCaixaDTO> listaContaCaixa { get; set; }
        private ContaCaixaDTO _contacaixaSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }

        public ContaCaixaViewModel()
        {
            try
            {
                listaContaCaixa = new ObservableCollection<ContaCaixaDTO>();
                primeiroResultado = 0;
                this.atualizarListaContaCaixa(0);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ContaCaixaDTO contacaixaSelected
        {
            get { return _contacaixaSelected; }
            set
            {
                _contacaixaSelected = value;
                notifyPropertyChanged("contacaixaSelected");
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
                            this.atualizarListaContaCaixa(1);
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
                            this.atualizarListaContaCaixa(-1);
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

        public void salvarAtualizarContaCaixa()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarContaCaixa(contacaixaSelected);
                    contacaixaSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaContaCaixa(int pagina)
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

                    List<ContaCaixaDTO> listaServ = serv.selectContaCaixaPagina(primeiroResultado, QUANTIDADE_PAGINA, new ContaCaixaDTO());

                    listaContaCaixa.Clear();

                    foreach (ContaCaixaDTO objAdd in listaServ)
                    {
                        listaContaCaixa.Add(objAdd);
                    }
                    contacaixaSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirContaCaixa()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteContaCaixa(contacaixaSelected);
                    contacaixaSelected = null;
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
