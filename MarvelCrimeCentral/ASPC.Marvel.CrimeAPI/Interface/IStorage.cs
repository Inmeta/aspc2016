using System;
using System.Linq;

namespace ASPC.Marvel.CrimeAPI
{
    public interface IStorage<T> where T : Node
    {
        void Remove(T node);

        T Find(Guid id);

        void Add(T node);

        IQueryable<T> Get();
    }
}