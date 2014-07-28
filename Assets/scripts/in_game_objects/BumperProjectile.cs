using UnityEngine;
using System.Collections;

public class BumperProjectile : BumperScript
{

  public class PegProjectile : PegScript
  {
    public float rotation_speed = 1f;

    Vector3 rotator = Vector3.zero;

    void Start()
    {
      rotator.z = rotation_speed;
    }

    void Update()
    {
      transform.Rotate(rotator * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
      Destroy(gameObject);
    }
  }
}
