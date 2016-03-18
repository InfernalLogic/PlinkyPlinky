using System;
using System.Reflection;

namespace Messaging
{
  public class ListenerMethodInfo
  {
    public object Caller { get; set; }
    public MethodInfo Callback { get; set; }
    public Type ParameterType { get; set; }
  }
}
