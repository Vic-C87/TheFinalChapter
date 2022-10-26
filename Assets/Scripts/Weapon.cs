using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    string myWeaponName;
    [SerializeField]
    bool myIsRanged;
    [SerializeField]
    float myDamage;
    [SerializeField]
    float myMeleeDistance;
    [SerializeField]
    Sprite mySprite;
    [SerializeField]
    Sprite myProjectileSprite;

    SWeapon myWeapon;

    // Start is called before the first frame update
    void Start()
    {
        myWeapon = new SWeapon(myWeaponName, myIsRanged, myDamage, myMeleeDistance, mySprite, myProjectileSprite);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SWeapon PickUpWeapon()
    {
        return myWeapon;
    }
}
