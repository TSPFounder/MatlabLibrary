public static class MatlabAddons
{
    public static async Task RequireAsync(IMatlabBackend m, params string[] products)
    {
        foreach (var p in products)
        {
            if (!await m.HasProductAsync(p))
                throw new InvalidOperationException($"Required MATLAB product not available: {p}");
        }
    }
}
