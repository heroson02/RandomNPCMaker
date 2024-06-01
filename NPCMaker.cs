using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

class NPCMaker : MonoBehaviour
{
	public List<NPC>	npcList;
	public List<Element>	elemList;	//elemType 크기에 맞는 배열 or 리스트?
	
	public createType				type;
	public float 			interval; //특정 시간마다 생성
	public int				createAmount;
	private GameObject currentNPC;
	private GameObject currentNPC_A;

	public float			spawnX = 0;
	public float			spawnY = 0;

	private Dictionary<NPC, (GameObject aObject, GameObject bObject, GameObject cObject)> npcObjects; // NPC와 세 개의 GameObject를 매핑

	// public void CreateNPC()
  // {
  //   NPC newNPC = new NPC(elemList);
  //   npcList.Add(newNPC);
	// 	DisplayNPC(newNPC);
  // }

	public void CreateNPC()
	{
		NPC newNPC = new NPC(elemList); // AType은 필요에 따라 변경
		npcList.Add(newNPC);
		
		// GameObject aObject = DisplayNPC(newNPC, "A");
		// GameObject bObject = DisplayNPC(newNPC, "B");
		GameObject cObject = DisplayNPC(newNPC, "C");

		// newNPC.aAppearance = aObject;
		// newNPC.bAppearance = bObject;
		newNPC.cAppearance = cObject;
		newNPC.currentForm = 2;
		// npcObjects.Add(newNPC, (aObject, bObject, cObject));
	}

	private GameObject DisplayNPC(NPC npc, string mode)
	{
		GameObject parentsObject = new GameObject("NPC_" + npcList.Count);
		foreach (var item in npc.selectedElem)
		{
			GameObject newObject = new GameObject("npc_" + npcList.Count + "_" + item.type);
			newObject.transform.SetParent(parentsObject.transform);

			SpriteRenderer spriteRenderer = newObject.AddComponent<SpriteRenderer>();

			string imagePath = item.imagePaths;
			Sprite sprite = LoadSpriteFromPath("Assets/Artwork/" + imagePath + ".png");
			if (sprite != null)
			{
				spriteRenderer.sprite = sprite;
			}

			switch (item.type)
			{
				case ElemType.Hair:
						newObject.transform.localPosition = new Vector3(0.27f, 10f, 0);
						newObject.transform.localScale = new Vector3(0.9f, 0.9f, 0);
						spriteRenderer.sortingOrder = 3;
						break;
				case ElemType.Hair_Back:
						newObject.transform.localPosition = new Vector3(0.27f, 10f, 0);
						newObject.transform.localScale = new Vector3(0.9f, 0.9f, 0);
						break;
				case ElemType.Head:
						newObject.transform.localPosition = new Vector3(0.3f, 9.8f, 0);
						newObject.transform.localScale = new Vector3(0.7f, 0.7f, 0);
						spriteRenderer.sortingOrder = 2;
						break;
				case ElemType.Eye:
						newObject.transform.localPosition = new Vector3(0, 8.5f, 0);
						spriteRenderer.sortingOrder = 3;
						break;
				case ElemType.Shirts:
						newObject.transform.localPosition = new Vector3(0.2f, 7f, 0);
						spriteRenderer.sortingOrder = 3;
						break;
				case ElemType.Pants:
						newObject.transform.localPosition = new Vector3(0.2f, 3.7f, 0);
						spriteRenderer.sortingOrder = 2;
						break;
				case ElemType.Foot:
						newObject.transform.localPosition = new Vector3(-0.19f, -1.4f, 0);
						spriteRenderer.sortingOrder = 2;
						break;
				case ElemType.basic:
						newObject.transform.localPosition = new Vector3(0, 10f, 0);
						spriteRenderer.sortingOrder = 1;
						break;
				default:
						newObject.transform.localPosition = Vector3.zero;
						break;
			}
		}

		currentNPC = parentsObject;
		parentsObject.transform.localPosition = new Vector2(spawnX, spawnY);
		return parentsObject;
	}

	private Sprite LoadSpriteFromPath(string imagePath)
	{
		// print("imagePath : " + imagePath);
		if (File.Exists(imagePath))
		{
			byte[] imageData = File.ReadAllBytes(imagePath);
			Texture2D texture = new Texture2D(2, 2);
			if (texture.LoadImage(imageData))
			{
					Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 1f));
					return sprite;
			}
		}
		return null;
	}

	void Start()
  {
		npcList = new List<NPC>();
		npcObjects = new Dictionary<NPC, (GameObject, GameObject, GameObject)>();
  }

   // Update is called once per frame
  void Update()
  {
      
  }

	public void TransformNPC()
	{
		// int type = (int)npcList[0].type;
		// if (npcList[0].currentForm == 0)
		// {
		// 	if (currentNPC_A)
		// 		currentNPC_A.SetActive(false);
		// 	if (currentNPC)
		// 		currentNPC.SetActive(true);
		// 	npcList[0].currentForm = 2;
		// }
		// else if (npcList[0].currentForm == 2)
		// {
		// 	GameObject newObject = new GameObject("npc_" + npcList.Count + "_" + type);
		// 	Debug.Log("Assets/Artwork/" + "A_Characters_" + type + ".png");
		// 	Sprite sprite = LoadSpriteFromPath("Assets/Artwork/" + "A_Characters_"+ type + ".png");
		// 	SpriteRenderer spriteRenderer = newObject.AddComponent<SpriteRenderer>();

		// 	if (sprite != null)
		// 	{
		// 		spriteRenderer.sprite = sprite;
		// 	}
		// 	newObject.transform.localPosition = new Vector3(0, 7, 0);
		// 	newObject.transform.localScale = new Vector3(5, 5, 5);
		// 	currentNPC.SetActive(false);
		// 	currentNPC_A = newObject;
		// 	npcList[0].currentForm = 0;
		// 	// spriteRenderer.sortingOrder = 5;
		// }
		
	}

	public void TransformAtoC() {}
	public void TransformCtoA() {}

	public void EraseNPC()
	{
		if (currentNPC)
		{
			DestroyObjectsRecursively(currentNPC);
		}
	}

// public void EraseNPC(NPC npc)
// {
// 	if (npcObjects.TryGetValue(npc, out var npcTuple))
// 	{
// 		DestroyObjectsRecursively(npcTuple.aObject);
// 		DestroyObjectsRecursively(npcTuple.bObject);
// 		DestroyObjectsRecursively(npcTuple.cObject);
// 		npcObjects.Remove(npc);
// 		npcList.Remove(npc);
// 	}
// }

	public void DestroyObjectsRecursively(GameObject obj)
    {
        // 자식 오브젝트들 삭제
        foreach (Transform child in obj.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // 오브젝트 자신 삭제
        GameObject.Destroy(obj);
    }

	public void PrintAllElements()
	{
		Debug.Log("Printing all elements:");
		foreach (Element element in elemList)
		{
			Debug.Log(element.ToString());
		}
	}
}

enum createType //몇 틱마다, 몇 초마다
{
	Tick = 1,
	Second = 2
}
