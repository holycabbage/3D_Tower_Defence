using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// fish man
/// </summary>
public class FishMenMonster : MonsterAI
{
    private bool BeganToEngage = false;

    private void Awake()
    {
        HP = 3;
        MaxHP = 3;
        addMoney = 2;
    }

    protected override void Attack()
    {
        if (BeganToEngage)
        {
            return;
        }

        BeganToEngage = true;
        transform.LookAt(Hero.Instance.transform);
        anim_Monster.SetTrigger("Attack");
        StartCoroutine(AttackHero());
    }

    private IEnumerator AttackHero()
    {
        while (Hero.Instance.is_Live)
        {
            yield return new WaitForSecondsRealtime(1f);
            Hero.Instance.ReceivedAttacks();
        }

        is_Attack = false;
    }
}