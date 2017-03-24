using UnityEngine;

public class Start_Menu_Camera : MonoBehaviour
{
	public static Start_Menu_Camera start_Menu_Camera = null;

	void Awake()
	{
		if(Start_Menu_Camera.start_Menu_Camera == null)
		{
			Object.DontDestroyOnLoad(this.gameObject);
			Start_Menu_Camera.start_Menu_Camera = this;
		}
		else if(Start_Menu_Camera.start_Menu_Camera != this)
		{
			Object.DestroyImmediate(this.gameObject);
		}
	}
}
