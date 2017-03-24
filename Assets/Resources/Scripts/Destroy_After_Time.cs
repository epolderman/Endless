using UnityEngine;

public class Destroy_After_Time : MonoBehaviour {
	[Tooltip("The amount of time in seconds for the gameObject attached to this script to exist before being destroyed.")]
	public float duration;

	void Start ()
	{
		Object.Destroy(this.gameObject, this.duration);
	}
}
