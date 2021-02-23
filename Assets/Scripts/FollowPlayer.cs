using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; //Referens till spelarens position - Robin

    void Update()
    {
        transform.position = new Vector3(player.position.x+4, player.position.y, player.position.z-10); //sätter kameran till spelarens position - Robin
    }
}
