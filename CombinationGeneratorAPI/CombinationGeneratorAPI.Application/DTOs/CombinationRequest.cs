namespace CombinationGeneratorAPI.Application.DTOs;
public record CombinationRequest(int N, int PageIndex, int PageSize = 10);
