using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BookingsExample.Domain
{
    public class RulesException : Exception
    {
        public readonly IList<RuleViolation> Errors = new List<RuleViolation>();
        private readonly static Expression<Func<object, object>> thisObject = x => x;

        public void ErrorForModel(string message)
        {
            Errors.Add(new RuleViolation { Property = thisObject, Message = message });
        }

        public class RuleViolation
        {
            public LambdaExpression Property { get; set; }
            public string Message { get; set; }
        }
    }

    // Strongly-typed version permits lambda expression syntax to reference properties
    public class RulesException<TModel> : RulesException
    {
        public void ErrorFor<TProperty>(Expression<Func<TModel, TProperty>> property,
        string message)
        {
            Errors.Add(new RuleViolation { Property = property, Message = message });
        }
    }
}