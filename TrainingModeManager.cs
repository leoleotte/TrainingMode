using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingMode
{
	public class TrainingModeManager : MonoBehaviour
	{
		#region Variables
		GameObject _trainingCanvas;
		Text _titleLabel;
		#endregion

		#region Unity Methods
		void Awake()
		{
			DontDestroyOnLoad(gameObject);
			UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnSceneChanged;
		}

		void Start()
		{
			Modding.Logger.Log("NEW BEHAVIOUR ACHIEVED");
		}

		void FixedUpdate()
		{
			//Modding.Logger.Log(string.Format("Fixed Update tick - {0}", Time.frameCount));
		}
		#endregion

		private void InitializeUI()
		{
			if (_trainingCanvas?.gameObject == null)
			{
				_trainingCanvas = CanvasUtil.CreateCanvas(RenderMode.ScreenSpaceOverlay, 1);
				//hold it under our wings to survive scene loads
				_trainingCanvas.transform.SetParent(gameObject.transform);
				_trainingCanvas.name = "TrainingCanvas";
			}

			Modding.Logger.Log(string.Format("Canvas active: {0}", _trainingCanvas.gameObject.activeInHierarchy));

			_trainingCanvas.SetActive(true);

			if (_titleLabel?.gameObject == null)
				_titleLabel = CanvasUtil.CreateTextPanel(_trainingCanvas.gameObject, "TrainingMode - Runtime", 12, TextAnchor.UpperRight,
					new CanvasUtil.RectData(new Vector2(200, 100), new Vector2(100, 100)))
					.GetComponentInChildren<Text>();

			_titleLabel.gameObject.SetActive(true);
		}

		private IEnumerator WaitForSceneChangedFrame(Action onFrameWait)
		{
			yield return new WaitForFixedUpdate();
			onFrameWait.Invoke();
		}



		#region Event Methods
		public void OnHeroUpdated()
		{
			var heroState = HeroController.instance.CanSuperDash();
			if (_titleLabel != null)
				_titleLabel.text = string.Format("CanSuperDash: {0}", heroState);
			//Modding.Logger.Log(string.Format("Hero state: {0}", heroState));
		}

		public void OnSceneChanged(UnityEngine.SceneManagement.Scene oldScene, UnityEngine.SceneManagement.Scene newScene)
		{
			//shitty callback for thread
			StartCoroutine(WaitForSceneChangedFrame(OnFrameWait));

			void OnFrameWait()
			{
				InitializeUI();
			}
		}
		#endregion
	}
}
