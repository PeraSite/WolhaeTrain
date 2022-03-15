using Aarthificial.Reanimation;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {
	[Name("Set Parameter Integer")]
	[Category("Reanimator")]
	public class ReanimatorSetInt : ActionTask<Reanimator> {
		public string parameter;
		public BBParameter<int> setTo;

		protected override string info {
			get { return string.Format("Reanim.SetInt {0} to {1}", parameter, setTo); }
		}

		protected override void OnExecute() {
			agent.Set(parameter, setTo.value);
			EndAction();
		}
	}
}
