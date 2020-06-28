using MagnetBuilder.MagnetSystem;
using UnityEngine;

public interface IMagnetBehaviour
{
	bool IsSnapped { get; }
	string SnappingTag { get; set; }
	void EnableSnapMode();
	void EnableDragMode();
	void AlignWithSnapPoint(Transform snapPoint);

	void SnapMagnet(Transform originalNode, Transform contactPoint);

	SnapPoint GetNode(SnapDirection snapDirection);

}