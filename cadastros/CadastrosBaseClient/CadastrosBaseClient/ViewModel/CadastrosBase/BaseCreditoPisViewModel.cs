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
    public class BaseCreditoPisViewModel : ERPViewModelBase
    {
        public ObservableCollection<BaseCreditoPisDTO> listaBaseCreditoPis { get; set; }
        private BaseCreditoPisDTO _basecreditopisSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }


        public BaseCreditoPisViewModel()
        {
            try
            {
                listaBaseCreditoPis = new ObservableCollection<BaseCreditoPisDTO>();
                primeiroResultado = 0;
                this.atualizarListaBaseCreditoPis(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseCreditoPisDTO basecreditopisSelected
        {
            get { return _basecreditopisSelected; }
            set
            {
                _basecreditopisSelected = value;
                notifyPropertyChanged("basecreditopisSelected");
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
                            this.atualizarListaBaseCreditoPis(1);
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
                            this.atualizarListaBaseCreditoPis(-1);
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

        public void salvarAtualizarBaseCreditoPis()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarBaseCreditoPis(basecreditopisSelected);
                    basecreditopisSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaBaseCreditoPis(int pagina)
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

                    List<BaseCreditoPisDTO> listaServ = serv.selectBaseCreditoPisPagina(primeiroResultado, QUANTIDADE_PAGINA, new BaseCreditoPisDTO());

                    listaBaseCreditoPis.Clear();

                    foreach (BaseCreditoPisDTO objAdd in listaServ)
                    {
                        listaBaseCreditoPis.Add(objAdd);
                    }
                    basecreditopisSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirBaseCreditoPis()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteBaseCreditoPis(basecreditopisSelected);
                    basecreditopisSelected = null;
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
