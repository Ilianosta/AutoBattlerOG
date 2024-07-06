using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public int id;
    public bool IsDead => stats.isDead;
    [SerializeField] private CharacterSO character;
    [SerializeField] private CharacterStats stats;
    public CharacterStats Stats => stats;
    public float ActualLife => stats.ActualLife;
    private int actualSkill = 0;
    private int ActualSkill
    {
        get { return actualSkill; }
        set
        {
            actualSkill = value;
            if (actualSkill > skills.Count - 2) actualSkill = 0;
            if (actualSkill < 0) actualSkill = 0;
        }
    }
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
        if (!GameManager.instance.activeTurn && !GameManager.GamePaused)
        {
            bool isMyTurn = UIManager.instance.UpdateCharVelocityIcon(id);
            if (isMyTurn) OnMyTurn();
        }
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

    public void OnMyTurn()
    {
        Cast();
    }

    public void Cast()
    {
        skills[actualSkill].Cast();
        ActualSkill++;
    }

    public void EndTurn() => GameManager.instance.activeTurn = false;

    public void TakeDamage(float amount)
    {
        GetComponentInChildren<Animator>().SetTrigger("TakingPunch");
        amount -= stats.GetStat(Stat.Type.armor).GetValue;
        if (amount > 0) stats.ActualLife -= amount;
    }

    IEnumerator RemoveModifierIn(StatModifier statModifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        stats.GetStat(statModifier.type).RemoveModifier(statModifier);
    }
}
