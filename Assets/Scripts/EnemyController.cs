using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Creature
{
    PlayerController player;

    private float[] distances = new float[8];

    void Start() {
        Initialize();
    }


    void Update() {
        if(Input.GetKeyUp(KeyCode.S)) {
            CanSee(pos, player.pos);
        }
    }

    public void UpdateHide() {
        if(CanSee(pos, player.pos)) {
            LookForHide();
        }
    }

    private void LookForHide() {
        ResetDistances();
        List<Node> neighbours = MapController.instance.grid.GetNeighbours(node);

        for(int nIndex = 0; nIndex < neighbours.Count; nIndex++) {
            Node n = neighbours[nIndex];
            distances[nIndex] = Vector2.Distance(n.position, player.pos);
            if(!CanSee(n.position, player.pos)) {
                //Debug.Log($"From {n.position} player cant see");
                MoveTo(n);
                break;
            }

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

    public void Initialize() {
        base.Initialize();
        player = FindObjectOfType<PlayerController>();
    }
}
