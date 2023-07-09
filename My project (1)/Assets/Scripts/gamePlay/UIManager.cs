using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager I;

    public Text score;
    public Slider health;

    private void Awake() {
        if(!I)I=this;
    }


    // Start is called before the first frame update
    void Start()
    {
        SetScore("0","3");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetScore(string newScore, string maxScore)
    {        
        score.text = "Score: " + newScore + "/" + maxScore;
    }

    public void SetHealth(float _health){
        health.value = _health;
    }
}
