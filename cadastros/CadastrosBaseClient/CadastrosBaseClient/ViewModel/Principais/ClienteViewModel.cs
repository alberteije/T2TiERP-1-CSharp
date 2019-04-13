using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CadastrosBaseClient.CadastrosPrincipaisReference;
using CadastrosBaseClient.Command;
using CadastrosBaseClient.Model;
using CadastrosBaseClient.ViewModel;
using SearchWindow;

namespace CadastrosBaseClient.View.CadastrosBase
{
	/// 
	/// The MIT License
	///
	/// Copyright: Copyright (C) 2010 T2Ti.COM
	///
	/// Permission is hereby granted, free of charge, to any person
	/// obtaining a copy of this software and associated documentation
	/// files (the "Software"), to deal in the Software without
	/// restriction, including without limitation the rights to use,
	/// copy, modify, merge, publish, distribute, sublicense, and/or sell
	/// copies of the Software, and to permit persons to whom the
	/// Software is furnished to do so, subject to the following
	/// conditions:
	///
	/// The above copyright notice and this permission notice shall be
	/// included in all copies or substantial portions of the Software.
	///
	/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
	/// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
	/// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
	/// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
	/// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
	/// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
	/// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
	/// OTHER DEALINGS IN THE SOFTWARE.
	///
	///        The author may be contacted at:
	///            t2ti.com@gmail.com
	///
	/// Autor: Albert Eije (t2ti.com@gmail.com)
	/// Version: 1.0
    public class ClienteViewModel : ERPViewModelBase
    {
        public ObservableCollection<ClienteDTO> ListaCliente { get; set; }
        private ClienteDTO _ClienteSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }


        public ClienteViewModel()
        {
            try
            {
                ListaCliente = new ObservableCollection<ClienteDTO>();
                primeiroResultado = 0;
                this.atualizarListaCliente(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ClienteDTO ClienteSelected
        {
            get { return _ClienteSelected; }
            set
            {
                _ClienteSelected = value;
                notifyPropertyChanged("ClienteSelected");
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
                            this.atualizarListaCliente(1);
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
                            this.atualizarListaCliente(-1);
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

        public void salvarAtualizarCliente()
        {
            try
            {
                using (ServicoCadastrosPrincipaisClient serv = new ServicoCadastrosPrincipaisClient())
                {
                    serv.salvarAtualizarCliente(ClienteSelected);
                    ClienteSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaCliente(int pagina)
        {
            try
            {
                using (ServicoCadastrosPrincipaisClient serv = new ServicoCadastrosPrincipaisClient())
                {
                    if (pagina == 0)
                        primeiroResultado = 0;
                    else if (pagina > 0)
                        primeiroResultado += QUANTIDADE_PAGINA;
                    else if (pagina < 0)
                        primeiroResultado -= QUANTIDADE_PAGINA;

                    List<ClienteDTO> listaServ = serv.selectClientePagina(primeiroResultado, QUANTIDADE_PAGINA, new ClienteDTO());

                    ListaCliente.Clear();

                    foreach (ClienteDTO objAdd in listaServ)
                    {
                        ListaCliente.Add(objAdd);
                    }
                    ClienteSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirCliente()
        {
            try
            {
                using (ServicoCadastrosPrincipaisClient serv = new ServicoCadastrosPrincipaisClient())
                {
                    serv.deleteCliente(ClienteSelected);
                    ClienteSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void pesquisarSituacaoForCli()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(SituacaoForCliDTO),
                    typeof(ServicoCadastrosBase));

                if (searchWindow.ShowDialog() == true)
                {
                    ClienteSelected.SituacaoForCli = (SituacaoForCliDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ClienteSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void pesquisarAtividadeForCli()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(AtividadeForCliDTO),
                    typeof(ServicoCadastrosBase));

                if (searchWindow.ShowDialog() == true)
                {
                    ClienteSelected.AtividadeForCli = (AtividadeForCliDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ClienteSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void pesquisarPessoa()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(PessoaDTO),
                    typeof(ServicoCadastrosPrincipais));

                if (searchWindow.ShowDialog() == true)
                {
                    ClienteSelected.Pessoa = (PessoaDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ClienteSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void pesquisarTributOperacaoFiscal()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(TributOperacaoFiscalDTO),
                    typeof(ServicoCadastrosBase));

                if (searchWindow.ShowDialog() == true)
                {
                    ClienteSelected.TributOperacaoFiscal = (TributOperacaoFiscalDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ClienteSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void pesquisarContabilConta()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(ContabilContaDTO),
                    typeof(ServicoCadastrosBase));

                if (searchWindow.ShowDialog() == true)
                {
                    ClienteSelected.ContabilConta = (ContabilContaDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ClienteSelected");
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
