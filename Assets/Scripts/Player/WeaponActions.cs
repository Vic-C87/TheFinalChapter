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

    [SerializeField]
    Sprite myKnife;
    [SerializeField]
    Sprite myBow;

    bool myIsFacingRight = true;
    bool myMeleeActivated = false;

    bool myStartedWeponAction = false;
    [SerializeField]
    float myAttackDuration = 2f;
    [SerializeField]
    float myAttackTimeStamp;

    [SerializeField]
    float mySwingSpeed;

    Vector3 mySwordSwingRotation = new Vector3(0, 0, 45);


    // Start is called before the first frame update
    void Start()
    {
        myLeftFacingTransform.gameObject.SetActive(false);
        myRightFacingTransform.gameObject.SetActive(true);
        BowActiveWeapon();
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
            ActiveTransform().eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void TurnLeft()
    {
        if (myIsFacingRight)
        {
            myLeftFacingTransform.gameObject.SetActive(true);
            myRightFacingTransform.gameObject.SetActive(false);
            myIsFacingRight = false;
            ActiveTransform().eulerAngles = new Vector3(0, 0, 0);
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
                ActiveTransform().eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    public void StartAttack()
    {
        myStartedWeponAction = true;
        myAttackTimeStamp = Time.realtimeSinceStartup;
        ActiveTransform().eulerAngles = new Vector3(0, 0, 0);

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
        ActiveTransform().eulerAngles = new Vector3(0, 0, 0);
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
        ActiveTransform().eulerAngles = new Vector3(0, 0, 0);
    }

    public void SetRangedSprite(Sprite aWeaponSprite)
    {
        myBow = aWeaponSprite;
    }

    public void SetMeleeSprite(Sprite aWeaponSprite)
    {
        myKnife = aWeaponSprite;
    }
}
