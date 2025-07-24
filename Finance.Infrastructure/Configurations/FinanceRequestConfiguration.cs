using Finance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Configurations;

public class FinanceRequestConfiguration : IEntityTypeConfiguration<FinanceRequest>
{
    public void Configure(EntityTypeBuilder<FinanceRequest> builder)
    {
        builder.ToTable("FinanceRequest");
    }
}
