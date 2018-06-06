using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.Services.Filtering
{
    public class DoubleSpecification<T> : ISpecification<T>
    {
        ISpecification<T> _first, _second;

        public DoubleSpecification(ISpecification<T> first, ISpecification<T> second) 
        {
            _first = first;
            _second = second;
        }

        public bool IsSatisfied(T item)
        {
            return _first.IsSatisfied(item) && _second.IsSatisfied(item);
        }
    }
}