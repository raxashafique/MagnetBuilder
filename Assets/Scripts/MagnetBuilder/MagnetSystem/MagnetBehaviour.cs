﻿using System;
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

	private void Start()
	{
		CheckRoot();
	}

	protected void CheckRoot()
	{
		if (!transform.root.name.Equals("Pivot"))
		{
			DragMode();
		}
	}

	public virtual void SnapMode()
	{
		isSnapped = true;
		gameObject.layer = LayerMask.NameToLayer("World");
		foreach (var snapPoint in snapPoints)
		{
			snapPoint.GetComponent<Collider>().enabled = true;
		}
	}

	public virtual void DragMode()
	{
		print($"DragMode");
		isSnapped = false;
		gameObject.layer = LayerMask.NameToLayer("Default");
		foreach (var snapPoint in snapPoints)
		{
			snapPoint.GetComponent<Collider>().enabled = false;
		}
	}

	public abstract void SetSnapPoint();

	public abstract void SnapMagnet(Transform snapPoint);


	[Button(ButtonSizes.Medium)]
	void GetSnapPoints()
	{
		snapPoints = GetComponentsInChildren<SnapPoint>().ToList();
	}


}