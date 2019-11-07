using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plater_attack : MonoBehaviour
{
    private float timeBtwAttack = 2;
    public float startTimeBtwAttack;
    public int damage;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Space)) {
                timeBtwAttack = startTimeBtwAttack;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                Debug.Log("Ipressedbuttonyes");
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                    Debug.Log("Ihithimseeeee");
                }
            }
        } else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}