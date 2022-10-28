using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField]
    float myMaxHP;
    [SerializeField]
    float myCurrentHP;
    [SerializeField]
    float mySpeed;
    [SerializeField]
    float myDamage;
    [SerializeField]
    float myMeleeDistance;
    [SerializeField]
    float myProjectileDamage;

    public AudioClip Walk;

    CapsuleCollider2D myCollider;
    Rigidbody2D myRigidbody;

    Vector2 myAimDirection;
    Vector2 myMoveDirection;

    bool myIsMoving = false;
    [SerializeField]
    bool myRangedWeaponActivated = false;

    [SerializeField]
    GameObject myProjectile;
    [SerializeField]
    LayerMask myEnemyLayerMask;
    HUDManager myHUDManager;
    [SerializeField]
    ParticleSystem myParticleSystem;

    [SerializeField]
    WeaponActions myWeaponActions;

    [SerializeField]
    float myDashSpeed;
    [SerializeField]
    float myDashTime;
    float myDashTimeStamp;

    bool myIsDashStarted = false;

    Weapon myCurrentClosestWeapon = null;
    SpriteRenderer mySpriteRenderer;

    [SerializeField]
    SpawnManager mySpawnManager;

    bool myIsZone2Activated = false;
    bool myIsZone3Activated = false;

    BookManager myBookManager;

    bool myIsLevel1 = false;
    bool myIsLevel2 = false;
    bool myIsLevel3 = false;

    AudioManager myAudioManager;

    void Start()
    {
        myAudioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCurrentHP = myMaxHP;
        myHUDManager = GameManager.myInstance.GetHUDManager();
        myWeaponActions = GetComponent<WeaponActions>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myBookManager = FindObjectOfType<BookManager>();
        mySpawnManager = FindObjectOfType<SpawnManager>();
        myIsLevel1 = true;
        DontDestroyOnLoad(this.gameObject);
        myHUDManager.UpdateHealthBar();
    }

    void Update()
    {
        CheckDashTime();
    }

    void FixedUpdate()
    {
        Move();
    }

    public CapsuleCollider2D GetPlayerCollider()
    {
        return myCollider;
    }

    public float GetCurrentHp()
    {
        return myCurrentHP;
    }

    public float GetSpeed()
    {
        return mySpeed;
    }

    public float GetDamage()
    {
        return myDamage;
    }

    public float GetMeleeDistance()
    {
        return myMeleeDistance;
    }

    public float GetProjectileDamage()
    {
        return myProjectileDamage;
    }

    public Sprite GetKnifeSprite()
    {
        return myWeaponActions.myKnife;
    }

    public Sprite GetBowSprite()
    {
        return myWeaponActions.myBow;
    }

    public GameObject GetProjectile()
    {
        return myProjectile;
    }

    public void TakeDamage(float anAmount)
    {
        myCurrentHP -= anAmount;
        myHUDManager.UpdateHealthBar();
        myParticleSystem.Play();
        if (myCurrentHP <= 0)
        {
            myCurrentHP = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died...");
        Time.timeScale = 0;
    }

    void Move()
    {
        if (myIsMoving)
            myRigidbody.velocity = myMoveDirection * mySpeed;

        if (myAimDirection.x > 0)
        {

            myWeaponActions.TurnRight();
            mySpriteRenderer.flipX = false;
        }
        else if (myAimDirection.x < 0)
        {
            myWeaponActions.TurnLeft();
            mySpriteRenderer.flipX = true;
        }

        myAudioManager.ActivateSound(AudioManager.ESoundNames.Walk);
    }

    void Shoot()
    {
        if (myAimDirection != Vector2.zero)
        {
            GameObject projectile = Instantiate<GameObject>(myProjectile, transform.position + (Vector3)myAimDirection, Quaternion.identity, GameManager.myInstance.transform);
            projectile.GetComponent<PlayerProjectile>().SetDirection(myAimDirection, myProjectileDamage);
        }
        myAudioManager.ActivateSound(AudioManager.ESoundNames.Range);

    }

    void Hit()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, myAimDirection, myMeleeDistance, myEnemyLayerMask);
        Debug.DrawRay(transform.position, myAimDirection, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(myDamage);
            }
        }
        myWeaponActions.StartAttack();
        myAudioManager.ActivateSound(AudioManager.ESoundNames.Hit);

    }

    public void GetMovementInput(InputAction.CallbackContext aCallbackContext)
    {
        if (aCallbackContext.phase == InputActionPhase.Performed)
        {
            if (myMoveDirection != aCallbackContext.ReadValue<Vector2>().normalized)
            {
                myMoveDirection = aCallbackContext.ReadValue<Vector2>().normalized;
            }
            myIsMoving = true;
        }

        if (aCallbackContext.phase == InputActionPhase.Canceled)
        {
            myIsMoving = false;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    public void GetAimInput(InputAction.CallbackContext aCallbackContext)
    {
        if (aCallbackContext.phase == InputActionPhase.Performed)
        {
            myAimDirection = aCallbackContext.ReadValue<Vector2>().normalized;
        }
    }

    public void GetWeaponSwitchInput(InputAction.CallbackContext aCallbackContext)
    {
        if (aCallbackContext.phase == InputActionPhase.Performed)
        {
            myRangedWeaponActivated = !myRangedWeaponActivated;
            myHUDManager.UpdateWeaponSlots();
            if (myRangedWeaponActivated)
            {
                myWeaponActions.BowActiveWeapon();
            }
            else
            {
                myWeaponActions.KnifeActiveWeapon();
            }
        }
    }

    public void GetAttackInput(InputAction.CallbackContext aCallbackContext)
    {
        if (aCallbackContext.phase == InputActionPhase.Performed)
        {
            if (myRangedWeaponActivated)
            {
                Shoot();
            }
            else
            {
                Hit();
            }
        }
    }

    public void GetDashInput(InputAction.CallbackContext aCallbackContext)
    {
        if (aCallbackContext.phase == InputActionPhase.Started)
        {
            if (!myIsDashStarted)
            {
                myDashTimeStamp = Time.realtimeSinceStartup;
                Debug.Log("Dashed");
                mySpeed *= myDashSpeed;
                myIsDashStarted = true;
            }
        }
    }

    public void GetPickUpInput(InputAction.CallbackContext aCallbackContext)
    {
        if (aCallbackContext.phase == InputActionPhase.Performed)
        {
            if (myCurrentClosestWeapon != null)
            {
                PickUpWeapon(myCurrentClosestWeapon.PickUpWeapon());
                myHUDManager.UpdateWeaponSlots();
            }
        }
    }

    void CheckDashTime()
    {
        if (myDashTime < Time.realtimeSinceStartup - myDashTimeStamp && myIsDashStarted)
        {
            Debug.Log("Stopped");
            mySpeed /= myDashSpeed;
            myIsDashStarted = false;
        }
    }

    public void PickUpWeapon(SWeapon aWeapon)
    {
        if (aWeapon.myIsRanged)
        {
            myProjectileDamage = aWeapon.myDamage;
            myWeaponActions.SetRangedSprite(aWeapon.mySprite);
            myHUDManager.SetNewRangedWeaponSprite(aWeapon.mySprite);
            myRangedWeaponActivated = true;
            //SetProjectileSprite
        }
        else
        {
            myDamage = aWeapon.myDamage;
            myMeleeDistance = aWeapon.myMeleeDistance;
            myWeaponActions.SetMeleeSprite(aWeapon.mySprite);
            myHUDManager.SetNewMeleeSprite(aWeapon.mySprite);
            myRangedWeaponActivated = false;
        }
    }

    public bool GetIsRangedWeaponActivated()
    {
        return myRangedWeaponActivated;
    }

    public float GetMaxHP()
    {
        return myMaxHP;
    }

    public float GetCurrentHP()
    {
        return myCurrentHP;
    }

    void SetCurrentClosestWeaponToPickUp(Weapon aWeaponObject)
    {
        if (myCurrentClosestWeapon != null)
        {
            myCurrentClosestWeapon.ActivatePickUpText(false);
        }

        myCurrentClosestWeapon = aWeaponObject;
        myCurrentClosestWeapon.ActivatePickUpText(true);
    }

    void DeActivatePickUpText()
    {
        myCurrentClosestWeapon.ActivatePickUpText(false);
        myCurrentClosestWeapon = null;
    }

    public void SetLevel(int aLevelID)
    {
        if (aLevelID == 2)
        {
            myIsLevel2 = true;
            myIsLevel1 = false;
            myIsLevel3 = false;
            SetLevel2SpawnPoint();
        }
        else if (aLevelID == 3)
        {
            myIsLevel2 = false;
            myIsLevel1 = false;
            myIsLevel3 = true;
            SetLevel3SpawnPoint();
        }
        myIsZone2Activated = false;
        myIsZone3Activated = false;
    }

    public void SetSpawnManager()
    {
        mySpawnManager = FindObjectOfType<SpawnManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            SetCurrentClosestWeaponToPickUp(collision.GetComponent<Weapon>());
        }
        if (collision.CompareTag("Zone2") && !myIsZone2Activated)
        {
            mySpawnManager.ActivateZoneTwo();
            if (myIsLevel1)
            {
                myBookManager.Chapter1Verse3();
            }
            if (myIsLevel2)
            {
                myBookManager.Chapter2Verse2();
            }
            myIsZone2Activated = true;
        }
        if (collision.CompareTag("Zone3") && !myIsZone3Activated)
        {
            mySpawnManager.ActivateZoneThree();
            if (myIsLevel1)
            {
                myBookManager.Chapter1Verse4();
            }
            if (myIsLevel2)
            {
                myBookManager.Chapter2Verse3();
            }
            myIsZone3Activated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            DeActivatePickUpText();
        }
    }

    public void GetSpawnInput(InputAction.CallbackContext aCallbackContext)
    {
        if (aCallbackContext.phase == InputActionPhase.Performed)
        {

        }
    }

    public void Level1Complete()
    {
        myBookManager.EndLevel1();
    }

    public void SetLevel2SpawnPoint()
    {
        transform.position = new Vector3(-98, -1, 0);
    }

    public void SetLevel3SpawnPoint()
    {
        transform.position = new Vector3(-9, 2, 0);
    }
}
