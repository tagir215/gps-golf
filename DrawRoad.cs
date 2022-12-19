using System.Reflection;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

[RequireComponent(typeof(MeshFilter))]

public class DrawRoad : MonoBehaviour
{
    void Awake(){
        Draw();
    }

    void Draw(){
        var sp = GameObject.Find("spawnerWay").GetComponent<GenerateMap>();

        Vector3[] vertices = new Vector3[sp.coordinates.Count*4]; 
        Vector3 lastleft = new Vector3(0,0,0);
        Vector3 lastright = new Vector3(0,0,0);
        int[] triangles = new int[sp.coordinates.Count*6];
                
        
            for(int i = 1; i<sp.coordinates.Count; i++){
                var meshfilter = GetComponent<MeshFilter>();
                
            
                Vector3 direction = new Vector3(sp.coordinates[i].x-sp.coordinates[i-1].x,0,sp.coordinates[i].z-sp.coordinates[i-1].z); 
                Vector3 left = new Vector3(direction.z*(-1),0,direction.x).normalized;
                int vr = (i-1)*4;
                    if(i>1){
                        if(Vector3.Distance(lastleft,sp.coordinates[i]+left*(sp.w/2))>Vector3.Distance(lastright,sp.coordinates[i]-left*(sp.w/2)))
                        {
                        vertices[0+vr] = lastleft;
                        vertices[1+vr] = sp.coordinates[i-1]-left*(sp.w/2);
                        }
                        else
                        {
                        vertices[0+vr] = sp.coordinates[i-1]+left*(sp.w/2);
                        vertices[1+vr] = lastright; 
                        }
                    }
                    else{
                        vertices[0+vr] = sp.coordinates[i-1]+left*(sp.w/2);
                        vertices[1+vr] = sp.coordinates[i-1]-left*(sp.w/2);
                    }
                    vertices[2+vr] = sp.coordinates[i]+left*(sp.w/2);
                    vertices[3+vr] = sp.coordinates[i]-left*(sp.w/2);
                    
                
                int tr = (i-1)*6;
            
                    triangles[0+tr]= 2+vr;
                    triangles[1+tr]= 1+vr;
                    triangles[2+tr]= 0+vr; 
                    triangles[3+tr]= 2+vr;
                    triangles[4+tr]= 3+vr;
                    triangles[5+tr]= 1+vr;
                
                transform.parent = GameObject.Find("world").transform.GetChild(sp.totalAreas);
                
                
                Mesh mesh = new Mesh();
                meshfilter.mesh = mesh;
                mesh.vertices = vertices;
                mesh.triangles = triangles;
                mesh.RecalculateNormals();
                lastleft = sp.coordinates[i]+left*(sp.w/2);
                lastright = sp.coordinates[i]-left*(sp.w/2);

           }
            
        
    }

    
    
    
}
