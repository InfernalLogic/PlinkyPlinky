
using System.Collections.Generic;

namespace Messaging
{
  public class TestMessage : Message { }
  public class TestMessageDerived : TestMessage { }
  public class ComponentMessage : Message
  {
    public TestMessage testMessage { get; set; }

    public override List<Message> GetComponents()
    {
      return new List<Message>
      {
        testMessage
      };
    }
  }

  public class TestListener
  {
    public uint componentmessage_callbacks = 0;
    public uint testmessage_callbacks = 0;
    public uint testmessage_derived_callbacks = 0;

    public void OnTestMessage(TestMessage t)
    {
      ++testmessage_callbacks;
    }

    public void OnTestMessageDerived(TestMessageDerived t)
    {
      ++testmessage_derived_callbacks;
    }

    public void OnComponentMessage(ComponentMessage t)
    {
      ++componentmessage_callbacks;
    }
  }

  public class UnregisterableListener
  {

  }
}
