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
    public class CstIcmsBViewModel : ERPViewModelBase
    {
        public ObservableCollection<CstIcmsBDTO> listaCstIcmsB { get; set; }
        private CstIcmsBDTO _csticmsbSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }


        public CstIcmsBViewModel()
        {
            try
            {
                listaCstIcmsB = new ObservableCollection<CstIcmsBDTO>();
                primeiroResultado = 0;
                this.atualizarListaCstIcmsB(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CstIcmsBDTO csticmsbSelected
        {
            get { return _csticmsbSelected; }
            set
            {
                _csticmsbSelected = value;
                notifyPropertyChanged("csticmsbSelected");
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
                            this.atualizarListaCstIcmsB(1);
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
                            this.atualizarListaCstIcmsB(-1);
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

        public void salvarAtualizarCstIcmsB()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarCstIcmsB(csticmsbSelected);
                    csticmsbSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaCstIcmsB(int pagina)
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

                    List<CstIcmsBDTO> listaServ = serv.selectCstIcmsBPagina(primeiroResultado, QUANTIDADE_PAGINA, new CstIcmsBDTO());

                    listaCstIcmsB.Clear();

                    foreach (CstIcmsBDTO objAdd in listaServ)
                    {
                        listaCstIcmsB.Add(objAdd);
                    }
                    csticmsbSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirCstIcmsB()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteCstIcmsB(csticmsbSelected);
                    csticmsbSelected = null;
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
