using UnityEngine;
using System.Collections;

public class PegHunter : MonoBehaviour
{
  [SerializeField]
  private float acceleration = 0.0f;
  [SerializeField]
  private float initial_bump_strength = 0.0f;
  [SerializeField]
  private float vector_clamping_threshold = 45.0f;

  private GameObject target;
  private Vector2 vector_to_target;

  void Start()
  {
    target = FindTargetPopper();

    SetTargetDirection();
    ApplyOrthogonalBumpForce();
  }

  void Update()
  {
    DestroyIfNoTargetFound();

    SetTargetDirection();
    rigidbody2D.AddForce(vector_to_target.normalized * acceleration * (1.0f / vector_to_target.magnitude));
  }

  private GameObject FindTargetPopper()
  {
    GameObject[] poppers = GameObject.FindGameObjectsWithTag("peg");
    return poppers[Random.Range(0, poppers.Length - 1)];
  }

  private void SetTargetDirection()
  {
    vector_to_target = target.transform.position - this.transform.position;

    if (Mathf.Abs(Vector2.Angle(this.rigidbody2D.velocity, vector_to_target)) < vector_clamping_threshold)
      this.rigidbody2D.velocity = vector_to_target.normalized * this.rigidbody2D.velocity.magnitude;
  }

  private void ApplyOrthogonalBumpForce()
  {
    Vector2 bump_direction = Vector2.zero;
    bump_direction.x = -vector_to_target.y;
    bump_direction.y = vector_to_target.x;

    this.rigidbody2D.AddForce(bump_direction.normalized * initial_bump_strength);
  }

  private void DestroyIfNoTargetFound()
  {
    if (!target)
      Destroy(this);
  }
}
