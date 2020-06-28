using System;
using System.Collections.Generic;
using System.Linq;
using MagnetBuilder.MagnetSystem;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class MagnetBehaviour : MonoBehaviour, IMagnetBehaviour
{
	[InlineEditor()] [SerializeField] protected List<SnapPoint> snapPoints;
	[SerializeField] protected bool isSnapped;

	public bool IsSnapped => isSnapped;
	public string SnappingTag { get; set; }


	protected void SetParentNodes()
	{
		foreach (var snapPoint in snapPoints)
		{
			snapPoint.SetParentNode(transform);
		}
	}


	protected void CheckRoot()
	{
		if (!transform.root.name.Equals("Pivot"))
		{
			EnableDragMode();
		}
	}

	public virtual void EnableSnapMode()
	{
		isSnapped = true;
		gameObject.layer = LayerMask.NameToLayer("World");
		foreach (var snapPoint in snapPoints)
		{
			snapPoint.GetComponent<Collider>().enabled = true;
		}
	}

	public virtual void EnableDragMode()
	{
		//print($"DragMode");
		isSnapped = false;
		gameObject.layer = LayerMask.NameToLayer("Default");
		foreach (var snapPoint in snapPoints)
		{
			snapPoint.GetComponent<Collider>().enabled = false;
		}
	}

	public abstract void AlignWithSnapPoint(Transform snapPoint);

	public abstract void SnapMagnet(Transform originalNode, Transform snapPoints1);

	public virtual SnapPoint GetNode(SnapDirection snapDirection)
	{
		return snapPoints.Find(point => point.snapDirection == snapDirection);
	}


	[Button(ButtonSizes.Medium)]
	void GetSnapPoints()
	{
		snapPoints = GetComponentsInChildren<SnapPoint>().ToList();
	}


}