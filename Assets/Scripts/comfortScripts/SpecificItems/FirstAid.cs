using UnityEngine;
using System.Collections;

public class FirstAid : ComfortItem
{

    void Awake()
    {
        base.Awake();
        fixes = "Infection";
    }
}
