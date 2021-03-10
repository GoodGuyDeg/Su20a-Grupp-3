using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutTheRope : MonoBehaviour
{
    public Rigidbody2D bridge;
    public GameObject bridgerelease; //referens till det som håller uppe bron - Robin 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "playerflame") //Om repet nuddar elden - Robin
        {
            transform.position += new Vector3(0, 10000, 0); //Flyttar iväg repet - Robin
            bridgerelease.transform.position += new Vector3(0, 10000, 0); //Flyttar iväg på collidern som håller uppe bron - Robi
            bridge.constraints = RigidbodyConstraints2D.None; //bro kan nu inte flytta på sig utan att repet är borta - EN
        }
    }
}
