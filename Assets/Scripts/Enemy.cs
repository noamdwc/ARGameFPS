using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int HealthPoints = 100;
    public int Score = 25;
    public int Armor = 2;

    private Camera playerCamera;

    public bool shouldLookAtPlayer = true;

    void Start() {
        playerCamera = FindObjectOfType<Camera>();
    }

    void Update() {
        if (shouldLookAtPlayer) {
            //Slowly look at the player
            Vector3 relativePos = playerCamera.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp( transform.rotation, toRotation, 1 * Time.deltaTime );
        }
    }

    public void TakeDamage(int damage)
    {
        // Text text = GameObject.Find("DebugText").GetComponent<Text>();
        // if (text != null) {
        //     text.text = $"DAMAGING";
        // }
        HealthPoints = HealthPoints - damage/Armor;
        // AudioManager.instance.Play("damge");
        if (HealthPoints <= 0)
        {
            CommitDeath();
        }
    }

    void CommitDeath()
    {
        ScoreClass.instance.add(Score);
        AudioManager.instance.Play("boom");
        Destroy(gameObject);
    }

}
