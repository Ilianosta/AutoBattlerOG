using UnityEngine;
public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected float amount;
    [SerializeField] protected bool isDamage;
    [SerializeField] protected bool isHeal;
    [SerializeField] protected StatModifier[] modifiers;
    [SerializeField] protected CharacterTargetSystem targetSystem;
    public bool imEnemy;
    public virtual void Awake()
    {
        targetSystem = new CharacterTargetSystem(this);
    }

    public abstract void Cast();

    public enum SkillTarget
    {
        none,
        myself,
        minHp,
        maxHp,
        maxAtk,
        minAtk,
        maxRange,
        minRange,
        all
    }
}
