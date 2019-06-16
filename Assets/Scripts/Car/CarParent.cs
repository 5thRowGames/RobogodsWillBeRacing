using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarParent : MonoBehaviour
{
    public Rigidbody kart;

    public bool smoothFollow = true;
    [SerializeField] private float previousYPosition;
    private float newPosition;
    [SerializeField] private float yPosition;
    [SerializeField] private float yVelocity;
    public float gap = 0.2f;
    public float smoothTime = 0.3f;
    public float maxSpeed = 5f;
    public bool differentHeights;

    private void Awake()
    {
        if (kart == null)
            kart = GetComponentInChildren<Rigidbody>();

        previousYPosition = kart.transform.position.y;
    }

    void Update()
    {
        FollowKart();
        differentHeights = transform.position.y == kart.transform.position.y ? false : true;
    }

    private void FollowKart()
    {
        yPosition = YPosition();

        if (smoothFollow)
            transform.position = new Vector3(kart.transform.position.x, yPosition, kart.transform.position.z);
        else
            transform.position = kart.transform.position;

        transform.eulerAngles = kart.transform.eulerAngles;
    }

    private float YPosition()
    {
        if (Mathf.Abs(previousYPosition - kart.transform.position.y) <= gap)
            newPosition = Mathf.SmoothDamp(previousYPosition, kart.transform.position.y, ref yVelocity, smoothTime);//, maxSpeed);
        else
            newPosition = kart.transform.position.y;

        previousYPosition = newPosition;

        return newPosition;
    }
}
