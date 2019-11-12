using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemastergems : MonoBehaviour
{
    public int points;
    public string pointsText;
    public float damage;
    public string damageText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointsText = ("Points " + points);
        damageText = ("Damage taken " + damage);
    }
}
