using UnityEngine;

//Esta clase ayuda a bindear el servicio de Input que se ha creado. Se necesita esto ya que la arquitectura de Cepa posee ciertos fallos aún por arreglar.
public class AwakeBindear : MonoBehaviour
{
    private void OnEnable()
    {
        Atto.Bind<IInputService,OwnInputProvider>();
    }
}
