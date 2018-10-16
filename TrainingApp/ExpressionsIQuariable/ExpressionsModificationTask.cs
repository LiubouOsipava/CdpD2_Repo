using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ExpressionsIQuariable
{
    [TestFixture]
    public class ExpressionsModificationTask
    {
        private static Dictionary<string, ConstantExpression> _values = new Dictionary<string, ConstantExpression>
        {
            ["x"] = Expression.Constant(4),
            ["y"] = Expression.Constant(7),
            ["z"] = Expression.Constant(2),
        };

        private static Expression<Func<int, int, int, int>> _testExpression = (x, y, z) => (x - 1) * (y + 1) + 1;

        [Test]
        public void IncrementDecrementTest()
        {
            var resultExpression =
                new ExpressionTransformator().VisitAndConvert(_testExpression, nameof(ExpressionTransformator));
            
            Console.WriteLine($"{_testExpression} = {_testExpression.Compile().Invoke(4, 7, 2)}");
            Console.WriteLine($"{resultExpression} = {resultExpression.Compile().Invoke(4, 7, 2)}");
        }

        [Test]
        public void ParametersSubstitutionTest()
        {
            var substitutedExpression =
                new ParameterTransformator(_values).VisitAndConvert(_testExpression, nameof(ParameterTransformator));
            Console.WriteLine($"{_testExpression} = {_testExpression.Compile().Invoke(4, 7, 2)}");
            Console.WriteLine($"{substitutedExpression} = {substitutedExpression.Compile().Invoke(4, 7, 2)}");
            
        }



        private class ExpressionTransformator : ExpressionVisitor
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
        private class ParameterTransformator : ExpressionVisitor
        {
            private Dictionary<string, ConstantExpression> constants;
            public ParameterTransformator(Dictionary<string, ConstantExpression> constants)
            {
                this.constants = constants;
            }

            protected override Expression VisitLambda<T>(Expression<T> node)
            {
                // Leave all parameters alone except the one we want to replace.
                var parameters = node.Parameters;

                return Expression.Lambda(Visit(node.Body), parameters);
            }
            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (node.NodeType == ExpressionType.Parameter)
                {
                    if (node.Type == typeof(int))
                    {
                        return constants[node.ToString()];
                    }
                }
                return base.VisitParameter(node);
            }
            

        }
    }
}
