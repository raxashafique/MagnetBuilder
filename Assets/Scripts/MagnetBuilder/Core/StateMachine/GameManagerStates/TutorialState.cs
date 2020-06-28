using MagnetBuilder.Core.Controllers;
using MagnetBuilder.Core.Controllers.UIControllers;
using MagnetBuilder.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace MagnetBuilder.Core.StateMachine.GameManagerStates
{
	internal class TutorialState : BaseMenuState
	{
		private readonly Button m_tutorialDoneButton;

		public bool HasTutorialFinished { get; private set; }

		public TutorialState(GameManager gameManager, UIPanel panel, Button tutorialDoneButton) : base(gameManager,
			panel)
		{
			m_tutorialDoneButton = tutorialDoneButton;
		}


		public override void OnEnter()
		{
			base.OnEnter();

			InputController.Instance.EnableInput();

			GameManager.StartTutorial();

			//BindButtons
			m_tutorialDoneButton.onClick.AddListener(OnTutorialDone);
		}

		public override void OnExit()
		{
			base.OnExit();

			InputController.Instance.DisableInput();
			PlayerPrefs.Save();

			m_tutorialDoneButton.onClick.RemoveListener(OnTutorialDone);
		}

		private void OnTutorialDone()
		{
			HasTutorialFinished = true;
			PlayerPrefs.SetInt(GameConstants.TutorialCompleted, 1);
		}
	}
}