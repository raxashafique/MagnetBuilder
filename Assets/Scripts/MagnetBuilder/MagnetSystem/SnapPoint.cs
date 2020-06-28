using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace MagnetBuilder.MagnetSystem
{
	[System.Serializable]
	public class SnapPoint : MonoBehaviour
	{
		public SnapDirection snapDirection;
		public bool isOccupied;
		[SerializeField] private Transform parentNode;
		[SerializeField] private Transform childNode;


		private void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere(transform.position, 0.2f);
		}

		public void SetParentNode(Transform node)
		{
			parentNode = node;
		}

		public void SetChildNode(Transform node)
		{
			childNode = node;
		}

		private void Update()
		{
			if (isOccupied)
				return;

			if (parentNode != null && childNode != null)
			{
				isOccupied = true;
			}
		}

		public void CheckOverlap(Transform snapObj)
		{
			var overlap = Physics.OverlapSphere(transform.position, 0.2f,
				LayerMask.GetMask("World"),
				QueryTriggerInteraction.Collide);

			print($"OverlapCount: {overlap.Length}");
			foreach (var overlapCollider in overlap)
			{
				print($"overlapColliderName: {overlapCollider.name}");

				if (overlapCollider.CompareTag("BallMagnet"))
				{
					overlapCollider.GetComponent<IMagnetBehaviour>().GetNode(snapDirection).SetChildNode(transform);
					SetChildNode(overlapCollider.transform);
					break;
				}

			}
		}

		public static SnapDirection GetInverseDirection(SnapDirection direction)
		{
			switch (direction)
			{
				case SnapDirection.Up:
					return SnapDirection.Down;
				case SnapDirection.Down:
					return SnapDirection.Up;
				case SnapDirection.Left:
					return SnapDirection.Right;
				case SnapDirection.Right:
					return SnapDirection.Left;
				case SnapDirection.Front:
					return SnapDirection.Back;
				case SnapDirection.Back:
					return SnapDirection.Front;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}


	public enum SnapDirection
	{
		Up,
		Down,
		Left,
		Right,
		Front,
		Back
	}
}