using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITaunt : MonoBehaviour {

    private AIStats aiStats;
    private RandomMovement aiMovement;
    private SpriteRenderer alertSprite;
    
    private void Start()
    {
        aiStats = gameObject.transform.parent.gameObject.GetComponentInChildren<AIStats>();

        if (!aiStats.isAggressive)
        {
            Destroy(gameObject);
        }
        else
        {
            aiMovement = gameObject.GetComponentInParent<RandomMovement>();
            alertSprite = GetComponent<SpriteRenderer>();
        }    
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger && !aiMovement.isAttacking)
        {
            aiMovement.isAttacking = true;
            yield return ShowAlert();
        }
    }

    private IEnumerator ShowAlert()
    {
        alertSprite.enabled = true;

        yield return new WaitForSeconds(1f);

        alertSprite.enabled = false;
    }

}
