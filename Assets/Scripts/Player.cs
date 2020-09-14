using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public int health = 100;
    public string playerName;
    public bool isDead;
    Material playerMaterial;

    public GameObject[] weapons;
    public Color green, blue, yellow, red;

    void Start()
    {
        ChangeWeapon(0);
        playerMaterial = GetComponent<Renderer>().material;
        playerMaterial.color = green;

       
    }

 
  

    void ChangeWeapon(int _weapon) // takes an int value
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);   // deactivates all weapons, and then sets the selected weapon to be active on the next line
        }

        weapons[_weapon].SetActive(true);  // sets the selected weapon (ChangeWeapon(selection) to be active
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            ChangeWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeWeapon(2);


        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }

    }

    void TakeDamage(int _damage)
    {
        health -= _damage;  // minuses the damage value from player health


        if (health > 60)                       // if this is true, it will not check the other else if's
            playerMaterial.color = Color.green;
        else if (health > 40)
            playerMaterial.color = Color.blue;                   // Change the player materials color based on the health they have 
        else if (health > 20)
            playerMaterial.color = Color.yellow;
        else
            playerMaterial.color = Color.red;


        if (health <= 0)  // Destroy this object when health is less than or equal to 0
        {
            isDead = true; 
        }
    }
}
