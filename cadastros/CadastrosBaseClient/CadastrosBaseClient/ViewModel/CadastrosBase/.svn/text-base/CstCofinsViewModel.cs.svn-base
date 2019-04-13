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
    public class CstCofinsViewModel : ERPViewModelBase
    {
        public ObservableCollection<CstCofinsDTO> listaCstCofins { get; set; }
        private CstCofinsDTO _cstcofinsSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }


        public CstCofinsViewModel()
        {
            try
            {
                listaCstCofins = new ObservableCollection<CstCofinsDTO>();
                primeiroResultado = 0;
                this.atualizarListaCstCofins(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CstCofinsDTO cstcofinsSelected
        {
            get { return _cstcofinsSelected; }
            set
            {
                _cstcofinsSelected = value;
                notifyPropertyChanged("cstcofinsSelected");
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
                            this.atualizarListaCstCofins(1);
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
                            this.atualizarListaCstCofins(-1);
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

        public void salvarAtualizarCstCofins()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarCstCofins(cstcofinsSelected);
                    cstcofinsSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaCstCofins(int pagina)
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

                    List<CstCofinsDTO> listaServ = serv.selectCstCofinsPagina(primeiroResultado, QUANTIDADE_PAGINA, new CstCofinsDTO());

                    listaCstCofins.Clear();

                    foreach (CstCofinsDTO objAdd in listaServ)
                    {
                        listaCstCofins.Add(objAdd);
                    }
                    cstcofinsSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirCstCofins()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteCstCofins(cstcofinsSelected);
                    cstcofinsSelected = null;
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
