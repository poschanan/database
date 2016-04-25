using UnityEngine;
using System.Collections;
using Mono.Data.SqliteClient;
using System.Data;
using System;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

	public Text highScoreTxt;

	void Start () {
		//Insert ();
		//Select ();
	}

	public void Insert(){
		string connectionString = "URI=file:" + Application.dataPath + "/myDatabase.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(connectionString);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = "INSERT INTO Score (HighScore,PlayerName,Level,HP,MP) VALUES (1000,'One',60,500,800);";
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();

		reader.Close();reader = null;
		dbcmd.Dispose();dbcmd = null;
		dbconn.Close();dbconn = null;
	}

	public void Select(){
		string connectionString = "URI=file:" + Application.dataPath + "/myDatabase.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(connectionString);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = "SELECT * FROM Score";
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while(reader.Read()) {
			int highestScore = reader.GetInt32(0);
			string name = reader.GetString(1);
			int lv = reader.GetInt32(2);
			int hp = reader.GetInt32(3);
			int mp = reader.GetInt32(4);
			highScoreTxt.text = "High Score = " + highestScore + "\nName = " + name + "\nLevel = " + lv + "\nHp = " + hp + "\nMp = " + mp;
			Debug.Log( "High Score = " + highestScore);
			Debug.Log( "Player Name = " + name);
			Debug.Log( "Level = " + lv);
			Debug.Log( "Hp = " + hp);
			Debug.Log( "Mp = " + mp);
		}
		reader.Close();reader = null;
		dbcmd.Dispose();dbcmd = null;
		dbconn.Close();dbconn = null;
	}
}
