using UnityEngine;
using System.Collections;

public class BackgroundObject : MonoBehaviour 
{
  [SerializeField]
	private float despawn_time;
  [SerializeField]
  private string target_layer = "";
	
	public BGObjectProperty size;
	public BGObjectProperty speed;
	public BGObjectProperty rotation;

	private float size_range,
				  speed_range;

	Transform sprite_transform;

	private float coefficient;

	private Vector3 move_vector = Vector3.zero;

	private Vector3 rotation_vector = Vector3.zero;

  void Awake()
  {
    gameObject.renderer.sortingLayerName = target_layer;
    sprite_transform = GetComponent<Transform>();
    coefficient = Random.Range(0.0f, 1.0f);

    CalculateRanges();
    CalculateScale();
  }

	void Start()
	{
		Destroy (gameObject, despawn_time);
    CalculateMoveVector();
    CalculateRotationVector();
	}

	void Update()
	{
		sprite_transform.position += (move_vector * Time.deltaTime);
	}

	public void SetMoveVector(Vector3 input)
	{
		move_vector = input;
	}

	void CalculateRanges()
	{
		size.CalculateRange();
		speed.CalculateRange();
		rotation.CalculateRange();
	}

	void CalculateMoveVector()
	{
		move_vector.x *= (speed.minimum + (speed.range * coefficient));
		move_vector.y *= (speed.minimum + (speed.range * coefficient));
	}

	void CalculateRotationVector()
	{
		rotation_vector.z = rotation.minimum + (rotation.range * coefficient);
	}

	void CalculateScale()
	{
		sprite_transform.localScale *= size.maximum - (size.range * coefficient);
	}

	void OnBecameInvisible()
	{
		Destroy (gameObject);
	}


}

