namespace CombinationGeneratorAPI.Application.DTOs;
public record PermutationItem(int[] Permutation, int Index);
public record GetAllResponse(IEnumerable<PermutationItem> Permutations, bool HasMore, int TotalPages);
