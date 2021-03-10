using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private CharacterController controller;
    private GameObject teleportPoint;
    public HealthBar healthbar;

    public float speed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    Vector3 velocity;
    Animator anim;
    public int maxHealth = 100;
    public int currentHealth;

    public bool stayOnTop = false;
    public float stayOnTopTime = 10f;

    [HideInInspector]
    public bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        controller = this.GetComponent<CharacterController>();
        anim = this.GetComponent<Animator>();
        healthbar.SetMaxHealth(maxHealth);
        teleportPoint = GameObject.FindGameObjectWithTag("TeleportPoint");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "gate")
        {
            controller.enabled = false;
            controller.transform.position = teleportPoint.transform.position;
            controller.enabled = true;
        }

        if (other.gameObject.tag == "TopFloorDeadEnd")
            stayOnTop = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TopFloorDeadEnd")
            stayOnTop = false;
    }

    void FixedUpdate()
    {
        if(!isDead)
        {
            if (controller.isGrounded && velocity.y < 0)
                velocity.y = -2f;

            if (Input.GetButtonDown("Jump") && controller.isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            anim.SetFloat("isRunning2", Mathf.Abs(x + y));

            Vector3 move = transform.right * x + transform.forward * y;
            controller.Move(move * Time.deltaTime * speed);

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            if (stayOnTop)
            {
                stayOnTopTime -= Time.deltaTime;
                if (stayOnTopTime <= 0)
                    Die();
            }

            if (this.transform.position.y <= -3)
            {
                Die();
            }
        }
    }

    public void TakeDamagePlayer(int amount)
    {
        currentHealth -= amount;

        healthbar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        anim.SetTrigger("isDead");
        Destroy(gameObject, 2f);
    }
}
