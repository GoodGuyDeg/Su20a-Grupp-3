using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHeal : MonoBehaviour
{
	public void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Heal")
        {
			Fuelbar fuelStats = GameObject.FindGameObjectWithTag("fuelhealth").GetComponent<Fuelbar>();
        }

	}
}
