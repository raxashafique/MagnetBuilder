using System.Linq;
using CustomUtilities;
using DG.Tweening;
using MagnetBuilder.MagnetSystem;
using UnityEngine;

public class RodMagnet : MagnetBehaviour
{
    private void Start()
    {
        base.CheckRoot();
        SnappingTag = "RodSnapPoint";
    }

    public override void SetSnapPoint()
    {

    }

    public override void SnapMagnet(Transform snapPoint)
    {
        transform.DORotate(snapPoint.rotation.eulerAngles, 0.25f)
                 .SetEase(Ease.InOutBack)
                 .OnComplete(() =>
                  {
                      transform.SetParent(snapPoint.root);

                      // snapPoints.Sort((a, b) =>
                      //     Vector3.Distance(snapPoint.position, a.transform.position).CompareTo(
                      //         Vector3.Distance(snapPoint.position, b.transform.position)));
                      //
                      // snapPoints[0].isOccupied = true;

                      // foreach (var point in snapPoints)
                      // {
                      //     point.OccupyContactPoints();
                      // }
                  });

        transform.DOMove(snapPoint.position, 0.25f)
                 .SetEase(Ease.InOutBack);

        snapPoint?.GetComponent<MaterialPropertyHandler>()
                  .SetColor(new Color(1, 1, 1, 0f));

        // m_currentContactCollider.transform.parent.GetComponent<IMagnetBehaviour>()
        // .SetSnapPoint(m_currentContactCollider.transform);

        snapPoint.GetComponent<Collider>().enabled = false;
        snapPoint.GetComponent<SnapPoint>().isOccupied = true;


        var colliders = transform.GetComponentsInChildren<Collider>();

        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }


    }
}
