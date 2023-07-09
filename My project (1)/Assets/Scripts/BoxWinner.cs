using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWinner : MonoBehaviour
{
   public PauseMenu _ScriptPuseMenu;

   private void OnTriggerEnter(Collider other) {
      if (other.gameObject.tag == "Player")
        {
            _ScriptPuseMenu.SetWinOrGameOver(true);
           
        }
   }
   
}
