using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float HealthPoints = 100f;
    public float Armor = 4f;

   // private Text playerHealthText;
    private Image playerHealthBar;
    // Start is called before the first frame update
    void Start()
    {
       // playerHealthText = GameObject.Find("HealthText").GetComponent<Text>();
        playerHealthBar = GameObject.Find("Health Bar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //playerHealthText.text = $"HEALTH: {(int)HealthPoints}";
        playerHealthBar.fillAmount = HealthPoints / 100;
    }

    public void TakeDamage(float damage) {
        HealthPoints = HealthPoints - damage/Armor;
        Debug.Log($"Player health: {HealthPoints}");
        if ((int)HealthPoints <= 0)
        {
            HealthPoints = 0f;
            CommitDeath();
        }
    }

    private void CommitDeath()
    {
        WaveManager2.instance.CommitDeath();
    }
}
