using System;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.ExpressionParameterReplacer
{
    public static class ParameterReplacer
    {
        /// <summary>
        /// Change the type of first expression parameter to selected.
        /// Expression body must be an equality comparison.
        /// </summary>
        /// <typeparam name="OldType"></typeparam>
        /// <typeparam name="NewType"></typeparam>
        /// <param name="expression"></param>
        /// <param name="changingType"></param>
        /// <returns></returns>
        public static Expression<Func<NewType, bool>> Replace<OldType,NewType>
            (Expression<Func<OldType, bool>> expression, Type changingType)
        {
            if (expression == null)
                throw new ArgumentNullException("expression is null.");
            if (changingType == null)
                throw new ArgumentNullException("changingType is null.");

            var name = expression.Parameters.First().Name;

            //Create new param of selected type with name of old param
            var param = Expression.Parameter(changingType, name);

            var binary = expression.Body as BinaryExpression;


            if (binary?.NodeType != ExpressionType.Equal)
                throw new InvalidOperationException("expression must be an equality comparison.");

            //Get left part of comparison. This part of us is not important.
            var right = binary.Right;

            //Get right part of comparison, for example: "role.Id"
            var left = binary.Left as MemberExpression;

            //Get name of member. For "role.Id" it will be "Id".
            var leftMemberName = left.Member.Name;

            //Get member of selected type with several name
            var newMember = changingType.GetMember(leftMemberName)[0];

            //Create new member expression with selected type's member.
            var newLeft = Expression.MakeMemberAccess(param,newMember);

            var newBody = Expression.Equal(newLeft, right);

            var result =  Expression.Lambda<Func<NewType, bool>>(newBody, param);
            return result;
        }   
    }
}
