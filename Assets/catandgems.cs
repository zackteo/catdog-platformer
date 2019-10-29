using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catandgems : MonoBehaviour
{
    private gamemastergems gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Gamemaster").GetComponent<gamemastergems>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gem"))
        {
            Destroy(collision.gameObject);
            gm.points = gm.points+ 1;
            Debug.Log("i_can_has_gem");
        }

    }
}
