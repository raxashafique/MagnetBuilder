using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace MagnetBuilder.Core.Controllers.UIControllers
{
	public class ConsentUIPanel:UIPanel
	{
		[TabGroup("PanelSpecificData")]
		[SerializeField]private TextMeshProUGUI gameNameText;

		[TabGroup("PanelSpecificData")] [SerializeField]
		private TextMeshProUGUI disclaimerText;

		[TabGroup("PanelSpecificData")] [SerializeField]
		private TextMeshProUGUI agreeText;

		private void Start()
		{
			gameNameText.text = GameConstants.GameName;
			disclaimerText.text = GameConstants.PrivacyPolicyText;
			agreeText.text = GameConstants.PrivacyPolicyFooter;
		}
	}
}