using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CadastrosBaseClient.CadastrosBaseReference;
using CadastrosBaseClient.Command;
using SearchWindow;
using CadastrosBaseClient.Model;

namespace CadastrosBaseClient.ViewModel.CadastrosBase
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
    public class ProdutoViewModel : ERPViewModelBase
    {
        public ObservableCollection<ProdutoDTO> ListaProduto { get; set; }
        private ProdutoDTO _ProdutoSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }


        public ProdutoViewModel()
        {
            try
            {
                ListaProduto = new ObservableCollection<ProdutoDTO>();
                primeiroResultado = 0;
                this.atualizarListaProduto(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProdutoDTO ProdutoSelected
        {
            get { return _ProdutoSelected; }
            set
            {
                _ProdutoSelected = value;
                notifyPropertyChanged("ProdutoSelected");
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
                            this.atualizarListaProduto(1);
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
                            this.atualizarListaProduto(-1);
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

        public void salvarAtualizarProduto()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarProduto(ProdutoSelected);
                    ProdutoSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaProduto(int pagina)
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

                    List<ProdutoDTO> listaServ = serv.selectProdutoPagina(primeiroResultado, QUANTIDADE_PAGINA, new ProdutoDTO());

                    ListaProduto.Clear();

                    foreach (ProdutoDTO objAdd in listaServ)
                    {
                        ListaProduto.Add(objAdd);
                    }
                    ProdutoSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirProduto()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteProduto(ProdutoSelected);
                    ProdutoSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void pesquisarProdutoSubGrupo()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(ProdutoSubGrupoDTO),
                    typeof(ServicoCadastrosBase));

                if (searchWindow.ShowDialog() == true)
                {
                    ProdutoSelected.ProdutoSubGrupo = (ProdutoSubGrupoDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ProdutoSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void pesquisarProdutoMarca()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(ProdutoMarcaDTO),
                    typeof(ServicoCadastrosBase));

                if (searchWindow.ShowDialog() == true)
                {
                    ProdutoSelected.ProdutoMarca = (ProdutoMarcaDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ProdutoSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void pesquisarTributGrupoTributario()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(TributGrupoTributarioDTO),
                    typeof(ServicoCadastrosBase));

                if (searchWindow.ShowDialog() == true)
                {
                    ProdutoSelected.TributGrupoTributario = (TributGrupoTributarioDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ProdutoSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void pesquisarAlmoxarifado()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(AlmoxarifadoDTO),
                    typeof(ServicoCadastrosBase));

                if (searchWindow.ShowDialog() == true)
                {
                    ProdutoSelected.Almoxarifado = (AlmoxarifadoDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ProdutoSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void pesquisarUnidadeProduto()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(UnidadeProdutoDTO),
                    typeof(ServicoCadastrosBase));

                if (searchWindow.ShowDialog() == true)
                {
                    ProdutoSelected.UnidadeProduto = (UnidadeProdutoDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ProdutoSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void pesquisarTributIcmsCustomCab()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(TributIcmsCustomCabDTO),
                    typeof(ServicoCadastrosBase));

                if (searchWindow.ShowDialog() == true)
                {
                    ProdutoSelected.TributIcmsCustomCab = (TributIcmsCustomCabDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ProdutoSelected");
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
