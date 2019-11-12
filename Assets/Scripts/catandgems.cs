using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catandgems : MonoBehaviour
{
    private gamemastergems gm;
    private Dog_Health dg;
    private float timeBtwPickup = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Gamemaster").GetComponent<gamemastergems>();
        dg = GameObject.FindGameObjectWithTag("Player").GetComponent<Dog_Health>();
    }

    // Update is called once per frame
    void Update()
    {
        timeBtwPickup -= Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gem"))
        {
            Destroy(collision.gameObject);
            if (timeBtwPickup <= 0)
            {
                gm.points = gm.points + 1;
                dg.Health = dg.Health - 1;
                timeBtwPickup = 0.2f;
            }
        }

    }
}
