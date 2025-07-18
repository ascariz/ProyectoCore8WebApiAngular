using ProyectoWeb.CrossCutting.Mappers.DtoToEntity;
using System.Linq.Expressions;

namespace ProyectoWeb.Repository.Helpers
{
    internal class CustomExpressionVisitor<TApp, TDb> : ExpressionVisitor
    {
        ParameterExpression _parameter;
        private readonly MapperBase _factory;

        public CustomExpressionVisitor(ParameterExpression parameter)
        {
            _parameter = parameter;
            _factory = FactorySwitcher.GetFactoryFor(typeof(TApp));
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _parameter;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.MemberType == System.Reflection.MemberTypes.Property)
            {
                MemberExpression memberExpression = null;
                var memberName = node.Member.Name;
                var otherMember = typeof(TDb).GetProperty(_factory.MapMemberFromApplicationToEntity(memberName));
                if (otherMember == null)
                {
                    MapperBase mapper = FactorySwitcher.GetFactoryFor(node.Expression.Type);
                    var transformedMemberName = mapper.MapMemberFromApplicationToEntity(memberName);
                    otherMember = typeof(TDb).GetProperty(_factory.MapMemberFromApplicationToEntity(transformedMemberName));
                }
                memberExpression = Expression.Property(Visit(node.Expression), otherMember);
                return memberExpression;
            }
            else
            {
                return base.VisitMember(node);
            }
        }
    }
}
