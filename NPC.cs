using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;

public class NPC //여러 element가 조합된 완전한 NPC
{
	public AType type;
	public List<Element>	selectedElem;

	public int currentForm;

	public GameObject aAppearance;
	public GameObject bAppearance;
	public GameObject cAppearance;

	//elemList에서 타입별로 imagePath에서 하나씩 
  void Start()
  {
      
  }

  void Update()
  {
      
  }

	public NPC(List<Element> _elemList)
	{
		type = GetRandomType();
		Debug.Log("NPC Type : " + type);

		var filteredElements = FilterElementsByAType(type, _elemList);
		selectedElem = SelectElementsForNPC(filteredElements);
		// PrintSelectedElements();
	}

	public AType GetRandomType()
	{
		System.Random random = new System.Random();

		// AType 열거형에서 모든 값의 배열을 가져온 후, 그 길이를 기준으로 무작위 인덱스 생성
		AType[] typeValues = (AType[])Enum.GetValues(typeof(AType));
		AType randomType = typeValues[random.Next(typeValues.Length)];

		return randomType;
	}

	// AType에 따라 Element를 분류하는 메소드
	private List<Element> FilterElementsByAType(AType type, List<Element> allElements)
	{
			return allElements.Where(e => e.targetA.Contains(type)).ToList();
	}

	public void PrintSelectedElements()
	{
		Debug.Log($"NPC Type: {type}");
		foreach (var elem in selectedElem)
		{
			Debug.Log($"Element Type: {elem.type}, Image Path: {elem.imagePaths}");
		}
	}


	public List<Element> SelectElementsForNPC(List<Element> elemList)
  {
    List<Element> selectedElements = new List<Element>();
    foreach (ElemType elemType in Enum.GetValues(typeof(ElemType)))
    {
        Element selectedElement = SelectElementByType(elemType, elemList);
        if (selectedElement != null)
        {
            selectedElements.Add(selectedElement);
        }
    }
		
    return selectedElements;
  }

	private Element SelectElementByType(ElemType type, List<Element> elemList)
	{
		List<Element> elementsOfType = new List<Element>();
		foreach (Element element in elemList)
		{
				if (element.type == type)
				{
						elementsOfType.Add(element);
				}
		}
		if (elementsOfType.Count > 0)
		{
				int randomIndex = UnityEngine.Random.Range(0, elementsOfType.Count);
				return elementsOfType[randomIndex];
		}
		return null;
	}
}