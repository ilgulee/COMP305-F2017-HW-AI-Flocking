using UnityEngine;

namespace Assets
{
	public class AllUnits : MonoBehaviour
	{
		public GameObject[] Units;

		public GameObject BoidPrefab;

		public int NumberOfUnits = 10;

		public Vector3 DefaultRange=new Vector3(5,5,5);

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
				var unitPosition=new Vector3(Random.Range(-DefaultRange.x,DefaultRange.y),
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
