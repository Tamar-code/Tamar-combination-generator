namespace CombinationGeneratorAPI.Application.Interfaces;

public interface ICombinationService
{
    long GetTotalCount(int n);
    (int[] permutation, bool hasMore) GetNext(int n, int currentIndex);
    IEnumerable<(int[] permutation, int index)> GetPage(int n, int fromIndex, int pageSize);
    int[] ComputePermutationAtIndex(int n, long index);
}
