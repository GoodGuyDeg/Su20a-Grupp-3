using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body; //referens till spelarens rigidbody - Robin
    public LayerMask water;

    [SerializeField, Range(150,300)] //gör en slider som man kan ändra i konsolen till spelaren speed - Robin
    float movementSpeed; //float till spelarens speed - Robin

    bool hasJumped = true; //en bool som frågar om spelaren har hoppat - Robin
    bool sliding; //om spelaren slidar eller inte - Robin
    bool canstand; //om spelaren kan stå upp eller inte - Robin

    public Animator crouch; //referens till animatorn där crouch finns. - Robin

    public float maxFuel = 1000; //variabel till maxFuel - Robin
    public float currentFuel; //sätter nuvarande fuel - Robin
    [SerializeField,Range(0.1f,2)]
    public float loseFuel = 0.1f; //hur mycket fuel man förlorar - Robin
    public float loseFuelInWater = 10; //variable för att förlora mer fuel i vatten - EN

    public Fuelbar fuelBar; //Referens till fuelbaren - Robin

    void Start()
    {
        body = GetComponent<Rigidbody2D>(); //hämtar rigidbodyn från spelaren - Robin
        sliding = false; //sätter sliding till false - Robin
        canstand = true; //spelaren kan stå blir true - Robin
        currentFuel = maxFuel; //sätter nuvarande fuel till maxfuel i början.
        fuelBar.SetMaxFuel(maxFuel); //sätter värdet på fuelbaren
    }
    void Update()
    {
        currentFuel -= loseFuel*Time.deltaTime; //tar bort fuel varje sekund - Robin
        fuelBar.SetHealth(currentFuel); //Uppdaterar så att man kan se den nuvarande fuelen - Robin

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
        if(collision.transform.tag == "MushroomHead")
        {
            body.velocity += new Vector2(0, 10); //går du på svampen studsar du uppåt - Robin
        }
        if(collision.transform.tag == "CantStand") //om tagen är Cantstand - Robin
        {
            canstand = false; //blir canstand false - Robin
        }
        if (collision.transform.tag == "Startingjump") //om spelaren triggar tag startingjump - Robin
        {
            Time.timeScale = 0.6f; //tiden slowar ned - Robin
        }
        
        //vid contakt med en liten olja/hjärta så fylls slidern på med 10 och objektet försvinner - EN
        if (collision.tag == "heal") 
        {
            TakeHealing(10);
            Destroy(collision.transform.gameObject);
        }
        //Plus 30 HP - EN
        if (collision.tag == "Heal")
        {
            TakeHealing(30);
            Destroy(collision.transform.gameObject);
        }
        //Plus 50 HP - EN
        if (collision.tag == "HEAL")
        {
            TakeHealing(50);
            Destroy(collision.transform.gameObject);
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

    //Kod för att ta up fuel av något - EN
    public void TakeHealing(int heal)
    {
        currentFuel += heal;
        currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            currentFuel -= loseFuel * Time.deltaTime * loseFuelInWater;
            print("water");
        }
    }

}
