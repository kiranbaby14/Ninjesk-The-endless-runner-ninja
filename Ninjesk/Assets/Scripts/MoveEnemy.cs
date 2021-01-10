using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{

	private Transform _player;
	public int speed = 10;
	public int distance = 50;

	void Start()
	{
		_player = GameObject.Find("Ninja").transform;


	}


	void Update()
	{

		if (Vector3.Distance(transform.position, _player.position) <= distance)
		{


			if (Explosion.caughtPlayer == true && Explosion.hasExploded == false)
			{
				speed = 0;
	
			}

			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);
		}

	}

	}
