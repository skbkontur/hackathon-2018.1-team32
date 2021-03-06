﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{

	public bool IsHaveKey;
	public bool D3OpenMessage;
	
	public GameObject Web;
	public GameObject Chest;
	public GameObject Torch;
	public GameObject Rat;
	public GameObject Ghost;
	public GameObject DialogOk;
	public GameObject Camera;
	public GameObject FightLabel;
	
	
	private const int NumberOfRooms = 5;
	private const int NumberOfWeb = 10;
	private const int NumberOfChests = 5;
	private const int NumberOfTorch = 10;
	private const int NumberOfEnemies = 5;
	
	
	private const int Height = 31;
	private const int Width = 60;

	private const int Left = (-2 * Width);
	private const int Top = (-4 * Height);

	private room[] rooms;
	
	private struct DialogStruct
	{
		private string msg;
		private string btnMsg;
	}
	
	private struct Room
	{
		public GameObject web;
		public GameObject chest;
		public GameObject torch;
		public GameObject Enemy;
		public DialogStruct GameEvent;
	}
	
	private Room[,] Level = new Room[NumberOfRooms,NumberOfRooms];
	

	
	// Use this for initialization
	void Start ()
    {
        Random.InitState(DateTime.Now.Millisecond);
        rooms = FindObjectsOfType<room>();
        print("Start Game");
		BuildLevel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void generateWeb()
	{
		for (var i = 0; i < NumberOfWeb; i++)
		{
			var x = Random.Range(0, NumberOfRooms);
			var y = Random.Range(0, NumberOfRooms);
			if (Level[x, y].web != null)
				continue;

			var w = Instantiate(Web);
			w.transform.position = new Vector3(x * Width + Left - 22, y * Height + Top + 8, 0);
			Level[x, y].web = w;
		}
	}

	private void generateChest()
	{
		for (var i = 0; i < NumberOfChests; i++)
		{
            var x = Random.Range(0, NumberOfRooms);
			var y = Random.Range(0, NumberOfRooms);
			if (Level[x, y].chest != null || (x==1 && y==0))
				continue;
			var c = Instantiate(Chest);
			c.transform.position = new Vector3(x*Width + Left, y*Height + Top - 12, 0);

		
			Level[x,y].chest = c;
		}
	}

	private void generateTorch()
	{
		for (var i = 0; i < NumberOfTorch; i++)
		{
			var x = Random.Range(0, NumberOfRooms);
			var y = Random.Range(0, NumberOfRooms);
			if (Level[x, y].torch != null)
				continue;
			var t = Instantiate(Torch);
			t.transform.position = new Vector3(x*Width + Left, y*Height + Top, 0);
			Level[x,y].torch = t;
		}
	}
	
	private void generateEnemies()
	{
	    var busy = new HashSet<int>();
		busy.Add(0); // Это стартовая позиция тут не нужен враг
	    for (var i = 0; i < NumberOfEnemies; i++)
	    {
	        int roomIndex;
		    do
		    {
		        roomIndex = Random.Range(0, NumberOfRooms * NumberOfRooms);
			    
		    } while (busy.Contains(roomIndex));

		    busy.Add(roomIndex);

		    
		    if (Random.Range(0, 4) > 2) // с шансом 1/3 выпадают призраки
		    {
			   var t = Instantiate(Ghost, new Vector3(16, -5, 0), Quaternion.identity);
			    t.transform.parent = rooms[roomIndex].transform;
			    t.transform.Translate(rooms[roomIndex].transform.position);
		    }
		    else
		    {
			    var t = Instantiate(Rat, new Vector3(16, -10, 0), Quaternion.identity);
			    t.transform.parent = rooms[roomIndex].transform;
			    t.transform.Translate(rooms[roomIndex].transform.position);
		    }
  
		    if (Random.Range(0, 100) < 10) // С шансом 1/10 враги предложат присоединиться 
		    {
				
		    }
		}
	}
	
	private void BuildLevel()
	{
		generateWeb();
		generateChest();
		generateTorch();
		generateEnemies();
	}

	public void DialogOkShow(string txt, string txtBtn)
	{
		DialogOk.GetComponentInChildren<Text>().text = txt;
		DialogOk.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = txtBtn;
		
		
		DialogOk.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, 0);
	}

	public void DialogOkHide()
	{
		DialogOk.transform.position = new Vector3(-585, -755, 0);
	}
	
}
