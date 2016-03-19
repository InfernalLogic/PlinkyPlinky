using Messaging;

public static class ServiceBus
{
  private static Hub message_hub = new Hub();
  
  public static void Attach(object registrant)
  {
    message_hub.Attach(registrant);
  }

  public static void Send(Message message)
  {
    message_hub.Send(message);
  }

  public static void Detach(System.Object registrant)
  {
    message_hub.Detach(registrant);
  }
}
