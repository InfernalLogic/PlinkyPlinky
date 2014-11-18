using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Publisher<T>
{
  protected List<Subscriber<T>> subscribers = new List<Subscriber<T>>();

  public void AddSubscriber(Subscriber<T> subscriber)
  {
    subscribers.Add(subscriber);
  }

  public void PublishMessage(T message)
  {
    foreach (Subscriber<T> subscriber in subscribers)
    {
      subscriber.ReceiveMessage(message);
    }
  }

}

