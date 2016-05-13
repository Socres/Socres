using System;
using System.Collections.Generic;
using Microsoft.Azure.Search.Models;

namespace Socres.Azure.Search.Factories
{
    public interface IFieldFactory
    {
        IEnumerable<Field> CreateFieldCollection(Type type);
    }
}