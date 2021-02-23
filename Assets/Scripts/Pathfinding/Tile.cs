using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Node node;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp() {
        if(!node.isOccupied) {
            PlayerController.instance.MoveTo(node);
        }        
    }
}
