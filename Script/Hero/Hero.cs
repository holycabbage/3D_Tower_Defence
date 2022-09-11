using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hero : Singleton<Hero>
{
    public NavMeshAgent _navMeshAgent;
    public Animator HeroAnim;
    private MonsterAI CurrAttackMonster; 

    private int HP = 10;
    private int MaxHP = 10;

    private bool move = true;
    private bool is_Attack = false;

    public bool is_Live = true;

    AudioSource sourse;
    public AudioClip injury;
    public AudioClip die;
    public AudioClip walk;
    public AudioClip attack;


    private void Start()
    {
        GameManager.Instance.Hero.text = "Hero HP" + HP;
        GameManager.Instance.SetHeroHP(HP, MaxHP);
        sourse = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (is_Attack)
        {
            return;
        }

        Move();
        CheckEnemy();
    }

    /// <summary>
    /// search enemy
    /// </summary>
    private void CheckEnemy()
    {
        var target = Physics.OverlapSphere(transform.position, 5);

        foreach (var item in target)
        {
            if (item.gameObject.CompareTag("Monster"))
            {
                OpenAttackState(item.GetComponent<MonsterAI>());
            }
        }
    }

    /// <summary>
    /// start attack
    /// </summary>
    private void OpenAttackState(MonsterAI monster)
    {
        if (is_Attack == true)
        {
            return;
        }

        is_Attack = true;
        CurrAttackMonster = monster;

        _navMeshAgent.SetDestination(transform.position); //stop move
        monster.OpenAttackState(); //start monster attack

        transform.LookAt(monster.transform.position);
        HeroAnim.SetBool("Idle", false);
        HeroAnim.SetTrigger("Attack");
        sourse.PlayOneShot(attack);
        StartCoroutine(OpenAttack());

        Debug.Log("hero starts to attack");
    }

    private IEnumerator OpenAttack()
    {
        while (CurrAttackMonster.HP > 0)
        {
            yield return new WaitForSecondsRealtime(1.09f);
            if (CurrAttackMonster != null)
            {
                CurrAttackMonster.ReceivedAttacks();
            }
        }

        is_Attack = false;
    }


    /// <summary>
    /// move
    /// </summary>
    private void Move()
    {
        if (_navMeshAgent.hasPath == false)
        {
            //wether arrive destination
            move = true;
            HeroAnim.SetBool("Idle", true);
            //sourse.PlayOneShot(walk);
        }
        else
        {
            HeroAnim.SetBool("Idle", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.name == "Terrain")
                {
                    _navMeshAgent.SetDestination(hit.point);
                    OpenMove();
                }
            }
        }
    }

    private void OpenMove()
    {
        if (move)
        {
            HeroAnim.SetTrigger("Run");
            move = false;
        }
    }

    /// <summary>
    /// get attack
    /// </summary>
    public void ReceivedAttacks()
    {
        HP--;
        // GameManager.Instance.Hero.text = "Hero HP: " + HP;

        sourse.PlayOneShot(injury);
        GameManager.Instance.Hero.text = "Hero HP:" + HP;
        GameManager.Instance.SetHeroHP(HP, MaxHP);

        if (HP <= 0)
        {
            sourse.PlayOneShot(die);
            is_Live = false;
            gameObject.SetActive(false);
        }
    }
}