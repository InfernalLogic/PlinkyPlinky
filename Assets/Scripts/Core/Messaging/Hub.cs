namespace Messaging
{
  using System;
  using System.Collections.Generic;

  internal class CallbackPackage
  {
    public Message Message { get; set; }
    public ListenerMethodInfo Listener { get; set; }
  }

  public class Hub
  {
    private Dictionary<Type, List<ListenerMethodInfo>> Streams = new Dictionary<Type, List<ListenerMethodInfo>>();

    public void Send(Message message)
    {
      var message_type = message.GetType();

      if (!Streams.ContainsKey(message_type))
        return;

      ListenerMethodInfo[] listeners = new ListenerMethodInfo[Streams[message_type].Count];
      Streams[message_type].CopyTo(listeners);

      foreach (var listener in listeners)
        listener.Callback.Invoke(listener.Caller, new object[] { message });

      var components = message.GetComponents();

      if (components == null)
        return;

      foreach (var component in components)
        if (component != null)
          Send(component);
    }

    public void Attach(System.Object registrant)
    {
      var listener_methods = Cache.GetListenerMethodInfo(registrant.GetType());

      if (listener_methods == null)
        return;

      foreach (var info in listener_methods)
      {
        if (!Streams.ContainsKey(info.ParameterType))
          Streams.Add(info.ParameterType, new List<ListenerMethodInfo>());

        if (Streams[info.ParameterType].Find(x => x.Caller == registrant) != null)
          return;

        Streams[info.ParameterType].Add(new ListenerMethodInfo
        {
          Caller = registrant,
          Callback = info.Callback,
          ParameterType = info.ParameterType,
        });
      }
    }

    public void Detach(System.Object registrant)
    {
      var listener_methods = Cache.GetListenerMethodInfo(registrant.GetType());

      if (listener_methods == null)
        return;

      foreach (var info in listener_methods)
      {
        if (!Streams.ContainsKey(info.ParameterType))
          return;

        var target = Streams[info.ParameterType].Find(x => x.Caller == registrant);
        Streams[info.ParameterType].Remove(target);

        if (Streams[info.ParameterType].Count == 0)
          Streams.Remove(info.ParameterType);
      }
    }
  }
}
