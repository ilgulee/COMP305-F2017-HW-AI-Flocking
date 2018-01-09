using UnityEngine;

namespace Assets
{
	public class AllUnits : MonoBehaviour
	{
		public GameObject[] Units;

		public GameObject BoidPrefab;

		public int NumberOfUnits = 10;

		public Vector3 DefaultRange=new Vector3(5,5,5);
	    public bool SeekGoal = true;
	    public bool Obedient = true;
	    public bool Willful = false;

	    [Range(0, 200)] public int NeighbourDistance = 50;
	    [Range(0, 2)] public float MaxForce = 0.5f;
	    [Range(0, 5)] public float MaxVelocity = 2.0f;

		void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(this.transform.position,DefaultRange*2);
			Gizmos.color=Color.green;
			Gizmos.DrawWireSphere(this.transform.position,0.2f);
		}

		// Use this for initialization
		void Start () {
			Units=new GameObject[NumberOfUnits];
			for (int i = 0; i < Units.Length; i++)
			{
				var unitPosition=new Vector3(Random.Range(-DefaultRange.x,DefaultRange.x),
												 Random.Range(-DefaultRange.y,DefaultRange.y),
												 Random.Range(0,0));
				Units[i] = Instantiate(BoidPrefab, this.transform.position + unitPosition, Quaternion.identity);
				Units[i].GetComponent<UnitBehavior>().UnitManager = this.gameObject;
			}
		}
	
		// Update is called once per frame
		void Update () {
		
		}
	}
}
