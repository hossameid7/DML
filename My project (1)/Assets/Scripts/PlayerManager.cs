using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    InputManager inputManager;
    PlayerLocomotion playerLocomotion;
    GrapplingGun grapplingGun;
    public UnityEngine.UI.Image adviceBox;
    public Text movementAdvice, jumpingAdvice, grapplingAdvice;

    public bool isInteracting;
    public bool firstTimeMoved = false;
    public bool firstTimeJumped = false;
    public bool passedGrappleTrigger = false;
    public bool firstTimeGrappled = false;

    [Header("Health")]
    public float MyHealth;
    public UnityEngine.UI.Image BoxHealth;

    public GameObject[] AllZombe;
    public Transform UnitZombe;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        grapplingGun = GetComponentInChildren<GrapplingGun>();

    }
    private void Start() {
        InvokeRepeating("FindEnemy",0.5f,1);
    }
    private void Update()
    {
        inputManager.HandleAllInputs();

        //UI
        BoxHealth.fillAmount = MyHealth /100f;
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }
    void FindEnemy()
    {
        
        AllZombe = GameObject.FindGameObjectsWithTag("ThisZombe");
        float closestDistance = Mathf.Infinity;
        GameObject NearestObject = null;
        foreach (GameObject solider in AllZombe)
        {
            float distance = Vector3.Distance(transform.position, solider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                NearestObject = solider;
            }

        }
        if (NearestObject != null && closestDistance <= 2.5f)
        {
        UnitZombe = NearestObject.transform;
        
           
        }
        else
        {
        UnitZombe = null;
 
        }


    }
    private void LateUpdate()
    {
        isInteracting = animator.GetBool("isInteracting");
        playerLocomotion.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerLocomotion.isGrounded);
        grapplingGun.DrawGrapplingGunRope();
        if(Input.GetButton("Fire1"))
        {
            animator.SetTrigger("_Atack");
        }


    }

    public void HandleAdvices()
    {
        if (firstTimeMoved && firstTimeJumped && firstTimeGrappled) return;

        if (firstTimeMoved) Destroy(movementAdvice);

        if (firstTimeJumped) Destroy(jumpingAdvice);

        if (firstTimeJumped && firstTimeMoved && !passedGrappleTrigger) adviceBox.enabled = false;

        if (passedGrappleTrigger)
        {
            adviceBox.enabled = true;
            grapplingAdvice.enabled = true;
        }

        if (passedGrappleTrigger && firstTimeGrappled) 
        {
            Destroy(grapplingAdvice);
            Destroy(adviceBox);
        }
    }

    public void _DamgeHealth(float Damge)
    {
        MyHealth -= Damge;
        if(MyHealth <=0){
            // When a player dies !
            animator.SetTrigger("_Death");
            PauseMenu._this.SetWinOrGameOver(false);

        }

    }
    public void AttackOne()
    {
      if(UnitZombe != null){

            Enemy_Zombie UnitScriptZombe= UnitZombe.GetComponent<Enemy_Zombie>();

            if(PauseMenu._this.NamberDifficulty == 0)
            {
                UnitScriptZombe.DamgeHealthEnemy (33.35f);

            } else if(PauseMenu._this.NamberDifficulty == 1)
            {
                UnitScriptZombe.DamgeHealthEnemy (25f);


            }
            else if(PauseMenu._this.NamberDifficulty == 2)
            {
                
                UnitScriptZombe.DamgeHealthEnemy (15f);


            }
        
    }
    }


}
