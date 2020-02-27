using System;
using System.Collections;   
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffensiveWeapon : MonoBehaviour
{

    public int damage = 10;
    public float range = 100f;

    public float fireRate = 15f;

    private float nextTimeToFire = 0f;

    private ParticleSystem muzzleFlash;

    public GameObject ImpactEffect;

    private Camera fpsCam;

    private bool soundFlag;
    // Start is called before the first frame update
    void Start()
    {
        fpsCam = GetComponentInParent<Camera>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (WaveManager2.instance.currentlyPlaying) {
            if (TryGetTouchPosition(out Vector2 touchPosition) && Time.time >= nextTimeToFire)
            {
                // Text text = GameObject.Find("DebugText").GetComponent<Text>();
                // if (text != null) {
                //     text.text = "SHOT!";
                // }
                nextTimeToFire = Time.time + 1f/fireRate;
                Shoot();
            }
        }
    }
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            touchPosition = new Vector2(mousePosition.x, mousePosition.y);
            return true;
        }
#else
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
#endif

        touchPosition = default;
        return false;
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        if(soundFlag)
            AudioManager.instance.Play("shote1");
        else
            AudioManager.instance.Play("shote2");
        soundFlag = !soundFlag;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.TakeDamage(damage);
                // Text text = GameObject.Find("DebugText").GetComponent<Text>();                
                // if (text != null) {
                //     // text.text = "GAVE DAMAGE";
                // }
                Debug.Log(enemy.HealthPoints);
                Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                //This particle automatically destroys itself.
            }
        }
    }
}
