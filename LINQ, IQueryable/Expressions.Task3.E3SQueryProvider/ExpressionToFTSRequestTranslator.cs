using Expressions.Task3.E3SQueryProvider.Models.Request;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Expressions.Task3.E3SQueryProvider
{
    public class ExpressionToFtsRequestTranslator : ExpressionVisitor
    {
        readonly StringBuilder _resultStringBuilder;

        public ExpressionToFtsRequestTranslator()
        {
            _resultStringBuilder = new StringBuilder();
        }

        public string Translate(Expression exp)
        {
            Visit(exp);

            return _resultStringBuilder.ToString();
        }

        #region protected methods

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {            
            var nodeArg = node.Arguments[0].ToString().Replace("\"", string.Empty).Trim();

            switch (node.Method.Name)
            {
                case "Where":
                    if (node.Method.DeclaringType == typeof(Queryable))             
                    {
                        var predicate = node.Arguments[1];
                        Visit(predicate);
                        return node;
                    }
                    break;
                case "StartsWith":
                    Visit(node.Object);
                    var startsWithConstant = Expression.Constant(nodeArg + ("*"), typeof(string));
                    Visit(startsWithConstant);
                    return node;
                case "EndsWith":
                    Visit(node.Object);
                    var endsWithConstant = Expression.Constant(("*") + nodeArg, typeof(string));
                    Visit(endsWithConstant);
                    return node;
                case "Contains":
                    Visit(node.Object);
                    var containsConstant = Expression.Constant(("*") + nodeArg + ("*"), typeof(string));
                    Visit(containsConstant);
                    return node;
            }

            return base.VisitMethodCall(node);
        }



        protected override Expression VisitBinary(BinaryExpression node)
        {
            var leftNodeValue = node.Left;
            var rightNodeValue = node.Right;
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    if (node.Left.NodeType != ExpressionType.MemberAccess)
                        leftNodeValue = node.Right;

                    if (node.Right.NodeType != ExpressionType.Constant)
                        rightNodeValue = node.Left;

                    Visit(leftNodeValue);
                    Visit(rightNodeValue);
                    break;

                case ExpressionType.AndAlso:
                    var ftsQueryRequest = new FtsQueryRequest();

                    Visit(leftNodeValue);
                    var leftSide = _resultStringBuilder;
                    ftsQueryRequest.Statements.Add(new Statement() { Query = leftSide.ToString() });
                    _resultStringBuilder.Clear();

                    Visit(rightNodeValue);
                    var rightSide = _resultStringBuilder;
                    ftsQueryRequest.Statements.Add(new Statement() { Query = rightSide.ToString() });
                    _resultStringBuilder.Clear();

                    var serializedftsQueryRequest = JsonConvert.SerializeObject(ftsQueryRequest);
                    _resultStringBuilder.Clear();

                    _resultStringBuilder.Append(serializedftsQueryRequest.ToString());
                    break;
                default:
                    throw new NotSupportedException($"Operation '{node.NodeType}' is not supported");
            };

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _resultStringBuilder.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _resultStringBuilder.Append("(");
            _resultStringBuilder.Append(node.Value);
            _resultStringBuilder.Append(")");

            return node;
        }

        #endregion
    }
}
