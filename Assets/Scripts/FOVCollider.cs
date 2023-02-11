using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Integrations.Match3;
using UnityEditor;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class FOVCollider : MonoBehaviour
    {
        // Start is called before the first frame update
        private float radius;
        private float angle;
        private GameObject player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            radius = 5f;
            angle = 45f;
        }

     
        private Vector2 DirectionFromAngle(float eulerY, float angleInDregrees)
        {
            angleInDregrees += eulerY;
            return new Vector2(Mathf.Sin(angleInDregrees + Mathf.Deg2Rad), Mathf.Cos(angleInDregrees + Mathf.Deg2Rad));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
            Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -angle / 2);
            Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, angle / 2);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
            Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

            if (true && player != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, player.transform.position);
            }

        }

        

 

    }
}