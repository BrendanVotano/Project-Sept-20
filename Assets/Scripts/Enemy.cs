using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Talk());
        //StartCoroutine(MoveRandom());
        StartCoroutine(MoveInDirection());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            StopAllCoroutines();
    }

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
            transform.Translate(Vector3.forward * Time.deltaTime);
            yield return null;
        }
        transform.Rotate(Vector3.up * 180);
        yield return new WaitForSeconds(1);
        StartCoroutine(MoveInDirection());        
    }
}
