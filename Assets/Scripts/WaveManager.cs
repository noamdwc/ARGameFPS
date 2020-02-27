using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{

    public Vector3 spawnPopint;

    public GameObject[] waves;

    private GameObject enemiesInstance;

    private bool isPlaying = false;

    private bool isStared = false;

    private bool isStopShooting = false;

    private int currentWave = -1;

    private Text waveText;

    public int sec2FadeAway;

    private bool isShowingWave;

    private float startTime;

    private bool isToched = false;

    private void Start()
    {
        waveText = GameObject.Find("Wave Number").GetComponent<Text>();
        waveText.enabled = false;
        SpawnEnemyGroup(Vector3.zero);
    }

    public void SpawnEnemyGroup(Vector3 sp) {
        spawnPopint = sp;
        nextWave(true);
        isPlaying = true;
        isStared = true;    
    }

    // Update is called once per frame
    void Update()
    {
        if (isShowingWave)
        {
            if (Time.time > startTime + sec2FadeAway)
                isShowingWave = false;
        }
        else
        {
            if (waveText.enabled)
            {
                waveText.enabled = false;
                loadWave();
            }
            waveFlow();
        }
    }

    private void waveFlow()
    {
        if (isPlaying)
        {
            Debug.Log(enemiesInstance.transform.childCount);
            if (enemiesInstance.transform.childCount <= 0)
            {
                nextWave();
            }
        }
        else if (isStared)
        {
            if (!TryGetTouchPosition(out Vector2 temp))
                isStopShooting = true;
            if (TryGetTouchPosition(out Vector2 touchPosition) && isStopShooting)
            {
                newGame();
            }
        }
    }

    private void gameOver()
    {
        waveText.enabled = true;
        waveText.text = $"You won\n" +  $"touch the screen to play again";
        isPlaying = false;
    }

    private void newGame()
    {
        waveText.enabled = false;
        ScoreClass.instance.reset();
        nextWave();
        isPlaying = true;
        isStopShooting = false;
    }


    private void nextWave(bool first = false)
    {
        if (first)
        {
            annoncedWave();
        }
        else
        {
            Destroy(enemiesInstance);
            currentWave = currentWave + 1;
            if (currentWave >= waves.Length)
            {
                currentWave = 0;
                gameOver();
            }
            else
                annoncedWave();
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

    private void annoncedWave()
    {
        isShowingWave = true;
        waveText.text = $"wave {currentWave + 1}";
        waveText.enabled = true;
        startTime = Time.time;
        //waveText.enabled = false;
    }

    private void loadWave()
    {
        var enemies2Copy = waves[currentWave];
        enemiesInstance = Instantiate(enemies2Copy, spawnPopint, enemies2Copy.transform.rotation);
    }

}
