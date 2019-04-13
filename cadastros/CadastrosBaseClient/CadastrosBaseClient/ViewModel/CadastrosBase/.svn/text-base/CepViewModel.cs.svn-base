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
    public class CepViewModel : ERPViewModelBase
    {
        public ObservableCollection<CepDTO> listaCep { get; set; }
        private CepDTO _cepSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }

        public CepViewModel()
        {
            try
            {
                listaCep = new ObservableCollection<CepDTO>();
                primeiroResultado = 0;
                this.atualizarListaCep(0);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CepDTO cepSelected
        {
            get { return _cepSelected; }
            set
            {
                _cepSelected = value;
                notifyPropertyChanged("cepSelected");
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
                            this.atualizarListaCep(1);
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
                            this.atualizarListaCep(-1);
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

        public void salvarAtualizarCep()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarCep(cepSelected);
                    cepSelected = null; 
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public void atualizarListaCep(int pagina)
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

                    List<CepDTO> listaServ = serv.selectCepPagina(primeiroResultado, QUANTIDADE_PAGINA, new CepDTO());

                    listaCep.Clear();

                    foreach (CepDTO objAdd in listaServ)
                    {
                        listaCep.Add(objAdd);
                    }
                    cepSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void excluirCep()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteCep(cepSelected);
                    cepSelected = null;
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
