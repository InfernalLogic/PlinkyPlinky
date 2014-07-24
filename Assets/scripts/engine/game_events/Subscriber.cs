using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Subscriber<T>
{
  private Queue<T> message_queue = new Queue<T>();

  public void ReceiveMessage(T message)
  {
    message_queue.Enqueue(message);
  }

  public T ReadNewestMessage()
  {
    return message_queue.Peek();
  }

  public void DeleteNewestMessage()
  {
    message_queue.Dequeue();
  }

  public bool IsEmpty()
  {
    return message_queue.Count <= 0;
  }
}

