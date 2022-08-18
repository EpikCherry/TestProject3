using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instant;

    private static int starAmount = 0;
    public static int StarAmount
    {
        get => starAmount;
        set
        {
            starAmount = value;
            UIManager.instant.starLabel.text = starAmount.ToString();
        }
    }

    private static int scoreAmount = 0;
    public static int ScoreAmount
    {
        get => scoreAmount;
        set
        {
            scoreAmount = value;
            UIManager.instant.scoreLabel.text = scoreAmount.ToString();
        }
    }

    public Vector2 spawnCoords;
    public float spawnYCoords;

    public GameObject basketPrefab;
    public GameObject starPregab;

    public UIManager uimanager;
    public AudioManager audioManager;

    private float yTempCoord = -1;
    private bool isleft = true;

    public void Awake()
    {
        instant = this;
        UIManager.instant = uimanager;
        StarAmount = 0;
        ScoreAmount = 0;
    }

    public void SpawnBasket(Basket basket)
    {
        yTempCoord += spawnYCoords;
        
        Basket newBasket = Instantiate(
            basketPrefab, 
            new Vector3((isleft? spawnCoords.x : spawnCoords.y) + Random.insideUnitCircle.x/2, yTempCoord + Random.insideUnitCircle.y, 0),
            Quaternion.Euler(0, 0, Random.Range(-30, 30))).GetComponent<Basket>();
        newBasket.downBasket = basket;
        isleft = !isleft;
        
        if (Random.Range(0, 2) == 0)
        {
            InteractableObject star = Instantiate(starPregab, newBasket.transform.position, Quaternion.identity).GetComponent<InteractableObject>();
            star.onTriggerEnter.AddListener(() => 
            {
                StarAmount++;
                Destroy(star.gameObject);
            });
        }
    }
}
