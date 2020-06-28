using MagnetBuilder.Core.Controllers;
using MagnetBuilder.Core.Controllers.UIControllers;
using MagnetBuilder.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace MagnetBuilder.Core.StateMachine.GameManagerStates
{
	internal class BaseMenuState:IState
	{
		protected readonly GameManager GameManager;
		protected readonly UIPanel Panel;

		public BaseMenuState(GameManager gameManager, UIPanel panel)
		{
			GameManager = gameManager;
			Panel = panel;
		}

		public virtual void Update()
		{

		}

		public virtual void OnEnter()
		{
			Debug.Log($"Entering State {GetType()}");
			BindClickSound();
			UIController.Instance.ShowPanel(Panel.Type);
		}

		public virtual void OnExit()
		{
			Debug.Log($"Exiting State {GetType()}");
			UnBindClickSound();
		}

		protected virtual void HidePanel()
		{
			Panel.backwardsComplete += () => Panel.gameObject.SetActive(false);
			Panel.HidePanel();
		}

		private void BindClickSound()
		{
			//Bind Sounds
			var buttons = Panel.GetComponentsInChildren<Button>();
			foreach (var button in buttons)
			{
				button.onClick.AddListener(SoundController.Instance.PlayButtonSound);
			}
		}

		private void UnBindClickSound()
		{
			///Unbind Sounds
			var buttons = Panel.GetComponentsInChildren<Button>();
			foreach (var button in buttons)
			{
				button.onClick.RemoveListener(SoundController.Instance.PlayButtonSound);
			}
		}
	}
}