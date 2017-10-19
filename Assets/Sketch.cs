using UnityEngine;
using Pathfinding.Serialization.JsonFx; //make sure you include this using
using System;

public class Sketch : MonoBehaviour {
    public GameObject myCubePrefab;
    public GameObject mySpherePrefab;
    public string _WebsiteURL = "http://lloydmc.azurewebsites.net/tables/Mountain?zumo-api-version=2.0.0";

    void Start () {
        //Reguest.GET can be called passing in your ODATA url as a string in the form:
        //http://{Your Site Name}.azurewebsites.net/tables/{Your Table Name}?zumo-api-version=2.0.0
        //The response produce is a JSON string
        string jsonResponse = Request.GET(_WebsiteURL);

        //Just in case something went wrong with the request we check the reponse and exit if there is no response.
        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }

        //We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
        Mountain[] mountains = JsonReader.Deserialize<Mountain[]>(jsonResponse);

        //----------------------
        //YOU WILL NEED TO DECLARE SOME VARIABLES HERE SIMILAR TO THE CREATIVE CODING TUTORIAL

        int i = 0;
        int totalCubes = 30;
        float totalDistance = 2.9f;
        //----------------------

        //We can now loop through the array of objects and access each object individually
        foreach (Mountain mountain in mountains)
        {
            //Example of how to use the object
            Debug.Log("This mountains name is: " + mountain.MountainName);
            //----------------------
            //YOUR CODE TO INSTANTIATE NEW PREFABS GOES HERE
            float perc = i / (float)totalCubes;
            float sin = Mathf.Sin(perc * Mathf.PI / 2);

            float x = (float)Convert.ToDouble(mountain.X);//1.8f + sin * totalDistance;
            float y = (float)Convert.ToDouble(mountain.Y);// 5.0f;
            float z = (float)Convert.ToDouble(mountain.Z);// 0.0f;
            GameObject newShape;
            if (mountain.Symbol.Equals("Cube"))
            {
                newShape = (GameObject)Instantiate(myCubePrefab, new Vector3(x*10, y * 10, z * 10), Quaternion.identity);
                newShape.GetComponent<CubeScript>().SetSize((float)Convert.ToDouble(mountain.Size));// (.45f * (1.0f - perc));
                newShape.transform.Find("New Text").GetComponent<TextMesh>().text = mountain.MountainName;//"Hullo Again";
            }
            else if (mountain.Symbol.Equals("Sphere"))
            {
                newShape = (GameObject)Instantiate(mySpherePrefab, new Vector3(x * 10, y * 10, z * 10), Quaternion.identity);
                newShape.GetComponent<CubeScript>().SetSize((float)Convert.ToDouble(mountain.Size));// (.45f * (1.0f - perc));
                newShape.transform.Find("New Text").GetComponent<TextMesh>().text = mountain.MountainName;//"Hullo Again";
            }
            //var newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);


            //newCube.GetComponent<CubeScript>().rotateSpeed = .2f + perc * 4.0f;
            //newShape.GetComponent<CubeScript>().SetSize(Convert.ToInt32(mountain.Size));// (.45f * (1.0f - perc));
            //newShape.transform.Find("New Text").GetComponent<TextMesh>().text = mountain.MountainName;//"Hullo Again";

            i++;

            //----------------------
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
