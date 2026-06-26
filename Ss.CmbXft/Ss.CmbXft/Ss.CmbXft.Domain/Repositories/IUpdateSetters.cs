using System.Linq.Expressions;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Domain.Repositories;

public interface IUpdateSetters<TEntity>
{
    void SetProperty<TProperty>(
        Expression<Func<TEntity, TProperty>> propertyExpression,
        TProperty value);
}