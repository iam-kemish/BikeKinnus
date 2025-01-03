﻿using System.Linq.Expressions;

namespace BikeKinnus.Repositary
{
    public interface IRepositary<T > where T : class
    {
        IEnumerable<T> GetAll(string? includeProperties = null);
        T Get(Expression<Func<T,bool>> func, string? includeProperties = null, bool Tracking = false);
        void Add(T item);
        void Remove(T item);
        void RemoveRange(IEnumerable<T> items);
    }
}
