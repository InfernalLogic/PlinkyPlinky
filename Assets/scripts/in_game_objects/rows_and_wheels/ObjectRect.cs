using UnityEngine;
using System.Collections.Generic;

public class ObjectRect : GeometricSpawner 
{
  [SerializeField]
  private float height = 3.0f;
  [SerializeField]
  private int objects_on_height = 1;
  [SerializeField]
  private float width = 3.0f;
  [SerializeField]
  private int objects_on_width = 1;

  private Vector2[] corners = new Vector2[4]; //counter-clockwise from top right

  protected override void SpawnObjects()
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

    corners[0] = offset;
    corners[1] = new Vector3(-offset.x, offset.y, 0.0f);
    corners[2] = new Vector3(-offset.x, -offset.y, 0.0f);
    corners[3] = new Vector3(offset.x, -offset.y, 0.0f);

    for (int i = 0; i < corners.GetLength(0); ++i)
      SpawnNewObjectAt(corners[i]);
  }

  private void SpawnSideObjects(Vector3 starting_corner, Vector3 ending_corner, int object_count)
  {
    Vector3 side = ending_corner - starting_corner;

    for (int i = 1; i <= object_count; ++i)
      SpawnNewObjectAt(starting_corner + side * ((float)i / (float)(object_count + 1)));
  }
}
