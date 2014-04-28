using UnityEngine;
using System.Collections;

public class BackgroundObject : MonoBehaviour 
{

	public float despawn_time;
	
	public BG_Object_Property size;
	public BG_Object_Property speed;
	public BG_Object_Property rotation;

	private float size_range,
				  speed_range;

	private bool has_been_visible = false;

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
		move_vector.x *= (speed.minimum + (speed.range * coefficient));
		move_vector.y *= (speed.minimum + (speed.range * coefficient));
	}

	void LoadTemporaryVariables()
	{
		sprite_transform = GetComponent<Transform>();
	}

	void CalculateCoefficient()
	{
		coefficient = Random.Range (0.0f, 1.0f);
	}

	void CalculateRanges()
	{
		//size_range = size_max - size_min;
		//speed_range = speed_max - speed_min;
		size.CalculateRange();
		speed.CalculateRange();
	}

	void CalculateScale()
	{
		sprite_transform.localScale *= size.maximum - (size.range * coefficient); //(size_max - (size_range * coefficient));
	}

	void OnBecameVisible()
	{
		has_been_visible = true;
	}

	void OnBecameInvisible()
	{
		if (has_been_visible)
		{
			Destroy (gameObject);
		}
	}

}

