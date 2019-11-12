using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plater_attack : MonoBehaviour
{
    private Cat_Health cg;
    private Rigidbody2D m_Rigidbody2D;

    [SerializeField] private float m_DashForce = 400f;

    private float timeBtwAttack = 2;
    public float startTimeBtwAttack;
    public int damage;

    private float timeBtwDash = 2;
    public float startTimeBtwDash;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    private CharacterController2D fd;

    // Start is called before the first frame update
    void Start()
    {
        cg = GameObject.FindGameObjectWithTag("Player2").GetComponent<Cat_Health>();
        fd = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
    }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Space)) {
                timeBtwAttack = startTimeBtwAttack;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                /*                for (int i = 0; i < enemiesToDamage.Length; i++)
                                {
                                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                                    Debug.Log("Ihithimseeeee");
                                    cg.Health = cg.Health - 1;
                                }*/
                if (enemiesToDamage[0])
                {
                    enemiesToDamage[0].GetComponent<Enemy>().TakeDamage(damage);
                    Debug.Log("Ihithimseeeee");
                    cg.Health = cg.Health - 1;

                }
            }
        } else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        if (timeBtwDash <= 0)
        {
            if (Input.GetKey(KeyCode.M))
            {
                Debug.Log("dashdash");
                timeBtwDash = startTimeBtwDash;
                if (fd.getFaceRight() == false)
                {
                    m_Rigidbody2D.AddForce(new Vector2(-m_DashForce, 0f));
                }else
                {
                    m_Rigidbody2D.AddForce(new Vector2(m_DashForce, 0f));
                }
            }
        }
        else
        {
            timeBtwDash -= Time.deltaTime;
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}