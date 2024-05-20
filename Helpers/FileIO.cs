using Newtonsoft.Json;

namespace QuizGame.Helpers;

public static class FileIO<T>
{
    public static List<T> Read(string path)
    {
        var content = File.ReadAllText(path);
        if (content == null || content.Length == 0)
        {
            return new List<T>();
        }
        try
        {
            var json = JsonConvert.DeserializeObject<List<T>>(content);
            return json;
        }
        catch
        {
            var json = JsonConvert.DeserializeObject<T>(content);
            var ls = new List<T>();
            ls.Add(json);
            return ls;
        }
    }

    public static void Write(string path, List<T> list)
    {
        var content = JsonConvert.SerializeObject(list, Formatting.Indented);
        File.WriteAllText(path, content);
    }
}
