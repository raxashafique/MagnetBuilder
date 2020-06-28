using System.Linq;
using CustomUtilities;
using DG.Tweening;
using MagnetBuilder.MagnetSystem;
using UnityEngine;

public class RodMagnet : MagnetBehaviour
{
	private Transform lastContact;
	private void Start()
	{
		CheckRoot();
		SetParentNodes();
		SnappingTag = "RodSnapPoint";
	}

	public override void SetSnapPoint()
	{
	}


	public override void SnapMagnet(Transform originalNode, Transform contactPoint)
	{
		lastContact = contactPoint;
		var point = contactPoint.transform;
		transform.DORotate(point.rotation.eulerAngles, 0.25f)
		         .SetEase(Ease.InOutBack)
		         .OnComplete(() =>
		          {
			          transform.SetParent(point.root);
			          CheckPoints();
		          });

		transform.DOMove(point.position, 0.25f)
		         .SetEase(Ease.InOutBack);

		point?.GetComponent<MaterialPropertyHandler>().SetColor(new Color(1, 1, 1, 0f)); return;


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