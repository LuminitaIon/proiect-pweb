using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace GoSaveMe.Commons.Processors
{
    public class GenericPopulator<T>
    {
        public virtual List<T> CreateList(SqlDataReader reader)
        {
            List<T> objList = new();

            Func<SqlDataReader, T> reader1 = GetReader(reader);

            while (reader.Read())
                objList.Add(reader1(reader));

            return objList;
        }

        private Func<SqlDataReader, T> GetReader(SqlDataReader reader)
        {
            List<string> stringList = new();

            for (int ordinal = 0; ordinal < reader.FieldCount; ++ordinal)
                stringList.Add(reader.GetName(ordinal));

            ParameterExpression parameterExpression = Expression.Parameter(typeof(SqlDataReader), nameof(reader));
            MethodInfo? method = typeof(SqlDataReader).GetMethod("GetValue");
            MemberExpression memberExpression = Expression.Field(null, typeof(DBNull), "Value");
            List<MemberBinding> memberBindingList = new();

            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                object? obj = null;

                if (property.PropertyType.IsValueType)
                    obj = Activator.CreateInstance(property.PropertyType);

                else if (property.PropertyType.Name.ToLower().Equals("string"))
                    obj = string.Empty;

                if (stringList.Contains(property.Name))
                {
                    ConstantExpression constantExpression = Expression.Constant(reader.GetOrdinal(property.Name));
                    MethodCallExpression methodCallExpression = Expression.Call(parameterExpression, method, (Expression)constantExpression);
                    BinaryExpression binaryExpression = Expression.NotEqual(memberExpression, methodCallExpression);
                    UnaryExpression unaryExpression1 = Expression.Convert(methodCallExpression, property.PropertyType);
                    UnaryExpression unaryExpression2 = Expression.Convert(Expression.Constant(obj), property.PropertyType);
                    MemberBinding memberBinding = Expression.Bind(typeof(T).GetMember(property.Name)[0], Expression.Condition(binaryExpression, unaryExpression1, unaryExpression2));
                    memberBindingList.Add(memberBinding);
                }
            }

            return Expression.Lambda<Func<SqlDataReader, T>>(Expression.MemberInit(Expression.New(typeof(T)), memberBindingList), parameterExpression).Compile();
        }
    }
}
