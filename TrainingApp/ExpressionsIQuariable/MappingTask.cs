using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ExpressionsIQuariable
{
    [TestFixture]
    public class MappingTask
    {
        public class Foo { }
        public class Bar { }

        [Test]
        public void TestMethod3()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();

            var res = mapper.Map(new Foo());
            Console.WriteLine(res);
        }
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
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
        {
            var sourceParam = Expression.Parameter(typeof(TSource));
            var mapFunction =
                Expression.Lambda<Func<TSource, TDestination>>(Expression.New(typeof(TDestination)),sourceParam);

            Console.WriteLine(mapFunction);
            return new Mapper<TSource, TDestination>(mapFunction.Compile());
        }
    }

    public class ClassVisitor : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.Subtract)
            {
                ParameterExpression param = null;
                ConstantExpression constant = null;
                if (node.Left.NodeType == ExpressionType.Parameter && node.Right.NodeType == ExpressionType.Constant)
                {
                    param = (ParameterExpression)node.Left;
                    constant = (ConstantExpression)node.Right;
                    if (param != null && constant != null && constant.Type == typeof(int) && (int)constant.Value == 1)
                    {
                        return Expression.Decrement(param);
                    }
                }
            }
            if (node.NodeType == ExpressionType.Add)
            {
                ParameterExpression param = null;
                ConstantExpression constant = null;
                if (node.Left.NodeType == ExpressionType.Parameter && node.Right.NodeType == ExpressionType.Constant)
                {
                    param = (ParameterExpression)node.Left;
                    constant = (ConstantExpression)node.Right;
                    if (param != null && constant != null && constant.Type == typeof(int) && (int)constant.Value == 1)
                    {
                        return Expression.Increment(param);
                    }
                }
            }
            return base.VisitBinary(node);
        }

    }

}
