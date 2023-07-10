using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Zombie : MonoBehaviour
{
    public Animator _Animator;
   public NavMeshAgent MyAgent;
   public float Aeara=20;

   public GameObject TheTarget;
    public bool DoneDeit;

   [Header("Health")]
    public float MyHealth = 100;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,Aeara);   
         TheTarget= GameObject.FindGameObjectWithTag("Player");
    }

   private void Update() 
   {
        MoveToPlayer();
   }

    void MoveToPlayer()
    {
        float Distance_Target = Vector3.Distance(transform.position,TheTarget.transform.position);
        if(Distance_Target <= Aeara)
        {

            if(Distance_Target >= 2f){
                MyAgent.isStopped = false;
                MyAgent.SetDestination(TheTarget.transform.position);
                _Animator.SetBool("_MoveThat",true);
                _Animator.SetBool("_Atack",false);

            }else
            {
                MyAgent.isStopped = true;
                Debug.Log("<color=red> Attact Player </color>");

                _Animator.SetBool("_Atack",true);
                _Animator.SetBool("_MoveThat",false);

            }
        }else
        {
                _Animator.SetBool("_Atack",false);
                _Animator.SetBool("_MoveThat",false);
        }

    }

    public void DamgeHealthEnemy(float _Damge)
    {
        if(DoneDeit == false){
        MyHealth -= _Damge;
        if(MyHealth<=0)
        {
            // What Happing if This Object Deat
            DoneDeit = true;
            _Animator.SetTrigger("_Death");
            this.enabled = false;
            Player.I.IncreamentScore();
        }
        }
    }

    public void AttackOne()
    {
        if(TheTarget != null)
        {
            PlayerManager _ScriptPlayer = TheTarget.GetComponent<PlayerManager>();

            if(PauseMenu._this.NamberDifficulty == 0)
            {
                _ScriptPlayer._DamgeHealth(3.5f);

            } else if(PauseMenu._this.NamberDifficulty == 1)
            {
                _ScriptPlayer._DamgeHealth(6.8f);

            }
            else if(PauseMenu._this.NamberDifficulty == 2)
            {
                _ScriptPlayer._DamgeHealth(13f);

            }
        }
    }
}
