using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagnetBuilder.Core.Controllers.UIControllers
{
	public class UIPanel : MonoBehaviour
	{
		[SerializeField] private List<DOTweenAnimation> animations=new List<DOTweenAnimation>();

		private int m_counter;
		private int m_direction;

		public Action backwardsComplete;
		public Action forwardComplete;
		[SerializeField] private UIPanelType type;

		[TabGroup("Debug")] [SerializeField] private bool isDebugMode;

		private void OnValidate()
		{
			GetAllTweens();
		}

		public UIPanelType Type
		{
			get => type;
			set => type = value;
		}

		public void ShowPanel()
		{
			TestPlayForward();
		}

		public void HidePanel()
		{
			TestPlayBackwards();
		}

		[TabGroup("Utilities")]
		[Button(ButtonSizes.Medium)]
		private void GetAllTweens()
		{
			animations = GetComponentsInChildren<DOTweenAnimation>().ToList();

			//FilterLoopingTweens
			var filteredAnimations = new List<DOTweenAnimation>();

			foreach (var doTweenAnimation in animations.Where(doTweenAnimation => doTweenAnimation.loops==1))
			{
				doTweenAnimation.autoKill = false;
				doTweenAnimation.autoPlay = false;
				filteredAnimations.Add(doTweenAnimation);
			}

			animations = filteredAnimations;
		}

		private void IncrementCounter()
		{
			m_counter++;

			if (isDebugMode)
			{
				Debug.Log($"Counter: {m_counter}\nTotal: {animations.Count}");
			}

			if (m_counter < animations.Count) return;

			if (m_direction == 1)
			{
				if (isDebugMode)
					Debug.Log("ForwardComplete");
				forwardComplete?.Invoke();
			}
			else
			{
				if (isDebugMode)
					Debug.Log("BackwardsComplete");
				backwardsComplete?.Invoke();
			}
		}

		//Test Area
		[TabGroup("Debug")]
		[Button(ButtonSizes.Small)]
		private void TestPlayForward()
		{
			m_counter = 0;
			m_direction = 1;

			try
			{
				foreach (var tween in animations.Select(doTweenAnimation => doTweenAnimation.GetTweens()).SelectMany(tweens => tweens))
				{
					if (tween != null) tween.onComplete += IncrementCounter;
					// tween.OnComplete(IncrementCounter);
				}

				if (isDebugMode)
					Debug.Log("PlayingForwards");
				foreach (var doTweenAnimation in animations) doTweenAnimation.DOPlayForward();
			}
			catch (Exception e)
			{
				Debug.LogError($"The parent: {name}:{type.ToString()} has no Child Tweens \n {e}");
			}
		}

		[TabGroup("Debug")]
		[Button(ButtonSizes.Small)]
		private void TestPlayBackwards()
		{
			m_counter = 0;
			m_direction = -1;

			try
			{
				foreach (var tween in animations.Select(doTweenAnimation => doTweenAnimation.GetTweens()).SelectMany(tweens => tweens))
				{
					if (tween != null) tween.onRewind += IncrementCounter;
					// tween.OnRewind(IncrementCounter);
				}
				if (isDebugMode)
					Debug.Log("PlayingBackwards");
				foreach (var doTweenAnimation in animations) doTweenAnimation.DOPlayBackwards();
			}
			catch (Exception e)
			{
				Debug.LogError($"The parent: {name}:{type.ToString()} has no Child Tweens \n {e}");
			}
		}

		private void OnDisable()
		{
			// foreach (var tween in animations.Select(doTweenAnimation => doTweenAnimation.GetTweens()).SelectMany(tweens => tweens))
			// {
			// 	tween.onComplete-=IncrementCounter;
			// 	tween.onRewind -= IncrementCounter;
			// 	// tween.OnComplete(IncrementCounter);
			// }
		}
	}
}