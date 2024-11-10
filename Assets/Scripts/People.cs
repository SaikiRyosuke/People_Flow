using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class People : MonoBehaviour
{
	public Type type;
	public Vector3 velocity = new Vector3();
	Vector3 f_soc = new Vector3(0,0,0);
	Vector3 f_ph = new Vector3(0,0,0);
	Vector3 f_b_soc = new Vector3(0,0,0);
	Vector3 f_b_ph = new Vector3(0,0,0);
	Vector3 e = new Vector3(0,0,0);
	public Vector3 pos = new Vector3(0,0,0);
	
	float r = 0f, d = 0f, deltaV = 0f, cos_phiAlphaBeta = 0f;
	Vector3 n = new Vector3(0,0,0), t = new Vector3(0,0,0);
	

	//parameters follow
	private float tau = 0.5f; //tau: relaxation time
	private Vector3 xi = new Vector3(0,0,0); //xi: fluctuation
	private float v0 = 2f; //v0: desired speed
	private Vector3 e0 = new Vector3(0,0,0);
	private float xiAmp = 3f;
	
	private float Aalpha = 1f; 	//Aalpha: the altitude of soc-force
	private float radius = 0.5f; //radius: the radius of the person's body
	private float BAlpha = 1f; //bAlpha: decrease rate in socio-phychological force
	private float lambdaAlpha = 0.1f; //lambdaAlpha: description of anisotropic force
	private float k = 10f; //k: body counteracting force
	private float kappa = 5f; //kappa: sliding friction force


	void Awake()
	{
		pos = this.transform.position;
	}

	void Start()
	{
		type = transform.parent.gameObject.GetComponent<Parent>().type;
		if(type == Type.A)	e0 = new Vector3(1,0,0);
		if(type == Type.B)	e0 = new Vector3(-1,0,0);
		velocity = v0*e0;
		/*startParent = start.GetComponent<Parent>();
		goalParent = start.GetComponent<Parent>();*/

	}

	void Update()
	{
		pos = this.transform.position;

		//目的地に到達したら削除
		if(type == Type.A && pos.x > Parameters.L){
			PeopleManager.Instance.RemovePeople(this);
			Destroy(this.gameObject);
		}	
		if(type == Type.B && pos.x < -Parameters.L){
			PeopleManager.Instance.RemovePeople(this);
			Destroy(this.gameObject);
		}	
		
		//物理量計算パート
		xi = new Vector3(Random.Range(-xiAmp,xiAmp), Random.Range(-xiAmp,xiAmp),0);
		e = velocity/velocity.magnitude;
		
		//2次元バージョン限定の処理。z座標を0にする。
		pos.z = 0; velocity.z = 0;
		//ここまで
		f_ph = new Vector3(0,0,0); f_soc = new Vector3(0,0,0);
		foreach(People people in PeopleManager.Instance.peopleList){
			if(people == this)	continue;
			else{
				r = this.radius + people.radius;
				d = (this.pos - people.pos).magnitude;
				if((r-d)/BAlpha < -5 || d == 0)	continue;
				n = (this.pos - people.pos) / d;
				t = new Vector3(-n.y, n.x);
				// Debug.Log(d); Debug.Log(people); Debug.Log(people.pos); Debug.Log(people.transform.position);
				deltaV = Vector3.Dot(people.velocity - this.velocity, t);
				cos_phiAlphaBeta = -(n.x * e.x + n.y * e.y);
				// Socio-phychological Force
				f_soc += Aalpha * Mathf.Exp((r-d)/BAlpha) * (lambdaAlpha + (1-lambdaAlpha)*(1+cos_phiAlphaBeta)/2) * n;
				// physical force
				f_ph += k* Theta(r-d) * n + kappa * Theta(r-d) * deltaV * t;
			}
			
		}
		
		//interactions from upper boundary(y = 10)
		r = this.radius;
		d = Parameters.L - this.pos.y;
		n = new Vector3(0f,-1f);
		t = new Vector3(-n.y, n.x);
		deltaV = Vector3.Dot(-this.velocity, t);
		f_b_soc = Aalpha * Mathf.Exp((r-d)/BAlpha) * (lambdaAlpha + (1-lambdaAlpha)*(1+cos_phiAlphaBeta)/2) * n;
		f_ph = k* Theta(r-d) * n + kappa * Theta(r-d) * deltaV * t;
		
		//interactions from lower boundary(y = -10)
		r = this.radius;
		d = this.pos.y + Parameters.L;
		n = new Vector3(0f,1f);
		t = new Vector3(-n.y, n.x);
		deltaV = Vector3.Dot(-this.velocity, t);
		f_b_soc += Aalpha * Mathf.Exp((r-d)/BAlpha) * (lambdaAlpha + (1-lambdaAlpha)*(1+cos_phiAlphaBeta)/2) * n;
		f_ph += k* Theta(r-d) * n + kappa * Theta(r-d) * deltaV * t;
		
		//velocity計算
		velocity += ( (v0*e0 + xi - velocity)/tau + Vector3.ClampMagnitude(f_soc + f_ph + f_b_soc + f_b_ph,10f) )* Time.deltaTime;
		pos += velocity * Time.deltaTime;
		this.transform.position = pos;
	}

	float Theta(float z)
	{
		if(z >= 0)	return z;
		else return 0f;
	}


}
