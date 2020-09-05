using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LearnBySpeaking.Infra.Data.Extensions
{
    public static class Extensions
    {
        //https://stackoverflow.com/a/51583047
        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            IEnumerator<TEntity> enumerator = query.Provider.Execute<IEnumerable<TEntity>>(query.Expression).GetEnumerator();
            object relationalCommandCache = enumerator.Private("_relationalCommandCache");
            SelectExpression selectExpression = relationalCommandCache.Private<SelectExpression>("_selectExpression");
            IQuerySqlGeneratorFactory factory = relationalCommandCache.Private<IQuerySqlGeneratorFactory>("_querySqlGeneratorFactory");

            QuerySqlGenerator sqlGenerator = factory.Create();
            Microsoft.EntityFrameworkCore.Storage.IRelationalCommand command = sqlGenerator.GetCommand(selectExpression);

            string sql = command.CommandText;
            return sql;
        }

        private static object Private(this object obj, string privateField) => obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
        private static T Private<T>(this object obj, string privateField) => (T)obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);


        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> whereClause)
        {
            if (condition)
                return query.Where(whereClause);

            return query;
        }

        public static DateTime? ToUTC(this DateTime? value)
        {
            if (value == null)
                return null;

            return ToUTC(value.Value);
        }

        public static DateTime ToUTC(this DateTime value)
        {
            return new DateTime(value.Ticks, DateTimeKind.Utc);
        }

        /// <summary>
        /// Extension for getting description attribute of enums
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerationValue"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T enumerationValue)
            where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
            }

            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return enumerationValue.ToString();
        }
    }

}