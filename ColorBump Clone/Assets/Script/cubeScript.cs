using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeScript : MonoBehaviour
{
    public float gravity;
    public float fall;
    private Rigidbody rb;
    private Renderer render;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, gravity, 0);
        rb = this.GetComponent<Rigidbody>();
        render = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x <= -13 || this.transform.position.x >= 13)
        {
            rb.constraints = RigidbodyConstraints.None;
            Physics.gravity = new Vector3(0, fall, 0);
        }

        if(this.transform.position.z >= 78)
        {
            rb.constraints = RigidbodyConstraints.None;
            Physics.gravity = new Vector3(0, fall, 0);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ChangerGreen")
        {
            render.material.color = Color.green;
            gameObject.tag = "Green";
        }

        if (other.gameObject.tag == "ChangerBlue")
        {
            render.material.color = Color.blue;
            gameObject.tag = "Blue";
        }

        if (other.gameObject.tag == "ChangerRed")
        {
            render.material.color = Color.red;
            gameObject.tag = "Red";
        }
    }
}
