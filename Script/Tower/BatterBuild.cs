using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterBuild : MonoBehaviour
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
            var game = Physics.OverlapSphere(transform.position, 3f);
            Dictionary<MonsterAI, bool> monster = new Dictionary<MonsterAI, bool>();
            foreach (var VARIABLE in game)
            {
                if (VARIABLE.gameObject.CompareTag("Monster"))
                {
                    var item = VARIABLE.gameObject.GetComponent<MonsterAI>();
                    if (monster.ContainsKey(item))
                    {
                        continue;
                    }

                    item.ReceivedAttacks(2);
                    monster.Add(item, true);
                }
            }

            monster = null; 
            // _monsterAI.ReceivedAttacks(2);

            Destroy(gameObject);
        }
    }
}