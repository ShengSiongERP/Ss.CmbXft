using Microsoft.EntityFrameworkCore.Query;
using Ss.CmbXft.Domain.Repositories;
using System.Linq.Expressions;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore;

/// <summary>
/// 解决参数params Expression<Func<T, object>>[] propertyExpressions的问题 Domain层Update无法依赖Microsoft.EntityFrameworkCore的问题
/// 适配器模式（Adapter）外部业务代码不直接依赖 UpdateSettersBuilder，只依赖抽象接口 IUpdateSetters<TEntity> 
/// </summary>
public class UpdateSettersAdapter<TEntity> : IUpdateSetters<TEntity>
{
    private readonly UpdateSettersBuilder<TEntity> _builder;

    public UpdateSettersAdapter(UpdateSettersBuilder<TEntity> builder)
    {
        _builder = builder;
    }

    public void SetProperty<TProperty>(
        Expression<Func<TEntity, TProperty>> propertyExpression,
        TProperty value)
    {
        _builder.SetProperty(propertyExpression, value);
    }
}