using TradeControl;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }catch (EntityNotFoundException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status404NotFound;

            var response = new { message = ex.Message };

            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            Console.WriteLine($"[{ DateTime.UtcNow.ToShortTimeString() }]Exceção não tratada lançada. Mensagem[{ex.Message}]");

            var response = new { message = "Erro interno", detail = "Um problema não esperado ocorreu." };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
