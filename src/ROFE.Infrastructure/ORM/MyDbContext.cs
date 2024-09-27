using MediatR;
using Microsoft.EntityFrameworkCore;
using ROFE.Domain.Abstractions;
using ROFE.Domain.Models.Operation;
using ROFE.Domain.Models.Portfolio;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ROFE.Infrastructure.ORM
{
    public class MyDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public DbSet<Operation> Operations { get; set; }
        public DbSet<StockOperation> OperationStocks { get; set; }
        public DbSet<MonetaryOperation> OperationMonetaries { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }


        public MyDbContext(DbContextOptions<MyDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            System.Diagnostics.Debug.WriteLine("MyDbContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            _ = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
