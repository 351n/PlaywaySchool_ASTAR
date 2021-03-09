using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Node node;
    public TextMesh debugText;

    private void Start() {
        node.debugText = debugText;
    }

    private void OnMouseUp() {
        if(!node.isOccupied) {
            PlayerController.instance.MoveTo(node);
            GameController.instance.UpdateEnemiesHides();
        }
    }
}
