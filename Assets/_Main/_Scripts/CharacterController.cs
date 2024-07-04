using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public int id;
    [SerializeField] private CharacterSO character;
    [SerializeField] private CharacterStats stats;

    private List<Skill> skills = new List<Skill>();

    private void Awake()
    {
        skills.AddRange(GetComponents<Skill>());
    }

    private void Start()
    {
        stats = new CharacterStats(character.stats);
        CreateCharacter();
    }

    private void Update()
    {
        if (!GameManager.instance.activeTurn && !GameManager.GamePaused) UIManager.instance.UpdateCharVelocityIcon(id);
    }
    // #region "Utilities"
    // private void GenerateModifier(Stat.Type statType, StatModifier.Type modType, float amount, float duration = -1)
    // {
    //     StatModifier statModifier = new StatModifier(statType, amount, modType, duration);

    //     Stat stat = stats.GetStat(statModifier.type);
    //     stat.AddModifier(statModifier);
    //     if (statModifier.duration > 0)
    //     {
    //         StartCoroutine(RemoveModifierIn(statModifier, statModifier.duration));
    //     }
    // }
    // private void GenerateModifier(StatModifier statModifier)
    // {
    //     Stat stat = stats.GetStat(statModifier.type);
    //     stat.AddModifier(statModifier);
    //     if (statModifier.duration > 0)
    //     {
    //         StartCoroutine("RemoveModifierIn", (statModifier, statModifier.duration));
    //     }
    // }
    // #endregion
    private void CreateCharacter()
    {
        UIManager.instance.CreateCharSpriteInVelocity(id, character.sprite, stats.GetStat(Stat.Type.speed).GetValue);
        UIManager.instance.UpdateCharStatusInUI(id, character.sprite);
    }

    public void Cast()
    {

    }

    public void TakeDamage(float amount)
    {
        amount -= stats.GetStat(Stat.Type.armor).GetValue;
        if (amount > 0) stats.ActualLife -= amount;
    }

    IEnumerator RemoveModifierIn(StatModifier statModifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        stats.GetStat(statModifier.type).RemoveModifier(statModifier);
    }
}
