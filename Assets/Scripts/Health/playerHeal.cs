using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHeal : MonoBehaviour
{
	public void SendHealing(int heal)
	{
		PlayerHealth playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
		playerStats.TakeHealing(heal);
	}
}
