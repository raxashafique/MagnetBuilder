using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MagnetBuilder.MagnetSystem
{
	[System.Serializable]
	public class SnapPoint : MonoBehaviour
	{
		private IMagnetBehaviour m_magnetBehaviour;
		public SnapDirection snapDirection;
		public bool isOccupied;

		private List<Collider> m_inContactColliders;

		private void Start()
		{
			m_magnetBehaviour = transform.parent.GetComponent<IMagnetBehaviour>();
			m_inContactColliders = new List<Collider>();
		}
		//
		// public void OccupyContactPoints()
		// {
		// 	foreach (var contactCollider in m_inContactColliders)
		// 	{
		// 		contactCollider.GetComponent<SnapPoint>().isOccupied = true;
		// 	}
		// }
		//
		// private void OnTriggerEnter(Collider other)
		// {
		// 	if (other.GetComponent<SnapPoint>())
		// 	{
		// 		m_inContactColliders.Add(other);
		// 	}
		// }
		//
		// private void OnTriggerExit(Collider other)
		// {
		// 	if (other.GetComponent<SnapPoint>())
		// 	{
		// 		m_inContactColliders.Remove(other);
		// 	}
		//
		// }
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