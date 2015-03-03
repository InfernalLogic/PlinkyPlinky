using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Interpolation { SLERP, LERP }

public class ChildScaler : MonoBehaviour 
{
  [SerializeField]
  private float max_scale = 2.0f;
  [SerializeField]
  private float smoothing = 0.7f;
  [SerializeField]
  private Interpolation interpolation;
  private List<ChildScalerTarget> children = new List<ChildScalerTarget>();

  private void Start()
  {
    RebuildChildren();
    StartCoroutine(ScaleChildren());
  }

  private void RebuildChildren()
  {
    foreach (Transform child in transform)
      children.Add(new ChildScalerTarget(child.gameObject,
                                         child.transform.localPosition,
                                         child.transform.localPosition * max_scale));
  }

  private IEnumerator ScaleChildren()
  {
    Vector3 target = Vector3.zero;
    ChildScalerTarget removal_target = null;

    while (gameObject.activeInHierarchy)
    {
      foreach (ChildScalerTarget child in children)
      {
        if(child.Target == null)
        {
          removal_target = child;
          break;
        }

        if (child.is_growing)
          target = child.EndingPosition;
        else
          target = child.StartingPosition;

        target = RotateVector(target, transform.rotation.z);
        MoveChild(child, target);
        CheckForDirectionChange(target, child);
      }

      RemoveDeadChildren(removal_target);

      yield return null;
    }
  }

  private void MoveChild(ChildScalerTarget child, Vector3 target)
  {
    if (interpolation == Interpolation.SLERP)
      child.Target.transform.localPosition = Vector3.Slerp(child.Target.transform.localPosition,
                                                           target,
                                                           smoothing * Time.deltaTime);
    else
      child.Target.transform.localPosition = Vector3.Lerp(child.Target.transform.localPosition,
                                                           target,
                                                           smoothing * Time.deltaTime);
  }

  private void CheckForDirectionChange(Vector3 target, ChildScalerTarget child)
  {
    if (Mathf.Abs((child.Target.transform.localPosition - target).magnitude) < 0.1)
      child.is_growing = !child.is_growing;
  }

  private void RemoveDeadChildren(ChildScalerTarget removal_target)
  {
    if (removal_target == null)
      return;

    children.Remove(removal_target);
    removal_target = null;
    
  }

  protected Vector3 RotateVector(Vector3 vector, float angle)
  {
    float cos = Mathf.Cos(Mathf.Deg2Rad * angle);
    float sin = Mathf.Sin(Mathf.Deg2Rad * angle);

    Vector3 new_vector = Vector3.zero;

    new_vector.x = cos * vector.x - sin * vector.y;
    new_vector.y = sin * vector.x + cos * vector.y;
    return new_vector;
  }
}
