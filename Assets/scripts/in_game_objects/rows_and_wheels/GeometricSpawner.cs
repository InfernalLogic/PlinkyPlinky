using UnityEngine;
using System.Collections.Generic;

public abstract class GeometricSpawner : MonoBehaviour
{
  [SerializeField]
  protected GameObject spawned_object;
  protected abstract void SpawnObjects();

  protected GameObject new_object;

  protected virtual void Awake()
  {
    if (OutdatedChildrenFound())
      DestroyAllChildren();

    SpawnObjects();
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

  protected void MakeObjectAChild(GameObject target_object)
  {
    target_object.transform.parent = gameObject.transform;
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

  protected void SpawnNewObjectAt(Vector3 target)
  {
    new_object = GameObject.Instantiate(spawned_object, transform.TransformPoint(transform.position),
                                  transform.rotation) as GameObject;

    new_object.transform.position = transform.TransformPoint(target);
    MakeObjectAChild(new_object);
  }
}
