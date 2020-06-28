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

	public override void SetSnapPoint()
	{
	}

	public override void SnapMagnet(Transform originalNode, Collider[] contactPoints)
	{
		if (contactPoints.Length==0)
		{
			return;
		}
		var point = contactPoints[0].transform;
		transform.DORotate(point.rotation.eulerAngles, 0.25f)
		         .SetEase(Ease.InOutBack)
		         .OnComplete(() => { transform.SetParent(point.root); });

		transform.DOMove(point.position, 0.25f)
		         .SetEase(Ease.InOutBack);

		point?.GetComponent<MaterialPropertyHandler>().SetColor(new Color(1, 1, 1, 0f));

		// m_currentContactCollider.transform.parent.GetComponent<IMagnetBehaviour>()
		// .SetSnapPoint(m_currentContactCollider.transform);

		//snapPoint.GetComponent<Collider>().enabled = false;
		//snapPoint.GetComponent<SnapPoint>().isOccupied = true;

		//Test
		// print($"RODMAGNET");
		// print($"OriginalNodeName: {originalNode.name}"); //Rod_i
		// print($"SnapNodeName: {snapPoints1.name} with Parent: {snapPoints1.transform.parent.name}"); //XYZ with Ball_1


		foreach (var contactPoint in contactPoints)
		{
			contactPoint.transform.parent.GetComponent<IMagnetBehaviour>()
			            .GetNode(contactPoint.GetComponent<SnapPoint>().snapDirection).SetChildNode(contactPoint.transform);

			var magnet = originalNode.GetComponent<IMagnetBehaviour>();
			if (magnet!=null)
			{
				magnet.GetNode(SnapDirection.South)
				            .SetChildNode(contactPoint.transform.parent);

				var snapPoint = contactPoint.GetComponent<SnapPoint>();
				if (snapPoint)
				{
					print(magnet.GetNode(SnapDirection.North).snapDirection);
					print(contactPoint.GetComponent<SnapPoint>().snapDirection);
					magnet.GetNode(SnapDirection.North).snapDirection =
						contactPoint.GetComponent<SnapPoint>().snapDirection;
				}
			}
		}

		// foreach (var point in snapPoints)
		// {
		// 	point.CheckForOverlap();
		// }

		// var colliders = transform.GetComponentsInChildren<Collider>();
		//
		// foreach (var collider in colliders)
		// {
		// 	collider.enabled = false;
		// }
	}

	public override SnapPoint GetNode(SnapDirection snapDirection)
	{
		return snapPoints.Find(point => point.snapDirection == snapDirection);
	}
}