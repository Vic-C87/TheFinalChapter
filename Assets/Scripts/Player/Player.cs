using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField]
    float myMaxHP;
    float myCurrentHP;
    [SerializeField]
    float mySpeed;
    [SerializeField]
    float myDamage;
    [SerializeField]
    float myMeleeDistance;
    [SerializeField]
    float myProjectileDamage;

    CircleCollider2D myCollider;
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

    void Start()
    {
        myCollider = GetComponent<CircleCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCurrentHP = myMaxHP;
        myHUDManager = GameManager.myInstance.GetHUDManager();
        //myHUDManager.UpdateHealthBar();
        myWeaponActions = GetComponent<WeaponActions>();
        //myParticleSystem = this.gameObject.GetComponentInChildren<ParticleSystem>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public CircleCollider2D GetPlayerCollider()
    {
        return myCollider;
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
        }
        else if (myAimDirection.x < 0)
        {
            myWeaponActions.TurnLeft();
        }
    }

    void Shoot()
    {
        if (myAimDirection != Vector2.zero)
        {
            GameObject projectile = Instantiate<GameObject>(myProjectile, transform.position + (Vector3)myAimDirection, Quaternion.identity, GameManager.myInstance.transform);
            projectile.GetComponent<PlayerProjectile>().SetDirection(myAimDirection, myProjectileDamage);
        }
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
        if (aCallbackContext.phase == InputActionPhase.Started)
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
}
