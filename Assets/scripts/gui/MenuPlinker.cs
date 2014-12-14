using UnityEngine;
using System.Collections;

public class MenuPlinker : MonoBehaviour 
{
  [SerializeField]
  private float movement_speed = 0f;

  private Vector3 movement_vector = Vector3.zero;

  void Start()
  {
    movement_vector.x = movement_speed;
  }

  void Update()
  {
    MovePlinker();
  }

  private void MovePlinker()
  {
    transform.position += movement_vector * Time.deltaTime;
  }

  void OnTriggerEnter2D(Collider2D trigger)
  {
    if (trigger.tag == "plinker_reverse_trigger")
    {
      ReverseDirection();
    }
  }

  void ReverseDirection()
  {
    movement_vector *= -1.0f;
  }
}
