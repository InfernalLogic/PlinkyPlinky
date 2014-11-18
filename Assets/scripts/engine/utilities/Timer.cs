using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour 
{
  [SerializeField]
  protected float duration = 0.0f;

  private float expiration_mark = 0.0f;

  public void Reset()
  {
    expiration_mark = Time.time + duration;
  }

  public bool IsExpired()
  {
    return expiration_mark < Time.time;
  }

  public void SetDuration(float duration)
  {
    this.duration = duration;
  }
}
