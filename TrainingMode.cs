using Modding;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingMode
{
    public class TrainingMode : Mod
    {
		#region Variables
		GameObject _trainingCanvas;
		Text _titleLabel;
		#endregion

		#region Initialization
		public override string GetVersion()
		{
			return "0.0.0.1";
		}

		public override void Initialize()
		{
			Log("TrainingMode started!");

			//register events
			GameManager.instance.OnFinishedEnteringScene += OnSceneEnter;
			ModHooks.Instance.HeroUpdateHook += OnHeroUpdated;
		}

		private void InitializeUI()
		{
			if (_trainingCanvas == null)
				_trainingCanvas = CanvasUtil.CreateCanvas(RenderMode.ScreenSpaceOverlay, 1);

			if (_titleLabel == null)
				_titleLabel = CanvasUtil.CreateTextPanel(_trainingCanvas.gameObject, "TrainingMode - Runtime", 12, TextAnchor.UpperRight, 
					new CanvasUtil.RectData(new Vector2(200, 100), new Vector2(100, 100)))
					.GetComponentInChildren<Text>();
		}
		#endregion

		#region Event Methods
		private void OnSceneEnter()
		{
			Log("SceneEntered");
			InitializeUI();
		}

		private void OnHeroUpdated()
		{
			var heroState = HeroController.instance.CanSuperDash();
			if (_titleLabel != null)
				_titleLabel.text = string.Format("CanSuperDash: {0}", heroState);
			//Log(string.Format("Hero state: {0}", heroState));
		}
		#endregion
	}
}
