using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// test
/// </summary>
public class HeroTest : MonoBehaviour
{
    public NavMeshAgent _navMeshAgent;
    public Transform Monster;

    // Start is called before the first frame update
    void Start()
    {
        // _navMeshAgent.speed = 10;
        // _navMeshAgent.SetDestination(Monster.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.name == "Terrain")
                {
                    _navMeshAgent.SetDestination(hit.point);
                }
            }
        }
    }
}