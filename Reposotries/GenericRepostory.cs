using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposotries
{
    public class GenericRepostory<T> : IGenericRepostory<T> where T : BaseModel
    {
        Project_Context Context;
        DbSet<T> Table;
        public GenericRepostory(Project_Context context)
        {
            Context = context;
            Table = Context.Set<T>();
        }

        public   IEnumerable<T> Get()
        {
           return  Table;
        }

        public T GetByID(int id)
        {
            return  Table.Find(id);
        }

        public void Add(T entity)
        {
             Table.Add(entity);
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }

        public void Remove(T entity)
        {
            Table.Remove(entity);
        }

       
    }
}
