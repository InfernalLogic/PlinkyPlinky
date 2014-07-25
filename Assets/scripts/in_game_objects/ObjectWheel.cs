using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ObjectWheel : MonoBehaviour
{
  [SerializeField]
  protected int object_count = 1;
  [SerializeField]
  protected float wheel_radius = 1.0f;
  [SerializeField]
  protected GameObject object_on_wheel;
  [SerializeField]
  protected float rotation_speed = 0.0f;
  [SerializeField]
  protected float starting_rotation = 0.0f;

  protected List<GameObject> children = new List<GameObject>();

  private GameObject new_object;
  private Vector3 wheel_rotator = Vector3.zero;

  void Awake()
  {
    if (OutdatedChildrenFound())
      DestroyAllChildren();

    SpawnWheelObjects();
  }


  void Update()
  {
    RotateWheel();
  }

  protected void RotateWheel()
  {
    transform.Rotate(wheel_rotator * Time.deltaTime);
  }

  protected void DestroyAllChildren()
  {
    foreach (Transform child in transform)
    {
      children.Add(child.gameObject);
    }

    foreach (GameObject child in children)
    {
      DestroyImmediate(child);
    }

    children.Clear();
  }

  protected bool OutdatedChildrenFound()
  {
    return transform.childCount > 0;
  }

  protected virtual void SpawnWheelObjects()
  {
    double degrees_between_objects = 360.0 / object_count;
    double current_object_rotation = degrees_between_objects;

    Vector3 rotator = Vector3.zero;
    rotator.z = (float)current_object_rotation;

    for (int i = 0; i < object_count; ++i)
    {
      new_object = AddObjectToWheel();
      MakeObjectAChild(new_object);
      SetObjectRotation(new_object, rotator);
      MoveObjectToPerimeterOfCircle(new_object);

      current_object_rotation += degrees_between_objects;

      rotator.z = (float)current_object_rotation;

      children.Add(new_object);
    }

    wheel_rotator.z = rotation_speed;

  }

  protected void MoveObjectToPerimeterOfCircle(GameObject target_object)
  {
    target_object.transform.position = transform.TransformPoint(wheel_radius * target_object.transform.right);
  }

  protected void SetObjectRotation(GameObject target_object, Vector3 rotation)
  {
    Vector3 adjusted_rotation = new Vector3(rotation.x, rotation.y, rotation.z + starting_rotation);
    target_object.transform.Rotate(adjusted_rotation);
  }

  protected void MakeObjectAChild(GameObject target_object)
  {
    target_object.transform.parent = gameObject.transform;
  }

  protected GameObject AddObjectToWheel()
  {
  return GameObject.Instantiate(object_on_wheel, transform.TransformPoint(transform.position),
                                transform.rotation) as GameObject;
  }
}