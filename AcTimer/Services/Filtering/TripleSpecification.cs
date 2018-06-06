using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.Services.Filtering
{
    public class TripleSpecification<T> : ISpecification<T>
    {
        ISpecification<T> _first, _second, _third;
        public TripleSpecification(ISpecification<T> first, ISpecification<T> second, ISpecification<T> third)
        {
            _first = first;
            _second = second;
            _third = third;
        }
        public bool IsSatisfied(T item)
        {
            return _first.IsSatisfied(item) && _second.IsSatisfied(item) && _third.IsSatisfied(item);
        }
    }
}