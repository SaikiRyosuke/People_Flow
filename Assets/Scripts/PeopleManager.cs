using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PeopleManager : MonoBehaviour
{
	public List<People> peopleList = new List<People>();
	public static PeopleManager Instance;
	public GameObject A,B, box;
	public float timeScale = 1.0f;
	public Slider timeScaleSlider;
	public Text timeScaleText;
	public int phiDivision = 50;	
	public float generateRate = 4;
	
	float[] phiArray, npArray, nmArray;
	List<GameObject> phiDisplay = new List<GameObject>();
	float phi;
	
	
    void Awake()
    {
        Instance = this;
		phiDivision = Parameters.Instance.phiDivision;
		generateRate = Parameters.Instance.generateRate;
		for(int i = 0; i < A.transform.childCount; i++){
			peopleList.Add(A.transform.GetChild(i).GetComponent<People>());
		}
		for(int i = 0; i < B.transform.childCount; i++){
			peopleList.Add(B.transform.GetChild(i).GetComponent<People>());
		}
    }
	
	void Start()
	{
		for(int i = 0; i < phiDivision; i++){
			var obj = Instantiate(box, new Vector3(15f,-Parameters.Instance.L + (2*i+1)*Parameters.Instance.L/phiDivision,0), Quaternion.identity);
			obj.transform.localScale = new Vector3(1,2*Parameters.Instance.L/phiDivision,1);
			phiDisplay.Add(obj);
		}
	}

    // Update is called once per frame
    void Update()
    {
		Time.timeScale = timeScale;
		npArray =new float[phiDivision];
		nmArray =new float[phiDivision];
		phiArray =new float[phiDivision];

		foreach(People people in peopleList){
			int k = (int)((people.pos.y + Parameters.Instance.L) * phiDivision / 2 / Parameters.Instance.L );
			if( k < 0 || k >= phiDivision)	continue;
			if(people.type == Type.A)	npArray[k] += 1;
			else if(people.type == Type.B) nmArray[k] += 1;
		}
		
		for(int i = 0; i < phiDivision; i++){
			if(npArray[i] == 0 && nmArray[i] == 0){
				phiArray[i] = 5;
				continue;
			}	
			var np = npArray[i];	var nm = nmArray[i];
			float phiI = (np-nm)/(np+nm);
			SpriteRenderer sr = phiDisplay[i].GetComponent<SpriteRenderer>();
			if(phiI >= 0)	sr.color = new Color(1, 1-phiI, 1-phiI);
			else if(phiI <= 0) sr.color = new Color(1+phiI,1+phiI, 1);
			//方式1(絶対値を取る)
			phiArray[i] = Mathf.Abs(phiI);
			//方式2(2乗する)
			// phiArray[i] = Mathf.Pow(phiI, 2);
		}
		int count = 0;	float sum = 0;
		//5をnullとして、nullを除いた平均をとってみる。
		for(int i = 0; i < phiDivision; i++){
			if(phiArray[i] != 5){
				count++;
				sum += phiArray[i];
			}
		}
		phi = sum / count;
		Debug.Log(phi);
    }
	
	public void AddPeople(People people)
	{
		peopleList.Add(people);
	}
	
	public void RemovePeople(People people)
	{
		peopleList.Remove(people);
	}
	
	public void ChangeTimeScale()
	{
		timeScale = timeScaleSlider.value;
		timeScaleText.text = timeScale.ToString();
	}
	
	public void ResetSim()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
