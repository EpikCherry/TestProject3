using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public Transform ballLock;
    public bool nextSwawn = true;
    public Basket downBasket;
    private bool pointGet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball ball = collision.GetComponent<Ball>();

        if (ball != null)
        {
            PlayerController.instant.basket = this;
            PlayerController.enabled = true;
            ball.transform.SetParent(transform);
            ball.rb2d.gravityScale = 0;
            ball.rb2d.freezeRotation = true;
            ball.rb2d.velocity = Vector2.zero;
            ball.transform.position = ballLock.position;

            GameManager.instant.audioManager.BasketSound();

            if (!pointGet)
            {
                GameManager.ScoreAmount++;
                pointGet = true;
            }

            if(nextSwawn)
            {
                GameManager.instant.SpawnBasket(this);
                Destroy(downBasket.gameObject);
                nextSwawn = false;
            }
        }        
    }
}
