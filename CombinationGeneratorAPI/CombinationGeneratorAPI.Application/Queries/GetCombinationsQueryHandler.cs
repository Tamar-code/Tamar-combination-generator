using CombinationGeneratorAPI.Application.DTOs;
using CombinationGeneratorAPI.Application.Interfaces;
using MediatR;

namespace CombinationGeneratorAPI.Application.Queries;

public class GetCombinationsQueryHandler : IRequestHandler<GetCombinationsQuery, CombinationResponse>
{
    private readonly ICombinationService _combinationService;

    public GetCombinationsQueryHandler(ICombinationService combinationService)
    {
        _combinationService = combinationService;
    }

    public Task<CombinationResponse> Handle(GetCombinationsQuery request, CancellationToken cancellationToken)
    {
        var total = _combinationService.GetTotalCount(request.N);
        var items = _combinationService.GetPage(request.N, request.PageIndex * request.PageSize, request.PageSize)
            .Select(x => x.permutation)
            .ToList();
        var totalPages = (int)Math.Ceiling((double)total / request.PageSize);

        return Task.FromResult(new CombinationResponse(items, request.PageIndex, request.PageSize, total, totalPages));
    }
}
