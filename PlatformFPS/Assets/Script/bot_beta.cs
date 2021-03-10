using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bot_beta : MonoBehaviour
{

    NavMeshAgent agent;
    GameObject[] goalPos;
    Animator anim;
    GameObject player;

    public float range = 3f;
    public int enemyDamage = 10;
    public float enemyHealth = 100f;
    public ParticleSystem poisonEffect;

    bool dead = false;

    void Start()
    {
        goalPos = GameObject.FindGameObjectsWithTag("goal");
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
    }

    bool PlayerIsAlive()
    {
        if (player == null)
            return false;
        else
            return true;
    }

    /*bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 rayToThePlayer = player.transform.position - this.transform.position;
        float angle = Vector3.Angle(rayToThePlayer, this.transform.forward);
        if (angle < 90)
        {
            if (Physics.Raycast(this.transform.position, rayToThePlayer, out hit, range))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    return true;
                }
            }
        }
        return false;
    }*/

    bool DistanceToPlayer()
    {

        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        if (dist < 3f)
            return true;
        else
            return false;
    }
    
    void Attack()
    {
        agent.isStopped = true;
        RaycastHit attackInfo;
        Vector3 rayToPlayer = player.transform.position - this.transform.position;
        Quaternion lookRot = Quaternion.LookRotation(rayToPlayer);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRot, 10f * Time.deltaTime);
        anim.SetTrigger("isAttack");
        //poisonEffect.Play();

        if (Physics.Raycast(this.transform.position, rayToPlayer, out attackInfo, range))
        {
            playerController playerSc = attackInfo.transform.GetComponent<playerController>();
            if (playerSc != null)
            {
                if(Random.Range(0f,50f) < 5)
                {
                    playerSc.TakeDamagePlayer(enemyDamage);
                }
            }
        }
    }

    void Follow()
    {
        anim.SetTrigger("isRunning");
        agent.SetDestination(player.transform.position);
    }

    void Pursue()
    {
        agent.isStopped = false;
        anim.SetTrigger("isRunning");
        if(agent.hasPath)
        {
            if(agent.remainingDistance < 3f)
            {
                agent.SetDestination(goalPos[Random.Range(0, goalPos.Length)].transform.position);
            }
            return;
        }
        else
        {
            agent.SetDestination(goalPos[Random.Range(0, goalPos.Length)].transform.position);
        }
    }

    public void TakeDamage(int amount)
    {
        enemyHealth -= amount;

        if (enemyHealth <= 0)
        {
            dead = true;
        }
    }

    public void Dead()
    {
        agent.isStopped = true;
        anim.SetTrigger("isDead");
        Destroy(gameObject, 3f);
        return;
    }

    void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (!dead)
        {
            if (PlayerIsAlive() && DistanceToPlayer())
            {
                Attack();
            }

            else if (!PlayerIsAlive())
            {
                Pursue();
            }
            else
            {
                Follow();
                agent.isStopped = false;
            }
        }
        else
            Dead();
    }
}
