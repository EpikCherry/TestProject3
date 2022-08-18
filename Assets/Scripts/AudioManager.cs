using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource click;
    [SerializeField] private AudioSource bascket;
    [SerializeField] private AudioSource ball;   
        
    public void ClickSound()
    {
        click.Play();
    }
    public void BasketSound()
    {
        bascket.Play();
    }
    public void BallSound()
    {
        ball.Play();
    }
}
