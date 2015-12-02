using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace UamTTA.Storage
{
    public class EFRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly Func<UamTTAContext> _contextFactory;

        public EFRepository(Func<UamTTAContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public T FindById(int id)
        {
            using (var context = _contextFactory())
            {
                return context.Set<T>().FirstOrDefault(x => x.Id == id);
            }
        }

        public System.Collections.Generic.IEnumerable<T> GetAll()
        {
            using (var context = _contextFactory())
            {
                return context.Set<T>().AsEnumerable().ToList();
            }
        }

        public IEnumerable<T> GetByIds(IEnumerable<int> ids)
        {
            using (var context = _contextFactory())
            {
                foreach (int id in ids)
                {
                    var result = FindById(id);
                    if (result != null)
                        yield return FindById(id);
                }
            }
        }

        public T Persist(T item)
        {
            using (var context = _contextFactory())
            {
                if (item.Id == null)
                {
                    context.Set<T>().Add(item);
                }
                else
                {
                    var original = context.Set<T>().Single(x => x.Id == item.Id);
                    context.Entry(original).CurrentValues.SetValues(item);
                }
                
                context.SaveChanges();
            }

            return FindById(item.Id.Value);
        }

        public void Remove(T item)
        {
            using (var context = _contextFactory())
            {
                context.Set<T>().Remove(context.Set<T>().Single(x => x.Id == item.Id));
                context.SaveChanges();
            }
        }

        public IEnumerable<T> Take(int count)
        {
            //Console.WriteLine("Takt(int count); from EFRepo");
            using (var context = _contextFactory())
            {
                //Console.WriteLine("Mam: " + GetAll().Count() + ", a oczekuję: " + count);

                if (GetAll().Count() < count)
                {
                    throw new ArgumentException();
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        yield return FindById(i);
                    }
                }
            }
        }
    }
}