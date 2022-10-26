using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected string myType;
    [SerializeField]
    protected float mySpeed;
    [SerializeField]
    protected float myCurrentHP;
    [SerializeField]
    protected float myMaxHP;
    [SerializeField]
    protected float myDamage;
    [SerializeField]
    protected float myAttackRate;

    protected float myTimeSinceLastAttack = 0;

    [SerializeField]
    protected Player myPlayer;

    protected SpriteRenderer mySpriteRenderer;

    protected CircleCollider2D myCircleCollider;

    protected virtual void Start()
    {
        myCurrentHP = myCurrentHP == 0 ? myMaxHP : myCurrentHP;
        myPlayer = GameManager.myInstance.GetPlayer();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myCircleCollider = GetComponent<CircleCollider2D>();
    }

    protected virtual void Update()
    {
        if (myPlayer.transform.position.x < transform.position.x)
        {
            mySpriteRenderer.flipX = false;
        }
        else
        {
            mySpriteRenderer.flipX = true;
        }
    }

    public virtual void TakeDamage(float someDamage)
    {
        myCurrentHP -= someDamage;
        if (myCurrentHP <= 0)
        {
            myCurrentHP = 0;
            Die();
        }
    }

    public virtual void Heal(float anAmount)
    {
        myCurrentHP = myCurrentHP + anAmount > myMaxHP ? myMaxHP: myCurrentHP + anAmount;
    }

    protected virtual void Attack()
    {
        
    }

    protected virtual void Move()
    {

    }

    protected virtual void Die()
    {
        Debug.Log(myType + " died");
        Destroy(this.gameObject);
    }

    protected bool CheckAttack()
    {
        if (myAttackRate < Time.realtimeSinceStartup - myTimeSinceLastAttack)
        {
            myTimeSinceLastAttack = Time.realtimeSinceStartup;
            return true;
        }

        return false;
    }
}
