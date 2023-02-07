using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Khoaluan.Configurations
{
    public class AddFundTransactionConfiguration:IEntityTypeConfiguration<AddFundTransaction>
    {
        public void Configure(EntityTypeBuilder<AddFundTransaction> builder)
        {
            builder.ToTable(nameof(AddFundTransaction));
            builder.HasKey(o => o.TransactionId);
            builder.HasOne(o=>o.Fund).WithMany(y=>y.AddFundTransactions).HasForeignKey(x=>x.FundId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o=>o.User).WithMany(y=>y.AddFundTransactions).HasForeignKey(x=>x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
