using UnityEngine;

namespace MagnetBuilder.Core
{
	public class EventHandler
	{
		//Game Manager Events
		//public static  EventListener<GameState> OnGameStateChanged = new EventListener<GameState>();


		//Game General Events
		public static EventListener OnGamePaused = new EventListener();
		public static EventListener OnGameReset = new EventListener();
		public static EventListener OnGameResume = new EventListener();
		public static EventListener OnRestart = new EventListener();
		public static EventListener OnGameToHome = new EventListener();

		public static EventListener OnSplashStarted = new EventListener();
		public static EventListener OnSplashEnded = new EventListener();

		public static EventListener OnMainMenu = new EventListener();

		public static EventListener OnTutorialStarted = new EventListener();
		public static EventListener OnTutorialEnded = new EventListener();

		public static EventListener OnLevelLoaded = new EventListener();
		public static EventListener OnLevelStarted = new EventListener();
		public static EventListener OnPauseToggled = new EventListener();

		public static EventListener OnLevelPreFail = new EventListener();
		public static EventListener OnLevelFail = new EventListener();

		public static EventListener OnLevelPreComplete = new EventListener();
		public static EventListener OnLevelComplete = new EventListener();

		//UI Events
		public static EventListener OnUserConsentAgreed = new EventListener();


		//Camera Events
		public static EventListener<Camera> OnCameraChange = new EventListener<Camera>();


		//InApp Events
		public static EventListener OnRemoveAdsPurchaseSuccess = new EventListener();
		public static EventListener OnAllGameUnlockPurchaseSuccess = new EventListener();
		public static EventListener<string> OnBuyINAPP = new EventListener<string>();
		public static EventCallBack<string, bool> HasPruchaseINAPP = new EventCallBack<string, bool>();
		public static EventListener<string> OnInAPPSuccess = new EventListener<string>();

		//Analytics
		public static EventListener<string> AnalyticsEvent = new EventListener<string>();

		//Ads
		public static EventListener<int> OnShowBanner = new EventListener<int>();
		public static EventListener<int> OnShowInterstitial = new EventListener<int>();
		public static EventListener<int> OnLoadrewarded = new EventListener<int>();
		public static EventCallBack<int, bool> IsRewardedAvailable = new EventCallBack<int, bool>();
		public static EventListener<int> OnRewardedShown = new EventListener<int>();
		public static EventListener<int> OnRewardedFailed = new EventListener<int>();
	}
}