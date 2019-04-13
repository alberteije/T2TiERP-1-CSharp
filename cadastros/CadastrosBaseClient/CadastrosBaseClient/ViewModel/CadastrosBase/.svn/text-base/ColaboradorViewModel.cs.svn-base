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
    public class ColaboradorViewModel : ERPViewModelBase
    {
        public ObservableCollection<ColaboradorDTO> ListaColaborador { get; set; }
        private ColaboradorDTO _ColaboradorSelected;
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }


        public ColaboradorViewModel()
        {
            try
            {
                ListaColaborador = new ObservableCollection<ColaboradorDTO>();
                primeiroResultado = 0;
                this.atualizarListaColaborador(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ColaboradorDTO ColaboradorSelected
        {
            get { return _ColaboradorSelected; }
            set
            {
                _ColaboradorSelected = value;
                notifyPropertyChanged("ColaboradorSelected");
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
                            this.atualizarListaColaborador(1);
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
                            this.atualizarListaColaborador(-1);
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

        public void salvarAtualizarColaborador()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.salvarAtualizarColaborador(ColaboradorSelected);
                    ColaboradorSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaColaborador(int pagina)
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

                    List<ColaboradorDTO> listaServ = serv.selectColaboradorPagina(primeiroResultado, QUANTIDADE_PAGINA, new ColaboradorDTO());

                    ListaColaborador.Clear();

                    foreach (ColaboradorDTO objAdd in listaServ)
                    {
                        ListaColaborador.Add(objAdd);
                    }
                    ColaboradorSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirColaborador()
        {
            try
            {
                using (ServicoCadastrosBaseClient serv = new ServicoCadastrosBaseClient())
                {
                    serv.deleteColaborador(ColaboradorSelected);
                    ColaboradorSelected = null;
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
                    ColaboradorSelected.ContabilConta = (ContabilContaDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ColaboradorSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void pesquisarSetor()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(SetorDTO),
                    typeof(ServicoCadastrosBase));

                if (searchWindow.ShowDialog() == true)
                {
                    ColaboradorSelected.Setor = (SetorDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ColaboradorSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void pesquisarCargo()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(CargoDTO),
                    typeof(ServicoCadastrosBase));

                if (searchWindow.ShowDialog() == true)
                {
                    ColaboradorSelected.Cargo = (CargoDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ColaboradorSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void pesquisarNivelFormacao()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(NivelFormacaoDTO),
                    typeof(ServicoCadastrosBase));

                if (searchWindow.ShowDialog() == true)
                {
                    ColaboradorSelected.NivelFormacao = (NivelFormacaoDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ColaboradorSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void pesquisarTipoColaborador()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(TipoColaboradorDTO),
                    typeof(ServicoCadastrosBase));

                if (searchWindow.ShowDialog() == true)
                {
                    ColaboradorSelected.TipoColaborador = (TipoColaboradorDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ColaboradorSelected");
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
                    ColaboradorSelected.Pessoa = (PessoaDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ColaboradorSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void pesquisarSituacaoColaborador()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(SituacaoColaboradorDTO),
                    typeof(ServicoCadastrosBase));

                if (searchWindow.ShowDialog() == true)
                {
                    ColaboradorSelected.SituacaoColaborador = (SituacaoColaboradorDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ColaboradorSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void pesquisarSindicato()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(SindicatoDTO),
                    typeof(ServicoCadastrosBase));

                if (searchWindow.ShowDialog() == true)
                {
                    ColaboradorSelected.Sindicato = (SindicatoDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("ColaboradorSelected");
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
