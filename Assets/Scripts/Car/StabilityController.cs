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
    [Tooltip("Layer para comprobar el ángulo de inclinación del coche con respecto del suelo")] public LayerMask inclinationLayer;
    [Tooltip("Fuerza que se aplica sobre el coche cuando está sobre la pared")]public float pushToTheGroundForce;
    public float angle;
    private const string wallLayerName = "Wall";
    public int OnTheWall { get; set; } // true cuando el coche está en contacto con el trigger una pared
    [SerializeField]private float distanceToApplyPushForce; // Distancia máxima del coche a la pared para empujar al primero hacia la segunda
    #endregion

    #region Properties
    [SerializeField] public bool IsUpsideDown { get; private set; } // Indica si el vehículo está boca abajo
    [SerializeField] public bool IsRotatingToIdentity { get; private set; }


    #endregion

    #region Components
    public MyCarController myCarController;
    public Rigidbody rb;
    public Transform carHelper;

    #endregion

    #region Unity Events

    private void Start()
    {
        if (myCarController != null)
            distanceToApplyPushForce = myCarController.ExtraHeightToAllowMovement + myCarController.ExtraHeightToNoGrounded + carHelper.GetComponent<CarHelper>().Height;
        else distanceToApplyPushForce = 4.5f;
    }

    private void FixedUpdate()
    {
        ClampRotation();
        CheckUpsideDown();
        GetOnOrOffTheWall();
        if(OnTheWall > 0)
        {
            myCarController.OnTheWall = true;
            myCarController.SetUpForceOnTheWall(); 
            PushToTheGround();
        }
        else
        {
            myCarController.OnTheWall = false;
            myCarController.SetUpForceOnTheGround();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer(wallLayerName))
        {
            OnTheWall += 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(wallLayerName))
        {
            OnTheWall -= 1;
        }
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
    }

    //private void ClampHeight()
    //{
    //    var distance = myCarController.DistanceToGround;
    //    if (distance > maxHeightAllowed)
    //    {
    //        myCarController.additiveDownForce = distance / maxHeightAllowed;
    //    }
    //    else
    //        myCarController.additiveDownForce = 1f;
    //}

    //private IEnumerator HeightToMaxAllowed()
    //{
    //    float timeCount = 0;
    //    float yVelocity = rb.velocity.y;
    //    while (timeCount < timeToMaxHeight)
    //    {
    //        var y = Mathf.Lerp(yVelocity, 0f, timeCount);
    //        rb.velocity = new Vector3(rb.velocity.x, y, rb.velocity.z);
    //        timeCount += Time.deltaTime;
    //        yield return null;
    //    }
    //}

    private void PushToTheGround()
    {
        Debug.Log("PushToTheGround!");
        //if (Physics.Raycast(carHelper.position, -carHelper.up, out RaycastHit hitInfo, distanceToApplyPushForce, inclinationLayer))
        //{
        //    var normal = hitInfo.normal;
        //    var collisionAngle = (Vector3.Angle(carHelper.up, -normal));
        //    //Debug.Log($"Ángulo sobre el suelo = {collisionAngle}");
        //    if (collisionAngle < angle || collisionAngle > -angle)
        //    {
        //        var force = Mathf.Lerp(0f, pushToTheGroundForce, myCarController.AccelerationInput);
        //        Debug.Log($"force = {force}");
                var force = Mathf.Clamp01(2f * rb.velocity.magnitude / myCarController.speedForce);
                rb.AddForce(-rb.transform.up * pushToTheGroundForce * force, ForceMode.Acceleration);

        //    }
        //}
        //else Debug.Log("Estoy volando?");
	}

    private void GetOnOrOffTheWall()
    {
        RaycastHit hitInfo;
        if(OnTheWall > 0)
        {
            if(Physics.Raycast(myCarController.carCorners[0].position, transform.forward, out hitInfo, 0.5f, inclinationLayer, QueryTriggerInteraction.Collide))
            {
                rb.AddForceAtPosition(transform.up * pushToTheGroundForce, myCarController.accelPoint.position);
                //rb.AddForceAtPosition(-transform.up * pushToTheGroundForce, myCarController.handBrakePoint.position);
            }
            else if(Physics.Raycast(myCarController.carCorners[1].position, transform.forward, out hitInfo, 0.5f, inclinationLayer, QueryTriggerInteraction.Collide))
            {
                rb.AddForceAtPosition(transform.up * pushToTheGroundForce, myCarController.accelPoint.position);
                //rb.AddForceAtPosition(-transform.up * pushToTheGroundForce, myCarController.handBrakePoint.position);
            }
        }
    }
}
