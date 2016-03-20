namespace Messaging
{
  using NUnit.Framework;
  [TestFixture]
  public class HubTests
  {
    private Hub hub;
    private TestListener listener;

    [SetUp]
    public void Init()
    {
      hub = new Hub();
      listener = new TestListener();
    }

    [Test]
    public void Attach()
    {
      hub.Attach(listener);
      hub.Send(new TestMessage());

      Assert.AreEqual(1, listener.testmessage_callbacks);
    }

    [Test]
    public void Attach_Repeated()
    {
      hub.Attach(listener);
      hub.Attach(listener);
      hub.Attach(listener);
      hub.Send(new TestMessage());

      Assert.AreEqual(1, listener.testmessage_callbacks);
    }

    [Test]
    public void Detach()
    {
      hub.Attach(listener);
      hub.Send(new TestMessage());
      hub.Detach(listener);
      hub.Send(new TestMessage());

      Assert.AreEqual(1, listener.testmessage_callbacks);
    }

    [Test]
    public void Detach_Repeated()
    {
      hub.Attach(listener);
      hub.Send(new TestMessage());
      hub.Detach(listener);
      hub.Detach(listener);
      hub.Detach(listener);
      hub.Send(new TestMessage());

      Assert.AreEqual(1, listener.testmessage_callbacks);
    }

    [Test]
    public void Detach_BeforeAttach()
    {
      hub.Detach(listener);
      hub.Send(new TestMessage());

      Assert.AreEqual(0, listener.testmessage_callbacks);
    }

    [Test]
    public void Send_RoutesInheritedTypesToInheritedTypeOnly()
    {
      hub.Attach(listener);
      hub.Send(new TestMessage());
      hub.Send(new TestMessageDerived());

      Assert.IsTrue(listener.testmessage_callbacks == 1 && listener.testmessage_derived_callbacks == 1);
    }

    [Test]
    public void Send_IgnoresNullComponents()
    {
      hub.Attach(listener);
      hub.Send(new TestMessage());
      hub.Send(new ComponentMessage());

      Assert.IsTrue(listener.testmessage_callbacks == 1 && listener.componentmessage_callbacks == 1);
    }

    [Test]
    public void Send_SendsComponentMessages()
    {
      hub.Attach(listener);
      hub.Send(new TestMessage());
      hub.Send(new ComponentMessage {
        testMessage = new TestMessage()
      });

      Assert.IsTrue(listener.testmessage_callbacks == 2 && listener.componentmessage_callbacks == 1);
    }

    [Test]
    public void Attach_AttachesMultipleInstancesOfSameType()
    {
      var second_listener = new TestListener();
      hub.Attach(listener);
      hub.Attach(second_listener);
      hub.Send(new TestMessage());

      Assert.AreEqual(1, listener.testmessage_callbacks);
      Assert.AreEqual(1, second_listener.testmessage_callbacks);
    }

    [Test]
    [ExpectedException(typeof(CannotRegisterType))]
    public void Attach_ThrowsExceptionWhenCannotRegisterType()
    {
      hub.Attach(new UnregisterableListener());
    }

    [Test]
    [Timeout(60000)]
    [Ignore]
    public void StressTest_Basic()
    {
      uint count = 1000000;
      hub.Attach(listener);

      for (uint i = 0; i < count; ++i)
        hub.Send(new TestMessage());

      Assert.AreEqual(count, listener.testmessage_callbacks);
    }

    [Test]
    [Ignore]
    [Timeout(60000)]
    public void StressTest_Component()
    {
      uint count = 1000000;
      hub.Attach(listener);

      for (uint i = 0; i < count; ++i)
      {
        hub.Send(new ComponentMessage
        {
          testMessage = new TestMessage()
        });
      }

      Assert.AreEqual(count, listener.testmessage_callbacks);
    }

    [Test]
    [Ignore]
    [Timeout(60000)]
    public void StressTest_Derived()
    {
      uint count = 1000000;
      hub.Attach(listener);

      for (uint i = 0; i < count; ++i)
        hub.Send(new TestMessageDerived());

      Assert.AreEqual(count, listener.testmessage_derived_callbacks);
    }
  }
}
