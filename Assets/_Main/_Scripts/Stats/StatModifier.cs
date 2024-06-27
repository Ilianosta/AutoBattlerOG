using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatModifier
{
    public StatModifier(Stat.Type type, float amount, Type modifierType, float duration = -1)
    {
        this.type = type;
        this.amount = amount;
        this.modifierType = modifierType;
        this.duration = duration;
    }
    [System.Serializable]
    public enum Type
    {
        add, multiply
    }
    public Stat.Type type;
    public Type modifierType;
    public float amount;
    public float duration;

}
