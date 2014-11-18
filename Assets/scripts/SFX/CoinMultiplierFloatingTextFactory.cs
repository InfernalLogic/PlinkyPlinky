using UnityEngine;
using System.Collections;

public class CoinMultiplierFloatingTextFactory : Singleton<CoinMultiplierFloatingTextFactory> 
{
  [SerializeField]
  private FloatingText multiplier_floating_text;

  public void GenerateMultiplierPopup(int multiplier, Vector3 target_position)
  {
    FloatingText new_text = (FloatingText)Instantiate(multiplier_floating_text, Vector3.zero, this.transform.rotation);
    new_text.SetText("X" + multiplier);

    GameObject text_holder = new GameObject();
    text_holder.transform.position = target_position;
    new_text.transform.parent = text_holder.transform;
    Destroy(text_holder.gameObject, 1.0f);
  }
}
