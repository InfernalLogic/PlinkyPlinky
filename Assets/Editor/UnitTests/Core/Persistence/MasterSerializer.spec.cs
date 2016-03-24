namespace Persistence
{
  using NUnit.Framework;
  using System;
  using System.Runtime.Serialization;
  [Serializable]
  class TestMasterSerializer : MasterSerializer
  {
    public PersistentList<string> stringlist { get; set; }
    public PersistentDictionary<string, string> stringdict { get; set; }

    public TestMasterSerializer(): base() { }

    public TestMasterSerializer(SerializationInfo info, StreamingContext context): base(info, context) { }
  }

  [TestFixture]
  class MasterSerializerTests
  {
    [Test]
    public void ShouldLoadAllPropertiesFromString()
    {
      var tester = new TestMasterSerializer
      {
        stringlist = new PersistentList<string>(),
        stringdict = new PersistentDictionary<string, string>()
      };

      tester.stringdict.Add("some", "thing");
      tester.stringdict.Add("another", "stuff");
      tester.stringlist.Add("thing");
      tester.stringlist.Add("stuff");

      var save_string = StringPacker.Pack(tester);

      var unpacked = StringPacker.Unpack<TestMasterSerializer>(save_string);

      Assert.AreEqual(unpacked.stringdict["some"], tester.stringdict["some"]);
      Assert.AreEqual(unpacked.stringlist[0], tester.stringlist[0]);
    }

    [Test]
    public void ShouldLoadFromDefaultFile()
    {
      var tester = new TestMasterSerializer
      {
        stringlist = new PersistentList<string>(),
        stringdict = new PersistentDictionary<string, string>()
      };

      tester.LoadFromDefaults(@"../../../Assets/Editor/UnitTests/Core/Persistence");

      Assert.AreEqual(tester.stringdict["some"], "thing");
      Assert.AreEqual(tester.stringlist[0], "thing");

    }
  }
}
