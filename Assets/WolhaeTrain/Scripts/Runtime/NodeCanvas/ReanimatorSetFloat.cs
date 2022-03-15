using Aarthificial.Reanimation;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {
	[Name("Set Parameter Float")]
	[Category("Reanimator")]
	public class ReanimatorSetFloat : ActionTask<Reanimator> {
		public string parameter;
		public BBParameter<float> setTo;

		protected override string info {
			get { return string.Format("Reanim.SetFloat {0} to {1}", parameter, setTo); }
		}

		protected override void OnExecute() {
			agent.Set(parameter, setTo.value);
			EndAction();
		}
	}
}
