using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeScript : MonoBehaviour
{   
    public Vector2 destination;
    public Vector2 secondDestination;
    public GameObject Node;
    public GameObject player;
    public GameObject lastNode;
    private LineRenderer lr;
    public List<GameObject> Nodes=new List<GameObject>();
    public Transform GroundCheckforHook;
    int vertexcount=2;
    public float throwingSpeed=0.5f;
    public float distance=0.1f;
    bool isDone=false;
    private float time;
    private float g=9.8f;
    public Vector3 mousePos;
    public float throwHorizontalSpeed;
    public float throwVerticalSpeed;
    float radiusforHook=0.1f;
    public LayerMask groundMask;
    public LayerMask objectsToHook;
    public bool isConnectedOne;
    public bool isConnectedTwo;
    bool isDoneConnectTwo;
    GameObject secondOne;
    [SerializeField] private bool onHit;
    void Start()
    {   
        Nodes.Add(gameObject);
        player=GameObject.FindGameObjectWithTag("Player");
        lastNode=gameObject;
        lr=gameObject.GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {   
        
        //Hook飞向目标点      
        transform.position=Vector2.MoveTowards(transform.position,destination,throwingSpeed);
        HitCheck();
        //飞到目标点以后判断是否命中可钩物体
        if(beInDestination()){
            if(onHit){
                //如果命中 输出Hit 状态变为isConnectedOne，isHanging，连接目标点与Hook
                Debug.Log("Hit");
                isConnectedOne=true; 
                player.GetComponent<Move>().isHanging=true;
                GameObject placeToHook=HitCheck().gameObject;
                placeToHook.GetComponent<HingeJoint2D>().enabled=true;
                placeToHook.GetComponent<HingeJoint2D>().connectedBody=gameObject.GetComponent<Rigidbody2D>();
                //destination=transform.position;
            }
            else {
                //如果没命中，输出Miss,保持原有状态，0.2s后摧毁物体，记为抛空
                Debug.Log("Miss");
                isConnectedOne=false;
                player.GetComponent<Move>().isHanging=false;
                Destroy(gameObject,0.2f);
            }
        }
        if (!isConnectedTwo){
            //若不是已经连接一端的情况（包括两种：抛出命中和抛空）
            //使用第一种生成node的方式（一端是玩家一端是目标点）
            CreateNodesforOneEndSituation();
            RenderLine();
        }
        else {
            //若一端已经连接，使用第二种生成node的方式
            CreateNodesforSecondEndSituation();
            RenderLine();
        }

        //渲染绳子
       
        
    }
    
    void RenderLine(){
        lr.positionCount=Nodes.Count+1;
        int i;
        for (i=0;i<Nodes.Count;i++){
            lr.SetPosition(i,Nodes[i].transform.position);
        }
        if (!isConnectedTwo){ //如果没有两头都连上物体，linerender的最后一个点在玩家身上渲染
            lr.SetPosition(i,player.transform.position);}
        else {
            //如果两头都连上了，最后一个点在第二个物体上渲染
            lr.SetPosition(i,secondOne.transform.position);
        }

    }
    Collider2D HitCheck(){
        onHit=false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(destination, radiusforHook, objectsToHook);
        for(int i=0;i<colliders.Length;i++){
            if (colliders[i].gameObject!=gameObject){
                onHit=true;
                player.GetComponent<Move>().isHanging=true;
                return colliders[i];
            }
        }
        return null;
    }
    public void ConnectTheotherSideofRope(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(secondDestination, radiusforHook, objectsToHook);
        Vector2 direction=destination-secondDestination;
        float distance12=Vector2.Distance(destination,secondDestination);
        for(int i=0;i<colliders.Length;i++){
            if (colliders[i].gameObject!=gameObject){
                //鼠标悬停位置如果可以连接
                //标记isConeectedTwo 为true
                secondOne=colliders[i].gameObject;
                isConnectedTwo=true;
            }
        }
                
        //Method 2: 直接连接两个点
        if (isConnectedTwo){
            Debug.Log("Number of Nodes:"+Nodes.Count);
            for (int j=Nodes.Count-1;j>0;j--){
                Destroy(Nodes[j]);
                Nodes.Remove(Nodes[j].gameObject);
            }
            player.GetComponent<Move>().isHanging=false;
        }            
    }
    void CreateNodesforOneEndSituation(){
        if (!beInDestination()){
            if (Vector2.Distance(player.transform.position,lastNode.transform.position)>distance){
                CreateNode();
            }
        }
        else if (!isDone){
            isDone=true;
            while(Vector2.Distance(player.transform.position,lastNode.transform.position)>distance){
                CreateNode();
            }
            lastNode.GetComponent<HingeJoint2D>().connectedBody=player.GetComponent<Rigidbody2D>();
        }
        else {
        }
    }
    void CreateNodesforSecondEndSituation(){
        //直接在两个点中间生成直线
        if (!isDoneConnectTwo){
            vertexcount=0;
            lastNode=gameObject;
            Vector2 difference =(secondDestination-destination).normalized;
            float distance12=Vector2.Distance(destination,secondDestination);
            float  number =distance12/distance; //两点间生成node的数量等于distance12（两点距离）/distance（每两个node间距）
            Vector2 posToCreateNode=destination;
            while (number>0){
                posToCreateNode+=difference*distance;
                GameObject go=Instantiate(Node,posToCreateNode,Quaternion.identity) as GameObject;
                go.transform.SetParent(transform);
                lastNode.GetComponent<HingeJoint2D>().connectedBody=go.GetComponent<Rigidbody2D>();
                lastNode=go;
                Nodes.Add(lastNode);
                vertexcount++;
                number--;
            }
            isDoneConnectTwo=true;
        }
        if(isDoneConnectTwo) return;
    }
    void CreateNode(){
        Vector2 posToCreateNode =player.transform.position-lastNode.transform.position;
        posToCreateNode.Normalize();
        posToCreateNode*=distance;
        posToCreateNode+=(Vector2)lastNode.transform.position;
        GameObject go=Instantiate(Node,posToCreateNode,Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        lastNode.GetComponent<HingeJoint2D>().connectedBody=go.GetComponent<Rigidbody2D>();
        lastNode=go;
        Nodes.Add(lastNode);
        vertexcount++;
    }
    bool beInDestination(){
        float x=transform.position.x;
        float y=transform.position.y;
        if (Mathf.Abs(x-destination.x)<0.02f&&Mathf.Abs(y-destination.y)<0.02f){
            return true;
        }
        else return false;
    }
}
