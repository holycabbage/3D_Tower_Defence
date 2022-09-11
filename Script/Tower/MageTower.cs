using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// magetower
/// </summary>
public class MageTower : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Pool;

    private GameObject currTarget;
    private bool is_Attack;

    private void Update()
    {
        LockTarget();
    }

    /// <summary>
    /// lock target
    /// </summary>
    private void LockTarget()
    {
        var game = Physics.OverlapSphere(transform.position, 20);
        foreach (var VARIABLE in game)
        {
            if (VARIABLE.gameObject.CompareTag("Monster"))
            {
                if (currTarget != null)
                {
                    return;
                }

                currTarget = VARIABLE.gameObject;
                break; //find the target
            }
        }

        AttackTarget();
    }

    /// <summary>
    /// attack target
    /// </summary>
    /// <param name="currTarget"></param>
    private void AttackTarget()
    {
        if (is_Attack)
            return;
        StartCoroutine(Attacktarget());
    }

    private IEnumerator Attacktarget()
    {
        is_Attack = true;
        while (currTarget != null && Disance() < 20)
        {
            yield return new WaitForSecondsRealtime(1f);
            if (currTarget != null) 
            {
                var item = Instantiate(Bullet, Pool, false);
                item.GetComponent<MageBuild>().Fire(currTarget.transform);
            }
        }

        currTarget = null;
        is_Attack = false;
    }

    private float Disance()
    {
        return Vector3.Distance(transform.position, currTarget.transform.position);
    }
}