using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// monster pool
/// </summary>
public class MonsertPool : Singleton<MonsertPool>
{
    public FishMenMonster fish;
    public RabbitMonster rabbit;
    public ArmatureMonster armature;
    public SuccubusMonster succubus;
    public malphiteMonster MalphiteMonster;

    public Dictionary<MonsterAI, bool> Monsters = new Dictionary<MonsterAI, bool>();

    private bool is_GameOver;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateMonster());
    }

    private void Update()
    {
        if (is_GameOver == false)
            return;

        if (Monsters.Count == 0)
        {
            GameManager.Instance.GameOverVic();
        }
    }


    public void GameOver()
    {
        StopAllCoroutines();
        foreach (var VARIABLE in Monsters)
        {
            if (VARIABLE.Key != null)
            {
                Destroy(VARIABLE.Key.gameObject);
            }
        }
    }

    private IEnumerator CreateMonster()
    {
        float time = 0;
        while (time < 13.5f)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            time += 1.5f;
            var item = Instantiate(fish, transform, false);
            item.gameObject.SetActive(true);
            item.gameObject.name = time.ToString();
            Monsters.Add(item, true);
            item.Open();
        }
        
        yield return new WaitForSecondsRealtime(10f);
        
        time = 0;
        while (time < 13.5f)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            time += 1.5f;
            var item = Instantiate(rabbit, transform, false);
            item.gameObject.SetActive(true);
            Monsters.Add(item, true);
            item.Open();
        }
        
        yield return new WaitForSecondsRealtime(8f);
        
        time = 0;
        while (time < 13.5f)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            time += 1.5f;
            var item = Instantiate(armature, transform, false);
            item.gameObject.SetActive(true);
            Monsters.Add(item, true);
            item.Open();
        }
        
        yield return new WaitForSecondsRealtime(13f);

        time = 0;
        while (time < 1f)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            time += 1.5f;
            var item = Instantiate(succubus, transform, false);
            item.gameObject.SetActive(true);
            Monsters.Add(item, true);
            item.Open();
        }

        yield return new WaitForSecondsRealtime(15f);
        time = 0;
        while (time < 1f)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            time += 1.5f;
            var item = Instantiate(MalphiteMonster, transform, false);
            item.gameObject.SetActive(true);
            Monsters.Add(item, true);
            item.Open();
        }


        is_GameOver = true;
    }
}