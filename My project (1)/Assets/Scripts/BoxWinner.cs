using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWinner : MonoBehaviour
{
   public PauseMenu _ScriptPuseMenu;
   public Player player;


    private void OnTriggerEnter(Collider other) {
      if (other.gameObject.tag == "Player")
        {
            if (player.playerData.score == 4)
                _ScriptPuseMenu.SetWinOrGameOver(true);
           
        }
   }
   
}
