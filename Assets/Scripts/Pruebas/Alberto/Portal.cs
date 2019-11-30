using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform targetPortal; // Portal destino
    public int index; // Índice de orden de este portal respecto del resto del circuito

    private MyCarController myCarController;
    private StabilityController stabilityController;
    public LayerMask playerLayer; // la layer de los coches
    private Vector3 teleportPoint; // punto donde teletransportar a los coches
    public float zOffset = 8f; // desplazamiento hacia delante respecto del punto de teletransporte
    private readonly float exitPortalSpeedMagnitude = 80f;
    private float portalEnterSpeedMagnitude;
    public Queue<Collider> carColliders; // Colliders de los coches que están esperando para ser teletransportados

    private bool isFirstCar = true;
    public God.Type nextAreaReligion;

    private Color portalColor;

    #region Unity Events
    private void OnTriggerEnter(Collider other)
    {

        if (isFirstCar)
        {
            isFirstCar = false;
            
            switch (nextAreaReligion)
            {
                case God.Type.Anubis:
                    portalColor = new Color(0, 0.03584905f, 0.2169811f, 0.35f);
                    SoundManager.Instance.PlayLoop(SoundManager.Music.Egipto);
                    break;
                
                case God.Type.Poseidon:
                    portalColor = new Color(0, 0.4941176f, 0.3372549f, 0.35f);
                    SoundManager.Instance.PlayLoop(SoundManager.Music.Griega);
                    break;
                
                case God.Type.Kali:
                    portalColor = new Color(0, 0.03584905f, 0.2169811f, 0.35f);
                    SoundManager.Instance.PlayLoop(SoundManager.Music.India);
                    break;
                
                case God.Type.Thor:
                    portalColor = new Color(0, 0.03584905f, 0.2169811f, 0.35f);
                    SoundManager.Instance.PlayLoop(SoundManager.Music.Nordica);
                    break;
                
                case God.Type.None:
                    portalColor = new Color(0, 0.1629f, 1, 0.35f);
                    SoundManager.Instance.PlayLoop(SoundManager.Music.Limbo);
                    break;
            }
            
            SoundManager.Instance.DelayPortalOutSound();
        }
        
        myCarController = other.GetComponentInParent<MyCarController>();
        stabilityController = other.GetComponentInParent<StabilityController>();

        if (myCarController != null)
        {
            HUDManager.Instance.FlashScreen(myCarController.god, portalColor);
            
            carColliders.Enqueue(other);
            
            if(myCarController != null)
            {
                portalEnterSpeedMagnitude = other.attachedRigidbody.velocity.magnitude;
                myCarController.IsBeingTeleported = true;
                myCarController.StopAllCoroutines();
            }
            if (stabilityController != null)
                stabilityController.StopRotationToIdentity();

            StartCoroutine(Teleport());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        myCarController = null;
        stabilityController = null;
    }

    bool m_Started;

    void Start()
    {
        //Use this to ensure that the Gizmos are being drawn when in Play Mode.
        m_Started = true;
        if(targetPortal != null)
            teleportPoint = targetPortal.TransformPoint(0f, 0f, zOffset);
        carColliders = new Queue<Collider>();
    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started && carColliders.Count > 0)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(teleportPoint, carColliders.Peek().transform.localScale * 2f);
    }

    #endregion

    private IEnumerator Teleport()
    {
        // Se comprueba en un área el doble de grande que el collider del coche para que no choquen varios coches teletransportados
        // de forma seguida.
        while (carColliders.Count > 0 && 
            Physics.OverlapBox(teleportPoint, carColliders.Peek().transform.localScale, targetPortal.rotation, playerLayer).Length > 0)
        {
            yield return null;
        }

        SetCar(carColliders.Peek().attachedRigidbody);
        var myCC = carColliders.Peek().GetComponentInParent<MyCarController>();
        if (myCC != null)
            myCC.IsBeingTeleported = false;
        
        carColliders.Dequeue();
    }

    private void SetCar(Rigidbody rb)
    {
        rb.velocity = targetPortal.TransformDirection(targetPortal.forward) * 0f;
        rb.angularVelocity = targetPortal.TransformDirection(targetPortal.forward) * 0f; // Vector3.zero;

        rb.position = teleportPoint;

        StartCoroutine(GoThroughPortalCameraCoroutine(rb.GetComponent<MyCarController>().ownCamera));
        
        rb.transform.forward = targetPortal.forward;

        var speed = portalEnterSpeedMagnitude < exitPortalSpeedMagnitude ? exitPortalSpeedMagnitude : portalEnterSpeedMagnitude;
        rb.rotation = targetPortal.rotation;
        rb.velocity = targetPortal.forward.normalized * speed;
    }

    IEnumerator GoThroughPortalCameraCoroutine(RacingCamera racingCamera)
    {
        yield return null;
        racingCamera.GoThroughPortal();
    }
}
