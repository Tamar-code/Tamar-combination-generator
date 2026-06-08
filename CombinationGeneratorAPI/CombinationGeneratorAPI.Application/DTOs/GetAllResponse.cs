namespace CombinationGeneratorAPI.Application.DTOs;
public record PermutationItem(int[] Permutation, string Index);
public record GetAllResponse(IEnumerable<PermutationItem> Permutations, bool HasMore, long TotalPages);
