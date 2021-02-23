using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Creature
{
    PlayerController player;
    void Start()
    {
        Initialize();        
    }


    void Update()
    {
        if(Input.GetKeyUp(KeyCode.S)) {
            CanSee(pos, player.pos);
        }

        if(CanSee(pos, player.pos)) {
            LookForHide();
        }
    }

    private void LookForHide() {
        List<Node> result = MapController.instance.grid.GetNeighbours(node);
        List<float> distances = new List<float>();
       
        foreach(Node n in result) {
            distances.Add(Vector2.Distance(n.position,player.pos));
            if(!CanSee(n.position, player.pos)) {
                Debug.Log($"From {n.position} player cant see");
                MoveTo(n);
                break;
            }

            float max = float.MinValue;

            for(int i = 0; i < distances.Count; i++) {
                if(distances[i] > max) {
                    max = distances[i];
                }
            }

            MoveTo(result[distances.IndexOf(max)]);
        }
    }

    public void Initialize() {
        base.Initialize();
        player = FindObjectOfType<PlayerController>();
    }
}
