using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{

    //TODO: Random shooting.

    private ParticleSystem gun1;
    private ParticleSystem gun2;
    public float rate = 2f;
    private float nextTimeToFire = 0f;

    private PlayerHealth player;

    public float damage = 10f;

    // Start is called before the first frame update
    void Start()
    {
        gun1 = GetComponentsInChildren<ParticleSystem>()[0];
        gun2 = GetComponentsInChildren<ParticleSystem>()[1];
        player = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTimeToFire)
        {
            // Text text = GameObject.Find("DebugText").GetComponent<Text>();
            // if (text != null) {
            //     text.text = "SHOT!";
            // }
            nextTimeToFire = Time.time + rate;
            Shoot();

            // Debug.Log($"Time: {Time.time}");
            // Debug.Log($"Next: {nextTimeToFire}");
        }
    }

    private void Shoot()
    {
        gun1.Play();
        gun2.Play();
        AudioManager.instance.Play("jet");
        player.TakeDamage(damage);
    }
}
