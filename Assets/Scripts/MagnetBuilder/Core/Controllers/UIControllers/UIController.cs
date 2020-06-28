using System.Linq;
using CustomUtilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace MagnetBuilder.Core.Controllers.UIControllers
{
	[RequireComponent(typeof(Button))]
	public class UIController : Singleton<UIController>
	{
		#region Panels

		private UIPanelType m_currentPanelType, m_previousPanelType;


		[BoxGroup("UIPanels", centerLabel: true)] [SerializeField][Required]
		private UIPanel tutorialPanel;

		[BoxGroup("UIPanels", centerLabel: true)] [SerializeField][Required]
		private UIPanel gamePlayPanel;

		[BoxGroup("UIPanels", centerLabel: true)] [SerializeField][Required]
		private UIPanel levelCompletePanel;

		[BoxGroup("UIPanels", centerLabel: true)] [SerializeField][Required]
		private UIPanel levelFailPanel;

		[BoxGroup("UIPanels", centerLabel: true)] [SerializeField][Required]
		private UIPanel mainMenuPanel;

		[BoxGroup("UIPanels", centerLabel: true)] [SerializeField][Required]
		private UIPanel splashPanel;



		public UIPanel TutorialPanel => tutorialPanel;
		public UIPanel GamePlayPanel => gamePlayPanel;
		public UIPanel LevelCompletePanel => levelCompletePanel;
		public UIPanel LevelFailPanel => levelFailPanel;
		public UIPanel MainMenuPanel => mainMenuPanel;
		public UIPanel SplashPanel => splashPanel;

		#endregion


		#region Buttons


		[BoxGroup("MainMenuButtons", centerLabel: true)] [SerializeField][Required]
		private Button mainMenuPlayButton;
		[BoxGroup("TutorialMenuButtons", centerLabel: true)] [SerializeField][Required]
		private Button tutorialDoneButton;
		[BoxGroup("WinMenuButtons", centerLabel: true)] [SerializeField][Required]
		private Button winContinueButton;
		[BoxGroup("FailMenuButtons", centerLabel: true)] [SerializeField][Required]
		private Button failContinueButton;


		public Button MainMenuPlayButton => mainMenuPlayButton;


		public Button WinContinueButton => winContinueButton;

		public Button FailContinueButton => failContinueButton;

		public Button TutorialDoneButton => tutorialDoneButton;

		#endregion

		[BoxGroup("Utilities")]
		[ShowInInspector] private UIPanel[] m_uiPanels;
		[BoxGroup("Utilities")]
		[ShowInInspector] private Button[] m_buttons;

		[Button(ButtonSizes.Medium)]
		private void ValidatePanels()
		{
			#region PanelValidation

			if (splashPanel != null)
			{
				splashPanel.name = "SplashCanvas";
				splashPanel.Type = UIPanelType.Splash;
			}
			if (mainMenuPanel != null)
			{
				mainMenuPanel.name = "MainMenuCanvas";
				mainMenuPanel.Type = UIPanelType.MainMenu;
			}
			if (gamePlayPanel != null)
			{
				gamePlayPanel.name = "GamePlayCanvas";
				gamePlayPanel.Type = UIPanelType.Gameplay;
			}
			if (levelCompletePanel != null)
			{
				levelCompletePanel.name = "LevelCompleteCanvas";
				levelCompletePanel.Type = UIPanelType.LevelComplete;
			}
			if (levelFailPanel != null)
			{
				levelFailPanel.name = "LevelFailCanvas";
				levelFailPanel.Type = UIPanelType.LevelFail;
			}

			m_uiPanels = GetComponentsInChildren<UIPanel>(true);

			#endregion

			#region ButtonValidation



			if (mainMenuPlayButton)
			{
				mainMenuPlayButton.name = "MainMenu_Play_Btn";
			}
			if (tutorialDoneButton)
			{
				tutorialDoneButton.name = "Tutorial_Done_Btn";
			}
			if (winContinueButton)
			{
				winContinueButton.name = "Win_Continue_Btn";
			}
			if (failContinueButton)
			{
				failContinueButton.name = "Fail_Continue_Btn";
			}
			m_buttons = GetComponentsInChildren<Button>(true);

			#endregion
		}

		protected override void Awake()
		{
			base.Awake();
			m_uiPanels = GetComponentsInChildren<UIPanel>(true);
		}

		[Button(ButtonSizes.Medium)]
		public void ShowPanel(UIPanelType type)
		{
			Debug.Log($"Showing Panel: {type.ToString()}");
			HidePanel(m_currentPanelType);
			m_currentPanelType = type;
			var gamePanel = m_uiPanels.First(panel => panel.Type == type);
			gamePanel.gameObject.SetActive(true);
			gamePanel.ShowPanel();
		}

		private void HidePanel(UIPanelType type)
		{
			m_previousPanelType = type;
			var gamePanel = m_uiPanels.First(panel => panel.Type == type);
			gamePanel.HidePanel();
			gamePanel.backwardsComplete += DisablePanel;
		}

		private void DisablePanel()
		{
			var gamePanel = m_uiPanels.First(panel => panel.Type == m_previousPanelType);
			gamePanel.gameObject.SetActive(false);
		}
	}

	[Title("Panel Type")]
	public enum UIPanelType
	{
		Splash,
		Consent,
		Tutorial,
		MainMenu,
		Settings,
		Gameplay,
		LevelComplete,
		LevelFail,
		Revive,
		Reward,
		Pause
	}
}