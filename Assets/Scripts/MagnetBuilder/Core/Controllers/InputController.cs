using CustomUtilities;
using Lean.Touch;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace MagnetBuilder.Core.Controllers
{
	public class InputController : Singleton<InputController>
	{
		//LeanTouch Hooks
		[SerializeField] private LeanTouch touch;
		[SerializeField] private LeanFingerSwipe fingerSwipeLeft;
		[SerializeField] private LeanFingerSwipe fingerSwipeRight;
		[SerializeField] private LeanFingerTap fingerTap;


		//InputManager Events
		public UnityEvent OnSwipeLeft;
		public UnityEvent OnSwipeRight;
		public UnityEvent OnTap;

		[BoxGroup("Settings")] [SerializeField]
		private bool isInputEnabled;

		[BoxGroup("Debugging")] [SerializeField]
		private bool debugEnabled;

		// Start is called before the first frame update
		void Start()
		{
			FindTouchReferences();

			fingerSwipeLeft.OnFinger.AddListener(OnFingerSwipeLeft);
			fingerSwipeRight.OnFinger.AddListener(OnFingerSwipeRight);
			fingerTap.OnFinger.AddListener(OnFingerTap);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				OnSwipeLeft?.Invoke();
			}

			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				OnSwipeRight?.Invoke();
			}

			if (Input.GetKeyDown(KeyCode.Space))
			{
				OnTap?.Invoke();
			}
		}

		[Button(ButtonSizes.Medium)]
		private void FindTouchReferences()
		{
			touch = FindObjectOfType<LeanTouch>();
			fingerSwipeLeft = GameObject.Find("SwipeLeft").GetComponent<LeanFingerSwipe>();
			fingerSwipeRight = GameObject.Find("SwipeRight").GetComponent<LeanFingerSwipe>();
			fingerTap = GameObject.Find("LeanTouch").GetComponent<LeanFingerTap>();
		}

		private void OnFingerSwipeLeft(LeanFinger leanFinger)
		{
			if (isInputEnabled)
			{
				if (debugEnabled)
					Debug.Log("Input: Swipe Left");
				OnSwipeLeft?.Invoke();
			}
			else
			{
				if (debugEnabled)
				{
					Debug.Log("Input not Enabled");
				}
			}
		}

		private void OnFingerSwipeRight(LeanFinger leanFinger)
		{
			if (isInputEnabled)
			{
				if (debugEnabled)
					Debug.Log("Input: Swipe Right");
				OnSwipeRight?.Invoke();
			}
			else
			{
				if (debugEnabled)
				{
					Debug.Log("Input not Enabled");
				}
			}
		}

		private void OnFingerTap(LeanFinger leanFinger)
		{
			if (isInputEnabled)
			{
				if (debugEnabled)
					Debug.Log("Input: Tap");
				OnTap?.Invoke();
			}
			else
			{
				if (debugEnabled)
				{
					Debug.Log("Input not Enabled");
				}
			}
		}

		private void OnDestroy()
		{
			fingerSwipeLeft.OnFinger.RemoveListener(OnFingerSwipeLeft);
			fingerSwipeRight.OnFinger.RemoveListener(OnFingerSwipeRight);
			fingerTap.OnFinger.RemoveListener(OnFingerTap);
		}

		public void EnableInput()
		{
			isInputEnabled = true;
		}

		public void DisableInput()
		{
			isInputEnabled = false;
		}
	}
}