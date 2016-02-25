using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPC.Marvel.CrimeAPI
{
    public class MemmoryStorage<T> : IStorage<T> where T : Node
    {
        private List<Node> Database = new List<Node>();

        public MemmoryStorage(List<Node> list)
        {
            this.Database = list;
        }

        public void Remove(T node)
        {
            this.Database.Remove(node);
            //this.OnChange(node, ChangeType.Delete);
        }

        public T Find(Guid id)
        {
            var res = Database.OfType<T>().SingleOrDefault(x => x.Id == id);
            //this.OnChange(res, CRUD.Read);
            return res;
        }

        public void Add(T node)
        {
            this.Database.Add(node);
            //this.OnChange(node, ChangeType.Create);
        }

        public IQueryable<T> Get()
        {
            return Database.OfType<T>().AsQueryable();
        }

        public EventHandler<ChangeType> OnChange;
    }
}