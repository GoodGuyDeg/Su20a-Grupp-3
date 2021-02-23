using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerHealth : MonoBehaviour
{
	public int health;

	public void TakeDamage(int damage)
	{
        if (health > 0)
        {
			health -= damage;
			Debug.Log("Health = " + health.ToString());
        }
		if (health == 0)
		{
			Debug.Log("Player Dead");
		}
	}

	public void TakeHealing(int heal)
	{
		if (health < 3)
		{
			health += heal;
			Debug.Log("Health = " + health.ToString());
		}
		if (health == 3)
		{
			Debug.Log("Full Health");
		}
	}
}

