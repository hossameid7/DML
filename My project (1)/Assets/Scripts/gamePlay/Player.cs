using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player I;
    public PlayerData playerData = new PlayerData();
    private void Awake() {
        if(!I)I=this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreamentScore(){
        playerData.score++;
        UIManager.I.SetScore(playerData.score.ToString(),playerData.maxScore.ToString());
    }
}
