using UnityEngine;
using System.Collections;

public class ParticleSystemInitializer : MonoBehaviour
{
  [SerializeField]
  private string target_layer = "";

  private ParticleSystem particle_system;

  void Awake()
  {
    gameObject.renderer.sortingLayerName = target_layer;

    Destroy(gameObject, gameObject.GetComponent<ParticleSystem>().duration);
  }
}
