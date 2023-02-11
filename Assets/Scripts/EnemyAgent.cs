
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Google.Protobuf.WellKnownTypes;
using Unity.MLAgents.Sensors;

namespace Cainos.PixelArtTopDown_Basic
{
    public class EnemyAgent : Agent
    {
        [SerializeField] private FOV fieldOfView;

        public float speed;

        private Animator animator;
        public Vector2 dir = Vector2.zero;
        [SerializeField] private PlayerAgent playerRef;
        private int playerPoints = 0;
        private Vector2 startingPos;
        private string startingLayer;

        public override void Initialize()
        {
            animator = GetComponent<Animator>();
            gameObject.layer = LayerMask.NameToLayer("Enemy");
            startingPos = transform.localPosition;
            startingLayer = GetComponent<SpriteRenderer>().sortingLayerName;
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            if (playerPoints < playerRef.value)
            {
                float changed = playerRef.value - playerPoints;
                AddReward( - changed / 20);
                Debug.Log(GetCumulativeReward());
                playerPoints = playerRef.value; 
            }
            AddReward(-0.0005f);
            var horAction = Mathf.FloorToInt(actions.DiscreteActions[0]);
            var verAction = Mathf.FloorToInt(actions.DiscreteActions[1]);

            Vector2 mov = Vector2.zero;
            if (horAction == 1)
            {
                // right
                dir.x = -1;
                mov.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (horAction == 2)
            {
                // left
                dir.x = 1;
                mov.x = 1;
                animator.SetInteger("Direction", 2);
            }

            if (verAction == 1)
            {
                // front
                dir.y = 1;
                mov.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (verAction == 2)
            {
                // back
                dir.y = -1;
                mov.y = -1;
                animator.SetInteger("Direction", 0);
            }
            
            dir.Normalize();

            if (fieldOfView != null)
            {
                fieldOfView.SetAimDirection(dir);
            }

            animator.SetBool("IsMoving", mov.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * mov;
        }


        public override void Heuristic(in ActionBuffers actionsOut)
        {
            Vector2 mov = Vector2.zero;
            var action = actionsOut.DiscreteActions;
            if (Input.GetKey(KeyCode.A))
            {
                action[0] = 1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                action[0] = 2; ;
            }

            if (Input.GetKey(KeyCode.W))
            {
                action[1] = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                action[1] = 2;
            }

        }

        public override void OnEpisodeBegin()
        {
            gameObject.transform.localPosition = startingPos;
            GetComponent<SpriteRenderer>().sortingLayerName = startingLayer;
        }

        private void Reset()
        {
            gameObject.transform.localPosition = startingPos;
            GetComponent<SpriteRenderer>().sortingLayerName = startingLayer;
            EndEpisode();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                AddReward(1.0f);
                Debug.Log("KILL");
                Reset();    
            }
        }

    }
}
