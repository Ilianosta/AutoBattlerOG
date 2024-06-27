using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private Stat[] stats;
    public Stat GetStat(Stat.Type type)
    {
        Stat requiredStat = stats.FirstOrDefault(stat => stat.GetStat == type);
        return requiredStat;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) GenerateModifier(Stat.Type.life, StatModifier.Type.add, 10);
        if (Input.GetKeyDown(KeyCode.B)) GenerateModifier(Stat.Type.life, StatModifier.Type.multiply, 10);
    }

    private void GenerateModifier(Stat.Type statType, StatModifier.Type modType, float amount)
    {
        StatModifier statModifier = new StatModifier(statType, amount, modType);
        
        Stat stat = GetStat(Stat.Type.life);
        Debug.Log("Pre-Stat:" + stat.GetType() + " || Value: " + stat.GetValue);
        stat.AddModifier(statModifier);
        Debug.Log("Post-Stat:" + stat.GetType() + " || Value: " + stat.GetValue);
    }
}
