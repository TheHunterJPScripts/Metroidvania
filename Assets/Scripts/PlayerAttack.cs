using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public float timeBetweenAttacks;
    private float timeForNexttAttack;
    [Space(5)]
    public Transform attackPos;
    public Vector2 attackRange;
    public LayerMask whatIsEnemie;
    [Space(5)]
    public int damage;

    private void Update()
    {
        if(timeForNexttAttack <= 0)
        {
            bool keyPressed = Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.E);

            if(keyPressed)
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, attackRange, 0, whatIsEnemie);
                foreach (var enemy in enemiesToDamage)
                {
                    Health health = enemy.GetComponent<Health>();
                    if (health == null)
                    {
                        Debug.Log("Intentando atacar enemigo sin vida.");
                    }
                    else
                    {
                        health.TakeDamage(damage);
                    }
                }
                timeForNexttAttack = timeBetweenAttacks;
            }
        } else
        {
            timeForNexttAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackPos.position, attackRange);
    }
}
