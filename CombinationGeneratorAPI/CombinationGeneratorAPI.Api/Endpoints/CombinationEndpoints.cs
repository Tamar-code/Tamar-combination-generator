using CombinationGeneratorAPI.Application.DTOs;
using CombinationGeneratorAPI.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace CombinationGeneratorAPI.Api.Endpoints;

public static class CombinationEndpoints
{
    public static void MapCombinationEndpoints(this WebApplication app)
    {
        // POST /api/start
        app.MapPost("/api/start", (StartRequest request, ICombinationService service, IMemoryCache cache) =>
        {
            if (request.N < 1 || request.N > 20)
                return Results.BadRequest("N must be between 1 and 20.");

            long total = service.GetTotalCount(request.N);
            cache.Set("current_n", request.N, TimeSpan.FromHours(1));
            cache.Set("current_index", 0L, TimeSpan.FromHours(1));

            return Results.Ok(new StartResponse(total.ToString()));
        })
        .WithName("Start")
        .WithOpenApi();

        // GET /api/next
        app.MapGet("/api/next", (ICombinationService service, IMemoryCache cache) =>
        {
            if (!cache.TryGetValue("current_n", out int n))
                return Results.BadRequest("Please call /api/start first.");

            cache.TryGetValue("current_index", out long currentIndex);
            long total = service.GetTotalCount(n);

            if (currentIndex >= total)
                return Results.Ok(new { message = "אין יותר קומבינציות", hasMore = false });

            var (permutation, hasMore) = service.GetNext(n, (int)currentIndex);
            cache.Set("current_index", currentIndex + 1, TimeSpan.FromHours(1));

            return Results.Ok(new NextResponse(permutation, (int)currentIndex + 1, hasMore));
        })
        .WithName("GetNext")
        .WithOpenApi();

        // GET /api/all
        app.MapGet("/api/all", (int page, int pageSize, int fromIndex, ICombinationService service, IMemoryCache cache) =>
        {
            if (!cache.TryGetValue("current_n", out int n))
                return Results.BadRequest("Please call /api/start first.");

            long total = service.GetTotalCount(n);
            int totalPages = (int)Math.Ceiling((double)total / pageSize);

            var items = service.GetPage(n, fromIndex, pageSize)
                .Select(x => new PermutationItem(x.permutation, x.index))
                .ToList();

            bool hasMore = fromIndex + pageSize < total;

            return Results.Ok(new GetAllResponse(items, hasMore, totalPages));
        })
        .WithName("GetAll")
        .WithOpenApi();
    }
}
