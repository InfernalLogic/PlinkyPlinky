using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ObjectWheel : GeometricSpawner
{
  [SerializeField]
  protected int object_count = 1;
  [SerializeField]
  protected float wheel_radius = 1.0f;
  [SerializeField]
  protected float rotation_speed = 0.0f;
  [SerializeField]
  protected float starting_rotation = 0.0f;

  private Vector3 wheel_rotator = Vector3.zero;

  void Update()
  {
    RotateWheel();
  }

  protected void RotateWheel()
  {
    transform.Rotate(wheel_rotator * Time.deltaTime);
  }

  protected override void SpawnObjects()
  {
    for (int i = 0; i < object_count; ++i)
      AddObjectNumber(i);

    wheel_rotator.z = rotation_speed;
  }

  private void AddObjectNumber(int number)
  {
    Vector3 new_position = Vector3.up * wheel_radius;
    float coefficient = (float)number / (float)object_count;
    float rotation = 360.0f * coefficient + starting_rotation;

    new_position = RotateVector(new_position, rotation);
    SpawnNewObjectAt(new_position);
    SetObjectRotation(new_object, rotation);
  }

  protected void SetObjectRotation(GameObject target_object, float rotation)
  {
    Vector3 adjusted_rotation = new Vector3(0.0f, 0.0f, rotation);
    target_object.transform.Rotate(adjusted_rotation);
  }
}