using CocShop.Core.Constaint;
using CocShop.Core.Data.Query;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CocShop.Core.Extentions
{
    public static class QueryableExtensions
    {
        private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

        private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields.First(x => x.Name == "_queryCompiler");

        private static readonly FieldInfo QueryModelGeneratorField = QueryCompilerTypeInfo.DeclaredFields.First(x => x.Name == "_queryModelGenerator");

        private static readonly FieldInfo DataBaseField = QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");

        private static readonly PropertyInfo DatabaseDependenciesField = typeof(Database).GetTypeInfo().DeclaredProperties.Single(x => x.Name == "Dependencies");

        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            var queryCompiler = (QueryCompiler)QueryCompilerField.GetValue(query.Provider);
            var modelGenerator = (QueryModelGenerator)QueryModelGeneratorField.GetValue(queryCompiler);
            var queryModel = modelGenerator.ParseQuery(query.Expression);
            var database = (IDatabase)DataBaseField.GetValue(queryCompiler);
            var databaseDependencies = (DatabaseDependencies)DatabaseDependenciesField.GetValue(database);
            var queryCompilationContext = databaseDependencies.QueryCompilationContextFactory.Create(false);
            var modelVisitor = (RelationalQueryModelVisitor)queryCompilationContext.CreateQueryModelVisitor();
            modelVisitor.CreateQueryExecutor<TEntity>(queryModel);
            var sql = modelVisitor.Queries.First().ToString();

            return sql;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortBy)
        {
            var expression = source.Expression;
            int count = 0;
            var sortList = (sortBy ?? "temp").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int fail = 0;
            foreach (var sortProperty in sortList)
            {
                var sortField = typeof(T).GetProperty(sortProperty?.Substring(1) ?? "0", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.Name;
                if (string.IsNullOrEmpty(sortField) && fail == sortList.Length - 1)
                {
                    sortField = Constants.DEAFAULT_SORT_FIELD;
                }
                if (string.IsNullOrEmpty(sortField))
                {
                    fail++;
                    continue;
                }
                var order = sortProperty.Substring(0, 1);
                switch (order)
                {
                    case "+":
                        order = Constants.SORT_BY_ASC;
                        break;
                    case "-":
                        order = Constants.SORT_BY_DESC;
                        break;
                    default:
                        order = Constants.DEAFAULT_SORT_BY;
                        break;
                }
                var parameter = Expression.Parameter(typeof(T), "x");
                var selector = Expression.PropertyOrField(parameter, sortField);
                var method = string.Equals(order, "desc", StringComparison.OrdinalIgnoreCase) ?
                    (count == 0 ? "OrderByDescending" : "ThenByDescending") :
                    (count == 0 ? "OrderBy" : "ThenBy");
                expression = Expression.Call(typeof(Queryable), method,
                    new Type[] { source.ElementType, selector.Type },
                    expression, Expression.Quote(Expression.Lambda(selector, parameter)));
                count++;
            }

            //var parameter = Expression.Parameter(typeof(T), "x");
            //var selector = Expression.PropertyOrField(parameter, sortField);
            //var method = string.Equals(sortBy, "desc", StringComparison.OrdinalIgnoreCase) ?
            //    "OrderByDescending" : "OrderBy";
            //expression = Expression.Call(typeof(Queryable), method,
            //                   new Type[] { source.ElementType, selector.Type },
            //                   expression, Expression.Quote(Expression.Lambda(selector, parameter)));

            return source.Provider.CreateQuery<T>(expression);

        }

    }
}


