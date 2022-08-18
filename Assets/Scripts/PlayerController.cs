using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instant;
    public new static bool enabled;

    [SerializeField, Range(0,2)] private float maxForce = 1;    
    [SerializeField] private float forceFactor;
    [SerializeField] private GameObject trajectoryDot;
    [SerializeField] private int number;    
    [SerializeField] private Ball ball;    

    public Basket basket;

    private GameObject[] trajectoryDots;
    private Vector2 startPos;
    private Vector2 endPos;
    private Rigidbody2D rb;
    private Vector2 forceAtPlayer;
    private Camera mainCam;

    void Start()
    {
        instant = this;
        mainCam = Camera.main;
        rb = ball.GetComponent<Rigidbody2D>();
        trajectoryDots = new GameObject[number];
    }

    // Можно переделать под LineRenderer для оптимизации, 
    // и переписать расчёт физической траектории для преград 

    void Update()
    {
        if (!enabled) return;

        if (Input.GetMouseButtonDown(0))
        {
            startPos = basket.transform.position;
            for (int i = 0; i < number; i++)
            {
                trajectoryDots[i] = Instantiate(trajectoryDot, basket.transform);
            }
        }
        if (Input.GetMouseButton(0))
        {
            endPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            Vector3 dir = Input.mousePosition - mainCam.WorldToScreenPoint(basket.transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            basket.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

            transform.position = endPos;
            forceAtPlayer = endPos - startPos;
            forceAtPlayer = Vector2.ClampMagnitude(forceAtPlayer, maxForce);

            for (int i = 0; i < number; i++)
            {
                trajectoryDots[i].transform.position = calculatePosition(i * 0.1f);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ball.rb2d.gravityScale = 1;
            ball.rb2d.freezeRotation = false;
            ball.transform.SetParent(null);            
            rb.velocity = new Vector2(-forceAtPlayer.x, -forceAtPlayer.y) * forceFactor;
            GameManager.instant.audioManager.BallSound();

            for (int i = 0; i < number; i++)
            {
                Destroy(trajectoryDots[i]);
            }
            
            enabled = false;
        }        
    }

    private Vector2 calculatePosition(float elapsedTime)
    {
        return new Vector2(startPos.x, startPos.y) +
               new Vector2(-forceAtPlayer.x, -forceAtPlayer.y) * forceFactor * elapsedTime +
               0.5f * Physics2D.gravity * elapsedTime * elapsedTime;
    }    
}
