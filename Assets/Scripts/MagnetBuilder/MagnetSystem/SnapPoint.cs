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

			foreach (var collider1 in test)
			{
				print($"{collider1.name}");
			}

			return test;
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