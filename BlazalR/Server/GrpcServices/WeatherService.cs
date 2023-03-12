namespace BlazalR.Server.GrpcServices;

using Grpc.Core;
using Google.Protobuf.WellKnownTypes;

public class WeatherService : WeatherForecasts.WeatherForecastsBase
{
	readonly ILogger<WeatherService> _logger;
	public WeatherService(ILogger<WeatherService> logger) : base()
	{
		_logger = logger;
	}

	private static readonly string[] Summaries = new[]
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	public override Task<WeatherReply> GetWeather(WeatherForecastModel request, ServerCallContext context)
	{
		var reply = new WeatherReply();
		var rng = new Random();

		try
		{
			reply.Forecasts.Add(Enumerable.Range(1, 10).Select(index => new WeatherForecastModel
			{
				DateTimeStamp = DateTime.SpecifyKind(DateTime.Now.AddDays(index), DateTimeKind.Utc).ToTimestamp(),
				TemperatureC = rng.Next(20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			}));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Exception getting weather", "WeatherService/GetWeather", request);
		}

		return Task.FromResult(reply);
	}
}