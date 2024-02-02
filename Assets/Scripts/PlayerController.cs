using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private FixedJoystick joystick;
   

    [SerializeField] private LayerMask enemyEncounterLayer;

    private Vector2 movement;
    private bool enablePlayer = true;
    private BattleScreen battleScreen;
    private void Start()
    {
        battleScreen = SceneManager.Instance.GetSceneByType<BattleScreen>();
    }
    private void Update()
    {
        //movement.x = Input.GetAxisRaw("Horizontal")/** joystick.Horizontal*/;
        //movement.y = Input.GetAxisRaw("Vertical")/* * joystick.Vertical*/;
        if (enablePlayer)
        {
            movement = Vector2.right.normalized * joystick.Horizontal + Vector2.up.normalized * joystick.Vertical;

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            // SoundManager.Instance.PlaySound("FootStep");
           

        }

        //if (!(joystick.Horizontal >= 0.01f || joystick.Horizontal <= -0.01f) && !(joystick.Vertical >= 0.01f || joystick.Vertical <= -0.01f))
        //{
        //    SoundManager.Instance.PlaySound("FootStep");
        //    Debug.Log(joystick.Horizontal);
        //}
        //else
        //{
        //    SoundManager.Instance.StopSoundImidiate("FootStep");
        //}


    }

    public void FootStepSound()
    {
        SoundManager.Instance.PlaySound("FootStep");
    }

    private void FixedUpdate()
    {
        if (enablePlayer)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

           
        }
      
    }

    private IEnumerator CooldownBeforeCheckingForEnemy()
    {
        yield return new WaitForSeconds(5f);
        battleScreen.isBattleFinished = false;
    }
    private void CheckForEnemyEncounter()
    {
        if (Physics2D.OverlapCircle(transform.position,1f,enemyEncounterLayer) != null && CheckIfPlayerIsMoving())
        {
            
            var randomNum = Random.Range(1, 101);

            if (randomNum <= 3) // to be adjusted
            {
                var mapScreen = SceneManager.Instance.GetSceneByType<MapScreen>();
                
                mapScreen.GoToBattle();
                Debug.Log("Enemy battle!!!");
            }
        }
    }

    private bool CheckIfPlayerIsMoving()
    {
        return joystick.Horizontal != 0 && joystick.Vertical != 0;
    }

    public void Setup()
    {
        enabled = true;
        enablePlayer = true;
    }

    public void UnSetup()
    {
        SoundManager.Instance.StopSoundImidiate("FootStep");
        movement = Vector2.zero;
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
        enablePlayer = false;
        enabled = false;
        
    }
}
