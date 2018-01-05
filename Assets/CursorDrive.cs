using UnityEngine;

namespace Assets
{
	public class CursorDrive : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update ()
		{
			transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}
}
