using UnityEngine;
using System.Collections;

public class AnimatorPlayer : MonoBehaviour {
	private Animator Animator_;
	void OnEnable()
	{
		if (Animator_ == null)
			Animator_ = GetComponent<Animator> ();
	}

	public void PlayThisAnima(string anim)
	{
		Animator_.SetTrigger (anim);
	}

	public void PlayCartMenuAnim()
	{
		if (Animator_.GetCurrentAnimatorStateInfo (0).IsName ("cartIn"))
			Animator_.SetTrigger ("cartOut");
		else
			Animator_.SetTrigger ("cartIn");
	} 
}
