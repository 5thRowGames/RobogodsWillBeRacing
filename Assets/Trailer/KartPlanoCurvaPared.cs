﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class KartPlanoCurvaPared : MonoBehaviour
{
    public bool activeDevice; //Para prueba solo

    public God.Type god;

    public float turboRechargeMultiplier;
    public float turboConsumeMultiplier;
    [SerializeField] private float turbo;

    public float Turbo
    {
        get => turbo;
        set => turbo = Mathf.Clamp(value, 0, 1);
    }

    public RacingCamera ownCamera;

    #region Members

    [Header("*Car Specs*")]
    [SerializeField] [Tooltip("Fuerza aplicada al acelerar")] public float speedForce = 50f;
    [SerializeField] [Tooltip("Impulso al acelerar con velocidad inferior a speedThreshold")] private float instantSpeedForce = 3f;
    [SerializeField] [Tooltip("¿Siempre aplicar la aceleración mínima?")] private bool alwaysAccelerate = true;
    [SerializeField] [Tooltip("Aceleración mínima aplicada en cada momento")] [Range(0f, 1f)] private float minAcceleration = 0.2f;
    [SerializeField] [Tooltip("Incremento de la fuerza de aceleración cuando se usa el turbo")] private float boostMultiplier = 3f;
    [SerializeField] [Tooltip("Divisor de la aceleración cuando el coche está en el aire")] private float accelerationDividerOnAir = 4f;
    [SerializeField] [Tooltip("")] private bool carUnderControl = false;
    [SerializeField] [Tooltip("Tiempo desde que se suelta el acelerador hasta que deja de acelerar el coche")] private float deaccelerationTime = 2f;
    [Tooltip("Contador del tiempo desde que se suelta el acelerador")] private float deaccelerationTimer;
    [SerializeField] [Tooltip("Velocidad de giro")] private float turnSpeed = 10f;
    [SerializeField] [Tooltip("Tiempo de turbo tras activarlo con derrape")] private float turboTime = 1f;
    [SerializeField] private float originalTurnSpeed;
    [SerializeField] private float boostTurnSpeed;
    [SerializeField] private float turboMultiplier;
    [SerializeField] private float addTurbo;
    [SerializeField] private float addSlow;
    [SerializeField] private float recoverSpeedFromTurbo;
    [SerializeField] private float recoverSpeedFromSlow;
    [SerializeField] [Tooltip("Decremento de la fuerza de aceleración cuando se usa el turbo")] private float slowDownMultiplier = 0.5f;
    [SerializeField] [Tooltip("Velocidad umbral. Si la velocidad del coche es menor se aplica el impulso de instantSpeedForce")] private float speedThreshold = 10f;
    [SerializeField] [Tooltip("Umbral de giro. Se considera que no se está girando si el valor de giro es menor a este")] [Range(0f, 1f)] private float steeringThreshold = 0.1f;
    [SerializeField] [Tooltip("Factor de reducción de la velocidad angular en cada FixedUpdate si no se está girando")] [Range(0f, 1f)] private float steeringReductionFactor = 0.9f;
    [SerializeField] [Tooltip("Velocidad mínima del forward del coche para invertir la dirección de giro (marcha atrás)")] private float minZVelocityToReverseSteering = -1f;
    [SerializeField] [Tooltip("Reducción de velocidad angular constante")] [Range(0f, 1f)] private float angularVelocityReductionFactor = 0.95f;
    [SerializeField] [Tooltip("Velocidad angular máxima")] private float maxAngularSpeed = 20f;
    [SerializeField] [Tooltip("Fuerza del freno")] private float brakeForce = 20f;
    [SerializeField] [Tooltip("Tiempo necesario para pasar de frenar a ir marcha atrás")] private float brakeToReverseTime = 0.5f;
    [SerializeField] [Tooltip("Cronómetro para pasar de frenar a ir marcha atrás")] private float brakeToReverseTimer;
    [SerializeField] [Tooltip("El coche va marcha atrás")] private bool isGoingBackwards = false;
    public bool IsGoingBackwards { get { isGoingBackwards = velocity.z < 0f; return isGoingBackwards; } }
    [SerializeField] [Tooltip("Indica si la velocidad del coche está por debajo del umbral")] private bool speedUnderThreshold;
    [SerializeField] [Tooltip("Fuerza en sentido opuesto al giro cuando se usa el freno de mano")] private float handBrakeForce = 30f;
    [SerializeField] [Tooltip("Factor por el que multiplicar handBrakeForce para aplicar freno")] [Range(0f, 2f)] private float handBrakeBrakeFactor = 0.5f;
    [SerializeField] [Tooltip("Segundos que hay que mantener el freno de mano pulsado para recibir un turbo tras soltarlo")] private float handBrakeTurboTime = 4f;
    [SerializeField] [Tooltip("Contador de los segundos que estamos pulsando el freno de mano")] private float handBrakeTimer;
    [SerializeField] [Tooltip("Tiempo de espera desde que se suelta el freno de mano y se activa el turbo")] private float handBrakeBoostWait = 0.2f;
    [SerializeField] [Tooltip("Distancia extra sobre wheelHeight para permitir el movimiento del coche")] private float extraHeightToAllowMovement = 0.5f;
    public float ExtraHeightToAllowMovement { get { return extraHeightToAllowMovement; } }
    [SerializeField] [Tooltip("Distancia extra sobre wheelHeight para considerar que el coche está en el aire")] private float extraHeightToNoGrounded = 1f;
    public float ExtraHeightToNoGrounded { get { return extraHeightToNoGrounded; } }
    [SerializeField] [Tooltip("Factor por el que multiplicar el giro y la velocidad cuando el coche está en el aire")] private float airLateralShiftFactor = 0.01f;
    [SerializeField] [Tooltip("Longitud de los rayos desde las esquinas del coche al suelo")] private float wheelHeight = 1f;
    [SerializeField] [Tooltip("Fuerza hacia el suelo a aplicar al vehículo cuando está en el aire")] private float downForce = 6f;
    [Tooltip("Fuerza hacia abajo añadida cuando se supera la altura máxima")] public float additiveDownForce = 1f;
    [SerializeField] [Tooltip("Fuerza vertical que se aplica desde las esquinas del coche hacia arriba. Sirve para mantenerlo flotando en el aire")] private float upForce = 20f;
    [SerializeField] private float originalUpForce;
    [SerializeField] private float upForceOnTheWall;
    [SerializeField] [Tooltip("Fuerza de salto vertical")] private float jumpForce = 20f;
    [SerializeField] [Tooltip("Tipo de suspension: Verdadero = usar una fuerza desde el centro del coche; Falso = usar una fuerza desde cada esquina del coche")] private bool useSimpleSuspension = false;
    [SerializeField] [Tooltip("Drag a usar cuando el coche está en el suelo")] private float groundDrag = 2f;
    [SerializeField] [Tooltip("Drag angular a usar cuando el coche está en el suelo")] private float groundAngularDrag = 1.5f;
    [SerializeField] [Tooltip("Drag a usar cuando el coche está en el aire")] private float airDrag = 0.5f;
    [SerializeField] [Tooltip("Drag angular a usar cuando el coche está en el aire")] private float airAngularDrag = 0.5f;
    [SerializeField] [Tooltip("Dirección donde aplicar la aceleración")] private Vector3 groundForward;

    [Tooltip("Posición del centro de masa del coche")] public Transform centerOfMass;
    [Tooltip("Punto desde donde se aplica la fuerza de aceleración")] public Transform accelPoint;
    [Tooltip("Punto desde donde se aplica la fuerza lateral del freno de mano")] public Transform handBrakePoint;
    [Tooltip("Esquinas del coche")] public List<Transform> carCorners; // 0 - FL, 1 - FR, 2 - BL, 3 - BR
    [Tooltip("El Rigidbody del coche")] public Rigidbody rb;
    [Tooltip("Layers con los que pueden chocar los raycasts de la suspensión del coche. No deben chocar con el propio layer del coche")] public LayerMask layerMask;

    [Tooltip("Layers de los jugadores")] public LayerMask playersMask;

    [Header("*Input Info*")]
    [SerializeField] private float accelerationInput;
    public float AccelerationInput { get { return accelerationInput; } set { accelerationInput = value; } }
    [SerializeField] private float steeringInput;
    [SerializeField] private float brakeInput;
    [SerializeField] private float handBrakeInput;
    [SerializeField] private bool boostInput;
    [SerializeField] private bool resetInput;
    [SerializeField] private bool jumpInput;
    /// <summary>
    /// Indica si el coche está sobre la pared
    /// </summary>
    public bool OnTheWall { get; set; }

    [Header("*More Info*")]
    [SerializeField] [Tooltip("Velocidad angular del coche")] private Vector3 rbAngularVelocity;
    [SerializeField] [Tooltip("Magnitud de la velocidad del coche")] public float velocityMagnitude;
    [SerializeField] [Tooltip("Vector de velocidad local del coche")] public Vector3 velocity;
    [SerializeField] public bool IsGrounded { get; private set; }
    private List<bool> cornerIsGrounded;
    [Tooltip("Nº de esquinas que deben estar en el suelo para considerar que el vehículo está en el suelo")]
    public int neededCornersToBeGrounded = 2;

    public float DistanceToGround
    {
        get
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, -transform.up, out hitInfo))
            {
                return hitInfo.distance;
            }
            else
                return -1f;
        }
    }
    public bool IsBeingTeleported = false;

    [Header("Helper")]
    public Transform helper;

    private List<RaycastHit> hitList;

    private Coroutine startTurboCoroutine;
    private Coroutine startSlowCoroutine;

    //Para sonidos
    private bool isBoosting;
    private bool isBraking;
    private bool isHorizontal;

    //Posiciones de parrilla de salida
    [SerializeField] private List<Transform> startingPositions;

    public string Name { get; private set; }

    #endregion

    #region Unity Events

    private void Awake()
    {
        startSlowCoroutine = null;
        startTurboCoroutine = null;

        Name = gameObject.name;
        turbo = 0;
    }

    private void Start()
    {
        rb.centerOfMass = transform.InverseTransformPoint(centerOfMass.position);
        hitList = new List<RaycastHit>();
        cornerIsGrounded = new List<bool>();
        for (int i = 0; i < carCorners.Count; i++)
        {
            hitList.Add(new RaycastHit());
            cornerIsGrounded.Add(false);
        }

        rb.maxAngularVelocity = maxAngularSpeed;
        brakeToReverseTimer = brakeToReverseTime;
        deaccelerationTimer = deaccelerationTime;
    }

    private void FixedUpdate()
    {
        rbAngularVelocity = rb.angularVelocity; // Para exponer la velocidad angular en el inspector
        velocityMagnitude = rb.velocity.magnitude;
        velocity = transform.InverseTransformDirection(rb.velocity);

        //if (steeringInput == 0f)
        //    steeringInput = 0.7f;
        //else steeringInput = 0f;

        // Suspensión
        if (useSimpleSuspension)
        {
            SimpleSuspension();
        }
        else
        {
            for (int i = 0; i < carCorners.Count; i++)
            {
                Suspension(carCorners[i], hitList[i], i);
            }
        }

        Move(); // Movimiento del vehículo

        // Para recolocar el vehículo
        if (resetInput)
        {
            if (startingPositions != null)
                ResetRacePosition();
            else
                ResetPosition();
            resetInput = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < carCorners.Count; i++)
        {
            // Puntos desde donde se lanzan los rayos para simular la suspensión
            Vector3 toPosition = new Vector3(carCorners[i].position.x, carCorners[i].position.y, carCorners[i].position.z);

            Gizmos.DrawSphere(toPosition, 0.1f);
        }
        Gizmos.DrawLine(transform.position, transform.position - transform.up);
        Gizmos.DrawCube(rb.position + rb.centerOfMass, 0.3f * Vector3.one);
        Gizmos.DrawLine(transform.position, transform.position - (transform.forward * accelerationInput));

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(accelPoint.position, 0.2f);
        Gizmos.DrawSphere(handBrakePoint.position, 0.15f);

        Gizmos.color = Color.magenta;
        if (hitList != null)
        {
            Gizmos.DrawLine(hitList[2].point, hitList[0].point);
            Gizmos.DrawLine(hitList[3].point, hitList[1].point);
            Gizmos.color = Color.black;
            Gizmos.DrawLine(hitList[2].point, hitList[3].point);

            // Puntos donde chocan los rayos de la suspensión en el suelo
            Gizmos.color = Color.yellow;
            foreach (var hit in hitList)
                Gizmos.DrawSphere(hit.point, 0.1f);
        }
    }

    #endregion

    public void Move()
    {
        CheckGrounded();

        if (IsGrounded)
        {
            rb.drag = groundDrag;
            rb.angularDrag = groundAngularDrag;
            Accelerate();
            HandBrake();
            Jump();
        }
        else // El coche está en el aire
        {
            rb.drag = airDrag;
            rb.angularDrag = airAngularDrag;
            if (helper != null)
                rb.AddForce(-helper.transform.up * (downForce + additiveDownForce), ForceMode.Impulse);
            Accelerate();
        }

        Steering(); // Según el coche esté en el suelo o en el aire, el giro será normal o lateral
    }

    private void Accelerate()
    {
        // El vector no será el forward del coche, sino un forward paralelo al suelo
        if (helper != null)
        {
            groundForward = helper.forward;
        }
        else
        {
            groundForward = transform.forward;
        }

        if (accelerationInput > 0f)
        {
            var force = IsGrounded ? accelerationInput : accelerationInput / accelerationDividerOnAir;
            rb.AddForce(groundForward * force * speedForce, ForceMode.Acceleration);
            deaccelerationTimer = deaccelerationTime;
        }
        else
        {
            if (!carUnderControl) return;

            deaccelerationTimer -= Time.deltaTime;
            if (deaccelerationTimer > 0f)
            {

                float accelerationInertia = Mathf.Lerp(0f, 1f, deaccelerationTimer / deaccelerationTime);
                rb.AddForce(groundForward * accelerationInertia * speedForce, ForceMode.Acceleration);
            }
        }

    }

    private void Brake()
    {
        if (brakeInput > 0f)
        {
            if (rb.velocity.magnitude < speedThreshold || velocity.z < 0f)
            {
                brakeToReverseTimer -= Time.deltaTime;
                if (brakeToReverseTimer <= 0f)
                {
                    rb.AddForceAtPosition(-transform.forward * brakeInput * brakeForce * 2f, accelPoint.position, ForceMode.Acceleration);
                }
            }
            else
            {

                rb.AddForceAtPosition(-transform.forward * brakeInput * brakeForce, accelPoint.position, ForceMode.Acceleration);
            }
        }
        else
            brakeToReverseTimer = brakeToReverseTime;
    }

    private void HandBrake()
    {
        // Freno de mano
        if (handBrakeInput > 0f)
        {
            if (rb.velocity.z > speedThreshold)
            {
                rb.AddForceAtPosition(transform.right * handBrakeInput * handBrakeForce * -steeringInput, handBrakePoint.position);
                // Freno del freno de mano
                if (rb.velocity.magnitude != 0f)
                {
                    rb.AddForceAtPosition(-transform.forward * handBrakeInput * brakeForce * handBrakeBrakeFactor, handBrakePoint.position, ForceMode.Acceleration);
                }
                handBrakeTimer += Time.deltaTime; // Se va incrementando el tiempo que está el freno de mano pulsado
            }
            else
            {
                brakeToReverseTimer -= Time.deltaTime;
                if (brakeToReverseTimer <= 0f)
                {
                    rb.AddForceAtPosition(-transform.forward * handBrakeInput * brakeForce * 2f, accelPoint.position, ForceMode.Acceleration);
                }
            }
        }
        else
        {
            brakeToReverseTimer = brakeToReverseTime;
            handBrakeTimer = 0f; // Reset del timer
        }
    }

    public void StartSlowDown()
    {
        if (startSlowCoroutine == null)
            startSlowCoroutine = StartCoroutine(SlowDownCoroutine());
    }

    private IEnumerator SlowDownCoroutine()
    {

        var originalSpeed = speedForce;

        speedForce -= addSlow;

        float timer = turboTime;
        while (timer >= 0f)
        {
            timer -= Time.deltaTime;

            speedForce += recoverSpeedFromSlow;

            if (speedForce > originalSpeed)
                break;

            yield return null;
        }

        speedForce = originalSpeed;

        startSlowCoroutine = null;
    }

    public void SimpleSlowDown()
    {
        if (startSlowCoroutine == null && startTurboCoroutine == null)
            startSlowCoroutine = StartCoroutine(SimpleSlowDownCoroutine());
        else Debug.Log($"{startSlowCoroutine}");
    }

    private IEnumerator SimpleSlowDownCoroutine()
    {
        var timer = 0f;
        while (timer < 2f)
        {
            accelerationInput *= slowDownMultiplier;
            Mathf.Clamp(accelerationInput, 0f, slowDownMultiplier);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            Debug.Log($"Slowing down: {timer}");
        }
        startSlowCoroutine = null;
    }

    public void SimpleTurbo(float waitBeforeBoost)
    {
        if (startTurboCoroutine == null && startSlowCoroutine == null)
            startTurboCoroutine = StartCoroutine(SimpleTurboCoroutine(waitBeforeBoost));
    }

    private IEnumerator SimpleTurboCoroutine(float waitBeforeBoost)
    {
        yield return new WaitForSeconds(waitBeforeBoost);

        SoundManager.Instance.PlayFx(SoundManager.Fx.Banda_Aceleracion);

        var timer = 0f;
        while (timer < 2f)
        {
            accelerationInput *= boostMultiplier;
            Mathf.Clamp(accelerationInput, 0.2f, boostMultiplier);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            Debug.Log($"Boosting: {timer}");
        }
        startTurboCoroutine = null;
    }

    public void StartTurbo(float waitBeforeBoost)
    {

        if (startTurboCoroutine == null)
            startTurboCoroutine = StartCoroutine(TurboCoroutine(waitBeforeBoost));
        else Debug.Log($"{startTurboCoroutine}");
    }

    private IEnumerator TurboCoroutine(float seconds)
    {

        yield return new WaitForSeconds(seconds); // Espera para que al jugador le dé tiempo de orientar el vehículo a su conveniencia antes del turbo

        var originalSpeedForce = speedForce;

        speedForce += addTurbo;

        rb.AddForce(turboMultiplier * speedForce * groundForward, ForceMode.Acceleration);

        float timer = turboTime;

        while (timer >= 0f) // && speedForce >= originalSpeedForce)
        {
            timer -= Time.deltaTime;
            speedForce -= recoverSpeedFromTurbo;

            if (speedForce < originalSpeedForce)
                break;

            yield return null;
        }

        speedForce = originalSpeedForce;

        startTurboCoroutine = null;
    }

    private void Steering()
    {
        if (IsGrounded)
        {
            if (Mathf.Abs(steeringInput) < steeringThreshold) // No se está girando
            {
                rb.angularVelocity *= steeringReductionFactor; // Reducir velocidad angular
            }
            else
            {
                if (velocity.z >= minZVelocityToReverseSteering)
                    rb.AddRelativeTorque(0f, steeringInput * turnSpeed, 0f);
                else
                    rb.AddRelativeTorque(0f, -steeringInput * turnSpeed, 0f);
            }
        }
        else // Cuando está en el aire el giro se convierte en movimiento lateral
        {
            rb.MovePosition(transform.position + (steeringInput * velocityMagnitude * airLateralShiftFactor * transform.right));
        }
        rb.angularVelocity *= angularVelocityReductionFactor;
    }

    private void SimpleSuspension()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, wheelHeight, layerMask))
        {
            float proportionalHeight = (wheelHeight - hit.distance) / wheelHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * upForce;
            rb.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }
    }

    private void Suspension(Transform corner, RaycastHit hit, int index)
    {
        if (Physics.Raycast(corner.position, -Vector3.up, out hit, wheelHeight, layerMask))
            rb.AddForceAtPosition(Vector3.up * upForce * (1.0f - (hit.distance / wheelHeight)), corner.position);

        hitList[index] = hit; // Guardo la información del impacto
    }

    private void Jump()
    {
        if (!jumpInput) return;

        if (helper != null)
            rb.AddForce(helper.up * jumpForce, ForceMode.VelocityChange);
        else
            rb.AddForce(rb.transform.up * jumpForce, ForceMode.VelocityChange);
    }

    private void CheckGrounded()
    {
        IsGrounded = Physics.Raycast(transform.position, -transform.up, wheelHeight + extraHeightToNoGrounded, layerMask);
    }

    public void ResetRacePosition()
    {
        foreach (Transform tr in startingPositions)
        {
            Collider[] colliders = Physics.OverlapSphere(tr.position, 4f, playersMask);
            if (colliders.Length == 0)
            {
                ResetPosition();
                transform.position = tr.position;
                transform.rotation = tr.rotation;
                break;
            }
        }
    }

    private void ResetPosition()
    {
        rb.rotation = Quaternion.Euler(0, rb.rotation.eulerAngles.y, 0f);
        rb.Sleep();
        rb.WakeUp();
    }

    public void SetUpForceOnTheWall()
    {
        upForce = upForceOnTheWall;
    }

    public void SetUpForceOnTheGround()
    {
        upForce = originalUpForce;
    }

}