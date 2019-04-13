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
    public class CstIpiViewModel : ERPViewModelBase
    {
        public ObservableCollection<CstIpiDTO> listaCstIpi { get; set; }
        private CstIpiDTO _cstipiSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }


        public CstIpiViewModel()
        {
            try
            {
                listaCstIpi = new ObservableCollection<CstIpiDTO>();
                primeiroResultado = 0;
                this.atualizarListaCstIpi(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CstIpiDTO cstipiSelected
        {
            get { return _cstipiSelected; }
            set
            {
                _cstipiSelected = value;
                notifyPropertyChanged("cstipiSelected");
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
                            this.atualizarListaCstIpi(1);
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
                            this.atualizarListaCstIpi(-1);
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

        public void salvarAtualizarCstIpi()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarCstIpi(cstipiSelected);
                    cstipiSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaCstIpi(int pagina)
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

                    List<CstIpiDTO> listaServ = serv.selectCstIpiPagina(primeiroResultado, QUANTIDADE_PAGINA, new CstIpiDTO());

                    listaCstIpi.Clear();

                    foreach (CstIpiDTO objAdd in listaServ)
                    {
                        listaCstIpi.Add(objAdd);
                    }
                    cstipiSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirCstIpi()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteCstIpi(cstipiSelected);
                    cstipiSelected = null;
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
