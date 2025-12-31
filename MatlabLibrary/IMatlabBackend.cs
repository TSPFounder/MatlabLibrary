using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IMatlabBackend
{
    Task EvalAsync(string matlabCode, CancellationToken ct = default);
    Task<T> FevalAsync<T>(string functionName, object[] args, CancellationToken ct = default);

    // Add-on discovery helpers
    Task<IReadOnlyList<string>> GetInstalledProductsAsync(CancellationToken ct = default);
    Task<bool> HasProductAsync(string productName, CancellationToken ct = default);
}
