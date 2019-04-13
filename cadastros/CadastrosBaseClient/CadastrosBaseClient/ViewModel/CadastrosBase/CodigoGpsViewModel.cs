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
    public class CodigoGpsViewModel : ERPViewModelBase
    {
        public ObservableCollection<CodigoGpsDTO> listaCodigoGps { get; set; }
        private CodigoGpsDTO _codigoGpsSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }

        public CodigoGpsViewModel()
        {
            try
            {
                listaCodigoGps = new ObservableCollection<CodigoGpsDTO>();
                primeiroResultado = 0;
                this.atualizarListaCodigoGps(0);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CodigoGpsDTO codigoGpsSelected
        {
            get { return _codigoGpsSelected; }
            set
            {
                _codigoGpsSelected = value;
                notifyPropertyChanged("codigoGpsSelected");
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
                            this.atualizarListaCodigoGps(1);
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
                            this.atualizarListaCodigoGps(-1);
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

        public void salvarAtualizarCodigoGps()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarCodigoGps(codigoGpsSelected);
                    codigoGpsSelected = null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void atualizarListaCodigoGps(int pagina)
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
                    List<CodigoGpsDTO> listaServ = serv.selectCodigoGpsPagina(primeiroResultado, QUANTIDADE_PAGINA, new CodigoGpsDTO());
                    listaCodigoGps.Clear();
                    foreach (CodigoGpsDTO objAdd in listaServ)
                    {
                        listaCodigoGps.Add(objAdd);
                    }
                    codigoGpsSelected = null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void excluirCodigoGps()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteCodigoGps(codigoGpsSelected);
                    codigoGpsSelected = null;
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
