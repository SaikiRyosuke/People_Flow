using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{
    public Type type = Type.A;
	public GameObject people;
	
	IEnumerator Start()
	{
		for(int i = 0; i < this.transform.childCount; i++){
			var obj = this.transform.GetChild(i);
			if(type == Type.A){
				obj.GetComponent<People>().velocity.x = 1f;
				obj.localScale = obj.GetComponent<People>().radius*2 * new Vector3(1,1,1);
			}	
			if(type== Type.B){
				obj.GetComponent<People>().velocity.x = -1f;
				obj.localScale = obj.GetComponent<People>().radius*2 * new Vector3(1,1,1);
			}
		}
		while(true){
			yield return new WaitForSeconds(1/PeopleManager.Instance.generateRate);
			GameObject obj = people;
			if(type == Type.A)	obj = Instantiate(people, new Vector3(-Parameters.Instance.L-10,Random.Range(-Parameters.Instance.L+2,Parameters.Instance.L-2),0), Quaternion.identity );
			if(type == Type.B)	obj = Instantiate(people, new Vector3(Parameters.Instance.L+10,Random.Range(-Parameters.Instance.L+2,Parameters.Instance.L-2),0), Quaternion.identity );
			obj.transform.parent = this.transform;
			obj.transform.GetComponent<People>().pos = obj.transform.position;
			PeopleManager.Instance.AddPeople(obj.transform.GetComponent<People>());
			
		}
		// yield return null;
	}
	
}
