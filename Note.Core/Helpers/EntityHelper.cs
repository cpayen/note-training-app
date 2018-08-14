using Note.Core.Models;
using System;

namespace Note.Core.Helpers
{
    public static class EntityHelper<T> where T : Entity
    {
        public static T Create()
        {
            var item = (T)Activator.CreateInstance(typeof(T));
            item.Id = Guid.NewGuid().ToString();
            item.Type = typeof(T).Name;
            return item;
        }
    }
}
