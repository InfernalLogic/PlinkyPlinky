using System;
using System.IO;
using UnityEngine;

namespace Persistence
{
  class Models : MonoBehaviour
  {

    private void Awake()
    {
      OnLoad(null);
    }

    private void OnEnable()
    {
      ServiceBus.Attach(this);
    }

    private void OnDisable()
    {
      ServiceBus.Detach(this);
    }

    public void Add(string user_input)
    {
    }

    public void OnSave(Save m)
    {
    }

    public void OnLoad(Load m)
    {

    }

    public void OnReset(Reset m)
    {
    }

    public void OnDelete(Delete m)
    {

    }
  }
}
