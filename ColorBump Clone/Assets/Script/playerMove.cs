using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMove : MonoBehaviour
{
    private Rigidbody rb;

    public float speed;
    public float turningSpeed;

    float sides;
    float forward;

    public Renderer rend;
    Color colorGreen = Color.green;
    Color colorBlue = Color.blue;
    Color colorRed = Color.red;
    Color colorTrasparent = Color.clear;

    public ParticleSystem destructionPart;
    public ParticleSystem trail;
    public ParticleSystem particalTrail;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rend = this.GetComponent<Renderer>();
        destructionPart = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Vector3.forward * Time.deltaTime * speed);
        
        forward = Input.GetAxisRaw("Vertical");
        Vector3 moveFor = Vector3.forward * forward;

        sides = Input.GetAxisRaw("Horizontal");
        Vector3 move = Vector3.right * sides;
        rb.velocity = (moveFor + move) * turningSpeed * Time.deltaTime;


        if (gameObject.transform.position.z >= 305 || gameObject.transform.position.x < -19 || gameObject.transform.position.x > 19)
            SceneManager.LoadScene(0);

        // FORWARD MOVEMENT PERMENANT
        //rb.AddForce(Vector3.forward * Time.deltaTime * speed);

        /*
        sides = Input.GetAxisRaw("Horizontal");
        Vector2 move = Vector2.right * sides;
        transform.Translate(move * turningSpeed * Time.deltaTime);
        */



        /*
        // SIDE MOVEMENT FOR INPUT WITH RB
        sides = Input.GetAxisRaw("Horizontal");
        Vector3 move = Vector2.right * sides;
        rb.AddForce(move * turningSpeed * Time.deltaTime);
        */

    }

    public void GameOver()
    {
        //Time.timeScale = 0f;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        speed = 0f;
        turningSpeed = 0f;
        Time.timeScale = 0.2f;
        //rend.gameObject.SetActive(false);

        rend.material.color = colorTrasparent;
        destructionPart.Play();
        trail.Stop();
        particalTrail.Stop();


    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ChangerGreen")
        {
            rend.material.color = colorGreen;
        }

        if (other.gameObject.tag == "ChangerBlue")
        {
            rend.material.color = colorBlue;
        }

        if (other.gameObject.tag == "ChangerRed")
        {
            rend.material.color = colorRed;
        }

        if(other.gameObject.tag == "Green")
        {
            if(rend.material.color == colorRed || rend.material.color == colorBlue)
            {
                GameOver();
            }
        }

        if (other.gameObject.tag == "Blue")
        {
            if (rend.material.color == colorRed || rend.material.color == colorGreen)
            {
                GameOver();
            }
        }

        if (other.gameObject.tag == "Red")
        {
            if (rend.material.color == colorGreen || rend.material.color == colorBlue)
            {
                GameOver();
            }
        }
    }
}
