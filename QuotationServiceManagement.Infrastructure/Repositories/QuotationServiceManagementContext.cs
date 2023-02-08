using System.Diagnostics;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using QuotationServiceManagement.Domain.AggregatesModel.ContractAggregate;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.InquiryPartyAggregate;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.QuotationAggregate;
using QuotationServiceManagement.Domain.Interface;
using QuotationServiceManagement.Infrastructure.EntityTypeConfiguration;

namespace QuotationServiceManagement.Infrastructure.Repositories
{
    public class QuotationServiceManagementContext : DbContext, IUnitOfWork
    {

        public const string DEFAULT_SCHEMA = "quotation";

        private readonly IMediator _mediator;

        private IDbContextTransaction _currentTransaction;

        public DbSet<Quotation> Quotations { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<InquiryParty> InquiryParties { get; set; }

        public bool HasActiveTransaction => _currentTransaction != null;

        public QuotationServiceManagementContext(DbContextOptions<QuotationServiceManagementContext> options) : base(options)
        {
        }

        public QuotationServiceManagementContext(
            DbContextOptions<QuotationServiceManagementContext> options,
            IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Debug.WriteLine("VentilationEnhancementProcessorContext::ctor ->" + GetHashCode());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new QuotationEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new InquiryPartyEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new QuotationItemEntityTypeConfiguration());

        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync();

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}