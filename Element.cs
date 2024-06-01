using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewElement", menuName = "Custom/Element")]
public class Element : ScriptableObject
{
	public ElemType 		type;
	public List<AType>	targetA;
	public string	imagePaths;

	public override string ToString()
	{
			return $"Type: {type}, Image Paths: {imagePaths}";
	}
}

//NPC의 각 파츠를 정의한 enum
public enum ElemType
{
	Hair = 1,
	Head = 2,
	Eye = 3,
	Shirts = 4,
	Pants = 5,
	Foot = 6,
	Hair_Back = 7,
	basic = 8
}

public enum AType
{
	//A는 남자, B는 여자 캐릭터.
	A = 0,
	B = 1
}