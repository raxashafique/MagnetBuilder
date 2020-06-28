using System;
using Cinemachine;
using CustomUtilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagnetBuilder.Core.Controllers
{
	public class CameraController:Singleton<CameraController>
	{
		[BoxGroup("GenericCameras")]
		[SerializeField] private CinemachineVirtualCamera vCamMainMenu;
		[BoxGroup("GenericCameras")]
		[SerializeField] private CinemachineVirtualCamera vCamGamePlay;
		[BoxGroup("GenericCameras")]
		[SerializeField] private CinemachineVirtualCamera vCamLevelComplete;


		public void ShowCamera(CameraType cameraType)
		{
			//Reset Camera Priorities
			vCamMainMenu.Priority = 0;
			vCamGamePlay.Priority = 0;
			vCamLevelComplete.Priority = 0;


			switch (cameraType)
			{
				case CameraType.MainMenu:
					vCamMainMenu.Priority = 1;
					break;
				case CameraType.Gameplay:
					vCamGamePlay.Priority = 1;
					break;
				case CameraType.LevelComplete:
					vCamLevelComplete.Priority = 1;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(cameraType), cameraType, null);
			}
		}

	}

	public enum CameraType
	{
		MainMenu,
		Gameplay,
		LevelComplete,
	}

}