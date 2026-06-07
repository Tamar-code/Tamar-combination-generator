namespace CombinationGeneratorAPI.Application.DTOs;
public record CombinationResponse(
    IEnumerable<int[]> Combinations,
    int PageIndex,
    int PageSize,
    long TotalCount,
    int TotalPages
);
