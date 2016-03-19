using System.Collections.Generic;

namespace Messaging
{
  public abstract class Message
  {
    public virtual List<Message> GetComponents() { return null; }
  }
}
