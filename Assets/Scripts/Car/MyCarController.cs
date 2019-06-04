using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class MyCarController : MonoBehaviour, IControllable
{
    public bool activeDevice; //Para prueba solo

    [Header("*Car Specs*")]
    [SerializeField] [Tooltip("Fuerza aplicada al acelerar")] private float speedForce = 50f;
    [SerializeField] [Tooltip("Impulso al acelerar con velocidad inferior a speedThreshold")] private float instantSpeedForce = 3f;
    [SerializeField] [Tooltip("¿Siempre aplicar la aceleración mínima?")] private bool alwaysAccelerate = true;
    [SerializeField] [Tooltip("Aceleración mínima aplicada en cada momento")] [Range(0f, 1f)] private float minAcceleration = 0.2f;
    [SerializeField] [Tooltip("Incremento de la fuerza de aceleración cuando se usa el turbo")] private float boostMultiplier = 3f;
    [SerializeField] [Tooltip("Velocidad de giro")] private float turnSpeed = 10f;
    [SerializeField] [Tooltip("Velocidad umbral. Si la velocidad del coche es menor se aplica el impulso de instantSpeedForce")] private float speedThreshold = 10f;
    //[SerializeField] [Tooltip("Fuerza impulso para girar")] private float turnImpulse = 2f;
    [SerializeField] [Tooltip("Umbral de giro. Se considera que no se está girando si el valor de giro es menor a este")] [Range(0f, 1f)] private float steeringThreshold = 0.1f;
    [SerializeField] [Tooltip("Factor de reducción de la velocidad angular en cada FixedUpdate si no se está girando")] [Range(0f, 1f)] private float steeringReductionFactor = 0.9f;
    [SerializeField] [Tooltip("Reducción de velocidad angular constante")] private float angularVelocityReductionFactor = 0.95f;
    [SerializeField] [Tooltip("Velocidad angular máxima")] private float maxAngularSpeed = 20f;
    [SerializeField] [Tooltip("Fuerza del freno")] private float brakeForce = 20f;
    [SerializeField] [Tooltip("Fuerza en sentido opuesto al giro cuando se usa el freno de mano")] private float handBrakeForce = 30f;
    //[SerializeField] [Tooltip("Factor de reducción de la velocidad en cada FixedUpdate cuando se usa el freno de mano. Actualmente no en uso.")] [Range(0f, 1f)] private float handBrakeBrakeFactor = 0.95f;
    [SerializeField] [Tooltip("Segundos que hay que mantener el freno de mano pulsado para recibir un turbo tras soltarlo")] private float handBrakeTurboTime = 4f;
    [SerializeField] [Tooltip("Contador de los segundos que estamos pulsando el freno de mano")] private float handBrakeTimer;
    [SerializeField] [Tooltip("Tiempo de espera desde que se suelta el freno de mano y se activa el turbo")] private float handBrakeBoostWait = 0.2f;
    [SerializeField] [Tooltip("Tiempo de turbo tras activarlo con derrape")] private float handBrakeBoostTime = 1f;
    [SerializeField] [Tooltip("Distancia extra sobre wheelHeight para permitir el movimiento del coche")] private float extraHeightToAllowMovement = 0.5f;
    [SerializeField] [Tooltip("Distancia extra sobre wheelHeight para considerar que el coche está en el aire")] private float exraHeightToNoGrounded = 1f;
    [SerializeField] [Tooltip("Factor por el que multiplicar el giro y la velocidad cuando el coche está en el aire")] private float airLateralShiftFactor = 0.01f;
    [SerializeField] [Tooltip("Longitud de los rayos desde las esquinas del coche al suelo")] private float wheelHeight = 1f;
    [SerializeField] [Tooltip("Fuerza hacia el suelo a aplicar al vehículo cuando está en el aire")] private float downForce = 6f;
    [SerializeField] [Tooltip("Fuerza vertical que se aplica desde las esquinas del coche hacia arriba. Sirve para mantenerlo flotando en el aire")] private float upForce = 20f;
    [SerializeField] [Tooltip("Fuerza de salto vertical")] private float jumpForce = 20f;
    [SerializeField] [Tooltip("Tipo de suspension: Verdadero = usar una fuerza desde el centro del coche; Falso = usar una fuerza desde cada esquina del coche")] private bool useSimpleSuspension = false;
    [SerializeField] [Tooltip("Drag a usar cuando el coche está en el suelo")] private float groundDrag = 2f;
    [SerializeField] [Tooltip("Drag angular a usar cuando el coche está en el suelo")] private float groundAngularDrag = 1.5f;
    [SerializeField] [Tooltip("Drag a usar cuando el coche está en el aire")] private float airDrag = 0.5f;
    [SerializeField] [Tooltip("Drag angular a usar cuando el coche está en el aire")] private float airAngularDrag = 0.5f;
    [SerializeField] [Tooltip("Dirección donde aplicar la aceleración")] private Vector3 groundForward;
    [SerializeField] [Tooltip("Grados máximos de rotación en X")] private float maxRotationX = 40f;
    [SerializeField] [Tooltip("Grados mínimos de rotación en X")] private float minRotationX = -40f;
    [SerializeField] [Tooltip("Grados máximos de rotación en Z")] private float maxRotationZ = 40f;
    [SerializeField] [Tooltip("Grados mínimos de rotación en Z")] private float minRotationZ = -40f;
    [SerializeField] [Tooltip("Tiempo para poner a 0 las rotaciones en X y Z cuando se llega a sus máximos")] private float timeToIdentityRotation = 1f;

    [Tooltip("Posición del centro de masa del coche")] public Transform centerOfMass;
    [Tooltip("Punto desde donde se aplica la fuerza de aceleración")] public Transform accelPoint;
    [Tooltip("Punto desde donde se aplica la fuerza lateral del freno de mano")] public Transform handBrakePoint;
    [Tooltip("Esquinas del coche")] public List<Transform> carCorners; // 0 - FL, 1 - FR, 2 - BL, 3 - BR
    [Tooltip("El Rigidbody del coche")] public Rigidbody rb;
    [Tooltip("Layers con los que pueden chocar los raycasts de la suspensión del coche. No deben chocar con el propio layer del coche")] public LayerMask layerMask;
    [Tooltip("Layer para ignorar todo lo que no sea el suelo")] public LayerMask groundLayer;

    [Header("*Input Info*")]
    [SerializeField] private float accelerationInput;
    [SerializeField] private float steeringInput;
    [SerializeField] private float brakeInput;
    [SerializeField] private float handBrakeInput;
    [SerializeField] private bool boostInput;
    [SerializeField] private bool resetInput;
    [SerializeField] private bool jumpInput;

    [Header("*More Info*")]
    [SerializeField] [Tooltip("Velocidad angular del coche")] private Vector3 rbAngularVelocity;
    [SerializeField] [Tooltip("Magnitud de la velocidad del coche")] private float velocity;
    [SerializeField] [Tooltip("Indica si el vehículo está en el suelo o en el aire")] private bool isGrounded;
    [SerializeField] [Tooltip("Indica si el vehículo está boca abajo")] private bool isUpsideDown;

    [Header("Helper")]
    public Transform helper;

    private List<RaycastHit> hitList;

    [SerializeField] private bool rcRunning;
    
    //Para sonidos
    private bool isBoosting;
    private bool isBraking;

    private void Start()
    {
        rb.centerOfMass = transform.InverseTransformPoint(centerOfMass.position);
        hitList = new List<RaycastHit>();
        for (int i = 0; i < carCorners.Count; i++)
            hitList.Add(new RaycastHit());

        rb.maxAngularVelocity = maxAngularSpeed;

        if (activeDevice)
            GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindBoth();

        Core.Input.AssignControllable(GetComponent<IncontrolProvider>(),this);
        
        //Sonidos
        AkSoundEngine.PostEvent("Coche_1_Motor_Start_Loop", gameObject);

    }

    public void Control(IDevice device)
    {
        boostInput = device.State.Jump.IsHeld;
        accelerationInput = device.State.RightTrigger.Value;
        steeringInput = device.State.Horizontal.Value;
        brakeInput = device.State.Fire.Value;
        handBrakeInput = device.State.LeftTrigger.Value;

        resetInput = device.State.Special.IsPressed;
        jumpInput = device.State.LeftBumper.IsPressed;

        if (alwaysAccelerate && accelerationInput < minAcceleration) accelerationInput = minAcceleration;
        
        if (boostInput && !isBoosting)
        {
            isBoosting = true;
            AkSoundEngine.PostEvent("Coche_1_turbo", gameObject);
        }

        if (!boostInput)
            isBoosting = false;
        else
            accelerationInput *= boostMultiplier;
        
        if (brakeInput > 0.9f && !isBraking)
        {
            isBraking = true;
            AkSoundEngine.PostEvent("Coche_1_Freno", gameObject);
        }

        if (brakeInput < 0.9f)
            isBraking = false;

        if (rb.velocity.magnitude < speedThreshold) accelerationInput *= instantSpeedForce;

        AkSoundEngine.SetRTPCValue("Player_Velocidad", rb.velocity.magnitude);

        ShaderPruebas.blurAmount = rb.velocity.magnitude / 500; //Borrar, solo era para pruebas
    }

    private void FixedUpdate()
    {
        rbAngularVelocity = rb.angularVelocity; // Para exponer la velocidad angular en el inspector
        velocity = rb.velocity.magnitude;

        // Suspensión
        if (useSimpleSuspension)
        {
            SimpleSuspension();
        }
        else
        {
            for(int i = 0; i < carCorners.Count; i++)
            {
                Suspension(carCorners[i], hitList[i], i);
            }
        }

        Move(); // Movimiento del vehículo

        // Para recolocar el vehículo
        if (resetInput)
        {
            ResetPosition();
            resetInput = false;
        }

        ClampRotation(); // Para evitar que el vehículo rote más de lo permitido
    }

    private void ClampRotation()
    {
        if (!isGrounded || isUpsideDown)
        {
            if (rb.rotation.eulerAngles.x > maxRotationX || rb.rotation.eulerAngles.x < minRotationX)
            {
                Quaternion rot = new Quaternion();
                rot.eulerAngles = new Vector3(0f, rb.rotation.eulerAngles.y, rb.rotation.eulerAngles.z);
                rb.MoveRotation(rb.rotation);
                rb.constraints = rb.constraints | RigidbodyConstraints.FreezeRotationX;
                if (!rcRunning) StartCoroutine(RotationToIdentity());
            }
            else rb.constraints = RigidbodyConstraints.None;

            if (rb.rotation.eulerAngles.z > maxRotationZ || rb.rotation.eulerAngles.z < minRotationZ)
            {
                Quaternion rot = new Quaternion();
                rot.eulerAngles = new Vector3(rb.rotation.eulerAngles.x, rb.rotation.eulerAngles.y, 0f);
                rb.MoveRotation(rb.rotation);
                rb.constraints = rb.constraints | RigidbodyConstraints.FreezeRotationZ;
                if (!rcRunning) StartCoroutine(RotationToIdentity());
            }
            else rb.constraints = RigidbodyConstraints.None;
        }
        else rb.constraints = RigidbodyConstraints.None;
    }

    private IEnumerator RotationToIdentity()
    {
        rcRunning = true;
        float timeCount = 0f;
        Quaternion fromRotation = new Quaternion(rb.rotation.x, rb.rotation.y, rb.rotation.z, rb.rotation.w);
        Quaternion toRotation = new Quaternion();

        while (timeCount <= timeToIdentityRotation)
        {
            toRotation = Quaternion.Euler(0f, rb.rotation.eulerAngles.y, 0f);
            rb.MoveRotation(Quaternion.Slerp(fromRotation, toRotation, timeCount));
            timeCount += Time.deltaTime;
            yield return null;
        }
        rcRunning = false;
    }

    public void Move()
    {
        CheckGrounded();

        if (isGrounded)
        {
            rb.drag = groundDrag;
            rb.angularDrag = groundAngularDrag;
            Accelerate();
            Brake();
            HandBrake();
            Jump();
        }
        else // El coche está en el aire
        {
            rb.drag = airDrag;
            rb.angularDrag = airAngularDrag;

            if (helper != null)
                rb.AddForce(-helper.transform.up * downForce, ForceMode.Impulse);
            if (jumpInput)
                Accelerate();
        }
        CheckUpsideDown();
        Steering(); // Según el coche esté en el suelo o en el aire, el giro será normal o lateral
    }

    private void Accelerate()
    {
        // El vector no será el forward del coche, sino un forward paralelo al suelo
        if (hitList[0].point != Vector3.zero && hitList[1].point != Vector3.zero)
        {
            groundForward = Vector3.Cross(hitList[1].point - hitList[0].point, hitList[1].normal).normalized;
        }
        else
        {
            groundForward = transform.forward;
        }

        rb.AddForceAtPosition(groundForward * accelerationInput * speedForce, accelPoint.position, ForceMode.Acceleration);
    }

    private void Brake()
    {
        if (brakeInput > 0f)
        {
            if (rb.velocity.magnitude < speedThreshold)
                rb.AddForceAtPosition(-transform.forward * brakeInput * brakeForce * instantSpeedForce, accelPoint.position, ForceMode.Acceleration);
            else
                rb.AddForceAtPosition(-transform.forward * brakeInput * brakeForce, accelPoint.position, ForceMode.Acceleration);
        }
    }

    private void HandBrake()
    {
        // Freno de mano
        if (handBrakeInput > 0f)
        {
            rb.AddForceAtPosition(transform.right * handBrakeInput * handBrakeForce * -steeringInput, handBrakePoint.position);
            // Freno del freno de mano
            if (rb.velocity.magnitude != 0f)
            {
                rb.AddForceAtPosition(-transform.forward * handBrakeInput * handBrakeForce * 2f, handBrakePoint.position, ForceMode.Acceleration);
                //rb.velocity *= handBrakeBrakeFactor;
            }
            handBrakeTimer += Time.deltaTime; // Se va incrementando el tiempo que está el freno de mano pulsado 
        }
        else
        {
            if (handBrakeTimer >= handBrakeTurboTime) // Turbo
            {
                Debug.Log("Turbo!");
                Turbo();
            }
            handBrakeTimer = 0f; // Reset del timer
        }
    }

    private void Turbo()
    {
        StartCoroutine(TurboCoroutine(handBrakeBoostWait));
    }

    private IEnumerator TurboCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds); // Espera para que al jugador le dé tiempo de orientar el vehículo a su conveniencia antes del turbo

        float timer = handBrakeBoostTime;
        while (timer >= 0f)
        {
            timer -= Time.deltaTime;
            rb.AddForceAtPosition(groundForward * boostMultiplier * speedForce, accelPoint.position, ForceMode.Acceleration);
            yield return null;
        }
    }

    private void Steering()
    {
        if (isGrounded)
        {
            if (Mathf.Abs(steeringInput) < steeringThreshold) // No se está girando
            {
                rb.angularVelocity *= steeringReductionFactor; // Reducir velocidad angular
            }
            else
            {
                //rb.AddRelativeTorque(0f, steeringInput * turnImpulse * turnSpeed, 0f);
                rb.AddRelativeTorque(0f, steeringInput * turnSpeed, 0f);
            }
        }
        else // Cuando está en el aire el giro se convierte en movimiento lateral
        {
            rb.MovePosition(transform.position + (transform.right * steeringInput * velocity * airLateralShiftFactor));
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
        if(Physics.Raycast(corner.position, -Vector3.up, out hit, wheelHeight, layerMask))
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
        isGrounded = Physics.Raycast(transform.position, -transform.up, wheelHeight + exraHeightToNoGrounded, layerMask);
    }

    private void CheckUpsideDown()
    {
        isUpsideDown = Physics.Raycast(transform.position, transform.up, Mathf.Infinity, groundLayer);
    }

    private void ResetPosition()
    {
        rb.rotation = Quaternion.Euler(0, rb.rotation.eulerAngles.y, 0f);
        rb.Sleep();
        rb.WakeUp();
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
        if(hitList != null)
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
}
