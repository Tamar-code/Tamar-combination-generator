using CombinationGeneratorAPI.Application.DTOs;
using MediatR;
namespace CombinationGeneratorAPI.Application.Queries;
public record GetCombinationsQuery(int N, int PageIndex, int PageSize = 10) : IRequest<CombinationResponse>;
