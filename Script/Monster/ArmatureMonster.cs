using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmatureMonster : MonsterAI
{
    private bool BeganToEngage = false;

    // Start is called before the first frame update
    void Start()
    {
        MaxHP = 8;
        HP = 8;
        Speed = 3;
        addMoney = 3;
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