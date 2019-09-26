using UnityEngine;

public class Prueba : MonoBehaviour
{
    void Start()
    {
        AkSoundEngine.PostEvent("Musica_Inicio", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * 10f * Time.deltaTime);
        
        if(Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * 10f * Time.deltaTime);
        
        if(Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * 10f * Time.deltaTime);
        
        if(Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * 10f * Time.deltaTime);
        
    }
}
