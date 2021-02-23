using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Creature
{
    #region Singleton

    public static PlayerController instance;

    void Awake() {
        instance = this;
    }

    #endregion
}
