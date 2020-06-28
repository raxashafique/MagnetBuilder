using System.Linq;
using CustomUtilities;
using DG.Tweening;
using MagnetBuilder.MagnetSystem;
using UnityEngine;

public class RodMagnet : MagnetBehaviour
{

	private void Start()
	{
		CheckRoot();
		SetParentNodes();
		SnappingTag = "RodSnapPoint";
	}

	public override void AlignWithSnapPoint(Transform snapPoint)
	{
		transform.DORotate(snapPoint.localRotation.eulerAngles, 0.25f)
		         .SetEase(Ease.InOutBack);
	}


	public override void SnapMagnet(Transform originalNode, Transform contactPoint)
	{
		EnableSnapMode();
		//Snap Magnet Logic
		//Rod is Always Right Aligned with the Ball
		contactPoint.GetComponent<SnapPoint>().SetChildNode(originalNode);
		originalNode.GetComponent<IMagnetBehaviour>().GetNode(SnapDirection.Right).SetChildNode(contactPoint);

		var snapPoint = originalNode.GetComponent<IMagnetBehaviour>().GetNode(SnapDirection.Left);
		snapPoint.snapDirection = SnapPoint.GetInverseDirection(contactPoint.GetComponent<SnapPoint>().snapDirection);


		var point = contactPoint.transform;

		//Tweens
		transform.DORotate(point.rotation.eulerAngles, 0.25f)
		         .SetEase(Ease.InOutBack)
		         .OnComplete(() =>
		          {
			          transform.SetParent(point.root);
			          //Check for Overlaps after Rotation Completes
			          snapPoint.CheckOverlap(originalNode);
		          });

		transform.DOMove(point.position, 0.25f)
		         .SetEase(Ease.InOutBack);

		point.GetComponent<MaterialPropertyHandler>().SetColor(new Color(1, 1, 1, 0f));




	}
}