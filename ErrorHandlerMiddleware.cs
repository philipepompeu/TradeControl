using System.Text.Json;
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
        catch (BadHttpRequestException ex) when (ex.Message.Contains("Request body too large"))
        {
            context.Response.StatusCode = StatusCodes.Status413PayloadTooLarge;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                status = 413,
                message = "O arquivo enviado excede o tamanho máximo permitido (5 MB)."
            }));
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
