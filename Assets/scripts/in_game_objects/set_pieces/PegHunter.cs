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

  private int popper_layer;

  void Start()
  {
    popper_layer = 1 << LayerMask.NameToLayer("3_pegs");

    if (!target)
      FindNewTarget();

    SetTargetDirection();
    ApplyOrthogonalBumpForce();
    StartCoroutine(IncreaseAcceleration());
  }

  void Update()
  {
    if (!target)
      FindNewTarget();

    SetTargetDirection();
    GetComponent<Rigidbody2D>().AddForce(vector_to_target.normalized * acceleration);
  }

  private IEnumerator IncreaseAcceleration()
  {
    yield return new WaitForSeconds(0.5f);
    acceleration *= 1.2f;
    StartCoroutine(IncreaseAcceleration());
  }//

  public void FindNewTarget()
  {
    RaycastHit2D closest_popper = Physics2D.CircleCast(transform.position, 100.0f, Vector2.zero, 1.0f, popper_layer);

    if (closest_popper.collider == null)
      Destroy(this.gameObject);

    target = closest_popper.collider.gameObject;
  }

  private void SetTargetDirection()
  {
    vector_to_target = target.transform.position - this.transform.position;
    this.GetComponent<Rigidbody2D>().velocity += (vector_to_target - this.GetComponent<Rigidbody2D>().velocity).normalized * vector_adjustment_factor;
  }

  private void ApplyOrthogonalBumpForce()
  {
    Vector2 bump_direction = Vector2.zero;
    bump_direction.x = -vector_to_target.y;
    bump_direction.y = vector_to_target.x;

    this.GetComponent<Rigidbody2D>().AddForce(bump_direction.normalized * initial_bump_strength);
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
