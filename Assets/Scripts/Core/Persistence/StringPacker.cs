namespace Persistence
{
  using System.Runtime.Serialization.Formatters.Binary;
  using System.IO;
  using System;
  using System.Runtime.Serialization;

  public static class StringPacker
  {
    public static string Pack(ISerializable directory)
    {
      using (MemoryStream save_stream = new MemoryStream())
      {
        var formatter = new BinaryFormatter();

        formatter.Serialize(save_stream, directory);
        save_stream.Position = 0;

        return Convert.ToBase64String(save_stream.ToArray());
      }
    }

    public static T Unpack<T>(string packed) where T: ISerializable
    {
      using (MemoryStream load_stream = new MemoryStream(Convert.FromBase64String(packed)))
      {
        var formatter = new BinaryFormatter();
        load_stream.Position = 0;
        return (T)(formatter.Deserialize(load_stream));
      }
    }
  }
}
