using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace CadastrosBaseService.Model
{
    public class PessoaDAL : NHibernateDAL<PessoaDTO>
    {
        public PessoaDAL(ISession session) : base(session) { }

        public PessoaDTO saveOrUpdate(PessoaDTO objeto)
        {
            try
            {
                base.saveOrUpdate<PessoaDTO>(objeto);

                IList<ContatoDTO> listaExclusaoContato = session.CreateCriteria(typeof(ContatoDTO)).
                    Add(Expression.Eq("IdPessoa", objeto.Id)).List<ContatoDTO>();

                foreach (ContatoDTO objLista in listaExclusaoContato)
                {
                    session.Delete(objLista);
                }

                if (objeto.ListaContato != null)
                    foreach (ContatoDTO objLista in objeto.ListaContato)
                    {
                        objLista.IdPessoa = objeto.Id;
                        session.Save(objLista);
                    }

                IList<EnderecoDTO> listaExclusaoEndereco = session.CreateCriteria(typeof(EnderecoDTO)).
                    Add(Expression.Eq("IdPessoa", objeto.Id)).List<EnderecoDTO>();

                foreach (EnderecoDTO objLista in listaExclusaoEndereco)
                {
                    session.Delete(objLista);
                }

                if (objeto.ListaEndereco != null)
                    foreach (EnderecoDTO objLista in objeto.ListaEndereco)
                    {
                        objLista.IdPessoa = objeto.Id;
                        session.Save(objLista);
                    }

                session.Flush();

                return objeto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<PessoaDTO> select(PessoaDTO objeto)
        {
            try
            {

                IList<PessoaDTO> resultado = base.select<PessoaDTO>(objeto);

                foreach (PessoaDTO objP in resultado)
                {
                    NHibernateDAL<ContatoDTO> DAL = new NHibernateDAL<ContatoDTO>(session);
                    objP.ListaContato = DAL.select<ContatoDTO>(
                        new ContatoDTO { IdPessoa = objP.Id });
                }

                foreach (PessoaDTO objP in resultado)
                {
                    NHibernateDAL<EnderecoDTO> DAL = new NHibernateDAL<EnderecoDTO>(session);
                    objP.ListaEndereco = DAL.select<EnderecoDTO>(
                        new EnderecoDTO { IdPessoa = objP.Id });
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<PessoaDTO> selectPagina(int primeiroResultado, int quantidadeResultados, PessoaDTO objeto)
        {
            try
            {
                IList<PessoaDTO> resultado = base.selectPagina<PessoaDTO>(primeiroResultado, quantidadeResultados, objeto);
                foreach (PessoaDTO objLista in resultado)
                {
                    NHibernateDAL<ContatoDTO> DAL = new NHibernateDAL<ContatoDTO>(session);
                    objLista.ListaContato = DAL.select<ContatoDTO>(
                        new ContatoDTO { IdPessoa = objLista.Id });
                }

                foreach (PessoaDTO objLista in resultado)
                {
                    NHibernateDAL<EnderecoDTO> DAL = new NHibernateDAL<EnderecoDTO>(session);
                    objLista.ListaEndereco = DAL.select<EnderecoDTO>(
                        new EnderecoDTO { IdPessoa = objLista.Id });
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int delete(PessoaDTO objeto)
        {
            try
            {
                IList<ContatoDTO> listaExclusaoContato = session.CreateCriteria(typeof(ContatoDTO)).
                    Add(Expression.Eq("IdPessoa", objeto.Id)).List<ContatoDTO>();

                foreach (ContatoDTO objLista in listaExclusaoContato)
                {
                    session.Delete(objLista);
                }

                IList<EnderecoDTO> listaExclusaoEndereco = session.CreateCriteria(typeof(EnderecoDTO)).
                    Add(Expression.Eq("IdPessoa", objeto.Id)).List<EnderecoDTO>();

                foreach (EnderecoDTO objLista in listaExclusaoEndereco)
                {
                    session.Delete(objLista);
                }

                int resultado = base.delete<PessoaDTO>(objeto);

                session.Flush();

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}