using System;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    public ParticleSystem interiorTrailLeft;
    public ParticleSystem interiorTrailRight;

    public Camera camera;

    private int oldMask;

    private void Awake()
    {
        oldMask = camera.cullingMask;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(60f * Time.deltaTime * Vector3.forward);

            interiorTrailLeft.startSpeed = 8f;
            interiorTrailRight.startSpeed = 8f;
        }
        else
        {
            interiorTrailLeft.startSpeed = 0;
            interiorTrailRight.startSpeed = 0;
        }
            
        
        if(Input.GetKey(KeyCode.A))
            transform.Rotate(-60f * Time.deltaTime * Vector3.up);
        
        if(Input.GetKey(KeyCode.D))
            transform.Rotate(60f * Time.deltaTime * Vector3.up);

        if (Input.GetKey(KeyCode.Space))
        {
            camera.cullingMask |= 1 << LayerMask.NameToLayer("TurboAnubis");
        }
        else
        {
            camera.cullingMask = oldMask;
        }

    }
}
