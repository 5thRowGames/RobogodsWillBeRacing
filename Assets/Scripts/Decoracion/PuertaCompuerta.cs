using System;
using DG.Tweening;
using UnityEngine;

public class PuertaCompuerta : MonoBehaviour
{
    public float time;
    public Vector3 finalPosition;
    private Compuerta compuerta;

    private Vector3 basePosition;

    private Tween open, close;

    private void Awake()
    {
        compuerta = transform.parent.GetComponent<Compuerta>();
        compuerta.OpenDoor += OpenDoor;
        compuerta.CloseDoor += CloseDoor;
    }

    private void Start()
    {
        basePosition = transform.localPosition;
    }

    public void OpenDoor()
    {
        close.Kill();
        open = transform.DOLocalMove(finalPosition, time);
        open.Play();
    }

    public void CloseDoor()
    {
        open.Kill();
        close = transform.DOLocalMove(basePosition, time);
        close.Play();

    }
}
