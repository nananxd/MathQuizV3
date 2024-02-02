using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerAvatarBattle : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private SpriteRenderer shadowSprite;
    private int health;
    private int damage;
    private Animator animator;
    private float speed = 20f;
    Vector3 startPos;
    private EnemyAvatarBattle enemy;
    private SpriteRenderer sprite;
    public int Health { get => health; set => health = value; }
    public int Damage { get => damage; set => damage = value; }

    public void Setup(int health,int damage)
    {
        this.health = health;
        this.damage = damage;

        healthBar.maxValue = this.health;
        healthBar.value = healthBar.maxValue;

        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        startPos = transform.position;
    }

    public void TakeDamage(int enemyDamage)
    {
        health -= enemyDamage;
        healthBar.value = health;
    }

    public void DamageEnemy()//use in animation events
    {
        enemy.TakeDamage(damage);
        //camera shake
        GameManager.Instance.ShakeCamera();
        SoundManager.Instance.PlaySound("PlayerAttack");
    }

    public void Attack()
    {
        //trigger animation
        if (animator == null)
        {
            return;
        }

        animator.SetTrigger("attack");
    }

    public bool IsDead()
    {
        return health <= 0;
    }
    public void SetEnemyReference(EnemyAvatarBattle enemyAvatarBattle)
    {
        enemy = enemyAvatarBattle;
    }
    public void MoveToEnemy(Transform enemyTransform)
    {
        StartCoroutine(MoveToEnemyCoroutine(enemyTransform));
    }

    private IEnumerator MoveToEnemyCoroutine(Transform enemyTransform)
    {
        float distance = Vector3.Distance(transform.position, enemyTransform.position);
        sprite.DOFade(0, .1f).SetEase(Ease.OutFlash);
        shadowSprite.DOFade(0, 1f).SetEase(Ease.OutFlash);
        while (distance > 2.5f)
        {
            float t = speed * Time.deltaTime / distance;
            //transform.position = Vector3.Lerp(transform.position,enemyTransform.position,t);
            Vector3 lerpPos = new Vector3(Mathf.Lerp(transform.position.x, enemyTransform.position.x, t), transform.position.y, transform.position.z);
            transform.position = lerpPos;
            distance = Vector3.Distance(transform.position, enemyTransform.position);
            yield return null;
        }
        sprite.DOFade(1f, .1f).SetEase(Ease.OutFlash);
        shadowSprite.DOFade(1f, 1f).SetEase(Ease.OutFlash);
        Attack();
        yield return new WaitForSeconds(1f);
        transform.position = startPos;
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        float screenPercentage = -13f;
        float screenHeight = Camera.main.orthographicSize * 2f;

        // Calculate the position based on the percentage of the screen
        float targetY = (screenPercentage / 100f) * screenHeight;

        // Set the player's position
        transform.position = new Vector3(transform.position.x, transform.position.y + targetY, transform.position.z);
    }
}
