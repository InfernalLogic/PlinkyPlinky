namespace Persistence
{
  using System.IO;
  using Newtonsoft.Json;
  using System.Runtime.Serialization;
  public static class DefaultLoader
  {
    public static T DeserializeFromFile<T>(string path)
    {
      using (var file = new FileStream(path, FileMode.Open))
      using (var reader = new StreamReader(file))
      {
        var json = reader.ReadToEnd();

        return JsonConvert.DeserializeObject<T>(json);
      }
    }
  }
}
