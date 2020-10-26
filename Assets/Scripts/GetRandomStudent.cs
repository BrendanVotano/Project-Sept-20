using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetRandomStudent : MonoBehaviour
{
    public List<string> names;
    public Text nameText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && names.Count != 0)
            GetName();
    }

    void GetName()
    {
        int rnd = Random.Range(0, names.Count);
        nameText.text = "Next student is..... " + names[rnd];
        names.Remove(names[rnd]);
    }
}
