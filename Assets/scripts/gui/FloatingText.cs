using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour 
{
  [SerializeField]
  private string target_layer = "7_floating_text";
  [SerializeField]
  private MeshRenderer mesh_renderer;
  [SerializeField]
  private TextMesh text_mesh;

  void Awake()
  {
    gameObject.GetComponent<Renderer>().sortingLayerName = target_layer;
  }

  public void DestroyText()
  {
    Destroy(gameObject);
  }

  public void SetText(string text)
  {
    text_mesh.text = text;
  }
}
