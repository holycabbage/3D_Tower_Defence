using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class malphiteMonster : MonsterAI
{
    private bool BeganToEngage = false;

    // Start is called before the first frame update
    void Start()
    {
        MaxHP = 25;
        HP = 25;
        Speed = 3;
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