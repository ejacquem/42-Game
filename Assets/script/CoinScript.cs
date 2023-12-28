using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Vector3 initialPosition;
    public float hoverHeight;
    public float hoverSpeed;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the vertical offset based on sine function
        float hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // Apply the offset to the Y-axis of the position
        Vector3 newPosition = initialPosition;
        newPosition.y += hoverOffset;

        // Update the item's position
        transform.position = newPosition;
    }
}
