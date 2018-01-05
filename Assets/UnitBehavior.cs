using UnityEngine;

namespace Assets
{
	public class UnitBehavior : MonoBehaviour
	{
		public GameObject UnitManager;
		public Vector2 Location=Vector2.zero;

		public Vector2 Velocity;
	    private Vector2 _goalPosition=Vector2.zero;

	    private Vector2 _currentForce;

		// Use this for initialization
		void Start () {
			Velocity=new Vector2(Random.Range(0.01f,0.1f),Random.Range(0.01f,0.1f));
			Location=new Vector2(this.gameObject.transform.position.x,this.gameObject.transform.position.y);
		}

		Vector2 Seek(Vector2 target)
		{
			return(target - Location);
		}

		void ApplyForce(Vector2 f)
		{
			Vector3 force=new Vector3(f.x,f.y,0);
			this.GetComponent<Rigidbody2D>().AddForce(force);
			Debug.DrawRay(this.transform.position,force,Color.blue);
		}

		void Flock()
		{
			Location = this.transform.position;
			Velocity = this.GetComponent<Rigidbody2D>().velocity;
			var goal = Seek(_goalPosition);
			_currentForce = goal;
			_currentForce = _currentForce.normalized;

			ApplyForce(_currentForce);
		}
		// Update is called once per frame
		void Update () {
			Flock();
			_goalPosition = UnitManager.transform.position;
		}
	}
}
