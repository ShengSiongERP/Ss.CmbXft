using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Domain.Repositories;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore;

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