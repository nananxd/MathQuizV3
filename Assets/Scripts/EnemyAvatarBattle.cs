using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyAvatarBattle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer shadowSprite;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI enemyNameTxt;
    [SerializeField] private float screenPercentage;
    private string currentQuestion;
    private string answer;
    private string hint;
    private List<string> steps;
    private string enemyName;
    private Dialouge dialogue;
    private QuestionsData currentQuestionData;

    private int health;
    private int damage;
    private string nameOfEnemy;
    private Animator animator;
    private Vector3 startPos;
    private float speed = 20f;
    private PlayerAvatarBattle player;
    private SpriteRenderer sprite;
    public int Damage { get => damage; set => damage = value;}
    public int Health { get => health; set => health = value;}
   

    #region Old setup code
    //public void Setup(EnemyData enemyData,QuestionsData question)
    //{
    //    spriteRenderer.sprite = enemyData.enemySprite;
    //    enemyName = enemyData.enemyName;
    //    dialogue = enemyData.startingDialogue;

    //    currentQuestionData = question;
    //    currentQuestion = currentQuestionData.questions.question;
    //    answer = currentQuestionData.questions.answer;
    //    hint = currentQuestionData.questions.hint;
    //    steps = currentQuestionData.questions.steps;
    //}
    #endregion

    public void Setup(int health,int damage,string nameOfEnemy)
    {
        this.health = health;
        this.damage = damage;
        this.nameOfEnemy = nameOfEnemy;

        //enemyNameTxt.text = this.nameOfEnemy;
        healthBar.maxValue = this.health;
        healthBar.value = healthBar.maxValue;

        animator = GetComponent<Animator>();
        startPos = transform.position;
        sprite = GetComponent<SpriteRenderer>();
    }
    public void UnSetup()
    {
        #region
        //var screen = SceneManager.Instance.GetSceneByType<DialougeManager>();
        //spriteRenderer.sprite = null;
        //currentQuestion = "";
        //answer = "";
        //hint = "";
        //enemyName = "";
        //dialogue = null;
        //currentQuestionData = null;
        //steps.Clear();
        //screen.OnFinishDialogue -= DialogueWhenClose;
        #endregion
    }

    public void SetupForStartingDialogue()
    {
        var screen = SceneManager.Instance.GetSceneByType<DialougeManager>();
        if (dialogue.linesToSay.Count <= 0)
        {
            screen.OnFinishDialogue += DialogueWhenClose;
            screen.OnFinishDialogue?.Invoke();
            return;
        }        
            
        
       
        screen.OnFinishDialogue += DialogueWhenClose;
        screen.Setup(dialogue);
        screen.SetDialogue(true);
        screen.Show();
    }

    private void DialogueWhenClose()
    {

    }

    public Dialouge GetEnemyDialogue()
    {
        return dialogue;
    }

    public string GetHint()
    {
        return hint;
    }

    public string GetAnswer()
    {
        return answer;
    }

    public void Attack()
    {
        if (animator == null)
        {
            return;
        }

        animator.SetTrigger("attack");
    }

    public void MoveToEnemy(Transform playerTransform)
    {
        StartCoroutine(MoveToEnemyCoroutine(playerTransform));
    }

    public void SetPlayerReference(PlayerAvatarBattle playerAvatar)
    {
        player = playerAvatar;
    }

    private IEnumerator MoveToEnemyCoroutine(Transform playerTransform)
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        sprite.DOFade(0, .2f).SetEase(Ease.OutFlash);
        shadowSprite.DOFade(0, .2f).SetEase(Ease.OutFlash);
       
        while (distance > 2f)
        {
            float t = speed * Time.deltaTime / distance;
            // transform.position = Vector3.Lerp(transform.position, playerTransform.position, t);
            Vector3 lerpPos = new Vector3(Mathf.Lerp(transform.position.x,player.transform.position.x,t),transform.position.y,transform.position.z);
            transform.position = lerpPos;
            distance = Vector3.Distance(transform.position, playerTransform.position);

            yield return null;
        }
        sprite.DOFade(1f, .1f).SetEase(Ease.OutFlash);
        shadowSprite.DOFade(1f, .2f).SetEase(Ease.OutFlash);
        Attack();
        yield return new WaitForSeconds(2f);
        transform.position = startPos;
        UpdatePosition();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.value = health;
    }

    public void DamagePlayer() //use in animation event
    {
        player.TakeDamage(damage);
        //camera shake 
        GameManager.Instance.ShakeCamera();
        SoundManager.Instance.PlaySound("EnemyAttack");
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void UpdatePosition()
    {
        
        float screenHeight = Camera.main.orthographicSize * 2f;

        // Calculate the position based on the percentage of the screen
        float targetY = (screenPercentage / 100f) * screenHeight;

        // Set the player's position
        transform.position = new Vector3(transform.position.x, transform.position.y+targetY, transform.position.z);
    }
}
