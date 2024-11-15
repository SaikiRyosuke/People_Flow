using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
	A,B,C,D
}

public class Parameters : MonoBehaviour
{
	public float L =10f;
	public int phiDivision = 50;
	public float generateRate = 4;


	//peoples individual parameters
	public float tau = 0.5f; //tau: relaxation time
	public float v0 = 2f; //v0: desired speed
	public float xiAmp = 3f;

	public float Aalpha = 1f; 	//Aalpha: the altitude of soc-force
	public float radius = 0.5f; //radius: the radius of the person's body
	public float BAlpha = 1f; //bAlpha: decrease rate in socio-phychological force
	public float lambdaAlpha = 0.1f; //lambdaAlpha: description of anisotropic force
	public float k = 10f; //k: body counteracting force
	public float kappa = 5f; //kappa: sliding friction force
	
	public static Parameters Instance;
	void Awake()
	{
		Instance = this;
		ParameterManager pm = new ParameterManager();
		string loadData = Resources.Load<TextAsset>("parameter").ToString();
		pm = JsonUtility.FromJson<ParameterManager>(loadData);
		
		//setting
		L = pm.L;
		phiDivision = pm.phiDivision;
		generateRate = pm.generateRate;
		tau = pm.tau;
		v0 = pm.v0;
		Aalpha = pm.Aalpha;
		radius = pm.radius;
		BAlpha = pm.BAlpha;
		lambdaAlpha = pm.lambdaAlpha;
		k = pm.k;
		kappa = pm.kappa;
	}
}

[SerializeField]
public class ParameterManager
{
	//System paramters
	public float L =10f;
	public int phiDivision = 50;
	public float generateRate = 4;


	//peoples individual parameters
	public float tau = 0.5f; //tau: relaxation time
	public float v0 = 2f; //v0: desired speed
	public float xiAmp = 3f;

	public float Aalpha = 1f; 	//Aalpha: the altitude of soc-force
	public float radius = 0.1f; //radius: the radius of the person's body
	public float BAlpha = 1f; //bAlpha: decrease rate in socio-phychological force
	public float lambdaAlpha = 0.1f; //lambdaAlpha: description of anisotropic force
	public float k = 10f; //k: body counteracting force
	public float kappa = 5f; //kappa: sliding friction force

}
