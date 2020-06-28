using System;
using CustomUtilities;
using DG.Tweening;
using MagnetBuilder.MagnetSystem;
using TMPro;
using UnityEngine;

public class BallMagnet : MagnetBehaviour
{
    private void Start()
    {
        CheckRoot();
        SetParentNodes();
        SnappingTag = "BallSnapPoint";
    }

    public override void AlignWithSnapPoint(Transform snapPoint)
    {
        return;
    }

    public override void SnapMagnet(Transform originalNode, Transform contactPoint)
    {
        EnableSnapMode();

        //Snap Magnet Logic
        //Rod is Always Right Aligned with the Ball
        contactPoint.GetComponent<SnapPoint>().SetChildNode(originalNode);
        originalNode.GetComponent<IMagnetBehaviour>().GetNode(contactPoint.GetComponent<SnapPoint>().snapDirection).SetChildNode(contactPoint);

        var point = contactPoint.transform;
        transform.DORotate(Vector3.zero, 0.25f)
                 .SetEase(Ease.InOutBack);

        transform.DOMove(point.position, 0.25f)
                 .SetEase(Ease.InOutBack)
                 .OnComplete(() =>
                  {
                      transform.SetParent(point.transform.root);
                      contactPoint.GetComponent<SnapPoint>().CheckOverlap(originalNode);
                  });

        point?.GetComponent<MaterialPropertyHandler>().SetColor(new Color(1, 1, 1, 0f));

    }

}
