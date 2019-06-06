using DG.Tweening;
using UnityEngine;

public class CameraUI : MonoBehaviour
{
    private Sequence firstSequence;
    private Sequence secondSequence;

    public AnimationCurve animationCurve;
    
    public Transform firstPosition;
    public Transform secondPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            firstSequence = DOTween.Sequence();
            firstSequence.Append(transform.DOMove(firstPosition.position, 1.5f).SetEase(animationCurve));
            firstSequence.Insert(0, transform.DORotate(firstPosition.rotation.eulerAngles, 1.5f).SetEase(animationCurve));
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            secondSequence = DOTween.Sequence();
            secondSequence.Append(transform.DOMove(secondPosition.position, 0.5f));
            secondSequence.Insert(0, transform.DORotate(secondPosition.rotation.eulerAngles, 0.5f).SetEase(animationCurve));
        }

    }
}
