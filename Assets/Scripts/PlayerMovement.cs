using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body; //referens till spelarens rigidbody - Robin

    [SerializeField, Range(150,300)] //gör en slider som man kan ändra i konsolen till spelaren speed - Robin
    float movementSpeed; //float till spelarens speed - Robin

    bool hasJumped = true; //en bool som frågar om spelaren har hoppat - Robin
    bool sliding; //om spelaren slidar eller inte - Robin
    bool canstand; //om spelaren kan stå upp eller inte - Robin

    public Animator crouch; //referens till animatorn där crouch finns. - Robin
    void Start()
    {
        body = GetComponent<Rigidbody2D>(); //hämtar rigidbodyn från spelaren - Robin
        sliding = false; //sätter sliding till false - Robin
        canstand = true; //spelaren kan stå blir true - Robin
    }
    void Update()
    {
        float hor = Input.GetAxis("Horizontal"); //hämtar inputen "horizontal" och bevarar den i floaten hor (horizontal) - Robin

        body.velocity = new Vector2(hor * movementSpeed * Time.deltaTime, body.velocity.y); //ändrar body.velocityns x värde beroende på vilken knapp man klickar på för att röra sig åt höger eller vänster (ändra inte y värdet det är body.velocity.y för att man ska kunna hoppa) - Robin

        if (sliding == false) //om sliding är false - Robin
        {
            if (Input.GetButtonDown("Jump") && !hasJumped) //Om spelaren klickar på jump och spelaren inte har hoppat så hoppar den - Robin
            {
                body.velocity = new Vector2(body.velocity.x, 12);
                hasJumped = true;
            }
        }
         if (Input.GetKeyDown(KeyCode.LeftShift) && !hasJumped) //om spelaren är på marken och håller ned shift - Robin
        {
            sliding = true; //sliding är true - Robin
            crouch.SetBool("Crouch",true); //spelar crouch animationen - Robin
        } else if (Input.GetKeyUp(KeyCode.LeftShift)) //annars om spelaren släpper på shift - Robin
        {
            if (canstand) //och om spelaren kan stå upp - Robin
            {
                sliding = false; //sliding på false - Robin
                crouch.SetBool("Crouch", false); //spelaren crouchar inte längre - Robin
            }
         }
    }
    private void OnCollisionEnter2D(Collision2D collision) //om spelaren collidar - Robin
    {
        hasJumped = false; //kan hoppa igen - Robin
    }
    private void OnTriggerEnter2D(Collider2D collision) //om spelaren entrar triggern - Robin
    {
        if(collision.transform.tag == "CantStand") //om tagen är Cantstand - Robin
        {
            canstand = false; //blir canstand false - Robin
        }
        if (collision.transform.tag == "Startingjump") //om spelaren triggar tag startingjump - Robin
        {
            Time.timeScale = 0.6f; //tiden slowar ned - Robin
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision) //om spelaren exitar triggern - Robin
    {
        if (collision.transform.tag == "CantStand") //om den lämnar triggern cantstand - Robin
        {
            canstand = true; //canstand blir nu true - Robin
            sliding = false; //sliding blir nu false spelaren ställer sig alltså upp - Robin
            crouch.SetBool("Crouch", false); //spelaren slutar croucha - Robin
        }
        if (collision.transform.tag == "Endingjump") //om spelaren triggar endingjump triggern - Robin
        {
            Time.timeScale = 1f; //blir tiden normal - Robin
        }
    }
}
