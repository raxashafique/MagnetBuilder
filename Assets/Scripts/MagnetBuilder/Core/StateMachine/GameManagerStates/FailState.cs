using MagnetBuilder.Core.Controllers.UIControllers;
using MagnetBuilder.Core.Managers;
using UnityEngine.UI;

namespace MagnetBuilder.Core.StateMachine.GameManagerStates
{
	internal class FailState:BaseMenuState
	{
		private readonly Button m_continueButton;

		public FailState(GameManager gameManager, UIPanel panel, Button continueButton):base(gameManager, panel)
		{
			m_continueButton = continueButton;
		}
	}
}