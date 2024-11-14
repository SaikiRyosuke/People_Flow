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
			}	
			if(type== Type.B){
				obj.GetComponent<People>().velocity.x = -1f;
			}
		}
		while(true){
			yield return new WaitForSeconds(1/PeopleManager.Instance.generateRate);
			GameObject obj = people;
			//発生位置をランダムに決める
			// if(type == Type.A)	obj = Instantiate(people, new Vector3(-Parameters.Instance.Lx-10,Random.Range(-Parameters.Instance.Ly+5,Parameters.Instance.Ly-5),0), Quaternion.identity );
			// if(type == Type.B)	obj = Instantiate(people, new Vector3(Parameters.Instance.Lx+10,Random.Range(-Parameters.Instance.Ly+5,Parameters.Instance.Ly-5),0), Quaternion.identity );
			// obj.transform.parent = this.transform;
			// obj.transform.GetComponent<People>().pos = obj.transform.position;
			// PeopleManager.Instance.AddPeople(obj.transform.GetComponent<People>());
			
			//発生を同じ場所で固定する。
			if(type == Type.A)	for(int i = -(int)Parameters.Instance.Ly + 2; i <= (int)Parameters.Instance.Ly - 2; i+=5){
				obj = Instantiate(people, new Vector3(-Parameters.Instance.Lx-10, i, 0), Quaternion.identity);
				obj.transform.parent = this.transform;
				obj.transform.GetComponent<People>().pos = obj.transform.position;
				PeopleManager.Instance.AddPeople(obj.transform.GetComponent<People>());
			}
			else if(type == Type.B)	for(int i = -(int)Parameters.Instance.Ly + 5; i <= (int)Parameters.Instance.Ly - 2; i+=5){
				obj = Instantiate(people, new Vector3( Parameters.Instance.Lx+10, i, 0), Quaternion.identity);
				obj.transform.parent = this.transform;
				obj.transform.GetComponent<People>().pos = obj.transform.position;
				PeopleManager.Instance.AddPeople(obj.transform.GetComponent<People>());
			}
			
		}

		// yield return null;
	}
	
}
