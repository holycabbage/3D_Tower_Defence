using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryTower : MonoBehaviour
{
    public Transform Battery;
    public GameObject Bullet;
    public Transform Pool;

    private GameObject currTarget;
    private bool is_Attack;


    private void Update()
    {
        RotateTo();
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
                // Battery.rotation.SetLookRotation(currTarget.transform.position);
                break; //find target
            }
        }

        AttackTarget();
    }

    private void RotateTo()
    {
        if (currTarget == null)
        {
            return;
        }

        Vector3 dir = currTarget.transform.position - Battery.transform.position;
        dir.y = 0;
        Quaternion q = Quaternion.LookRotation(dir);
        Battery.rotation = Quaternion.Slerp(Battery.rotation, q, 5);
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
            yield return new WaitForSecondsRealtime(3f);
            if (currTarget != null) //might be killed at that moment
            {
                var item = Instantiate(Bullet, Pool, false);
                item.GetComponent<BatterBuild>().Fire(currTarget.transform);
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