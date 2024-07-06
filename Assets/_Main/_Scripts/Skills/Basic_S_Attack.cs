using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_S_Attack : Skill
{
    public override void Cast()
    {
        List<CharacterController> charTargets = new List<CharacterController>();
        charTargets.AddRange(targetSystem.GetTargets());
        animator.SetTrigger("Punch");
        foreach (CharacterController character in charTargets)
        {
            character.TakeDamage(amount);
        }
    }
}
