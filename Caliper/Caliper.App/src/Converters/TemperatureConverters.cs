namespace Caliper.App.Converters;

internal static class TemperatureConverters
{
    public static float CelsiusToKelvin(float celsius) => celsius + 273.15f;
}