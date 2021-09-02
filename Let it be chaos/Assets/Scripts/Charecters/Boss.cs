using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    protected float timer;
    protected Enemy enemy;

    protected virtual void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public virtual void Die()
    {

    }
}
