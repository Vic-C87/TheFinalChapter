using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Enemy
{ 
    // Start is called before the first frame update
    void Start()
    {
        myCurrentHP = myCurrentHP == 0 ? myMaxHP : myCurrentHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
