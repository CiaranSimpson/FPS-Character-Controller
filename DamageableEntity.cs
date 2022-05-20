using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Tooltip("An entity with health that can be destroyed via enough damage")]
public class DamageableEntity : Entity
{
    public bool deadOnStart = false;

    public int currentHealth = 50;
    public int maximumHealth = 50;

    private void Start()
    {
        if (deadOnStart)
        {
            ZeroHealthEvent();
        }
    }

    public void TakeDamage(int damage)
    {
        //minus damage from health
        currentHealth -= damage;

        //if health is no zero or less, kill
        if (currentHealth == 0 || currentHealth < 0)
        {
            ZeroHealthEvent();
        }
    }

    public void TakeDamage(DamageInfo info)
    {
        //minus damage from health
        currentHealth -= info.damageAmount;

        //if health is no zero or less, kill
        if (currentHealth == 0 || currentHealth < 0)
        {
            ZeroHealthEvent();
        }
    }

    public virtual void ZeroHealthEvent()
    {
        if (GetComponent<Breakable>())
        {
            GetComponent<Breakable>().Break();
        }
    }

}

