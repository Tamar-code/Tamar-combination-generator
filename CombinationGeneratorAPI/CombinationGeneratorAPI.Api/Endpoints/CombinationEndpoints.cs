using CombinationGeneratorAPI.Application.DTOs;
using CombinationGeneratorAPI.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace CombinationGeneratorAPI.Api.Endpoints;

public static class CombinationEndpoints
{
    private record SessionState(int N, long CurrentIndex);

    private static string GetCacheKey(string sessionId) => $"session_{sessionId}";

    public static void MapCombinationEndpoints(this WebApplication app)
    {
        // POST /api/start
        app.MapPost("/api/start", (StartRequest request, ICombinationService service, IMemoryCache cache) =>
        {
            if (request.N < 1 || request.N > 20)
                return Results.BadRequest("N must be between 1 and 20.");

            long total = service.GetTotalCount(request.N);
            var sessionId = Guid.NewGuid().ToString("N");
            cache.Set(GetCacheKey(sessionId), new SessionState(request.N, 0L), TimeSpan.FromHours(1));

            return Results.Ok(new StartResponse(total.ToString(), sessionId));
        })
        .WithName("Start")
        .WithOpenApi();

        // GET /api/next
        app.MapGet("/api/next", (string sessionId, ICombinationService service, IMemoryCache cache) =>
        {
            if (string.IsNullOrWhiteSpace(sessionId))
                return Results.BadRequest("Missing sessionId.");

            if (!cache.TryGetValue(GetCacheKey(sessionId), out SessionState? state) || state is null)
                return Results.BadRequest("Please call /api/start first.");

            long total = service.GetTotalCount(state.N);
            if (state.CurrentIndex >= total)
                return Results.Ok(new { message = "אין יותר קומבינציות", hasMore = false });

            var (permutation, hasMore) = service.GetNext(state.N, state.CurrentIndex);
            cache.Set(GetCacheKey(sessionId), state with { CurrentIndex = state.CurrentIndex + 1 }, TimeSpan.FromHours(1));

            return Results.Ok(new NextResponse(permutation, (state.CurrentIndex + 1).ToString(), hasMore));
        })
        .WithName("GetNext")
        .WithOpenApi();

        // GET /api/all
        app.MapGet("/api/all", (int page, int pageSize, long fromIndex, string sessionId, ICombinationService service, IMemoryCache cache) =>
        {
            if (string.IsNullOrWhiteSpace(sessionId))
                return Results.BadRequest("Missing sessionId.");

            if (pageSize <= 0)
                return Results.BadRequest("pageSize must be greater than 0.");

            if (fromIndex < 0)
                return Results.BadRequest("fromIndex must be non-negative.");

            if (!cache.TryGetValue(GetCacheKey(sessionId), out SessionState? state) || state is null)
                return Results.BadRequest("Please call /api/start first.");

            long total = service.GetTotalCount(state.N);
            long totalPages = (long)Math.Ceiling((double)total / pageSize);

            var items = service.GetPage(state.N, fromIndex, pageSize)
                .Select(x => new PermutationItem(x.permutation, x.index.ToString()))
                .ToList();

            bool hasMore = fromIndex + pageSize < total;

            return Results.Ok(new GetAllResponse(items, hasMore, totalPages));
        })
        .WithName("GetAll")
        .WithOpenApi();
    }
}
