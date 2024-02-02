using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArea : MonoBehaviour
{
    [SerializeField] private LayerMask enemyArea;
    [SerializeField] private LayerMask nonEnemyArea;
    [SerializeField] private Collider2D areaCollider;
    
    public void Setup()
    {
        //StartCoroutine(DelayInit());
    }

    public void Unsetup()
    {
        //gameObject.layer = LayerMask.NameToLayer("EnemyArea");
        areaCollider.enabled = false;
    }

    private IEnumerator DelayInit()
    {
        yield return new WaitForSeconds(.5f);
        //gameObject.layer = LayerMask.NameToLayer("NonEnemyArea");
        areaCollider.enabled = true;
    }
}
