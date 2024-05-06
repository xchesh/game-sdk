using System;
using System.Collections.Generic;

namespace GameSdk.Core.Essentials
{
    public class InstancesManager<T>
    {
        private Dictionary<Type, (T instance, int count)> Items { get; } = new();

        public T Get(Type type)
        {
            if (Items.TryGetValue(type, out var value) is false)
            {
                throw new InvalidOperationException($"Instance for type {type} not found");
            }

            return value.instance;
        }

        public void Register(Type type, T instance)
        {
            if (Items.ContainsKey(type) is false)
            {
                Items.Add(type, (instance, 0));
            }

            Items[type] = (instance, Items[type].count + 1);
        }

        public void Unregister(Type type)
        {
            if (Items.ContainsKey(type))
            {
                Items[type] = (Items[type].instance, Items[type].count - 1);

                if (Items[type].count < 1)
                {
                    Items.Remove(type);
                }
            }
        }
    }
}
