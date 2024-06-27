using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [System.Serializable]
    public enum Type
    {
        life,
        armor,
        attack
    }

    // Typing
    [SerializeField] private Type type;
    public Type GetStat => type;

    // Values
    [SerializeField] private float value;
    public float GetValue
    {
        get
        {
            float totalValue = value;
            foreach (StatModifier modifier in modifiers)
            {
                if (modifier.modifierType == StatModifier.Type.add) totalValue += modifier.amount;
                if (modifier.modifierType == StatModifier.Type.multiply) totalValue *= modifier.amount;
            }
            return totalValue;
        }
    }

    // Mods
    private List<StatModifier> modifiers = new List<StatModifier>();

    public void AddModifier(StatModifier statModifier)
    {
        if (statModifier.amount != 0) modifiers.Add(statModifier);
        var sortedList = modifiers.OrderBy(x => x.modifierType == StatModifier.Type.multiply).ToList();
        modifiers = sortedList;
    }
    public void RemoveModifier(StatModifier statModifier)
    {
        if (statModifier.amount != 0) modifiers.Remove(statModifier);
        var sortedList = modifiers.OrderBy(x => x.modifierType == StatModifier.Type.multiply).ToList();
        modifiers = sortedList;
    }
}