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
    public class OperadoraCartaoViewModel : ERPViewModelBase
    {
        public ObservableCollection<OperadoraCartaoDTO> listaOperadoraCartao { get; set; }
        private OperadoraCartaoDTO _operadoracartaoSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }


        public OperadoraCartaoViewModel()
        {
            try
            {
                listaOperadoraCartao = new ObservableCollection<OperadoraCartaoDTO>();
                primeiroResultado = 0;
                this.atualizarListaOperadoraCartao(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OperadoraCartaoDTO operadoracartaoSelected
        {
            get { return _operadoracartaoSelected; }
            set
            {
                _operadoracartaoSelected = value;
                notifyPropertyChanged("operadoracartaoSelected");
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
                            this.atualizarListaOperadoraCartao(1);
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
                            this.atualizarListaOperadoraCartao(-1);
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

        public void salvarAtualizarOperadoraCartao()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarOperadoraCartao(operadoracartaoSelected);
                    operadoracartaoSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaOperadoraCartao(int pagina)
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

                    List<OperadoraCartaoDTO> listaServ = serv.selectOperadoraCartaoPagina(primeiroResultado, QUANTIDADE_PAGINA, new OperadoraCartaoDTO());

                    listaOperadoraCartao.Clear();

                    foreach (OperadoraCartaoDTO objAdd in listaServ)
                    {
                        listaOperadoraCartao.Add(objAdd);
                    }
                    operadoracartaoSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirOperadoraCartao()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteOperadoraCartao(operadoracartaoSelected);
                    operadoracartaoSelected = null;
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
