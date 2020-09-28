using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType myType;
    public float speed = 1;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        //StartCoroutine(Talk());
        //StartCoroutine(MoveRandom());
        StartCoroutine(MoveInDirection());
    }

    private void Initialize()
    {
        //If we are the archer, set speed to 3 and health to 50
        if(myType == EnemyType.ARCHER)
        {
            speed = 3;
            health = 50;
        }
        //If we are the one hand, set speed to 2 and health to 100
        if (myType == EnemyType.ONEHAND)
        {
            speed = 2;
            health = 100;
        }            
        //If we are the two hand, set speed to 1 and health to 1
        if (myType == EnemyType.TWOHAND)
        {
            speed = 1;
            health = 150;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            TakeDamage(10);
    }

    public void TakeDamage(int _damage)
    {
        GameEvents.ReportEnemyHit();
        health -= _damage;
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        //Report to the game events that this enemy has died
        GameEvents.ReportEnemyDied(this);
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
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            yield return null;
        }
        transform.Rotate(Vector3.up * 180);
        yield return new WaitForSeconds(1);
        StartCoroutine(MoveInDirection());        
    }
    #endregion
}
