
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Cainos.PixelArtTopDown_Basic
{
    public class PlayerAgent : Agent
    {
        public float speed;

        private Animator animator;
        public Vector2 dir = Vector2.zero;

        public int value = 0;
        private bool isCollectable = false;
        private List<GameObject> touchedObjects = new List<GameObject>();

        [SerializeField] private Text valueText;
        private Vector2 startingPos;
        private string startingLayer;


        public override void Initialize()
        {
            animator = GetComponent<Animator>();
            gameObject.layer = LayerMask.NameToLayer("Player");
            startingPos = transform.localPosition;
            startingLayer = GetComponent<SpriteRenderer>().sortingLayerName;

        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            var horAction = Mathf.FloorToInt( actions.DiscreteActions[0]);
            var verAction = Mathf.FloorToInt( actions.DiscreteActions[1]);
            Vector2 mov = Vector2.zero;
            AddReward(-0.0005f);
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
            Reset();
        }


        private void Reset()
        {
            gameObject.transform.localPosition = startingPos;
            GetComponent<SpriteRenderer>().sortingLayerName = startingLayer;
            value = 0;
            valueText.text = "" + value;
            foreach (GameObject item in touchedObjects)
            {
                item.SetActive(true);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Coin"))
            {
                value++;
                valueText.text = "" + value;
                isCollectable = true;
                AddReward(0.1f);
            }
            else if (collision.gameObject.CompareTag("Diamond"))
            {
                value = value + 15;
                isCollectable = true;
                AddReward(1.5f);
            }

            if (isCollectable)
            {
                Debug.Log(GetCumulativeReward());
                touchedObjects.Add(collision.gameObject);
                collision.gameObject.SetActive(false);
                valueText.text = "" + value;
                isCollectable = false;
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("DEAD");
                AddReward(-3.0f);
                Debug.Log(GetCumulativeReward());
                EndEpisode();
                Reset();
            }
        }
    }
}
