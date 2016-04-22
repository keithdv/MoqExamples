using Spackle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EfDal.Test
{

    /// <summary>
    /// Use the Spackle library to fill an object's values
    /// with random values
    /// Very useful for creating random mock data and dtos.
    /// </summary>
    public static class EntityCreator
    {
        public static T Create<T>()
                        where T : new()
        {
            return EntityCreator.Create<T>(new RandomObjectGenerator(), null);
        }
        public static T Create<T>(Action<T> modifier)
                        where T : new()
        {
            var entity = EntityCreator.Create<T>(new RandomObjectGenerator(), modifier);
            return entity;
        }

        public static T Create<T>(RandomObjectGenerator generator)
            where T : new()
        {
            return EntityCreator.Create<T>(generator, null);
        }

        public static T Create<T>(RandomObjectGenerator generator, Action<T> modifier)
                where T : new()
        {

            var entity = new T();

            Update(entity, generator, modifier);

            return entity;
        }

        public static void Update<T>(T entity)
        {
            EntityCreator.Update<T>(entity, new RandomObjectGenerator(), null);
        }
        public static void Update<T>(T entity, Action<T> modifier)
        {
            EntityCreator.Update<T>(entity, new RandomObjectGenerator(), modifier);
        }

        public static void Update<T>(T entity, RandomObjectGenerator generator)
        {
            EntityCreator.Update<T>(entity, generator, null);
        }

        public static void Update<T>(T entity, RandomObjectGenerator generator, Action<T> modifier)
        {

            if (generator == null)
            {
                generator = new RandomObjectGenerator();
            }

            foreach (var property in
                            typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                            .Where(_ => _.CanWrite
                                                && !_.PropertyType.FullName.StartsWith("Aon.GRiDS.Validate") // Don't include Aon.GRiDS.Validate values
                                                && !_.PropertyType.IsInterface))
            {
                property.SetValue(entity,
                                typeof(RandomObjectGenerator)
                                                .GetMethod("Generate", Type.EmptyTypes)
                                                .MakeGenericMethod(new[] { property.PropertyType })
                                                .Invoke(generator, null));
            }

            if (modifier != null)
            {
                modifier(entity);
            }

        }


    }
}
