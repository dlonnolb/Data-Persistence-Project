using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public MainManager Manager;
    private AudioSource sonidos;
    [SerializeField] private AudioClip pierdes;

    private void Start()
    {
        sonidos = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        sonidos.PlayOneShot(pierdes);
        Manager.GameOver();
    }
}
