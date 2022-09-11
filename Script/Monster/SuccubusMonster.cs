using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusMonster : MonsterAI
{
    private bool BeganToEngage = false;

    // Start is called before the first frame update
    void Start()
    {
        MaxHP = 20;
        HP = 20;
        Speed = 5;
        addMoney = 5;
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
            yield return new WaitForSecondsRealtime(0.5f);
            Hero.Instance.ReceivedAttacks();
        }

        is_Attack = false;
    }
}