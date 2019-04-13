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
    public class NcmViewModel : ERPViewModelBase
    {
        public ObservableCollection<NcmDTO> listaNcm { get; set; }
        private NcmDTO _ncmSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }


        public NcmViewModel()
        {
            try
            {
                listaNcm = new ObservableCollection<NcmDTO>();
                primeiroResultado = 0;
                this.atualizarListaNcm(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NcmDTO ncmSelected
        {
            get { return _ncmSelected; }
            set
            {
                _ncmSelected = value;
                notifyPropertyChanged("ncmSelected");
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
                            this.atualizarListaNcm(1);
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
                            this.atualizarListaNcm(-1);
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

        public void salvarAtualizarNcm()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarNcm(ncmSelected);
                    ncmSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaNcm(int pagina)
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

                    List<NcmDTO> listaServ = serv.selectNcmPagina(primeiroResultado, QUANTIDADE_PAGINA, new NcmDTO());

                    listaNcm.Clear();

                    foreach (NcmDTO objAdd in listaServ)
                    {
                        listaNcm.Add(objAdd);
                    }
                    ncmSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirNcm()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteNcm(ncmSelected);
                    ncmSelected = null;
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
