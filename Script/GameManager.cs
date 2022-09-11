using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// game manager
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public Text Hero;
    public Image HeroHP;

    public Text City;
    public Image CityHP;

    public Text Money;
    public GameObject GameVictory;
    public GameObject GameFail;

    public GameObject Player;
    public Hero _Hero;
    public Transform TowerBase;

    public GameObject BuildUI;
    public Button btn_Tower1;
    public Button btn_Tower2;


    public GameObject Tower1;
    public GameObject Tower2;

    private GameObject currBuildTarget; //current building

    [HideInInspector] public int money = 5;

    private void Awake()
    {
        money = 5;
        btn_Tower1.onClick.AddListener(BuildTower1);
        btn_Tower2.onClick.AddListener(BuildTower2);
    }

    public void SetHeroHP(float currHP, float MaxHP)
    {
        HeroHP.fillAmount = currHP / MaxHP;
    }

    public void SetCityHP(float currHP, float MaxHP)
    {
        CityHP.fillAmount = currHP / MaxHP;
    }


    /// <summary>
    /// gate damage
    /// </summary>
    public void GateReceivedDamage() => Gate.Instance.ReceivedDamage();

    public void ToBuildTower(GameObject towerBase, Vector2 mousePosstion)
    {
        if (currBuildTarget != null && currBuildTarget == towerBase)
        {
            currBuildTarget = null;
            BuildUI.SetActive(false);
            return;
        }

        currBuildTarget = towerBase;
        BuildUI.transform.localPosition = mousePosstion;
        BuildUI.SetActive(true);
    }

    /// <summary>
    /// tower 1
    /// </summary>
    public void BuildTower1()
    {
        if (money < 5)
        {
            Debug.LogError("money is not enough");
            return;
        }

        money -= 5;
        BuildTower(Tower1);
    }

    /// <summary>
    /// tower 2
    /// </summary>
    public void BuildTower2()
    {
        if (money < 3)
        {
            Debug.LogError("money is not enough");
            return;
        }

        money -= 3;
        BuildTower(Tower2);
    }

    public void BuildTower(GameObject tower)
    {
        var item = Instantiate(tower, TowerBase, false);
        item.transform.position = currBuildTarget.transform.position;
        item.transform.localScale = Vector3.one * 4.8f;
        Destroy(currBuildTarget);
        BuildUI.SetActive(false);
        currBuildTarget = null;
    }

    private void Update()
    {
        Money.text = "Currency: " + money;
        HeroHP.transform.parent.LookAt(Player.transform);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    //射线不会透过UI执行其他的操作
                    return;
                }

                if (hit.collider.gameObject.CompareTag("Tower"))
                {
                    //build tower
                    float Y = Input.mousePosition.y - (Screen.height / 2);
                    float X = Input.mousePosition.x - (Screen.width / 2);
                    Vector2 mousePosstion = new Vector2(X, Y);
                    ToBuildTower(hit.collider.gameObject, mousePosstion);
                }
            }
        }
    }

    /// <summary>
    /// game win
    /// </summary>
    public void GameOverVic()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    /// <summary>
    /// game losed
    /// </summary>
    public void GameOverGameFail()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}