using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Creature
{
    PlayerController player;
    List<Node> neighbours;

    private float[] distances = new float[8];


    void Update() {
        if(Input.GetKeyUp(KeyCode.S)) {
            CanSee(pos, player.pos);
        }
    }

    override public void Initialize() {
        base.Initialize();
        player = FindObjectOfType<PlayerController>();
    }

    public void UpdateHide() {
        if(player) {
            MapController.instance.CalculateHides(player);
            if(CanSee(pos, player.pos)) {
                LookForHide();
            }
        }
    }

    private void LookForHide() {
        for (int r = 1; r <= 10; r++) {
            Debug.Log("There");
            neighbours = MapController.instance.grid.GetNeighbours(node,range: r);

            for(int nIndex = 0; nIndex < neighbours.Count; nIndex++) {
                Node n = neighbours[nIndex];
                if(n.canHide) {
                    MoveTo(n);
                    return;
                }                
            }
        }

        ResetDistances();

        neighbours = MapController.instance.grid.GetNeighbours(node,5);
        for(int nIndex = 0; nIndex < neighbours.Count; nIndex++) {
            Node n = neighbours[nIndex];
            distances[nIndex] = Vector2.Distance(n.position, player.pos);
            float max = float.MinValue;
            int maxIndex = 0;

            for(int i = 0; i < distances.Length; i++) {
                if(distances[i] > max) {
                    max = distances[i];
                    maxIndex = i;
                }
            }

            MoveTo(neighbours[maxIndex]);
        }
    }

    private void ResetDistances() {
        for(int i = 0; i < distances.Length; i++) {
            distances[i] = 0;
        }
    }

   
}
