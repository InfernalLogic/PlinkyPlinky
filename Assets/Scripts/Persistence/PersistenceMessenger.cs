using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Messaging;

namespace Persistence
{
  public class Load : Message { }
  public class Save : Message { }
  public class Reset : Message { }
  public class Delete : Message { }

  public class PersistenceMessenger : MonoBehaviour
  {
    public void Load()
    {
      ServiceBus.Send(new Load());
    }

    public void Save()
    {
      ServiceBus.Send(new Save());
    }

    public void Delete()
    {
      ServiceBus.Send(new Delete());
    }
  }
}

