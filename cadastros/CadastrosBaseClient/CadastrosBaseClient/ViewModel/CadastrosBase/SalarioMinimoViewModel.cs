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
    public class SalarioMinimoViewModel : ERPViewModelBase
    {
        public ObservableCollection<SalarioMinimoDTO> listaSalarioMinimo { get; set; }
        private SalarioMinimoDTO _salarioMinimoSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }

        public SalarioMinimoViewModel()
        {
            try
            {
                listaSalarioMinimo = new ObservableCollection<SalarioMinimoDTO>();
                primeiroResultado = 0;
                this.atualizarListaSalarioMinimo(0);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public SalarioMinimoDTO salarioMinimoSelected
        {
            get { return _salarioMinimoSelected; }
            set
            {
                _salarioMinimoSelected = value;
                notifyPropertyChanged("salarioMinimoSelected");
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
                            this.atualizarListaSalarioMinimo(1);
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
                            this.atualizarListaSalarioMinimo(-1);
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

        public void salvarAtualizarSalarioMinimo()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarSalarioMinimo(salarioMinimoSelected);
                    salarioMinimoSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaSalarioMinimo(int pagina)
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

                    List<SalarioMinimoDTO> listaServ = serv.selectSalarioMinimoPagina(primeiroResultado, QUANTIDADE_PAGINA, new SalarioMinimoDTO());

                    listaSalarioMinimo.Clear();

                    foreach (SalarioMinimoDTO objAdd in listaServ)
                    {
                        listaSalarioMinimo.Add(objAdd);
                    }
                    salarioMinimoSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirSalarioMinimo()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteSalarioMinimo(salarioMinimoSelected);
                    salarioMinimoSelected = null;
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
