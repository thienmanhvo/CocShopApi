using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CocShop.Service.Helpers
{
    public static class LinqHelper<T> where T : class
    {
        public async static Task<Expression<Func<T, bool>>> StringToExpression(string stringExpression)
        {

            var options = ScriptOptions.Default.AddReferences(typeof(T).Assembly);

            return await CSharpScript.EvaluateAsync<Expression<Func<T, bool>>>(stringExpression, options);
        }

    }
}
