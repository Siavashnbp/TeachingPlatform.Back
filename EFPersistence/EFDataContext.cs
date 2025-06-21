using Applications.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace EFPersistence
{
    public class EFDataContext(
        DbContextOptions option, IUserInfoReader userInfoReader) : IdentityDbContext(option)
    {
        private readonly string? _tenantId = userInfoReader?.TenantId;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(IEFPersistenceAssembly).Assembly);
            AddGlobalQueryFilter<ITenant>(builder, _=>_.TenantId == _tenantId);

            base.OnModelCreating(builder);
        }

        private void AddGlobalQueryFilter<T>(ModelBuilder builder, Expression<Func<T, bool>> filter)
        {
            var entities = builder.Model.GetEntityTypes().Where(e =>
                            e.BaseType is not null &&
                            e.ClrType.GetInterface(typeof(ITenant).Name) is not null)
                            .Select(e => e.ClrType);
            foreach (var entityType in entities)
            {
                var parameterType =
                    Expression.Parameter(builder.Entity(entityType)
                    .Metadata.ClrType);
                builder.Entity(entityType).HasQueryFilter(
                    Expression.Lambda(filter,
                    Expression.Parameter(builder.Entity(entityType).Metadata.ClrType)));
            }
        }
    }
}
