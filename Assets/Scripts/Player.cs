using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon
{
    FIRE,
    WATER,
    POISON
}

public enum Status
{
    HEALTHY,
    POISONED,
    DEAD
}

public class Player : MonoBehaviour
{
    [Header("Player Variables")]
    public Status playerStatus;
    public float speed;
    private float translation;
    private float straffe;

    public int health = 100;
    public string playerName;
    Material playerMaterial;

    [Header("Weapons")]
    public Weapon playerWeapon;
    public GameObject[] weapons;
    public GameObject[] projectilePrefabs;
    public Color green, blue, yellow, red;

    public Transform firingPoint;

    [Header("Audio Effects")]
    AudioSource audioSource;
    public float stepRate = 0.5f;
    public float stepCoolDown;
    

    void Start()
    {
        //Set our defaults
        playerStatus = Status.HEALTHY;
        playerWeapon = Weapon.FIRE;

        ChangeWeapon(0);
        playerMaterial = GetComponent<Renderer>().material;
        playerMaterial.color = green;

        audioSource = GetComponent<AudioSource>();
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

        if (Input.GetButtonDown("Fire1"))
            FireWeapon();

        translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(straffe, 0, translation);

        stepCoolDown -= Time.deltaTime;
        if((translation != 0 || straffe != 0) && stepCoolDown < 0)
        {
            audioSource.pitch = Random.Range(0.85f, 1.15f);
            audioSource.PlayOneShot(AudioManager.instance.playerFootStep);
            stepCoolDown = stepRate;
        }
    }

    void FireWeapon()
    {
        GameObject go = Instantiate(projectilePrefabs[0], firingPoint.position, transform.rotation);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        Destroy(go, 2);
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
            playerStatus = Status.DEAD;
        }
    }
}
