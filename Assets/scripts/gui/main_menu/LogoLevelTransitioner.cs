using UnityEngine;
using System.Collections;

public class LogoLevelTransitioner : MonoBehaviour 
{
  [SerializeField]
  private string target_level;
  [SerializeField]
  private float wait_time;

  void Start()
  {
    StartCoroutine(Transition());
  }

  private IEnumerator Transition()
  {
    yield return new WaitForSeconds(wait_time);
    Application.LoadLevel(target_level);
  }

}
