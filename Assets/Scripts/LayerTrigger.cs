using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    //when object exit the trigger, put it to the assigned layer and sorting layers
    //used in the stair objects for player to travel between layers
    public class LayerTrigger : MonoBehaviour
    {
        public string sortingLayer;

        private void OnTriggerExit2D(Collider2D other)
        {  

            other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;
            SpriteRenderer[] srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
            MeshRenderer[] mrs = other.gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach ( SpriteRenderer sr in srs)
            {
                sr.sortingLayerName = sortingLayer;
            }
            foreach( MeshRenderer mr in mrs)
            {
                mr.sortingLayerName = sortingLayer;
            }
        }

    }
}
