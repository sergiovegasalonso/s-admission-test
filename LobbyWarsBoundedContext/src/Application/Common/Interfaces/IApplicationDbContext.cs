using Microsoft.EntityFrameworkCore;

namespace SignaturitAdmissionTest.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<object> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
