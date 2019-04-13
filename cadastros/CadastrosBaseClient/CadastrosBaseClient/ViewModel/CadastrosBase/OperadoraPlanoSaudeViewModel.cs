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
    public class OperadoraPlanoSaudeViewModel : ERPViewModelBase
    {
        public ObservableCollection<OperadoraPlanoSaudeDTO> listaOperadoraPlanoSaude { get; set; }
        private OperadoraPlanoSaudeDTO _operadoraplanosaudeSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }


        public OperadoraPlanoSaudeViewModel()
        {
            try
            {
                listaOperadoraPlanoSaude = new ObservableCollection<OperadoraPlanoSaudeDTO>();
                primeiroResultado = 0;
                this.atualizarListaOperadoraPlanoSaude(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OperadoraPlanoSaudeDTO operadoraplanosaudeSelected
        {
            get { return _operadoraplanosaudeSelected; }
            set
            {
                _operadoraplanosaudeSelected = value;
                notifyPropertyChanged("operadoraplanosaudeSelected");
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
                            this.atualizarListaOperadoraPlanoSaude(1);
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
                            this.atualizarListaOperadoraPlanoSaude(-1);
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

        public void salvarAtualizarOperadoraPlanoSaude()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarOperadoraPlanoSaude(operadoraplanosaudeSelected);
                    operadoraplanosaudeSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaOperadoraPlanoSaude(int pagina)
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

                    List<OperadoraPlanoSaudeDTO> listaServ = serv.selectOperadoraPlanoSaudePagina(primeiroResultado, QUANTIDADE_PAGINA, new OperadoraPlanoSaudeDTO());

                    listaOperadoraPlanoSaude.Clear();

                    foreach (OperadoraPlanoSaudeDTO objAdd in listaServ)
                    {
                        listaOperadoraPlanoSaude.Add(objAdd);
                    }
                    operadoraplanosaudeSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirOperadoraPlanoSaude()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteOperadoraPlanoSaude(operadoraplanosaudeSelected);
                    operadoraplanosaudeSelected = null;
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
