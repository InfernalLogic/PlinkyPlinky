using UnityEngine;
using System.Collections;

public class AutoPlinker : MonoBehaviour 
{
  [SerializeField]
  private float start_delay = 3.0f;
  private float period = 1.0f;
  private Plinker plinker = null;
  private float last_drop_time = 0.0f;

  private void Start()
  {
    GameObject plinker_object = GameObject.FindGameObjectWithTag("plinker");
    plinker = plinker_object.GetComponent<Plinker>();
  }

  public void UserDroppedBall()
  {
    
    last_drop_time = Time.time;
    StartCoroutine(TryAutoDrop(start_delay + 0.1f));
  }

  private IEnumerator TryAutoDrop(float delay)
  {
    yield return new WaitForSeconds(delay);

    if (Time.time - last_drop_time >= delay)
    {
      plinker.DropBall();
      StartCoroutine(TryAutoDrop(period));
    }
  }
}
