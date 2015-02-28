using UnityEngine;
using System.Collections;

public class PegHunter : MonoBehaviour
{
  [SerializeField]
  private float acceleration = 0.0f;
  [SerializeField]
  private float initial_bump_strength = 0.0f;
  [SerializeField]
  private float vector_adjustment_factor = 0.02f;

  private GameObject target;
  private Vector2 vector_to_target;

  void Start()
  {
    SetTargetDirection();
    ApplyOrthogonalBumpForce();
    StartCoroutine(IncreaseAcceleration());
  }

  void Update()
  {
    FindNewTarget(null);

    SetTargetDirection();
    rigidbody2D.AddForce(vector_to_target.normalized * acceleration);
  }

  private IEnumerator IncreaseAcceleration()
  {
    acceleration *= 1.05f;
    print("Accelerated");
    yield return new WaitForSeconds(0.5f);
    StartCoroutine(IncreaseAcceleration());
  }

  public void FindNewTarget(GameObject spawning_popper)
  {
    target = GameObject.FindWithTag("peg");

    if (!target)
      Destroy(this.gameObject);
  }

  private void SetTargetDirection()
  {
    vector_to_target = target.transform.position - this.transform.position;
    this.rigidbody2D.velocity += (vector_to_target - this.rigidbody2D.velocity) * vector_adjustment_factor;
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
    if (!target || target.gameObject == null)
      Destroy(this.gameObject);
  }

  void OnBecameInvisible()
  {
    Destroy(this.gameObject);
  }
}
