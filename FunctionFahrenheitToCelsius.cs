using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace FunctionFahrenheitToCelsius;
public class FunctionFahrenheitToCelsius
{
    private readonly ILogger<FunctionFahrenheitToCelsius> _logger;

    public FunctionFahrenheitToCelsius(ILogger<FunctionFahrenheitToCelsius> log)
    {
        _logger = log;
    }

    [FunctionName("ConvertsFahrenheitToCelsius")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "Conversion" })]
    [OpenApiParameter(name: "fahrenheit", In = ParameterLocation.Path, Required = true, Type = typeof(double), Description = "The value in **Fahrenheit** to convertion to Celsius")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK Request")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ConvertsFahrenheitToCelsius/{fahrenheit}")] HttpRequest req,
        double fahrenheit)
    {
        _logger.LogInformation($"Received parameter: {fahrenheit}", fahrenheit);

        var valueInCelsius = (fahrenheit - 32) * 5 / 9;

        string responseMessage = $"The value in Fahrenheit {fahrenheit} in Celsius is : {valueInCelsius}";

        _logger.LogInformation($"Converted. Resulted {valueInCelsius}");

        return new OkObjectResult(responseMessage);
    }
}


