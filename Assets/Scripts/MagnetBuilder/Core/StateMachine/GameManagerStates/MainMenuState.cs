using MagnetBuilder.Core.Controllers;
using MagnetBuilder.Core.Controllers.UIControllers;
using MagnetBuilder.Core.Managers;
using UnityEngine;
using UnityEngine.UI;
using CameraType = MagnetBuilder.Core.Controllers.CameraType;

namespace MagnetBuilder.Core.StateMachine.GameManagerStates
{
	internal class MainMenuState : BaseMenuState
	{
		private readonly Button m_playButton;

		public MainMenuState(GameManager gameManager,UIPanel panel, Button playButton) : base(gameManager,panel)
		{
			m_playButton = playButton;
		}

		public bool HasPressedSettings { get; private set; }

		public bool HasPressedPlay { get; private set; }


		public override void OnEnter()
		{
			base.OnEnter();

			//Reset State
			HasPressedPlay = false;
			HasPressedSettings = false;


			//Camera Setting
			CameraController.Instance.ShowCamera(CameraType.MainMenu);

			//Bind Buttons
			m_playButton.onClick.AddListener(OnPlayButton);

			//BindClickSound();

			if (m_playButton==null)
			{
				Debug.Log("PlayBtnNull");
			}

			if (SoundController.Instance==null)
			{
				Debug.Log("SoundControllerNull");
			}


		}

		public override void OnExit()
		{
			base.OnExit();

			//UnBind Buttons
			m_playButton.onClick.RemoveListener(OnPlayButton);

			//UnBindClickSound();

			//Reset State
			HasPressedPlay = false;
			HasPressedSettings = false;

		}

		private void OnPlayButton()
		{
			//Camera Setting
			CameraController.Instance.ShowCamera(CameraType.Gameplay);

			m_playButton.interactable = false;
			Panel.backwardsComplete += () => HasPressedPlay = true;
			HidePanel();
		}

		private void OnRemoveAdsButton()
		{
			//Make a call to InAPP Manager
		}
	}
}