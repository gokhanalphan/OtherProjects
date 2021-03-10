using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bot : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Seek (Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee(Vector3 location)
    {
        Vector3 fleeVector = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }

    void Pursue()
    {
        Vector3 playerDir = player.transform.position - this.transform.position;

        float relativeHeading = Vector3.Angle(this.transform.forward, this.transform.TransformVector(player.transform.forward));
        float toPlayer = Vector3.Angle(this.transform.forward, this.transform.TransformVector(playerDir));

        if((toPlayer > 90 && relativeHeading < 20) || player.GetComponent<playerController>().speed < 0.01f)
        {
            Seek(player.transform.position);
            return;
        }

        float lookAhead = playerDir.magnitude / (agent.speed + player.GetComponent<playerController>().speed);
        Seek(player.transform.position + player.transform.forward * lookAhead);
    }

    void Evade()
    {
        Vector3 playerDir = player.transform.position - this.transform.position;
        float lookAhead = playerDir.magnitude / (agent.speed + player.GetComponent<playerController>().speed);
        Flee(player.transform.position + player.transform.forward * lookAhead);
    }

    Vector3 wanderTarget = Vector3.zero;
    void Wander()
    {
        float wanderRadius = 5;
        float wanderDist = 5;
        float wanderJitter = 1;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 playerLocal = wanderTarget + new Vector3(0, 0, wanderDist);
        Vector3 playerWorld = this.gameObject.transform.InverseTransformVector(playerLocal);

        Seek(playerWorld);
    }

    void Hide()
    {
        float dist = Mathf.Infinity;
        Vector3 choosenSpot = Vector3.zero;

        for(int i = 0; i < world.Instance.GetHidingSpots().Length; i++)
        {
            Vector3 hideDir = world.Instance.GetHidingSpots()[i].transform.position - player.transform.position;
            Vector3 hidePos = world.Instance.GetHidingSpots()[i].transform.position + hideDir.normalized * 10;

            if(Vector3.Distance(this.transform.position, hidePos) < dist)
            {
                choosenSpot = hidePos;
                dist = Vector3.Distance(this.transform.position, hidePos);

            }
        }

        Seek(choosenSpot);
    }

    void CleverHide()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;

        GameObject chosenGO = world.Instance.GetHidingSpots()[0];

        for (int i = 0; i < world.Instance.GetHidingSpots().Length; i++)
        {
            Vector3 hideDir = world.Instance.GetHidingSpots()[i].transform.position - player.transform.position;
            Vector3 hidePos = world.Instance.GetHidingSpots()[i].transform.position + hideDir.normalized * 10;

            if (Vector3.Distance(this.transform.position, hidePos) < dist)
            {
                chosenSpot = hidePos;
                chosenDir = hideDir;
                chosenGO = world.Instance.GetHidingSpots()[i];
                dist = Vector3.Distance(this.transform.position, hidePos);
            }
        }

        Collider hideCol = chosenGO.GetComponent<Collider>();
        Ray backRay = new Ray(chosenSpot, -chosenDir.normalized);

        RaycastHit info;
        float distance = 100.0f;
        hideCol.Raycast(backRay, out info, distance);

        Seek(info.point + chosenDir.normalized * 2);
    }

    bool CanSeePlayer()
    {
        RaycastHit raycastInfo;
        Vector3 raytoPlayer = player.transform.position - this.transform.position;
        if(Physics.Raycast(this.transform.position, raytoPlayer, out raycastInfo))
        {
            if (raycastInfo.transform.gameObject.tag == "Player")
                return true;
        }
        return false;
    }

    bool PlayerCanSeeMe()
    {
        Vector3 toAgent = this.transform.position - player.transform.position;
        float lookingAngle = Vector3.Angle(player.transform.forward, toAgent);

        if(lookingAngle < 60)
        {
            return true;
        }
        return false;
    }

    bool coolDown = false;

    void BehaviourCoolDown()
    {
        coolDown = false;
    }

    bool PlayerinRange()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < 10)
            return true;
        return false;
    }

    void Update()
    {
        if (!coolDown)
        {
            if(!PlayerinRange())
            {
                Wander();
            }

            else if (CanSeePlayer() && PlayerCanSeeMe())
            {
                CleverHide();
                coolDown = true;
                Invoke("BehaviourCoolDown", 3);
            }
            else
                Pursue();
        }

        //Seek(player.transform.position);
        //Wander();

        //Hide();


        //Flee();
        //Pursue();
        //Evade();
    }
}
