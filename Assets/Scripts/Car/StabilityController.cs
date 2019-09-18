using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabilityController : MonoBehaviour
{
    #region Members
    [SerializeField] [Tooltip("Grados máximos de rotación en X")] private float maxRotationX = 40f;
    [SerializeField] [Tooltip("Grados mínimos de rotación en X")] private float minRotationX = -40f;
    [SerializeField] [Tooltip("Grados máximos de rotación en Z")] private float maxRotationZ = 40f;
    [SerializeField] [Tooltip("Grados mínimos de rotación en Z")] private float minRotationZ = -40f;
    [SerializeField] [Tooltip("Tiempo para poner a 0 las rotaciones en X y Z cuando se llega a sus máximos")] private readonly float timeToIdentityRotation = 1f;
    [Tooltip("Layer para ignorar todo lo que no sea el suelo")] public LayerMask groundLayer;
    [Tooltip("Máxima altura permittida para el coche")]public float maxHeightAllowed = 3f;
    [Tooltip("Tiempo para bajar hasta la altura máxima permitida para el coche")] public float timeToMaxHeight;
    private float timeToMaxHeightTimer;
    #endregion

    #region Properties
    [SerializeField] public bool IsUpsideDown { get; private set; } // Indica si el vehículo está boca abajo
    [SerializeField] public bool IsRotatingToIdentity { get; private set; }


    #endregion

    #region Components
    public MyCarController myCarController;
    public Rigidbody rb;

    #endregion

    #region Unity Events

    private void FixedUpdate()
    {
        if(!myCarController.IsBeingTeleported)
            ClampRotation();
        CheckUpsideDown();
        ClampHeight();
    }

    #endregion

    private void CheckUpsideDown()
    {
        IsUpsideDown = Physics.Raycast(transform.position, transform.up, Mathf.Infinity, groundLayer);
    }

    private void ClampRotation()
    {
        if (!myCarController.IsGrounded || IsUpsideDown)
        {
            if (rb.rotation.eulerAngles.x > maxRotationX || rb.rotation.eulerAngles.x < minRotationX)
            {
                Quaternion rot = new Quaternion
                {
                    eulerAngles = new Vector3(0f, rb.rotation.eulerAngles.y, rb.rotation.eulerAngles.z)
                };
                rb.MoveRotation(rb.rotation);
                rb.constraints = rb.constraints | RigidbodyConstraints.FreezeRotationX;
                if (!IsRotatingToIdentity) StartCoroutine(RotationToIdentity());
            }
            else rb.constraints = RigidbodyConstraints.None;

            if (rb.rotation.eulerAngles.z > maxRotationZ || rb.rotation.eulerAngles.z < minRotationZ)
            {
                Quaternion rot = new Quaternion();
                rot.eulerAngles = new Vector3(rb.rotation.eulerAngles.x, rb.rotation.eulerAngles.y, 0f);
                rb.MoveRotation(rb.rotation);
                rb.constraints = rb.constraints | RigidbodyConstraints.FreezeRotationZ;
                if (!IsRotatingToIdentity) StartCoroutine(RotationToIdentity());
            }
            else rb.constraints = RigidbodyConstraints.None;
        }
        else rb.constraints = RigidbodyConstraints.None;
    }

    public void StopRotationToIdentity()
    {
        StopCoroutine(RotationToIdentity());
    }

    private IEnumerator RotationToIdentity()
    {
        IsRotatingToIdentity = true;
        Debug.Log("Started rotating to identity");
        float timeCount = 0f;
        Quaternion fromRotation = new Quaternion(rb.rotation.x, rb.rotation.y, rb.rotation.z, rb.rotation.w);
        Quaternion toRotation = new Quaternion();

        while (timeCount <= 0.3f)
        {
            toRotation = Quaternion.Euler(0f, rb.rotation.eulerAngles.y, 0f);
            rb.MoveRotation(Quaternion.Slerp(fromRotation, toRotation, timeCount));
            timeCount += Time.deltaTime;
            yield return null;
        }
        IsRotatingToIdentity = false;
        Debug.Log("Finished rotating to identity");
    }

    private void ClampHeight()
    {
        var distance = myCarController.DistanceToGround;
        if (distance > maxHeightAllowed)
        {
            myCarController.additiveDownForce = distance / maxHeightAllowed;
        }
        else
            myCarController.additiveDownForce = 1f;
    }

    private IEnumerator HeightToMaxAllowed()
    {
        float timeCount = 0;
        float yVelocity = rb.velocity.y;
        while (timeCount < timeToMaxHeight)
        {
            var y = Mathf.Lerp(yVelocity, 0f, timeCount);
            rb.velocity = new Vector3(rb.velocity.x, y, rb.velocity.z);
            timeCount += Time.deltaTime;
            yield return null;
        }
    }

}
