using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBuild : MonoBehaviour
{
    private Transform Target;
    private MonsterAI _monsterAI;
    private bool is_Fire;

    public void Fire(Transform target)
    {
        Target = target;
        _monsterAI = target.GetComponent<MonsterAI>();
    }

    private void Update()
    {
        if (Target == null && is_Fire)
        {
            Destroy(gameObject);
            return;
        }
        else if (Target == null)
            return;


        transform.position = Vector3.MoveTowards(transform.position, Target.position, Time.deltaTime * 40);
        is_Fire = true;
        if (Vector3.Distance(transform.position, Target.transform.position) < 1f)
        {
            _monsterAI.ReceivedAttacks();
            Destroy(gameObject);
        }
    }
}