 using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Collector : MonoBehaviour
{
    private int value = 0;
    private bool isCollectable = false;

    [SerializeField] private Text valueText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            value++;
            valueText.text = "" + value;
            isCollectable = true;
        }
        else if (collision.gameObject.CompareTag("Diamond"))
        {
            value = value + 15;
            isCollectable = true;
        }
        if (isCollectable)
        {
            Destroy(collision.gameObject);
            valueText.text = "" + value;
            isCollectable = false;
        }
    }
}
