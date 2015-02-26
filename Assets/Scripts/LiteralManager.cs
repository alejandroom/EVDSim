using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;

public class LiteralManager {

	private string path;

	private Dictionary<string, Dictionary<string, string>> databasesByLanguage;
	
	private Dictionary<string, string> actualDatabase;

	private static LiteralManager instance;

	public static LiteralManager getInstance() {
		if (instance == null) {
			instance = new LiteralManager();
		}
		return instance;
	}

	public LiteralManager(){
		path = "Files/Language/";
		string[] files = new string[2] {"literalsEVDSim_es", "literalsEVDSim_en"};
		//string[] files = new string[2] {"literals_prueba_es.txt", "literals_prueba_en.txt"};
		string[] languages = new string[2] {"ES", "EN"};
		databasesByLanguage = loadDatabasesFromFiles (files, languages);
		setLocale ("ES");
	}

	public bool setLocale(string locale){
		Dictionary<string, string> database = databasesByLanguage[locale];

		if (database != null) {
			actualDatabase = database;
			return true;
		}

		return false;
	}

	public string getValue(string code){
		try{
			return actualDatabase[code];
		}catch(Exception){
			//Debug.Log("Clave no encontrada en literals: " + code);
			return code;
		}
	}
	
	Dictionary<string, Dictionary<string, string>> loadDatabasesFromFiles(string[] files, string[] languages){
		Dictionary<string, Dictionary<string, string>> databases = new Dictionary<string, Dictionary<string, string>>();

		if (files.Length != languages.Length)
			Debug.LogError ("Load error at LiteralManager: files and languages do not match!");

		for (int i = 0 ; i < files.Length ; i ++) {
			string file = files[i];
			string language = languages [i];
			databases.Add(language, loadDatabaseFromFile(file));
		}

		return databases;
	}
	
	
	Dictionary<string, string> loadDatabaseFromFile(string file){
		Dictionary<string, string> database = new Dictionary<string, string>();

		try{
			TextAsset textAsset = (TextAsset) Resources.Load(path+file);

			foreach(string line in textAsset.text.Split('\n')){
				if (line == "" || line[0] == '#')
					continue;
				string[] parameters = line.Split(new Char[] {':'}, 2);
				database.Add(parameters[0], parameters[1]);
			}
		}catch (Exception e){
			Debug.LogError(e.Message);
		}
		
		return database;
	}
}
