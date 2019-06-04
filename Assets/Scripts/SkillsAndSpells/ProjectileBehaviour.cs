using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float speed;

    void Start()
    {
        Invoke("DestroyAfter10", 10f);    
    }

    void Update ()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

    void OnCollisionEnter(Collision collision)
    {
        DestroyThisProjectile();
    }

    private void DestroyThisProjectile()
    {
        Destroy(gameObject);
    }
}
