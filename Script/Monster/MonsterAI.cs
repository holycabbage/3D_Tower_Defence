using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// monster AI
/// </summary>
[RequireComponent(typeof(CharacterController))]
public abstract class MonsterAI : MonoBehaviour
{
    public Animator anim_Monster;
    public Image img_HP;
    public List<Transform> Node; 

    [HideInInspector] protected float Speed = 5;

    private CharacterController _controller; 

    private bool is_Open = false; 
    private int index = 0; 
    private Transform currTarget; 
    protected bool is_Attack = false;
    public float MaxHP;
    public int HP; 
    protected int addMoney; 

    private bool is_Move; 


    /// <summary>
    /// start
    /// </summary>
    public void Open()
    {
        MaxHP = HP;
        if (Node.Count == 0)
        {
            Debug.LogError(gameObject.name + "Node is Null");
            return;
        }

        _controller = GetComponent<CharacterController>();
        currTarget = Node[index];
        is_Open = true;
        anim_Monster.SetTrigger("Move"); 
        is_Move = true;
    }


    // Update is called once per frame
    private void Update()
    {
        img_HP.transform.parent.LookAt(GameManager.Instance.Player.transform);
        if (is_Attack)
        {
            is_Move = false;
            Attack(); 
            return;
        }

        if (is_Open == false || index > Node.Count)
            return;

        var distance = Vector3.Distance(transform.position, currTarget.position);

        if (distance <= 3f)
            NextNode();
        else
        {
            transform.LookAt(currTarget.position);
            transform.position = Vector3.MoveTowards(transform.position, currTarget.position, Time.deltaTime * Speed);
            if (is_Move == false)
            {
                anim_Monster.SetTrigger("Move"); 
                is_Move = true;
            }

            Walk();
        }
    }

    /// <summary>
    /// attack
    /// </summary>
    protected virtual void Attack()
    {
    }

    public void OpenAttackState() => is_Attack = true;


    /// <summary>
    /// next node
    /// </summary>
    private void NextNode()
    {
        index++;
        if (index + 1 > Node.Count)
        {
            Siege();
            return;
        }

        currTarget = Node[index];
    }

    /// <summary>
    /// move
    /// </summary>
    protected virtual void Walk()
    {
    }

    /// <summary>
    /// siege
    /// </summary>
    private void Siege()
    {
        GameManager.Instance.GateReceivedDamage();
        Destroy(gameObject);
    }

    /// <summary>
    /// get attack
    /// </summary>
    public void ReceivedAttacks(int i = 1)
    {
        HP -= i;
        img_HP.fillAmount = HP / MaxHP;
        if (HP <= 0)
        {
            this?.Death();
            Debug.Log("Monster Death");
        }
    }

    /// <summary>
    /// death
    /// </summary>
    private void Death()
    {
        GameManager.Instance.money += addMoney;
        MonsertPool.Instance.Monsters.Remove(this);
        Destroy(gameObject);
    }
}