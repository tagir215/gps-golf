using System.Threading.Tasks;
using System.Net.Mime;
using System.Security.AccessControl;
using System.IO;
using System;
using System.Diagnostics.Tracing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;



public class GenerateMap : MonoBehaviour
{
    public Transform trg;
    public GameObject flag;
    public GameObject globe;
    public Material matery;
    public UnityEngine.UI.Text holesman;
    public Vector2 position;
    private float updateInterval=5f;
    public float alt = 114.59155902616464175359630962821f*100;

    public string location= "00.0";

    public List<Vector3>coordinates;
    public float w;
    public float lonff = 22.335561f;
    public float latff = 60.437111f;


    public int flags = 0;
    XmlReaderSettings settings = new XmlReaderSettings();
    List<string>openPaths = new List<string>();
    GameObject area;
    GameObject locale;
    GameObject areacurrent;
    public int totalAreas = 0;
    Mesh mesh;
    string minus = "-";

    Vector3 get3dCoordinate(float latf, float lonf){
        return new Vector3(alt*Mathf.Sin(latf)*Mathf.Cos(lonf),alt*Mathf.Sin(latf)*Mathf.Sin(lonf),alt*Mathf.Cos(latf));
    }

    void UpdateMap()
    {
        
            
        if(latff<0) 
            minus = "-";
        else
            minus = ""; 
              
        string lats = minus+((latff).ToString()+"000").Replace(",","").Replace(".","");
        lats = lats.Substring(0,3);
        if(lonff<0) 
            minus = "-";
        else
            minus = "";  
        string lons = minus+((lonff).ToString()+"000").Replace(",","").Replace(".","");
        lons = lons.Substring(0,3);
        

        location = lats+lons;
        UnityEngine.Debug.Log(location);
        if(openPaths.Contains(location)==false || openPaths.Count==0)
    {
        int linescount = 0;
        UnityEngine.Debug.Log(location);
        UnityEngine.Debug.Log(openPaths.Count);

        locale = new GameObject();
        locale.name = "X"+location;
                    
        
        
        XmlTextReader reader = new XmlTextReader("https://storage.googleapis.com/newfinlandx/newFinlandZ/"+"X"+location+".osm");
        while(reader.Read())
        {

            if(reader.Name == "way" )
            {
                w = 0.1f;
                coordinates = new List<Vector3>();
                
                XmlReader r = reader.ReadSubtree();
                while(r.Read())
                {
                    
                    if(r.Name.Length>1 && r.Name.Substring(0,1)=="C")
                    {
                    string lat = r.GetAttribute("lat").Replace(".",",");
                    string lon = r.GetAttribute("lon").Replace(".",",");
                    float latf = float.Parse(lat);
                    float lonf = float.Parse(lon);


                    Vector3 coordinate = get3dCoordinate(latf,lonf);
                    
                    coordinates.Add(coordinate*-1);
                    }
                    else if(r.Name=="Z")
                    {
                        string type = r.GetAttribute("highway");
                        switch(type)
                        {
                            case "motorway": {w=1.5f;} break;
                            case "trunk":{w=1.3f;} break;
                            case "primary": {w=1.2f;} break;
                            case "secondary":{w=0.9f;} break;
                            case "tertiary": {w=0.7f;}break;
                            case "unclassified":{w=0.6f;} break;
                            case "residential":{w=0.6f;} break;
                            case "service":{w=0.4f;} break;

                            case "motorway_link":{w=1.3f;} break;
                            case "trunk_link":{w=0.9f;} break;
                            case "primary_link":{w=0.8f;} break;
                            case "secondary_link":{w=0.6f;} break;
                            case "motorway_junction":{w=0.9f;} break;
                            
                            case "living_street":{w=0.9f;} break;
                            case "pedestrian":{w=0.2f;} break;
                            case "bicycle_road":{w=0.2f;} break;
                            case "cyclestreet":{w=0.3f;} break;
                            case "track":{w=0.2f;} break;
                            case "bus_guideway":{w=0.2f;} break;
                            case "busway":{w=0.02f;} break;
                            case "raceway":{w=1.0f;} break;
                            case "road":{w=0.2f;} break;
                            case "construction":{w=0.9f;} break;
                            case "escape":{w=0.3f;} break;

                            case "footway":{w=0.2f;} break;
                            case "cycleway":{w=0.2f;} break;
                            case "bridleway":{w=0.1f;} break;
                            case "path":{w=0.1f;} break;
                            case "steps":{w=0.2f;} break;
                            case "escalator":{w=0.2f;} break;

                        }
                        
                    }

                }
                
                if(linescount>=1000)
                {
                    Mesh();
                    area.AddComponent<MeshCollider>();

                    linescount = 0;
                    totalAreas ++;
                    ind = 1;

                }
                if(linescount == 0)
                {
                    area = new GameObject();
                    area.name = "area"+totalAreas;
                    area.AddComponent<MeshFilter>();
                    area.AddComponent<MeshRenderer>().material = matery;
                    
                    area.transform.parent =GameObject.Find("X"+location).transform;
                    vertices = new Vector3[20000*4]; 
                    triangles = new int[20000*6];

                }
                Draw();
                
                linescount++;
            }

        

    //end loop   
        }
            Mesh();
            totalAreas=0;
            ind = 1;
            linescount=0;
            openPaths.Add(location);
    
    //flags
            List<float>Xzonecord = new List<float>();
            float[] Yzonecords = new float[160];
            for(int a=0; a<160; a++)
            {
                Xzonecord.Add((float.Parse(lats))/10+(a)*(0.0025f/4));
                Yzonecords[a]=(float.Parse(lons))/10+(a)*(0.0025f/4);

            }
            System.Random random = new System.Random();

            for(int b=0; b<160; b++)
            {
                int c = random.Next(0,160-b);
                Vector3 fcoordinate = get3dCoordinate(Xzonecord[c],Yzonecords[b]);
                Vector3 fdirection =new Vector3(0,0,0)- fcoordinate;
              
                GameObject newflag = GameObject.Instantiate(flag,-fcoordinate,Quaternion.LookRotation(-fdirection));
                newflag.SetActive(true);
                Xzonecord.RemoveAt(c);
                flags++;
            }

            

        
    }


           
    }
    void Start() 
    {
        
        //Vector3 startpoint = GameObject.Find("GlobeHolder").transform.TransformDirection( new Vector3(alt*Mathf.Sin(latff)*Mathf.Cos(lonff),alt*Mathf.Sin(latff)*Mathf.Sin(lonff),alt*Mathf.Cos(latff)));

    UpdateMap();

       //GameObject.Find("GlobeHolder").transform.rotation = Quaternion.FromToRotation(startpoint.normalized,new Vector3(0,10,0));

    }

    void Update()
    {
    updateInterval -= Time.deltaTime;
    if(updateInterval<=0)
    {
    UpdateMap();
    updateInterval = 5f;
    }
     
    }  
        Vector3[] vertices; 
        int[] triangles;
        int ind = 1;
    
    void Draw()
    {
        


        Vector3 lastleft = new Vector3(0,0,0);
        Vector3 lastright = new Vector3(0,0,0);
                
        
            for(int i = 1; i<coordinates.Count; i++)
            {
            
                Vector3 direction = coordinates[i]-coordinates[i-1]; 
                Vector3 left = Vector3.Cross(coordinates[i],direction).normalized;
                int vr = (ind-1)*4;
                    if(i>1)
                    {
                        if(Vector3.Distance(lastleft,coordinates[i]+left*(w/2))>Vector3.Distance(lastright,coordinates[i]-left*(w/2))){
                            vertices[0+vr] = lastleft;
                            vertices[1+vr] = coordinates[i-1]-left*(w/2);
                        }
                        else{
                            vertices[0+vr] = coordinates[i-1]+left*(w/2);
                            vertices[1+vr] = lastright; 
                        }
                    }
                    else{
                        vertices[0+vr] = coordinates[i-1]+left*(w/2);
                        vertices[1+vr] = coordinates[i-1]-left*(w/2);
                    }
                    vertices[2+vr] = coordinates[i]+left*(w/2);
                    vertices[3+vr] = coordinates[i]-left*(w/2);
                    
                
                int tr = (ind-1)*6;
            
                    triangles[0+tr]= 0+vr;
                    triangles[1+tr]= 1+vr;
                    triangles[2+tr]= 2+vr; 
                    triangles[3+tr]= 1+vr;
                    triangles[4+tr]= 3+vr;
                    triangles[5+tr]= 2+vr;
                
                lastleft = coordinates[i]+left*(w/2);
                lastright = coordinates[i]-left*(w/2);
                ind ++;
                 
                

           }
                   //GameObject.Instantiate(GameObject.Find("Sphere"),GameObject.Find("GlobeHolder").transform.TransformPoint(coordinates[0]),transform.rotation,GameObject.Find("GlobeHolder").transform);
    
        
    }

    void Mesh(){
        var meshfilter = GameObject.Find("X"+location).transform.GetChild(totalAreas).GetComponent<MeshFilter>();

        Mesh mesh = new Mesh();
        meshfilter.mesh = mesh;
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        //mesh.RecalculateNormals();
                
    }
   
}