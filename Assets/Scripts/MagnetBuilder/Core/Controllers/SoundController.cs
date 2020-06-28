using CustomUtilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagnetBuilder.Core.Controllers
{
	public class SoundController : Singleton<SoundController>
	{
		[BoxGroup("AudioSources")]
		[SerializeField]
		private AudioSource sfx1;
		[BoxGroup("AudioSources")]
		[SerializeField]
		private AudioSource sfx2;
		[BoxGroup("AudioSources")]
		[SerializeField]
		private AudioSource bgm;

		[BoxGroup("AudioClips")] [SerializeField]
		private AudioClip buttonSound;

		[BoxGroup("Utilities")]
		[Button(ButtonSizes.Medium)]
		private void GenerateSoundControllerElements()
		{
			Debug.Log($"Running");
			if (sfx1==null)
			{
				var obj = new GameObject("SFX1");
				obj.transform.parent = transform;
				obj.AddComponent<AudioSource>();
				sfx1 = obj.GetComponent<AudioSource>();
			}

			if (sfx2==null)
			{
				var obj = new GameObject("SFX2");
				obj.transform.parent = transform;
				obj.AddComponent<AudioSource>();
				sfx2 = obj.GetComponent<AudioSource>();
			}

			if (bgm==null)
			{
				var obj = new GameObject("BGM");
				obj.transform.parent = transform;
				obj.AddComponent<AudioSource>();
				bgm = obj.GetComponent<AudioSource>();
				bgm.loop = true;
			}
		}

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void PlayButtonSound()
		{
			sfx2.PlayOneShot(buttonSound);
		}
	}
}