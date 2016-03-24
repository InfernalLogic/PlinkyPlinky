namespace Persistence
{
  using System;
  using System.IO;
  using System.Runtime.Serialization;
  using System.Linq;

  [Serializable]
  public abstract class MasterSerializer : ISerializable
  {
    public MasterSerializer()
    {
    }

    public MasterSerializer(SerializationInfo info, StreamingContext context)
    {
      var enumerator = info.GetEnumerator();

      while (enumerator.MoveNext())
        this.GetType().GetProperty(enumerator.Name).SetValue(this, enumerator.Value, null);
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      foreach(var property in GetType().GetProperties())
        info.AddValue(property.Name, this.GetType().GetProperty(property.Name).GetValue(this, null));
    }

    public virtual void LoadFromDefaults(string DefaultDirectory = @"Defaults\")
    {
      foreach (var property in GetType().GetProperties())
      {
        var type = property.PropertyType;

        if (!type.GetInterfaces().Contains(typeof(ILoadFromJson)))
          continue;

        var default_filepath = Path.Combine(DefaultDirectory, property.Name + ".json");
        //if (!File.Exists(default_filepath))
        //  continue;

        var instance = this.GetType().GetProperty(property.Name).GetValue(this, null);

        type.GetMethod("LoadFromJson").Invoke(instance, new object[] { default_filepath });
      }
    }
  }
}
