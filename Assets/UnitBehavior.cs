using UnityEngine;
using UnityEngine.UI;

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
			if (force.magnitude > UnitManager.GetComponent<AllUnits>().MaxForce)
			{
				force = force.normalized;
				force *= UnitManager.GetComponent<AllUnits>().MaxForce;
			}
			this.GetComponent<Rigidbody2D>().AddForce(force);

			if (this.GetComponent<Rigidbody2D>().velocity.magnitude > UnitManager.GetComponent<AllUnits>().MaxVelocity)
			{
				this.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity.normalized;
				this.GetComponent<Rigidbody2D>().velocity *= UnitManager.GetComponent<AllUnits>().MaxVelocity;
			}
			Debug.DrawRay(this.transform.position,force,Color.blue);
		}

		void Flock()
		{
			Location = this.transform.position;
			Velocity = this.GetComponent<Rigidbody2D>().velocity;
			if (UnitManager.GetComponent<AllUnits>().Obedient && Random.Range(0, 50) <= 1)
			{
				Vector2 ali = Align();
				Vector2 coh = Cohesion();
				if (UnitManager.GetComponent<AllUnits>().SeekGoal)
				{
					var goal = Seek(_goalPosition);
					_currentForce = goal + ali + coh;
				}
				else
				{
					_currentForce = ali + coh;
				}
				
				_currentForce = _currentForce.normalized;
			}
			if (UnitManager.GetComponent<AllUnits>().Willful && Random.Range(0, 50) <= 1)
			{
				if (Random.Range(0, 50) < 1)
				{
					_currentForce=new Vector2(Random.Range(0.01f,0.1f),Random.Range(0.01f,0.1f));
				}
			}
			ApplyForce(_currentForce);
		}
		// Update is called once per frame
		void Update () {
			Flock();
			_goalPosition = UnitManager.transform.position;
		}

		Vector2 Align()
		{
			float neighbordist = UnitManager.GetComponent<AllUnits>().NeighbourDistance;
			Vector2 sum = Vector2.zero;
			int count = 0;
			foreach (GameObject other in UnitManager.GetComponent<AllUnits>().Units)
			{
				if (other == this.gameObject) continue;
				float d = Vector2.Distance(Location, other.GetComponent<UnitBehavior>().Location);
				if (d < neighbordist)
				{
					sum += other.GetComponent<UnitBehavior>().Velocity;
					count++;
				}
			}
			if (count > 0)
			{
				sum /= count;
				Vector2 steer = sum - Velocity;
				return steer;
			}
			return Vector2.zero;
		}

		Vector2 Cohesion()
		{
			float neighbordist = UnitManager.GetComponent<AllUnits>().NeighbourDistance;
			Vector2 sum = Vector2.zero;
			int count = 0;
			foreach (GameObject other in UnitManager.GetComponent<AllUnits>().Units)
			{
				if (other == this.gameObject) continue;
				float d = Vector2.Distance(Location, other.GetComponent<UnitBehavior>().Location);
				if (d < neighbordist)
				{
					sum += other.GetComponent<UnitBehavior>().Velocity;
					count++;
				}
			}
			if (count > 0)
			{
				sum /= count;
				return Seek(sum);
			}
			return Vector2.zero;
		}
	}
}
