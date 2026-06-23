using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore;

/// <summary>
/// ModelBuilder 扩展方法，用于应用命名约定
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// 应用下划线命名约定
    /// 将所有实体的属性从驼峰式命名转换为下划线命名
    /// </summary>
    /// <param name="modelBuilder">ModelBuilder 实例</param>
    public static void ApplySnakeCaseNamingConvention(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // 转换表名（如果未显式配置）
            var tableName = entity.GetTableName();
            if (tableName != null)
            {
                // 检查是否已经显式配置了表名
                var defaultTableName = entity.ClrType.Name;
                if (tableName.Equals(defaultTableName, StringComparison.OrdinalIgnoreCase))
                {
                    // 如果表名与类名相同，则转换为下划线命名
                    entity.SetTableName(ToSnakeCase(defaultTableName));
                }
            }

            // 转换所有属性名
            foreach (var property in entity.GetProperties())
            {
                var propertyName = property.Name;
                var columnName = property.GetColumnName();
                
                // 如果列名与属性名相同，则转换为下划线命名
                if (columnName != null && columnName.Equals(propertyName, StringComparison.OrdinalIgnoreCase))
                {
                    property.SetColumnName(ToSnakeCase(propertyName));
                }
            }

            // 转换主键约束名称
            foreach (var key in entity.GetKeys())
            {
                var keyName = key.GetName();
                if (keyName != null)
                {
                    key.SetName(ToSnakeCase(keyName));
                }
            }

            // 转换外键约束名称
            foreach (var foreignKey in entity.GetForeignKeys())
            {
                var foreignKeyName = foreignKey.GetConstraintName();
                if (foreignKeyName != null)
                {
                    foreignKey.SetConstraintName(ToSnakeCase(foreignKeyName));
                }
            }

            // 转换索引名称
            foreach (var index in entity.GetIndexes())
            {
                var indexName = index.GetDatabaseName();
                if (indexName != null)
                {
                    index.SetDatabaseName(ToSnakeCase(indexName));
                }
            }
        }
    }

    /// <summary>
    /// 将驼峰式命名转换为下划线命名
    /// </summary>
    /// <param name="input">驼峰式字符串</param>
    /// <returns>下划线命名字符串</returns>
    /// <example>
    /// ToSnakeCase("UserName") => "user_name"
    /// ToSnakeCase("UserID") => "user_id"
    /// ToSnakeCase("CreateTime") => "create_time"
    /// </example>
    public static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        // 处理连续大写字母的情况，如 "UserID" -> "user_id"
        // 先在连续大写字母和小写字母之间插入下划线
        var result = Regex.Replace(input, @"([A-Z]+)([A-Z][a-z])", "$1_$2");
        // 在小写字母和大写字母之间插入下划线
        result = Regex.Replace(result, @"([a-z\d])([A-Z])", "$1_$2");
        // 将所有字符转换为小写
        return result.ToLower();
    }
}
