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

	public override void SnapMagnet(Transform originalNode, Transform contactPoint)
	{
		var point = contactPoint.transform;
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
		 print($"SnapNodeName: {contactPoint.name} with Parent: {contactPoint.transform.parent.name}"); //XYZ with Ball_1



		 

		var contactPoints = contactPoint.GetComponent<SnapPoint>().CheckOverlap();

		foreach (var collider1 in contactPoints)
		{
			print($"{collider1.name}");
		}
		print($"_____________________________");
		foreach (var contact in contactPoints)
		{
			if (contact.CompareTag("RodSnapPoint"))
			{
				print($"{contact.name}");
				contact.GetComponent<SnapPoint>().SetChildNode(transform);
			}
		}

		// #region Work
		//
		// contactPoint.transform.parent.GetComponent<IMagnetBehaviour>()
		//             .GetNode(contactPoint.GetComponent<SnapPoint>().snapDirection).SetChildNode(contactPoint.transform);
		//
		// var magnet = originalNode.GetComponent<IMagnetBehaviour>();
		//
		// if (magnet == null) return;
		//
		// magnet.GetNode(SnapDirection.South)
		//       .SetChildNode(contactPoint.transform.parent);
		//
		// var snapPoint = contactPoint.GetComponent<SnapPoint>();
		//
		// magnet.GetNode(SnapDirection.North).snapDirection =
		// 	contactPoint.GetComponent<SnapPoint>().snapDirection;
		//
		// #endregion


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