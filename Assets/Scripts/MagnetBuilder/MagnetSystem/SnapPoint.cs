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

		public Collider[] CheckOverlap()
		{
			print($"Calling Check on {this.name}");
			var test = Physics.OverlapSphere(transform.position, 1f,
				LayerMask.GetMask("World"),
				QueryTriggerInteraction.Collide);

			return test;
		}

		public void Check(Transform snapObj)
		{

			print($"Calling check on {transform.name}");
			var test = Physics.OverlapSphere(transform.position, 1.5f,
				LayerMask.GetMask("World"),
				QueryTriggerInteraction.Collide);


			test.ToList().Sort((a, b) =>
				Vector3.Distance(transform.position, a.transform.position).CompareTo(
					Vector3.Distance(transform.position, b.transform.position)));

			var snapPoint = test.ToList().First(collider1 => collider1.GetComponent<SnapPoint>());
			this.SetChildNode(snapPoint.transform);
			//test[0].transform.GetComponent<SnapPoint>().SetChildNode(transform);

			return;
			foreach (var collider1 in test)
			{
				print($"{collider1.name}");
				if (collider1.GetComponent<SnapPoint>())
				{
					 print($"{collider1.name} Parent Node: {collider1.GetComponent<SnapPoint>().parentNode.name}" +
					       $"\nSnap Parent Name: {snapObj.parent.name} and snap obj Name: {snapObj.name}");

					if (collider1.GetComponent<SnapPoint>().parentNode.name.Equals(snapObj.parent.name))
					{
						// print($"Parents matched");

							SetChildNode(snapObj);
							childNode.GetComponent<SnapPoint>().SetChildNode(transform);


						break;
					}
					else if (snapObj.parent.GetComponent<RodMagnet>())
					{
						snapObj.parent.GetComponent<RodMagnet>().CheckPoints();
					}
					// else if (transform.parent.GetComponent<BallMagnet>()&& collider1.GetComponent<SnapPoint>().snapDirection == snapDirection
					// && collider1.CompareTag("BallSnapPoint"))
					// {
					// 	print($"SnapDirection: {snapDirection.ToString()}\n" +
					// 	      $"Collider1{collider1.name} snap Direction: {collider1.GetComponent<SnapPoint>().snapDirection}");
					// 	SetChildNode(snapObj);
					// 	childNode.GetComponent<SnapPoint>().SetChildNode(transform);
					// }
				}
			}
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


		private void OnTriggerEnter(Collider other)
		{
			// if (other.GetComponent<SnapPoint>())
			// {
			// 	//print($"SnapPointTriggeredWith {other.name}");
			// 	m_inContactColliders.Add(other);
			// }
		}

		private void OnTriggerExit(Collider other)
		{
			// if (other.GetComponent<SnapPoint>())
			// {
			// 	m_inContactColliders.Remove(other);
			// }
		}
	}


	public enum SnapDirection
	{
		Up,
		Down,
		North,
		South,
		East,
		West,
		NorthEast,
		NorthWest,
		SouthEast,
		SouthWest,
	}
}