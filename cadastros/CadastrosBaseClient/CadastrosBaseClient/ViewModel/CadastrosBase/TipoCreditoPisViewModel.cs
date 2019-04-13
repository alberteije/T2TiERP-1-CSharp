using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using CadastrosBaseClient.CadastrosBaseReference;
using CadastrosBaseClient.Command;
using CadastrosBaseClient.View.CadastrosBase;

namespace CadastrosBaseClient.ViewModel.CadastrosBase
{
    public class TipoCreditoPisViewModel : ERPViewModelBase
    {
        public ObservableCollection<TipoCreditoPisDTO> lista { get; set; }
        private ICommand seguinteCommand;
        private ICommand anteriorCommand;
        private int primeiroResultado;
        protected const int QUANTIDADE_RESULTADO = 20;
        private TipoCreditoPisDTO _ItemSelecionado;
        private bool _isEditar { get; set; }

        public TipoCreditoPisViewModel()
        {
            try
            {
                lista = new ObservableCollection<TipoCreditoPisDTO>();
                IsListar = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoCreditoPisDTO ItemSelecionado
        {
            get { return _ItemSelecionado; }
            set
            {
                _ItemSelecionado = value;
                notifyPropertyChanged("ItemSelecionado");
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
                            this.atualizarLista(1);
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
                            this.atualizarLista(-1);
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

        public void salvarAtualizar()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarTipoCredioPis(ItemSelecionado);
                    ItemSelecionado = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarLista(int pagina)
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    if (pagina == 0)
                        primeiroResultado = 0;
                    else if (pagina > 0)
                        primeiroResultado += QUANTIDADE_RESULTADO;
                    else if (pagina < 0)
                        primeiroResultado -= QUANTIDADE_RESULTADO;

                    List<TipoCreditoPisDTO> listaServ = serv.selectTipoCredioPisPagina(primeiroResultado, QUANTIDADE_RESULTADO, new TipoCreditoPisDTO());

                    lista.Clear();

                    foreach (TipoCreditoPisDTO objAdd in listaServ)
                    {
                        lista.Add(objAdd);
                    }
                    ItemSelecionado = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluir()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteTipoCredioPis(ItemSelecionado);
                    ItemSelecionado = null;
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


