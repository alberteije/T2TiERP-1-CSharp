using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Criterion;
using NHibernate;

namespace CadastrosBaseService.Model
{
    public class PessoaFisicaJuridicaDAL<T> : NHibernateDAL<T>
    {
        public PessoaFisicaJuridicaDAL(ISession session) : base (session)
        { 
        }
        public override IList<T> selectPagina<T>(int primeiroResultado, int quantidadeResultados, T objeto)
        {
            try
            {
                IList<T> resultado = new List<T>();
                Example example = Example.Create(objeto).EnableLike(MatchMode.Anywhere).IgnoreCase().ExcludeNulls().ExcludeZeroes();

                PessoaDTO pessoa = (PessoaDTO)objeto.GetType().GetProperty("Pessoa").GetValue(objeto, null);

                if (pessoa == null)
                    pessoa = new PessoaDTO();

                NHibernateDAL<PessoaDTO> pessoaDAL = new NHibernateDAL<PessoaDTO>(session);

                ICriteria crit = session.CreateCriteria(typeof(T)).
                    Add(example).Add(Expression.In("Pessoa", pessoaDAL.select<PessoaDTO>((PessoaDTO)pessoa).ToArray()));

                resultado = crit.SetFirstResult(primeiroResultado)
                    .SetMaxResults(quantidadeResultados).List<T>();
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override IList<T> select<T>(T objeto)
        {
            try
            {
                IList<T> resultado = new List<T>();
                Example example = Example.Create(objeto).EnableLike(MatchMode.Anywhere).IgnoreCase().ExcludeNulls().ExcludeZeroes();

                PessoaDTO pessoa = (PessoaDTO) objeto.GetType().GetProperty("Pessoa").GetValue(objeto, null);

                if (pessoa == null)
                    pessoa = new PessoaDTO();

                NHibernateDAL<PessoaDTO> pessoaDAL = new NHibernateDAL<PessoaDTO>(session);
                                
                ICriteria crit = session.CreateCriteria(typeof(T)).
                    Add(example).Add(Expression.In("Pessoa", pessoaDAL.select<PessoaDTO>((PessoaDTO)pessoa).ToArray()));

                resultado = crit.List<T>();
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}