//-----------------------------------------------------------------------
// <copyright file="ExpressionVisitor.cs" company="YuGuan Corporation">
//     Copyright (c) YuGuan Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DevLib.Repository.EntityFramework
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Class ExpressionVisitor.
    /// </summary>
    /// <typeparam name="TExpression">The type of the expression.</typeparam>
    public class ExpressionVisitor<TExpression> : ExpressionVisitor where TExpression : Expression
    {
        /// <summary>
        /// The visitor.
        /// </summary>
        private readonly Func<TExpression, Expression> _visitor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionVisitor{TExpression}"/> class.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public ExpressionVisitor(Func<TExpression, Expression> visitor)
        {
            this._visitor = visitor;
        }

        /// <summary>
        /// Visits the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="visitor">The visitor.</param>
        /// <returns>Expression instance.</returns>
        public static Expression Visit(Expression expression, Func<TExpression, Expression> visitor)
        {
            return new ExpressionVisitor<TExpression>(visitor).Visit(expression);
        }

        /// <summary>
        /// Visits the specified expression.
        /// </summary>
        /// <typeparam name="TDelegate">The type of the delegate.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="visitor">The visitor.</param>
        /// <returns>Expression{TDelegate} instance.</returns>
        public static Expression<TDelegate> Visit<TDelegate>(Expression<TDelegate> expression, Func<TExpression, Expression> visitor)
        {
            return (Expression<TDelegate>)new ExpressionVisitor<TExpression>(visitor).Visit(expression);
        }

        /// <summary>
        /// Visits the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Expression instance.</returns>
        public override Expression Visit(Expression expression)
        {
            if (expression is TExpression && this._visitor != null)
            {
                expression = this._visitor(expression as TExpression);
            }

            return base.Visit(expression);
        }
    }
}
