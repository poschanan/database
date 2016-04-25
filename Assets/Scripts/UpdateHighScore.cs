using UnityEngine;
using System.Collections;
using Mono.Data.SqliteClient;
using System.Data;
using System;
using UnityEngine.UI;

public class UpdateHighScore : MonoBehaviour {

	public InputField nameInput;
	public InputField scoreInput;

	public void Insert(){
		string connectionString = "URI=file:" + Application.dataPath + "/myDatabase.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(connectionString);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();

		string sqlQuery = "Update Score set HighScore = "
			+ scoreInput.text+" Where PlayerName = '"
			+ nameInput.text+"'";

		//string sqlQuery = "Delete from Score Where PlayerName = '"
		//	+ nameInput.text+"'";

		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();

		reader.Close();reader = null;
		dbcmd.Dispose();dbcmd = null;
		dbconn.Close();dbconn = null;
	}
}
