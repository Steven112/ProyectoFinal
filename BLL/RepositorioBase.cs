﻿using LibraryServices.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices.BLL
{
    public class RepositorioBase<T>: IDisposable, IRepository<T> where T : class
    {
         internal Contexto _contexto;

        public RepositorioBase(Contexto contexto)
        {
            _contexto = contexto;
        }
        public virtual bool Guardar(T entity)
        {
            bool paso = false;

            try
            {
                if (_contexto.Set<T>().Add(entity) != null)
                    paso = _contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }
        public virtual bool Modificar(T entity)
        {
            RepositorioBase<T> repositorio = new RepositorioBase<T>(new Contexto());
            bool paso = false;
            _contexto = new Contexto();
            try
            {
                _contexto.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                paso = _contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }
        public void Dispose()
        {
            _contexto.Dispose();
        }
        public virtual bool Eliminar(int id)
        {
            bool paso = false;

            try
            {
                T Entity = _contexto.Set<T>().Find(id);
                _contexto.Set<T>().Remove(Entity);

                paso = _contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }
        public virtual T Buscar(int id)
        {
            T entity;

            try
            {
                entity = _contexto.Set<T>().Find(id);


            }
            catch (Exception)
            {
                throw;
            }
            return entity;
        }
        public List<T> GetList(Expression<Func<T, bool>> expression)
        {
            List<T> Lista = new List<T>();
            try
            {
                Lista = _contexto.Set<T>().Where(expression).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return Lista;

        }
    }

}
