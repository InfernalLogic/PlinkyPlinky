using UnityEngine;
using System.Collections;

public class BackgroundObject : MonoBehaviour 
{

	public float size_min,
				 size_max,							 
				 speed_min,
				 speed_max,
				 despawn_time;

	private float size_range,
				  speed_range;


	SpriteRenderer sprite_renderer;
	
	Transform sprite_transform;

	private float coefficient;

	private Vector3 move_vector = Vector3.zero;

	void Start()
	{
		LoadTemporaryVariables ();

		CalculateCoefficient ();

		CalculateRanges ();

		CalculateScale ();

		CalculateMoveVector ();

		Destroy (gameObject, despawn_time);

	}

	void FixedUpdate()
	{
		sprite_transform.position += move_vector;
	}

	public void SetMoveVector(Vector3 input)
	{
		move_vector = input;
		//Debug.Log ("BGobject move vector: <" + move_vector.x + ", " + move_vector.y + ", " + move_vector.z + ">");
	}

	void CalculateMoveVector()
	{
		move_vector.x *= (speed_min + (speed_range * coefficient));
		move_vector.y *= (speed_min + (speed_range * coefficient));

	}

	void LoadTemporaryVariables()
	{
		sprite_renderer = GetComponent<SpriteRenderer>();
		sprite_transform = GetComponent<Transform>();
	}

	void CalculateCoefficient()
	{
		coefficient = Random.Range (0.0f, 1.0f);
		//Debug.Log ("BG Object Coefficient: " + coefficient);
	}

	void CalculateRanges()
	{
		size_range = size_max - size_min;
		speed_range = speed_max - speed_min;
	}

	void CalculateScale()
	{
		sprite_transform.localScale *= (size_max - (size_range * coefficient));
	}

}
