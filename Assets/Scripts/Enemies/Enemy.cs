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
    protected Transform myPlayer;

    protected virtual void Start()
    {
        myCurrentHP = myCurrentHP == 0 ? myMaxHP : myCurrentHP;
        myPlayer = GameManager.myInstance.GetPlayer();
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

    protected virtual float Attack()
    {
        return myDamage;
    }

    protected virtual void Move()
    {

    }

    protected virtual void Die()
    {
        Debug.Log(myType + " died");
    }
}
