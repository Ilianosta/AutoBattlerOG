using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CharacterStats
{
    public bool isDead = false;
    [SerializeField] private List<Stat> stats = new List<Stat>();
    private float actualLife;
    public float ActualLife
    {
        get
        {
            return actualLife;
        }
        set
        {
            actualLife = value;

            float maxLife = GetStat(Stat.Type.life).GetValue;

            if (actualLife <= 0)
            {
                actualLife = 0;
                isDead = true;
            }
            if (actualLife > maxLife)
            {
                actualLife = maxLife;
                isDead = false;
            }
        }
    }

    public CharacterStats(List<Stat> stats)
    {
        if (this.stats == null) stats = new List<Stat>();
        this.stats.Clear();

        foreach (Stat stat in stats)
        {
            this.stats.Add(new Stat(stat));
        }
        this.actualLife = GetStat(Stat.Type.life).GetValue;
    }

    public Stat GetStat(Stat.Type type)
    {
        Stat requiredStat = stats.FirstOrDefault(stat => stat.GetStat == type);
        return requiredStat;
    }
}
