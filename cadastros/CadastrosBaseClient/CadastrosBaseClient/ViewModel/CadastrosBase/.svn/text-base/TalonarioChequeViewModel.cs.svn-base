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
    public class TalonarioChequeViewModel : ERPViewModelBase
    {
        public ObservableCollection<TalonarioChequeDTO> listaTalonarioCheque { get; set; }
        private TalonarioChequeDTO _talonariochequeSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }


        public TalonarioChequeViewModel()
        {
            try
            {
                listaTalonarioCheque = new ObservableCollection<TalonarioChequeDTO>();
                primeiroResultado = 0;
                this.atualizarListaTalonarioCheque(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TalonarioChequeDTO talonariochequeSelected
        {
            get { return _talonariochequeSelected; }
            set
            {
                _talonariochequeSelected = value;
                notifyPropertyChanged("talonariochequeSelected");
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
                            this.atualizarListaTalonarioCheque(1);
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
                            this.atualizarListaTalonarioCheque(-1);
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

        public void salvarAtualizarTalonarioCheque()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarTalonarioCheque(talonariochequeSelected);
                    talonariochequeSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaTalonarioCheque(int pagina)
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

                    List<TalonarioChequeDTO> listaServ = serv.selectTalonarioChequePagina(primeiroResultado, QUANTIDADE_PAGINA, new TalonarioChequeDTO());

                    listaTalonarioCheque.Clear();

                    foreach (TalonarioChequeDTO objAdd in listaServ)
                    {
                        listaTalonarioCheque.Add(objAdd);
                    }
                    talonariochequeSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirTalonarioCheque()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteTalonarioCheque(talonariochequeSelected);
                    talonariochequeSelected = null;
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
