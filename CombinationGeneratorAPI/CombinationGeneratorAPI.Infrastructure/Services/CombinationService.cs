using CombinationGeneratorAPI.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace CombinationGeneratorAPI.Infrastructure.Services;

public class CombinationService : ICombinationService
{
    private readonly IMemoryCache _cache;

    public CombinationService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public long GetTotalCount(int n)
    {
        long result = 1;
        for (int i = 1; i <= n; i++) result *= i;
        return result;
    }

    public int[] ComputePermutationAtIndex(int n, long index)
    {
        var numbers = Enumerable.Range(1, n).ToList();
        var result = new int[n];
        long total = GetTotalCount(n);

        for (int i = n; i > 0; i--)
        {
            total /= i;
            int selected = (int)(index / total);
            result[n - i] = numbers[selected];
            numbers.RemoveAt(selected);
            index %= total;
        }

        return result;
    }

    public (int[] permutation, bool hasMore) GetNext(int n, long currentIndex)
    {
        long total = GetTotalCount(n);
        if (currentIndex >= total)
            return (Array.Empty<int>(), false);

        var permutation = ComputePermutationAtIndex(n, currentIndex);
        bool hasMore = currentIndex + 1 < total;
        return (permutation, hasMore);
    }

    public IEnumerable<(int[] permutation, long index)> GetPage(int n, long fromIndex, int pageSize)
    {
        long total = GetTotalCount(n);
        var result = new List<(int[], long)>();

        for (int i = 0; i < pageSize; i++)
        {
            long idx = fromIndex + i;
            if (idx >= total) break;
            result.Add((ComputePermutationAtIndex(n, idx), idx + 1));
        }

        return result;
    }
}
