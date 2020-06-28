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

    public override void SetSnapPoint()
    {

    }

    public override void SnapMagnet(Transform originalNode, Transform contactPoint)
    {
        var point = contactPoint.transform;
        transform.DORotate(Vector3.zero, 0.25f)
                 .SetEase(Ease.InOutBack);

        transform.DOMove(point.position, 0.25f)
                 .SetEase(Ease.InOutBack)
                 .OnComplete(() =>
                  {

                      transform.SetParent(point.transform.root);

                      // snapPoints[0].isOccupied = true;
                  });

        point?.GetComponent<MaterialPropertyHandler>().SetColor(new Color(1, 1, 1, 0f));

        // m_currentContactCollider.transform.parent.GetComponent<IMagnetBehaviour>()
        // .SetSnapPoint(m_currentContactCollider.transform);

        // snapPoint.GetComponent<Collider>().enabled = false;
        // snapPoint.GetComponent<SnapPoint>().isOccupied = true;

        print($"BALLMAGNET");
        print($"OriginalNodeName: {originalNode.name}"); //Rod_i
        //print($"SnapNodeName: {snapPoints1.name} with Parent: {snapPoints1.transform.parent.name}"); //XYZ with Ball_1


        var contactPoints = contactPoint.GetComponent<SnapPoint>().CheckOverlap();

        foreach (var collider1 in contactPoints)
        {
            print($"{collider1.name}");
        }
        print($"_____________________________");
        foreach (var contact in contactPoints)
        {
            if (contact.CompareTag("BallSnapPoint"))
            {
                print($"{contact.name}");
                contact.GetComponent<SnapPoint>().SetChildNode(transform);
            }
        }

        #region MyRegion

        point.transform.parent.GetComponent<IMagnetBehaviour>()
             .GetNode(point.GetComponent<SnapPoint>().snapDirection)
             .SetChildNode(point.transform);


        var pole = GetInverseDirection(point.GetComponent<SnapPoint>().snapDirection);

        originalNode.GetComponent<IMagnetBehaviour>().GetNode(pole).SetChildNode(point.transform.parent);

        #endregion




        // foreach (var contactPoint in contactPoints)
        // {
        //     contactPoint.transform.parent.GetComponent<IMagnetBehaviour>()
        //                 .GetNode(contactPoint.GetComponent<SnapPoint>().snapDirection)
        //                 .SetChildNode(contactPoint.transform);
        //
        //
        //     var pole = GetInverseDirection(contactPoint.GetComponent<SnapPoint>().snapDirection);
        //
        //     originalNode.GetComponent<IMagnetBehaviour>().GetNode(pole).SetChildNode(contactPoint.transform.parent);
        // }
    }

    private static SnapDirection GetInverseDirection(SnapDirection direction)
    {
        switch (direction)
        {
            case SnapDirection.Up:
               return SnapDirection.Down;
                break;
            case SnapDirection.Down:
                return SnapDirection.Up;
                break;
            case SnapDirection.North:
                return SnapDirection.South;
                break;
            case SnapDirection.South:
                return SnapDirection.North;
                break;
            case SnapDirection.East:
                return SnapDirection.West;
                break;
            case SnapDirection.West:
                return SnapDirection.East;
                break;
            case SnapDirection.NorthEast:
                return SnapDirection.SouthWest;
                break;
            case SnapDirection.NorthWest:
                return SnapDirection.SouthEast;
                break;
            case SnapDirection.SouthEast:
                return SnapDirection.NorthWest;
                break;
            case SnapDirection.SouthWest:
                return SnapDirection.NorthEast;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override SnapPoint GetNode(SnapDirection snapDirection)
    {
        return snapPoints.Find(point => point.snapDirection == snapDirection);

    }
}
