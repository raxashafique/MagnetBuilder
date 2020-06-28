using System;
using CustomUtilities;
using MagnetBuilder.Core.Controllers.UIControllers;
using MagnetBuilder.Core.StateMachine;
using MagnetBuilder.Core.StateMachine.GameManagerStates;
using UnityEngine;

namespace MagnetBuilder.Core.Managers
{
	public class GameManager : Singleton<GameManager>
	{
		private GameStateMachine m_gameStateMachine;

		// Start is called before the first frame update
		private void Start()
		{
			m_gameStateMachine = new GameStateMachine();

			var splash = new SplashState(this, UIController.Instance.SplashPanel);
			var mainMenu = new MainMenuState(this, UIController.Instance.MainMenuPanel,
				UIController.Instance.MainMenuPlayButton);
			var tutorial = new TutorialState(this, UIController.Instance.TutorialPanel,
				UIController.Instance.TutorialDoneButton);
			var gameplay = new GamePlayState(this, UIController.Instance.GamePlayPanel);
			var win = new WinState(this, UIController.Instance.LevelCompletePanel,
				UIController.Instance.WinContinueButton);
			var fail = new FailState(this, UIController.Instance.LevelFailPanel,
				UIController.Instance.FailContinueButton);

			//Adding Transitions
			At(splash, mainMenu, CanShowMainMenu());
			// At(mainMenu, tutorial, CanPlayTutorial());
			At(mainMenu, gameplay, CanPlayGame());
			// At(tutorial, gameplay, () => tutorial.HasTutorialFinished);
			At(gameplay, win, GameToWin());
			At(gameplay, fail, GameToFail());
			At(win, mainMenu, WinToMainMenu());




			void At(IState from, IState to, Func<bool> condition) =>
				m_gameStateMachine.AddTransition(from, to, condition);


			//Adding Conditions for Transitions
			Func<bool> CanShowMainMenu() => () =>
				splash.HasSplashEnded;
			Func<bool> CanPlayGame() => () =>
				mainMenu.HasPressedPlay && PlayerPrefs.GetInt(GameConstants.TutorialCompleted, 0) == 1;
			Func<bool> CanPlayTutorial() => () =>
				mainMenu.HasPressedPlay && PlayerPrefs.GetInt(GameConstants.TutorialCompleted, 0) == 0;
			Func<bool> GameToWin() => () => gameplay.HasWon;
			Func<bool> GameToFail() => () => gameplay.HasLost;
			Func<bool> WinToMainMenu() => () => win.HasPressedContinue;


			//SetDefaultState
			m_gameStateMachine.SetState(splash);
		}

		private void Update() => m_gameStateMachine.Update();

		public void StartGame()
		{
			Debug.Log($"Starting Game");
		}
		public void StartTutorial()
		{
			Debug.Log($"Starting Tutorial");
		}

		public void LevelCompleted()
		{
			Debug.Log($"Level Completed");
		}
	}
}