﻿//Solution SettingsConfigHelper from andyp.dev
namespace WebMvc;
public class SettingsConfigHelper
{
    private static SettingsConfigHelper _appSettings;

    public string appSettingValue { get; set; }

    public static string AppSetting(string Key)
    {
        _appSettings = GetCurrentSettings(Key);
        return _appSettings.appSettingValue;
    }

    public SettingsConfigHelper(IConfiguration config, string Key)
    {
        this.appSettingValue = config.GetValue<string>(Key);
    }

    public static SettingsConfigHelper GetCurrentSettings(string Key)
    {
        var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables();

        IConfigurationRoot configuration = builder.Build();

        var settings = new SettingsConfigHelper(configuration.GetSection("ApplicationSettings"), Key);

        return settings;
    }
}