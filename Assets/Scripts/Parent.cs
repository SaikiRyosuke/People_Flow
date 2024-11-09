using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{
    public Type type = Type.A;
	public GameObject people;
	public float generateRate = 2;
	
	IEnumerator Start()
	{
		for(int i = 0; i < this.transform.childCount; i++){
			if(type == Type.A)	this.transform.GetChild(i).GetComponent<People>().velocity.x = 1f;
			if(type== Type.B) this.transform.GetChild(i).GetComponent<People>().velocity.x = -1f;
		}
		while(true){
			yield return new WaitForSeconds(1/generateRate);
			GameObject obj = people;
			if(type == Type.A)	obj = Instantiate(people, new Vector3(-Parameters.L-10,Random.Range(-Parameters.L+2,Parameters.L-2),0), Quaternion.identity );
			if(type == Type.B)	obj = Instantiate(people, new Vector3(Parameters.L+10,Random.Range(-Parameters.L+2,Parameters.L-2),0), Quaternion.identity );
			obj.transform.parent = this.transform;
			obj.transform.GetComponent<People>().pos = obj.transform.position;
			PeopleManager.Instance.AddPeople(obj.transform.GetComponent<People>());
			
		}
		// yield return null;
	}
	
}
