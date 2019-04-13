using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace SpedService.Model
{
    public class EmpresaDAL : NHibernateDAL<EmpresaDTO>
    {
        public EmpresaDAL(ISession session) : base(session) { }

        public EmpresaDTO saveOrUpdate(EmpresaDTO objeto)
        {
            try
            {
                base.saveOrUpdate<EmpresaDTO>(objeto);

                IList<EnderecoDTO> listaExclusaoDepreciacao = session.CreateCriteria(typeof(EnderecoDTO)).
                    Add(Expression.Eq("IdEmpresa", objeto.Id)).List<EnderecoDTO>();

                foreach (EnderecoDTO objLista in listaExclusaoDepreciacao)
                {
                    session.Delete(objLista);
                }
                
                if(objeto.ListaEndereco != null)
                foreach (EnderecoDTO objLista in objeto.ListaEndereco)
                {
                    objLista.IdEmpresa = objeto.Id;
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

        public EmpresaDTO selectId(int id)
        {
            try
            {
                EmpresaDTO resultado = session.Get<EmpresaDTO>(id);
                
                NHibernateDAL<EnderecoDTO> DAL = new NHibernateDAL<EnderecoDTO>(session);
                resultado.ListaEndereco = DAL.select<EnderecoDTO>(new EnderecoDTO { IdEmpresa = resultado.Id });

                if (resultado.ListaEndereco == null)
                    resultado.ListaEndereco = new List<EnderecoDTO>();

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IList<EmpresaDTO> select(EmpresaDTO objeto)
        {
            try
            {

                IList<EmpresaDTO> resultado = base.select<EmpresaDTO>(objeto);

                foreach (EmpresaDTO objP in resultado)
                {
                    NHibernateDAL<EnderecoDTO> DAL = new NHibernateDAL<EnderecoDTO>(session);
                    objP.ListaEndereco = DAL.select<EnderecoDTO>(
                        new EnderecoDTO { IdEmpresa = objP.Id });

                    if (objP.ListaEndereco == null)
                        objP.ListaEndereco = new List<EnderecoDTO>();
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<EmpresaDTO> selectPagina(int primeiroResultado, int quantidadeResultados, EmpresaDTO objeto)
        {
            try
            {
                IList<EmpresaDTO> resultado = base.selectPagina<EmpresaDTO>(primeiroResultado, quantidadeResultados, objeto);
                foreach (EmpresaDTO objLista in resultado)
                {
                    NHibernateDAL<EnderecoDTO> DAL = new NHibernateDAL<EnderecoDTO>(session);
                    objLista.ListaEndereco = DAL.select<EnderecoDTO>(
                        new EnderecoDTO { IdEmpresa = objLista.Id });

                    if (objLista.ListaEndereco == null)
                        objLista.ListaEndereco = new List<EnderecoDTO>();
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int delete(EmpresaDTO objeto)
        {
            try
            {
                IList<EnderecoDTO> listaExclusaoDepreciacao = session.CreateCriteria(typeof(EnderecoDTO)).
                    Add(Expression.Eq("IdEmpresa", objeto.Id)).List<EnderecoDTO>();

                foreach (EnderecoDTO objLista in listaExclusaoDepreciacao)
                {
                    session.Delete(objLista);
                }

                int resultado = base.delete<EmpresaDTO>(objeto);

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