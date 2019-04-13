using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CadastrosBaseClient.ViewModel;
using CadastrosBaseClient.CadastrosPrincipaisReference;
using CadastrosBaseClient.Command;
using SearchWindow;
using CadastrosBaseClient.Model;

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
    public class FornecedorViewModel : ERPViewModelBase
    {
        public ObservableCollection<FornecedorDTO> ListaFornecedor { get; set; }
        private FornecedorDTO _FornecedorSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }


        public FornecedorViewModel()
        {
            try
            {
                ListaFornecedor = new ObservableCollection<FornecedorDTO>();
                primeiroResultado = 0;
                this.atualizarListaFornecedor(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FornecedorDTO FornecedorSelected
        {
            get { return _FornecedorSelected; }
            set
            {
                _FornecedorSelected = value;
                notifyPropertyChanged("FornecedorSelected");
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
                            this.atualizarListaFornecedor(1);
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
                            this.atualizarListaFornecedor(-1);
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

        public void salvarAtualizarFornecedor()
        {
            try
            {
                using (ServicoCadastrosPrincipaisClient serv = new ServicoCadastrosPrincipaisClient())
                {
                    serv.salvarAtualizarFornecedor(FornecedorSelected);
                    FornecedorSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaFornecedor(int pagina)
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

                    List<FornecedorDTO> listaServ = serv.selectFornecedorPagina(primeiroResultado, QUANTIDADE_PAGINA, new FornecedorDTO());

                    ListaFornecedor.Clear();

                    foreach (FornecedorDTO objAdd in listaServ)
                    {
                        ListaFornecedor.Add(objAdd);
                    }
                    FornecedorSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirFornecedor()
        {
            try
            {
                using (ServicoCadastrosPrincipaisClient serv = new ServicoCadastrosPrincipaisClient())
                {
                    serv.deleteFornecedor(FornecedorSelected);
                    FornecedorSelected = null;
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
                    FornecedorSelected.SituacaoForCli = (SituacaoForCliDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("FornecedorSelected");
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
                    FornecedorSelected.AtividadeForCli = (AtividadeForCliDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("FornecedorSelected");
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
                    FornecedorSelected.Pessoa = (PessoaDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("FornecedorSelected");
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
                    FornecedorSelected.ContabilConta = (ContabilContaDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("FornecedorSelected");
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
