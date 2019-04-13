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
    public class CstIcmsAViewModel : ERPViewModelBase
    {
        public ObservableCollection<CstIcmsADTO> listaCstIcmsA { get; set; }
        private CstIcmsADTO _csticmsaSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }


        public CstIcmsAViewModel()
        {
            try
            {
                listaCstIcmsA = new ObservableCollection<CstIcmsADTO>();
                primeiroResultado = 0;
                this.atualizarListaCstIcmsA(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CstIcmsADTO csticmsaSelected
        {
            get { return _csticmsaSelected; }
            set
            {
                _csticmsaSelected = value;
                notifyPropertyChanged("csticmsaSelected");
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
                            this.atualizarListaCstIcmsA(1);
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
                            this.atualizarListaCstIcmsA(-1);
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

        public void salvarAtualizarCstIcmsA()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarCstIcmsA(csticmsaSelected);
                    csticmsaSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaCstIcmsA(int pagina)
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

                    List<CstIcmsADTO> listaServ = serv.selectCstIcmsAPagina(primeiroResultado, QUANTIDADE_PAGINA, new CstIcmsADTO());

                    listaCstIcmsA.Clear();

                    foreach (CstIcmsADTO objAdd in listaServ)
                    {
                        listaCstIcmsA.Add(objAdd);
                    }
                    csticmsaSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirCstIcmsA()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteCstIcmsA(csticmsaSelected);
                    csticmsaSelected = null;
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
