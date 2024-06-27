using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatModifier
{
    public StatModifier(Stat.Type type, float amount, Type modifierType)
    {
        this.type = type;
        this.amount = amount;
        this.modifierType = modifierType;
    }
    [System.Serializable]
    public enum Type
    {
        add, multiply
    }
    public Stat.Type type;
    public float amount;
    public Type modifierType;
}
