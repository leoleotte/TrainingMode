using Modding;
using UnityEngine;

namespace TrainingMode
{
    public class TrainingMode : Mod
	{
		#region Variables
		GameObject _trainingModeManager;
		#endregion

		#region Initialization
		public override string GetVersion()
		{
			return string.Format("0.0.0.1 - {0}", System.DateTime.Now.ToShortTimeString());
		}

		public override void Initialize()
		{
			Log("TrainingMode started!");

			//inject our friendly manager into the scene
			_trainingModeManager = new GameObject("TrainingModeManager", typeof(TrainingModeManager));

			//register events
			ModHooks.Instance.HeroUpdateHook += _trainingModeManager.GetComponent<TrainingModeManager>().OnHeroUpdated;
		}
		#endregion
	}
}
