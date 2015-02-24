using UnityEngine;
using System.Collections;

public class AutoPlinker : MonoBehaviour 
{
  [SerializeField]
  private float start_delay = 5.0f;
  private float period = 5.0f;
  private Plinker plinker = null;
  private float next_drop_time = 0.0f;
  private AutoPlinkerUpgrade auto_plinker_upgrade;

  public float Cooldown { get { return CalculateCooldown(); } }

  public bool Enabled { get { return is_enabled; } }
  private bool is_enabled = true;

  private void Start()
  {
    GameObject plinker_object = GameObject.FindGameObjectWithTag("plinker");
    plinker = plinker_object.GetComponent<Plinker>();
    auto_plinker_upgrade = FindObjectOfType<AutoPlinkerUpgrade>();

    ResetTimer(start_delay);
  }

  private void Update()
  {
    if (IsCooledDown() && HaveUpgrade() && is_enabled)
    {
      plinker.AutoDrop();
      ResetTimer(CalculateCooldown());
    }
  }

  private void OnGUI()
  {
    if (HaveUpgrade())
    {
      if (is_enabled)
        GUI.Label(new Rect(Screen.width * (1.5f / 3.0f), 0, 100, 100), "[A]uto: ON");
      else
        GUI.Label(new Rect(Screen.width * (1.5f / 3.0f), 0, 100, 100), "[A]uto: OFF");
    }
  }

  public void StartInitialCooldown()
  {
    ResetTimer(start_delay);
  }

  public void ToggleEnable()
  {
    is_enabled = !is_enabled;
  }

  private void ResetTimer(float delay)
  {
    next_drop_time = Time.time + delay;
  }

  private bool IsCooledDown()
  {
    return Time.time >= next_drop_time;
  }

  private float CalculateCooldown()
  {
    return (period / auto_plinker_upgrade.GetValue());
  }

  private bool HaveUpgrade()
  {
    return auto_plinker_upgrade.GetValue() > 0;
  }
}
