namespace Persistence
{
  using System;
  using System.Runtime.Serialization;

  [Serializable]
  class PersistentData : MasterSerializer, ISerializable
  {
    public PersistentData() : base() { }
    public PersistentData(SerializationInfo info, StreamingContext context) : base(info, context) { }
  }
}
