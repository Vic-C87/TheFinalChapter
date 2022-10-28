using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActions : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer myLeftFacingSpriteRenderer;
    [SerializeField]
    SpriteRenderer myRightFacingSpriteRenderer;

    [SerializeField]
    Transform myLeftFacingTransform;
    [SerializeField]
    Transform myRightFacingTransform;

    public Sprite myKnife;
    public Sprite myBow;

    bool myIsFacingRight = true;
    bool myMeleeActivated = false;

    bool myStartedWeponAction = false;
    [SerializeField]
    float myAttackDuration = 2f;
    [SerializeField]
    float myAttackTimeStamp;

    [SerializeField]
    float mySwingSpeed;

    Vector3 myRightFacingRotation = new Vector3(0, 0, 45);
    Vector3 myLeftFacingRotation = new Vector3(0, 0, -45);


    // Start is called before the first frame update
    void Start()
    {
        myLeftFacingTransform.gameObject.SetActive(false);
        myRightFacingTransform.gameObject.SetActive(true);
        BowActiveWeapon();
        SetOriginalRotation();
    }


    void FixedUpdate()
    {
        ActivateWeapon();
    }

    public void TurnRight()
    {
        if (!myIsFacingRight)
        {
            myLeftFacingTransform.gameObject.SetActive(false);
            myRightFacingTransform.gameObject.SetActive(true);
            myIsFacingRight = true;
            ActiveTransform().eulerAngles = myRightFacingRotation;
        }
    }

    public void TurnLeft()
    {
        if (myIsFacingRight)
        {
            myLeftFacingTransform.gameObject.SetActive(true);
            myRightFacingTransform.gameObject.SetActive(false);
            myIsFacingRight = false;
            ActiveTransform().eulerAngles = myLeftFacingRotation;
        }
    }

    public void ActivateWeapon()
    {
        if (myMeleeActivated && myStartedWeponAction)
        {
            float directionMultiplier;

            if (myIsFacingRight)
            {
                directionMultiplier = -1;
            }
            else
            {
                directionMultiplier = 1;
            }
            ActiveTransform().RotateAround(ActiveTransform().position, Vector3.forward, directionMultiplier * mySwingSpeed * Time.deltaTime);

            if (myAttackDuration < Time.realtimeSinceStartup - myAttackTimeStamp)
            {
                Debug.Log("Stop");
                myStartedWeponAction = false;
                SetOriginalRotation();
            }
        }
    }

    public void StartAttack()
    {
        myStartedWeponAction = true;
        myAttackTimeStamp = Time.realtimeSinceStartup;
        SetOriginalRotation();
    }

    Transform ActiveTransform()
    {
        if (myIsFacingRight)
        {
            return myRightFacingTransform;
        }
        return myLeftFacingTransform;
    }

    public void KnifeActiveWeapon()
    {
        myRightFacingTransform.gameObject.SetActive(true);
        myRightFacingSpriteRenderer.sprite = myKnife;
        myRightFacingTransform.gameObject.SetActive(false);

        myLeftFacingTransform.gameObject.SetActive(true);
        myLeftFacingSpriteRenderer.sprite = myKnife;
        myLeftFacingTransform.gameObject.SetActive(false);

        if (myIsFacingRight)
        {
            myRightFacingTransform.gameObject.SetActive(true);
        }
        else
        {
            myLeftFacingTransform.gameObject.SetActive(true);
        }

        myMeleeActivated = true;
        SetOriginalRotation();
    }

    public void BowActiveWeapon()
    {
        myRightFacingTransform.gameObject.SetActive(true);
        myRightFacingSpriteRenderer.sprite = myBow;
        myRightFacingTransform.gameObject.SetActive(false);

        myLeftFacingTransform.gameObject.SetActive(true);
        myLeftFacingSpriteRenderer.sprite = myBow;
        myLeftFacingTransform.gameObject.SetActive(false);

        if (myIsFacingRight)
        {
            myRightFacingTransform.gameObject.SetActive(true);
        }
        else
        {
            myLeftFacingTransform.gameObject.SetActive(true);
        }

        myMeleeActivated = false;
        SetOriginalRotation();
    }

    public void SetRangedSprite(Sprite aWeaponSprite)
    {
        myBow = aWeaponSprite;
        BowActiveWeapon();
    }

    public void SetMeleeSprite(Sprite aWeaponSprite)
    {
        myKnife = aWeaponSprite;
        KnifeActiveWeapon();
    }

    void SetOriginalRotation()
    {
        if (myIsFacingRight)
        {
            ActiveTransform().eulerAngles = myRightFacingRotation;
        }
        else
        {
            ActiveTransform().eulerAngles = myLeftFacingRotation;
        }
    }
}
