using UnityEngine;
using System.Collections.Generic;

public class ObjectRect : MonoBehaviour 
{
  [SerializeField]
  private float height = 3.0f;
  [SerializeField]
  private int objects_on_height = 1;
  [SerializeField]
  private float width = 3.0f;
  [SerializeField]
  private int objects_on_width = 1;
  [SerializeField]
  private GameObject object_on_rect;

  private Vector2[] corners = new Vector2[4]; //clockwise from top right

  private GameObject new_object;

  void Awake()
  {
    if (OutdatedChildrenFound())
      DestroyAllChildren();

    SpawnRectObjects();
  }

  private void SpawnRectObjects()
  {
    SpawnCornerObjects();
    SpawnSideObjects(corners[0], corners[1], objects_on_width);
    SpawnSideObjects(corners[1], corners[2], objects_on_height);
    SpawnSideObjects(corners[2], corners[3], objects_on_width);
    SpawnSideObjects(corners[3], corners[0], objects_on_height);
  }

  private void SpawnCornerObjects()
  {
    Vector3 offset = new Vector3(width, height);

    for (int i = 0; i < corners.GetLength(0); ++i)
    {
      corners[i] = RotateVector(offset, (float)(90 * i));
      SpawnNewObjectAt(corners[i]);
    }
  }

  private void SpawnSideObjects(Vector3 starting_corner, Vector3 ending_corner, int object_count)
  {
    Vector3 side = ending_corner - starting_corner;

    for (int i = 1; i <= object_count; ++i)
      SpawnNewObjectAt(starting_corner + side * ((float)i / (float)(object_count + 1)));
  }

  protected bool OutdatedChildrenFound()
  {
    return transform.childCount > 0;
  }

  protected void DestroyAllChildren()
  {
    List<GameObject> children = new List<GameObject>();

    foreach (Transform child in transform)
      children.Add(child.gameObject);

    foreach (GameObject child in children)
      DestroyImmediate(child);
  }

  protected void SpawnNewObjectAt(Vector3 target)
  {
    new_object = GameObject.Instantiate(object_on_rect, transform.TransformPoint(transform.position),
                                  transform.rotation) as GameObject;

    new_object.transform.position = transform.TransformPoint(target);

    new_object.transform.parent = gameObject.transform;
  }

  private Vector3 RotateVector(Vector3 vector, float angle)
  {
    vector.x = Mathf.Cos(Mathf.Deg2Rad * angle) * vector.x - Mathf.Sin(Mathf.Deg2Rad * angle) * vector.x;
    vector.y = Mathf.Cos(Mathf.Deg2Rad * angle) * vector.y + Mathf.Sin(Mathf.Deg2Rad * angle) * vector.y;
    return vector;
  }
}
