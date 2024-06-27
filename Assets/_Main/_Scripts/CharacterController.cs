using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : CharacterStats
{
    public void Cast()
    {

    }

    public void TakeDamage(float amount)
    {
        Stat stat = GetStat(Stat.Type.life);
    }
}
