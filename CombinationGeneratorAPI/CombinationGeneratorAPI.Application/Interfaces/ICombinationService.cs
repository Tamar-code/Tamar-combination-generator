namespace CombinationGeneratorAPI.Application.Interfaces;

public interface ICombinationService
{
    long GetTotalCount(int n);
    (int[] permutation, bool hasMore) GetNext(int n, long currentIndex);
    IEnumerable<(int[] permutation, long index)> GetPage(int n, long fromIndex, int pageSize);
    int[] ComputePermutationAtIndex(int n, long index);
}
