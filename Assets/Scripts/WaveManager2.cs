using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager2 : MonoBehaviour
{

    static public WaveManager2 instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    public Vector3 spawnPopint;

    public GameObject[] waves;

    private GameObject currentEnemies;

    public bool currentlyInBreak = false;

    public bool currentlyPlaying = false;

    private Text waveText;

    private int currentWaveNumber = -1;

    private float timeForBreak = 3f;

    private float waveBreakTimer = 0f;

    public PlayerHealth playerHealth;

    private bool clickedOnce = false;

    private bool isStopShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        waveText = GameObject.Find("Wave Number").GetComponent<Text>();
        waveText.enabled = false;
        //BeginWaves(Vector3.zero);
    }

    internal void CommitDeath()
    {
        currentWaveNumber = waves.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaveNumber < waves.Length)
        {


            if (currentlyInBreak && waveBreakTimer >= 0f)
            {
                if (currentWaveNumber == 0)
                {
                    waveText.text = $"Lets start small\ntap on the screen to shoot";
                } else if (currentWaveNumber == 1)
                {
                    waveText.text = $"Shoot anything that moves.\nwave {currentWaveNumber}";
                } else
                {
                    waveText.text = $"wave {currentWaveNumber}";
                }
                waveText.enabled = true;
                waveBreakTimer -= Time.deltaTime;
            }
            else if (!currentlyPlaying)
            {
                waveText.enabled = false;
                currentlyInBreak = false;
                currentlyPlaying = true;
                var enemies2Copy = waves[currentWaveNumber];
                currentEnemies = Instantiate(enemies2Copy, spawnPopint, enemies2Copy.transform.rotation);
            }

            if (currentlyPlaying)
            {
                if (currentEnemies.GetComponentsInChildren<Enemy>().Length   <= 0)
                {
                    Destroy(currentEnemies);
                    currentWaveNumber += 1;
                    currentlyPlaying = false;
                    currentlyInBreak = true;
                    waveBreakTimer = 3f;
                }
            }
        } else
        {
            if ((int)playerHealth.HealthPoints <= 0)
            {
                Destroy(currentEnemies);
                waveText.text = $"you died... click to play again!";
                waveText.enabled = true;

            }
            else
            {
                waveText.text = $"congrats! click to play again!";
                waveText.enabled = true;

            }


            if (!TryGetTouchPosition(out Vector2 temp))
                isStopShooting = true;
            if (TryGetTouchPosition(out Vector2 touchPosition) && isStopShooting)
            {
                isStopShooting = false;
                playerHealth.HealthPoints = 100;
                clickedOnce = false;
                ScoreClass.instance.reset();
                currentWaveNumber = 0;
                currentlyPlaying = false;
                currentlyInBreak = true;
                waveBreakTimer = 3f;
            }
        }
    }

    //Begin the game
    public void BeginWaves(Vector3 sp)
    {
        spawnPopint = sp;
        currentWaveNumber += 1;
        currentlyPlaying = false;
        currentlyInBreak = true;
        waveBreakTimer = 3f;
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

}
