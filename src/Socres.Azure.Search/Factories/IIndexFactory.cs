using System;
using Microsoft.Azure.Search.Models;

namespace Socres.Azure.Search.Factories
{
    public interface IIndexFactory
    {
        Index CreateIndexFrom<T>(string name);
        Index CreateIndexFrom(Type type, string name);
    }
}