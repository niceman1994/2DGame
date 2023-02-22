using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Object
{
    [SerializeField] private GameObject SideWeapoon1;
    [SerializeField] private GameObject SideWeapoon2;
    [SerializeField] private GameObject UpMissile;
    [SerializeField] private GameObject downMissile;
    [SerializeField] private GameObject downArms;

    public override void Initialize()
    {
        base.Name = "Boss";
        base.Hp = 250;
        base.Speed = 3.0f;
        base.ObjectAnim = null;
    }

    public override void Progress()
    {
        
    }

    public override void Release()
    {
        
    }

    
}
