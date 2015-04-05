﻿using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour 
{
  [SerializeField]
  private bool is_clone = false;
  [SerializeField]
  private GameObject bomb_clone;
  [SerializeField]
  private float clone_launch_force_multiplier = 50.0f;
  [SerializeField]
  private GameObject peg_hunter;

  private PlinkagonUpgrade clone_ball_upgrader;
  private PlinkagonUpgrade peg_hunter_upgrader;

  private static int bomb_count = 0;
  public static int BombCount { get { return bomb_count; } }
  private static int clone_count = 0;
  public static int CloneCount { get { return clone_count; } }

  private int coins_hit_by_this_bomb = 0;
  private int bombs_spawned = 0;
  private int peg_hunters_spawned = 0;

  void Awake()
  {
    clone_ball_upgrader = GameObject.Find("clone_ball_upgrader").GetComponent<PlinkagonUpgrade>();
    peg_hunter_upgrader = GameObject.Find("peg_hunter_upgrader").GetComponent<PlinkagonUpgrade>();
  }

	void Start () 
	{
    if (!is_clone)
      ++bomb_count;
    else
      ++clone_count;

    Destroy(gameObject, 45.0f);
  }

  void OnDestroy()
  {
    if (!is_clone)
      --bomb_count;
    else
      --clone_count;
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (CollidedWithABumper(collision) && clone_ball_upgrader.RollProcs(bombs_spawned) > 0 && !is_clone)
    {
      ++bombs_spawned;
      SpawnNewBomb();
    }
    if (CollidedWithAPeg(collision) && peg_hunter_upgrader.RollProcs(peg_hunters_spawned) > 0 && !is_clone)
    {
      ++peg_hunters_spawned;
      SpawnNewPegHunter();
    }
  }

  void SpawnNewPegHunter()
  {
    GameObject new_peg_hunter = Instantiate(peg_hunter, this.transform.position, this.transform.rotation) as GameObject;
    new_peg_hunter.GetComponent<PegHunter>().FindNewTarget();
  }

  private bool CollidedWithABumper(Collision2D collision)
  {
    return collision.gameObject.tag == "bumper";
  }

  private bool CollidedWithAPeg(Collision2D collision)
  {
    return collision.gameObject.tag == "peg";
  }

  private void SpawnNewBomb()
  {
    Vector2 force_being_added = GetComponent<Rigidbody2D>().velocity;
    force_being_added *= clone_launch_force_multiplier;
    force_being_added.x *= -1.0f;

    Vector3 target_position = transform.position;

    target_position.x += force_being_added.normalized.x;
    target_position.y += force_being_added.normalized.y;

    GameObject new_bomb = (GameObject)Instantiate(bomb_clone, target_position, transform.rotation);

    new_bomb.gameObject.GetComponent<Rigidbody2D>().AddForce(force_being_added);
  }

	void OnBecameInvisible()
	{
		Destroy (gameObject);
	}

  public void IncrementCoinsHit()
  {
    ++coins_hit_by_this_bomb;

    if (coins_hit_by_this_bomb >= 2)
      GameEvents.Publish(GameEvents.multi_plink_event);
  }
}
