using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballHit : MonoBehaviour {

    private Vector3 whereToAttack = Vector3.zero;
    private Rigidbody2D fireballBody;

    private void Start()
    {
        fireballBody = GetComponent<Rigidbody2D>(); //get fireball body 
        fireballBody.transform.position = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().transform.position;
        whereToAttack = Animate();
    }

    private void Update()
    {
        fireballBody.transform.position += whereToAttack * Time.deltaTime;
    }

    private Vector3 Animate()
    {
        var wherePlayerLook = GetWherePlayerLook();

        var fireballAnimator = GetComponent<Animator>();

        fireballAnimator.SetFloat("posX", wherePlayerLook.x);
        fireballAnimator.SetFloat("posY", wherePlayerLook.y);
        fireballAnimator.SetBool("isHit", false);

        return wherePlayerLook;
    }

    private Vector2 GetWherePlayerLook()
    {
        var animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        var x = animator.GetFloat("inputX");
        var y = animator.GetFloat("inputY");

        return new Vector2(x, y);
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
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

        yield return new WaitForSeconds(0.05f);

        Destroy(gameObject);
    }

    private void DamageEnemy(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<AIStats>().ManageHealth(
                FindObjectOfType<PlayerStats>().FireballAttackValue);

            collision.gameObject.transform.parent.gameObject.GetComponent<RandomMovement>().isAttacking = true;
        }
    }

}
