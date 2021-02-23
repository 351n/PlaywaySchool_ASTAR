using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Singleton

    public static GameController instance;

    void Awake() {
        instance = this;
    }

    #endregion

    public MapController map;

    void Start() {
        map.Initialize();
        //map.TestPathfinding();

        InitializeEntities();
    }

    private static void InitializeEntities() {
        Entity[] entities = FindObjectsOfType<Entity>();

        foreach(Entity e in entities) {
            e.Initialize();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
