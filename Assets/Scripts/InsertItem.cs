using UnityEngine;
using System.Collections;
using Mono.Data.SqliteClient;
using System.Data;
using System;
using UnityEngine.UI;

public class InsertItem : MonoBehaviour {

	public InputField nameInput;
	public InputField itemInput;
	public Text itemListTxt;

	public void Insert(){
		int accountID = FindAccountID ();
		string connectionString = "URI=file:" + Application.dataPath + "/myDatabase.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(connectionString);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();

		//Enabling Foreign Key Support for sqlite version3
		string enforcePK = "PRAGMA foreign_keys = ON";
		string sqlQuery = "INSERT INTO Item (ItemName,accountID) VALUES ('"+ itemInput.text+"',"+ accountID+")";
		dbcmd.CommandText = enforcePK +";"+sqlQuery;

		IDataReader reader = dbcmd.ExecuteReader();

		reader.Close();reader = null;
		dbcmd.Dispose();dbcmd = null;
		dbconn.Close();dbconn = null;
	}

	private int FindAccountID(){
		int accountID = 0;
		string connectionString = "URI=file:" + Application.dataPath + "/myDatabase.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(connectionString);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();

		string sqlQuery = "SELECT ID FROM Account Where Username = '"
			+ nameInput.text+"'";
		
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();

		while(reader.Read()) {
			accountID = reader.GetInt32 (0);
		}

		reader.Close();reader = null;
		dbcmd.Dispose();dbcmd = null;
		dbconn.Close();dbconn = null;
		return accountID;
	}

	public void Show(){
		string connectionString = "URI=file:" + Application.dataPath + "/myDatabase.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(connectionString);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();

		string sqlQuery = "SELECT ItemName FROM Item INNER JOIN Account ON Item.AccountID=Account.ID Where Username = '"
			+ nameInput.text+"'";

		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while(reader.Read()) {
			string itemName = reader.GetString(0);
			itemListTxt.text += itemName + "\n";
		}
		reader.Close();reader = null;
		dbcmd.Dispose();dbcmd = null;
		dbconn.Close();dbconn = null;
	}
}
