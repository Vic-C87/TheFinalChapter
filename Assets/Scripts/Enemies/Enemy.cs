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

    protected CapsuleCollider2D myCapsulecollider;

    [SerializeField]
    ParticleSystem myParticleSystem;

    AudioSource myHurtSound;

    protected virtual void Start()
    {
        myCurrentHP = myCurrentHP == 0 ? myMaxHP : myCurrentHP;
        myPlayer = GameManager.myInstance.GetPlayer();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myCapsulecollider = GetComponent<CapsuleCollider2D>();
        myHurtSound = GetComponentInChildren<AudioSource>();
    }

    protected virtual void Update()
    {
        if (myPlayer.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public virtual void TakeDamage(float someDamage)
    {
        myCurrentHP -= someDamage;
        myParticleSystem.Play();
        myHurtSound.Play();
        if (myCurrentHP <= 0)
        {
            myCurrentHP = 0;
            Debug.Log(myType + " died");
            GameManager.myInstance.myLevelManager.RemoveEnemyFromList();
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
        GameManager.myInstance.DropHealth(transform.position);
        this.gameObject.SetActive(false);
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
