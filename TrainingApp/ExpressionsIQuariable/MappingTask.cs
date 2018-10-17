using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace ExpressionsIQuariable
{
    [TestFixture]
    public class MappingTask
    {

        public class Foo
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class Bar
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }

        [Test]
        public void TestMethod3()
        {
            Dictionary<string, string> mapping = new Dictionary<string, string>
            {
                ["FirstName"] = "Name",
                ["LastName"] = "Surname"
            };
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>(new Foo
            {
                FirstName = "Alex",
                LastName = "Ivanov"
            }, mapping);
            var res = mapper.Map(new Foo());
            Console.WriteLine(res);
        }



        public class Mapper<TSource, TDestination>
        {
            Func<TSource, TDestination> mapFunction;
            internal Mapper(Func<TSource, TDestination> func)
            {
                mapFunction = func;
            }
            public TDestination Map(TSource source)
            {
                return mapFunction(source);
            }
        }
        public class MappingGenerator
        {
            public Mapper<TSource, TDestination> Generate<TSource, TDestination>(TSource source, Dictionary<string,string> mapping)
            {
                var sourceParam = Expression.Parameter(typeof(TSource));
                var returnValue = Expression.New(typeof(TDestination));

                var propsList = typeof(TSource).GetProperties().ToList();

                var members = propsList.Select(propertyInfo =>
                    Expression.Bind(typeof(TDestination).GetProperty(mapping[propertyInfo.Name]), Expression.PropertyOrField(sourceParam,propertyInfo.Name)));
               
                var mapFunction = Expression.Lambda<Func<TSource, TDestination>>(Expression.MemberInit(returnValue, members),sourceParam);
                
                Console.WriteLine(mapFunction);
                return new Mapper<TSource, TDestination>(mapFunction.Compile());
            }
        }

        

    }
}
