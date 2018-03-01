using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballHit : MonoBehaviour {

    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Location_announcer")
        {
            DamageEnemy(collision);

            yield return DestroyThisFireball();
        }
    }

    private IEnumerator DestroyThisFireball()
    {
        GetComponent<Animator>().SetBool("isHit", true);

        FindObjectOfType<Attack>().isHit = true;

        yield return new WaitForSeconds(0.05f);

        Destroy(gameObject);
    }

    private void DamageEnemy(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<AIStats>().ManageHealth(
                FindObjectOfType<PlayerStats>().fireballAtack);
        }
    }

}
