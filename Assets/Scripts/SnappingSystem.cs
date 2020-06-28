using System;
using System.Collections.Generic;
using CustomUtilities;
using DG.Tweening;
using MagnetBuilder.MagnetSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class SnappingSystem : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
	private Camera m_mainCamera;

	private List<Collider> m_inContactColliders;
	private Collider m_currentContactCollider;
	private IMagnetBehaviour m_magnetBehaviour;

	// Start is called before the first frame update
	void Start()
	{
		m_mainCamera = Camera.main;
		m_inContactColliders = new List<Collider>();
		m_magnetBehaviour = GetComponent<IMagnetBehaviour>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (m_magnetBehaviour.IsSnapped || !other.CompareTag(m_magnetBehaviour.SnappingTag))
			return;

		if (other.GetComponent<SnapPoint>().isOccupied)
			return;

		//print($"{this.name} Triggered with: {other.name} of {other.transform.parent.name}");

		if (!m_inContactColliders.Contains(other))
		{
			m_inContactColliders.Add(other);
		}


		if (m_inContactColliders.Count > 1)
		{
			m_inContactColliders.Sort((a, b) =>
				Vector3.Distance(transform.position, a.transform.position).CompareTo(
					Vector3.Distance(transform.position, b.transform.position)));
		}

		if (m_currentContactCollider != m_inContactColliders[0])
		{
			m_currentContactCollider?.GetComponent<MaterialPropertyHandler>()
			                         .SetColor(new Color(1, 1, 1, 0f));
		}

		m_currentContactCollider = m_inContactColliders[0];

		m_currentContactCollider.GetComponent<MaterialPropertyHandler>()
		                        .SetColor(new Color(1, 1, 1, 0.5f));


		transform.DORotate(m_currentContactCollider.transform.rotation.eulerAngles, 0.25f)
		         .SetEase(Ease.InOutBack);
	}

	private void OnTriggerExit(Collider other)
	{
		if (m_magnetBehaviour.IsSnapped || !other.CompareTag(m_magnetBehaviour.SnappingTag))
			return;

		if (m_inContactColliders.Contains(other))
		{
			m_inContactColliders.Remove(other);
		}

		other.GetComponent<MaterialPropertyHandler>().SetColor(new Color(1, 1, 1, 0f));
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (m_magnetBehaviour.IsSnapped)
			return;

		var zOffSet = m_mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
		var point = m_mainCamera.ScreenToWorldPoint(new Vector3(eventData.position.x,
			eventData.position.y, zOffSet));
		var ray = m_mainCamera.ScreenPointToRay(new Vector3(eventData.position.x,
			eventData.position.y, 0));

		if (Physics.Raycast(ray, out var rayCastHit, 100f,
			LayerMask.GetMask($"World")))
		{
			point.y = rayCastHit.point.y;
		}

		transform.localPosition = point;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		print($"OnPointerUp");
		if (m_magnetBehaviour.IsSnapped || m_currentContactCollider.GetComponent<SnapPoint>().isOccupied)
			return;


		m_magnetBehaviour.SnapMagnet(this.transform, m_inContactColliders.ToArray());
		m_magnetBehaviour.SnapMode();
	}


	public void OnPointerDown(PointerEventData eventData)
	{
		print($"OnPointerDown");
	}
}