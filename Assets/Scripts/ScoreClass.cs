using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreClass : MonoBehaviour
{
    static public ScoreClass instance;

    private int _score = 0;
    public int score { get { return _score; } }

    private Text textMesh;

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

    private void Start()
    {
        textMesh = GameObject.Find("Score Text").GetComponent<Text>();
    }

    public void add(int p)
    {
        _score += p;
        updateText();
    }

    public void reset()
    {
        _score = 0;
        updateText();
    }

    private void updateText()
    {
        textMesh.text = "score: " + _score;
    }

}
