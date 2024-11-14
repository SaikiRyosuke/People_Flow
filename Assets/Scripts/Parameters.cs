using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
	A,B,C,D
}

public class Parameters : MonoBehaviour
{
	public float Lx = 50f;
	public float Ly = 10f;
	public int phiDivision = 50;
	public float generateRate = 4;


	//peoples individual parameters
	public float tau = 0.5f; //tau: relaxation time
	public float v0 = 2f; //v0: desired speed
	public float xiAmp = 3f;

	public float Aalpha_same = 0.1f; 	//Aalpha: the altitude of soc-force
	public float Aalpha_opp = 1f; 	//Aalpha: the altitude of soc-force
	public float radius = 0.5f; //radius: the radius of the person's body
	public float BAlpha = 1f; //bAlpha: decrease rate in socio-phychological force
	public float lambdaAlpha = 0.1f; //lambdaAlpha: description of anisotropic force
	public float k = 10f; //k: body counteracting force
	public float kappa = 5f; //kappa: sliding friction force
	public float CAlpha = 1;
	public float d_dir = 1f;
	
	public static Parameters Instance;
	void Awake()
	{
		Instance = this;
		ParameterManager pm = new ParameterManager();
		string loadData = Resources.Load<TextAsset>("parameter").ToString();
		pm = JsonUtility.FromJson<ParameterManager>(loadData);
		
		//setting
		Lx = pm.Lx;
		Ly = pm.Ly;
		phiDivision = pm.phiDivision;
		generateRate = pm.generateRate;
		tau = pm.tau;
		v0 = pm.v0;
		Aalpha_same = pm.Aalpha_same;
		Aalpha_opp = pm.Aalpha_opp;
		radius = pm.radius;
		BAlpha = pm.BAlpha;
		lambdaAlpha = pm.lambdaAlpha;
		k = pm.k;
		kappa = pm.kappa;
		CAlpha = pm.CAlpha;
		d_dir = pm.d_dir;
	}
}

[SerializeField]
public class ParameterManager
{
	//System paramters
	public float Lx = 100f;
	public float Ly = 10f;
	public int phiDivision = 50;
	public float generateRate = 4;


	//peoples individual parameters
	public float tau = 0.5f; //tau: relaxation time
	public float v0 = 2f; //v0: desired speed
	public float xiAmp = 3f;

	public float Aalpha_same = 1f; 	//Aalpha: the altitude of soc-force
	public float Aalpha_opp = 1f; 	//Aalpha: the altitude of soc-force
	public float radius = 0.1f; //radius: the radius of the person's body
	public float BAlpha = 1f; //bAlpha: decrease rate in socio-phychological force
	public float lambdaAlpha = 0.1f; //lambdaAlpha: description of anisotropic force
	public float k = 10f; //k: body counteracting force
	public float kappa = 5f; //kappa: sliding friction force
	public float CAlpha = 1f;
	public float d_dir = 2f;

}
