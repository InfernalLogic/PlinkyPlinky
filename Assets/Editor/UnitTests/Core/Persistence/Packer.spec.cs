namespace Persistence
{
  using NUnit.Framework;
  using System;

  [Serializable]
  class ComplexObject
  {
    public int TestInt { get; set; }
    public float TestFloat { get; set; }
    public string TestString { get; set; }
  }

  [TestFixture]
  class PackerTests
  {
    [Test]
    public void CanDeserializePersistentDictionary()
    {
      var complex_dir = new PersistentDictionary<int, ComplexObject>();
      string test = "test";

      for (int i = 0; i < 100; ++i)
        complex_dir.Add(i, new ComplexObject
        {
          TestFloat = (float)(i * 3.0f),
          TestInt = i,
          TestString = test + i.ToString()
        });

      var save_string = StringPacker.Pack(complex_dir);
      var deserialized_dir = StringPacker.Unpack<PersistentDictionary<int, ComplexObject>>(save_string);

      Assert.AreEqual(deserialized_dir[10].TestInt, 10);
      Assert.AreEqual(deserialized_dir.Count, complex_dir.Count);
    }

    [Test]
    public void CanDeserializePersistentList()
    {
      var list = new PersistentList<string>();

      for (int i = 0; i < 100; ++i)
        list.Add("Test " + i.ToString());

      var save_string = StringPacker.Pack(list);

      var deserialized_list = StringPacker.Unpack<PersistentList<string>>(save_string);

      Assert.AreEqual(deserialized_list[10], "Test 10");
      Assert.AreEqual(deserialized_list.Count, deserialized_list.Count);
    }
  }
}

