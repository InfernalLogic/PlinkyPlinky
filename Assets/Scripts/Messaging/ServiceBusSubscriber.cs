using UnityEngine;

namespace Assets.Scripts
{
  public class ServiceBusSubscriber : MonoBehaviour
  {
    private void OnEnable()
    {
      ServiceBus.Attach(this);
    }

    private void OnDisable()
    {
      ServiceBus.Detach(this);
    }
  }
}
