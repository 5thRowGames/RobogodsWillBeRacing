using System;
using DG.Tweening;
using UnityEngine;

public class PuertaCompuertaMenu : MonoBehaviour
{
    public float time;
    public Vector3 finalPosition;
    private CompuertaActivaMenu compuertaActiva;

    private Vector3 basePosition;

    private Tween open, close;

    private void Awake()
    {
        compuertaActiva = transform.parent.GetComponent<CompuertaActivaMenu>();

        if (compuertaActiva != null)
        {
            compuertaActiva.OpenDoor += OpenDoor;
            compuertaActiva.CloseDoor += CloseDoor;
        }
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
