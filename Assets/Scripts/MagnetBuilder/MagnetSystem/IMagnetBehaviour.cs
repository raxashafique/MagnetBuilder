using MagnetBuilder.MagnetSystem;
using UnityEngine;

public interface IMagnetBehaviour
{
	bool IsSnapped { get; }
	string SnappingTag { get; set; }
	void SnapMode();
	void DragMode();
	void SetSnapPoint();

	void SnapMagnet(Transform originalNode, Collider[] contactPoints);

	SnapPoint GetNode(SnapDirection snapDirection);
}