using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyNpc : MonoBehaviour
{
    // Start is called before the first frame update
    public Topic topic;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer shadowSprite;
    [SerializeField] BoxCollider2D boxCol;
    [SerializeField] private Animator anim;

    [Header("Wait time in seconds")]
    [SerializeField] private float waitTime;
    [SerializeField] private float waitTimeIfKilled;
    [SerializeField] private float waitTimeIfNo;

    public bool isHidden;
    public bool isKilled;
   
    public void HideEnemyNpc()
    {
        sprite.DOFade(0, .5f).SetEase(Ease.Linear);
        shadowSprite.DOFade(0, .5f).SetEase(Ease.Linear);
        boxCol.enabled = false;
        anim.enabled = false;
        isHidden = true;
    }

    public void ShowEnemyNpc()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(waitTime);
        sprite.DOFade(1f, .5f).SetEase(Ease.Linear);
        shadowSprite.DOFade(1f, .5f).SetEase(Ease.Linear);
        boxCol.enabled = true;
        anim.enabled = true;
        isHidden = false;

        if (isKilled)
        {
            isKilled = false;
        }
    }

    public void RespawnNoDelay()
    {
        sprite.DOFade(1f, .5f).SetEase(Ease.Linear);
        shadowSprite.DOFade(1f, .5f).SetEase(Ease.Linear);
        boxCol.enabled = true;
        anim.enabled = true;
        isHidden = false;

        if (isKilled)
        {
            isKilled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            // call ui disable player movement
            var mapScreen = SceneManager.Instance.GetSceneByType<MapScreen>();           
            var mapUi = SceneManager.Instance.GetSceneByType<MapUiScreen>();
            var player = mapScreen.Player;

            player.UnSetup();
            mapUi.ConFirmBattle.ShowPanel();
            mapScreen.CurrentEnemyNpc = this;

        }
    }

    public void SetWaitTime(bool isKilled)
    {
        if (isKilled)
        {
            waitTime = waitTimeIfKilled;
        }
        else
        {
            waitTime = waitTimeIfNo;
        }
    }
}
