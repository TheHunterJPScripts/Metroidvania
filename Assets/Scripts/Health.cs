using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
        string s = "Enemy took: " + damage + " health";
        Debug.Log(s);
    }
}
