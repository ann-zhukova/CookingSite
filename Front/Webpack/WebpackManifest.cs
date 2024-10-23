using System.Text.Json;

namespace Front.Webpack;

public class WebpackManifest
{
    private readonly Dictionary<string, string> _manifest;

    public WebpackManifest(string manifestPath)
    {
        var json = File.ReadAllText(manifestPath);
        _manifest = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    }

    public string GetFilePath(string key)
    {
        return _manifest.ContainsKey(key) ? _manifest[key] : null;
    }
}