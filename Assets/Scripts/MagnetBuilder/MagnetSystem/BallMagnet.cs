using System;
using CustomUtilities;
using DG.Tweening;
using MagnetBuilder.MagnetSystem;
using TMPro;
using UnityEngine;

public class BallMagnet : MagnetBehaviour
{
    private Transform lastContact;
    private void Start()
    {

        CheckRoot();
        SetParentNodes();
        SnappingTag = "BallSnapPoint";

    }

    public override void SetSnapPoint()
    {

    }

    public override void SnapMagnet(Transform originalNode, Transform contactPoint)
    {
        lastContact = contactPoint;
        var point = contactPoint.transform;
        transform.DORotate(Vector3.zero, 0.25f)
                 .SetEase(Ease.InOutBack);

        transform.DOMove(point.position, 0.25f)
                 .SetEase(Ease.InOutBack)
                 .OnComplete(() =>
                  {

                      transform.SetParent(point.transform.root);
                      CheckPoints();
                      // snapPoints[0].isOccupied = true;
                  });

        point?.GetComponent<MaterialPropertyHandler>().SetColor(new Color(1, 1, 1, 0f));
    }



    public void CheckPoints()
    {
        foreach (var snapPoint in snapPoints)
        {
            snapPoint.Check(lastContact);
        }
    }




    public override SnapPoint GetNode(SnapDirection snapDirection)
    {
        return snapPoints.Find(point => point.snapDirection == snapDirection);

    }
}
