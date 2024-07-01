using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : CharacterStats
{
    [SerializeField] private Sprite sprite;
    private void CreateCharacter()
    {
        UIManager.instance.CreateCharSpriteInVelocity(sprite, GetStat(Stat.Type.speed).GetValue);
    }
    public void Cast()
    {

    }

    public void TakeDamage(float amount)
    {
        Stat stat = GetStat(Stat.Type.life);
    }
}
