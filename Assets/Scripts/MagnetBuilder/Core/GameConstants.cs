namespace MagnetBuilder.Core
{
	public static class GameConstants
	{
		//public static float SplashDuration = 4f; //Not used since GameManager State Machine Implementation

		public const string GameName = "Guilty : Throne Edition!";
		public const string StudioName = "Digital Toys Studio";

		public const string UserConsentAgreed = "UserConsentAgreed"; //0 = NO, 1 = YES
		public static float UserConsentClickDuration = 0.15f;


		public static string PrivacyPolicyHeader = "Thank you for installing " + GameName;

		public const string PrivacyPolicyText = " We respect and value your privacy and data security and would like to get your consent on processing your game data in making "
		                                      + GameName + " better and show you only relevant ads. " + "You can check in detail how we use your data here";

		public const string PrivacyPolicyFooter = " I agree to the terms of " + StudioName + " and their Partners. " + "This is to confirm that i'm older than 16 years or have my guardians permission.";

		public const string PrivacyPolicyLink = "http://www.digitaltoysstudios.com/privacypolicy/privacy.html";

		public const string TutorialCompleted = "TutorialCompleted"; //0 = NO, 1 = YES

		public static string Vc1 = "VC1";

		public static string LevelNumberPref = "LevelNumber";
	}
}