﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyType myType;
    public PatrolType patrolType;
    public float speed = 1;
    public int health;
    public float detectDistance = 8;
    Animator anim;
    NavMeshAgent agent;
    List<GameObject> waypoints;
    int currentWaypoint = 0;
    GameObject player;
    public float undetectTimer = 5;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        waypoints = EnemyManager.instance.waypoints;
        Initialize();
        //StartCoroutine(Talk());
        //StartCoroutine(MoveRandom());
        //StartCoroutine(MoveInDirection());
        currentWaypoint = Random.Range(0, waypoints.Count);
        agent.SetDestination(waypoints[currentWaypoint].transform.position);
        audioSource = GetComponent<AudioSource>();
    }

    private void Initialize()
    {
        switch(myType)
        {
            case EnemyType.ARCHER:
                speed = 3;
                health = 50;
                break;
            case EnemyType.ONEHAND:
                speed = 2;
                health = 100;
                break;
            case EnemyType.TWOHAND:
                speed = 1;
                health = 150;
                break;
            default:
                speed = 2;
                health = 100;
                break;
        }
        patrolType = PatrolType.RANDOM;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            TakeDamage(10);

        Patrol();
    }

    void Patrol()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if(distToPlayer <= detectDistance)
        {
            if (Physics.Linecast(transform.position, player.transform.position, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    if (patrolType != PatrolType.CHASE)
                    {
                        patrolType = PatrolType.DETECT;
                    }
                }
            }
        }

        switch(patrolType)
        {
            case PatrolType.CHASE:
                agent.SetDestination(player.transform.position);
                if (distToPlayer > detectDistance)
                    patrolType = PatrolType.DETECT;
                break;
            case PatrolType.DETECT:
                agent.SetDestination(transform.position);
                undetectTimer -= Time.deltaTime;
                if(undetectTimer <= 0)
                {
                    if (distToPlayer <= detectDistance)
                    {
                        patrolType = PatrolType.CHASE;
                        undetectTimer = 5;
                    }
                    else
                    {
                        patrolType = PatrolType.RANDOM;
                        agent.SetDestination(waypoints[currentWaypoint].transform.position);
                    }
                }
                break;
            default:
                float dist = Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position);
                if (dist < 0.1f)
                {
                    if (patrolType == PatrolType.LINEAR)
                        currentWaypoint = currentWaypoint < waypoints.Count - 1 ? currentWaypoint += 1 : 0;
                    else
                        currentWaypoint = Random.Range(0, waypoints.Count);

                    agent.SetDestination(waypoints[currentWaypoint].transform.position);
                }
                undetectTimer = 5;
                break;
        }
    }

    public void TakeDamage(int _damage)
    {
        GameEvents.ReportEnemyHit();
        health -= _damage;
        if (health <= 0)
            Die();
        else
        {
            int rnd = Random.Range(1, 4);
            switch(rnd)
            {
                case 1:
                    anim.SetTrigger("Hit1");
                    break;
                case 2:
                    anim.SetTrigger("Hit2");
                    break;
                case 3:
                    anim.SetTrigger("Hit3");
                    break;
            }
            
            audioSource.PlayOneShot(AudioManager.instance.GetHitSound());
        }
            
    }

    public void Footstep()
    {
        audioSource.PlayOneShot(AudioManager.instance.GetFootStepSound());
    }

    public void Die()
    {
        //Report to the game events that this enemy has died
        GameEvents.ReportEnemyDied(this);

        audioSource.clip = AudioManager.instance.GetDieSound();
        audioSource.Play();
        //Get a random number, append it to the animation trigger name and set the animation to play
        int rnd = Random.Range(1, 4);
        anim.SetTrigger("Death" + rnd.ToString());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            TakeDamage(10);
            Destroy(collision.gameObject);
        }
    }

    #region Coroutines
    IEnumerator Talk()
    {
        print("Hello...");
        yield return new WaitForSeconds(1);
        print("My name is");
        yield return new WaitForSeconds(1);
        print("...");
        yield return new WaitForSeconds(0.2f);
        print("I forget");
    }

    //I added a fnction to get the enemy to move randomly
    IEnumerator MoveRandom()
    {
        Vector3 newPos = new Vector3(Random.Range(-5f,5f), 0, Random.Range(-5f, 5f));
        transform.position = newPos;
        yield return new WaitForSeconds(3);
        StartCoroutine(MoveRandom());
    }

    IEnumerator MoveInDirection()
    {
        for (int i = 0; i < 500; i++)
        {
            //Tried to instantiate at random points but didn't work as intended
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            yield return null;
        }
        transform.Rotate(Vector3.up * 180);
        yield return new WaitForSeconds(1);
        StartCoroutine(MoveInDirection());        
    }
    #endregion
}
