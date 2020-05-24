using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogController : MonoBehaviour
{
    public string [] cowboyself={"哎~又是在地下城无聊的一天，真希望能出去走走啊。","先去看看老大有什么安排吧。",
    "主角：前面不太好过啊，还好我带了绳子！"};
    public string [] hint={"（提示：A和D键控制人物左右移动，空格跳跃。）","(提示：靠近按W与NPC对话)","（提示：鼠标右键点击可交互元件即可连上绳索造桥行走。)",
    "(连上绳索后按K键可将两个可交互元件相连)","(提示：连接绳索后按右键取消连接)"};
    public string [] BF={"厂长：……我提过这是城里的规定，没有特殊情况不能去警戒区的。",
    "奶爸：厂长，我知道。但是…这孩子这几天也不知道怎么了",
    "怎么哄都不行就是吵着要看萤火虫，又只有那个地方才有",
    "您看…能不能批我半天假？",
    "厂长：这是规定，下次互贸来的货物如果有我一定给你留心。带着孩子回去吧。",
    "奶爸：……"};
    //对话  B：boss F：奶爸 G：女工 C:主角
    public string [] father={"奶爸：…我的小祖宗，你可别哭了！"};
    public string [] boss={"厂长：东西送到了吗？","厂长：好好干活别到处乱跑。"};
    public string [] girl={"女工：称手的工具才是人类的朋友！"};
    public string [] CB={"主角：早上好，老大！我今天可没迟到吧！","厂长：今天还挺早。刚好之前女工来找我要工具，帮我给她带过去吧。",
    "主角：（果然早起准没好事！）交给我啦。","厂长：前几天有塌陷，路上不好走，小心点。"};
    public string [] CG={"主角：这是老大给你的东西！","女工：谢啦！"};
    public string [] plot={"数台整形机一齐运作，石料厂粉尘滚滚，如同它每一个忙碌的早晨。或许现在并不是早晨。在这座地下城中，晨昏全听钟表来定。",
    "握着手杖的老人，第七次低头看了看腕表。即使是在工厂里，他也挺直腰背穿得笔挺，碎石的尘屑都避开他板正的袖口。",
    "一切井然有序。石料厂如此，街道上穿行的人和机器也如此，这座地下城也是如此。",
    "直到空气被一声惨叫撕裂。",
    "紧急通知：警戒区Ⅱ型清洁机器人出现程序错误，有关部门正着手修复总程序，请居民自觉避开Ⅱ型清洁机器人。",
    "紧急通知：警戒区有不明生物游荡，请不要离开安全地带。",
    "紧急通知：…警戒区出现安全事故，请下列人员到议事厅集合：……",
    "紧急通知：…有关部门正在紧急处理以上事故，请无关人员保持镇静，不要离开…",
    "主角：……又来了！总是在吓唬人！我偏要去看看！"};

    public Text DialogText;
    public Text HintText;
    bool haveDonefirstDialog;
    bool haveDonesecondDialog;
    bool haveDonethirdDialog;
    bool haveDonefourthDialog;
    bool haveDoneSelfDialog;
    private Vector3 playerPos;
    private GameObject player;
    private int i1,i2,i3,i4,ihint=0;
    private Rigidbody2D rigidbody;
    public Transform Father;
    public Transform BOSS;
    public Transform Girl;
    public Vector3 FatherPos;
    public Vector3 BOSSPos;
    public Vector3 GirlPos;
    private float speakRange=1f;
    bool haveDeliverd;
    void Start()
    {
        DialogText.text=cowboyself[0];
        player=GameObject.FindGameObjectWithTag("Player");
        rigidbody=player.GetComponent<Rigidbody2D>();
        FatherPos=Father.position;
        BOSSPos=BOSS.position;
        GirlPos=Girl.position;
        HintText.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {   
        playerPos=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        if (playerPos.x<3.3f&&!haveDonefirstDialog){
            //自我对白
            if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space)){
                DialogText.text=cowboyself[1];
                haveDoneSelfDialog=true;
                StartCoroutine(waits(DialogText,1));
                HintText.text=hint[0];
                HintText.enabled=true;
                StartCoroutine(waits(HintText,2));
            }
        }
        if (playerPos.x>=3.3f&&!haveDonefirstDialog){
            //走近房子，触发第一段对白
            StopControlPlayer();
            DialogText.text=BF[i1];
            if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space)){
                i1+=1;
                if (i1>=BF.Length) {
                    haveDonefirstDialog=true;
                    GiveBackControl();
                    HintText.text=hint[1];
                    HintText.enabled=true;
                    StartCoroutine(waits(HintText,2));
                }       
            }   
        }
        if (playerPos.x>=10f&&!haveDonesecondDialog){
            //走近厂长，触发第二段对白
            StopControlPlayer();
            DialogText.text=CB[i2];
            if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space)){
                i2+=1;
                if (i2>=CB.Length) {
                    haveDonesecondDialog=true;
                    GiveBackControl();
                }       
            }   
        }
        if (playerPos.x>=29.5f&&playerPos.x<=32f&&playerPos.y>=-0.8f){
            //绳索桥区域，提示如何造桥
            
            if (player.GetComponent<Move>().isHanging==false){
                HintText.text=hint[2];
            }
            else if (player.GetComponent<Move>().isHanging){
                HintText.text=hint[3];
            }
            HintText.enabled=true;
        }
        
        if (playerPos.x>=42f&&playerPos.x<=45f&&playerPos.y>=-1f){
            //过桥后，提示如何拆桥
            if (GameObject.FindGameObjectWithTag("Hook")!=null){
                HintText.text=hint[4];
                HintText.enabled=true;
                player.GetComponent<Move>().enabled=false;
                rigidbody.velocity=new Vector2(0f,rigidbody.velocity.y);
                player.GetComponent<Animator>().SetFloat("Speed",0);
                if (Input.GetMouseButtonDown(1)){
                    GiveBackControl();
                    Destroy(GameObject.FindGameObjectWithTag("Hook"));
                    player.GetComponent<Move>().isHanging=false;
                    player.GetComponent<CharacterControl2D>().isHanging=false;
        }
            }
            else {
                HintText.enabled=false;
            }
            
        }
        
        if (playerPos.x>=45f&&playerPos.x<=47f&&playerPos.y>-0.6f&&!haveDonethirdDialog){
            //走近女工，触发交物品
            StopControlPlayer();
            DialogText.text=CG[i3];
            if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space)){
                i3+=1;
                if (i3>=CG.Length) {
                    haveDonethirdDialog=true;
                    GiveBackControl();
                    haveDeliverd=true;
                }       
            }   
        }
        if (playerPos.x>=53f&&haveDonethirdDialog&&!haveDonefourthDialog){
            //走到地图边缘，触发过女工对话，并且未触发最后一次对话
            StopControlPlayer();
            DialogText.text=plot[i4];
            if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space)){
                i4+=1;
                if (i4>=plot.Length) {
                    haveDonefourthDialog=true;
                    GiveBackControl();
                    SceneManager.LoadScene(2);
                    
                }       
            }   
        }
        DiaLogWithNPC();
        
    }
    IEnumerator waits(Text text,float time){
        yield return new WaitForSeconds(time);
        text.enabled=false;
    }
    void StopControlPlayer(){
        player.GetComponent<Move>().enabled=false;
        rigidbody.velocity=new Vector2(0f,rigidbody.velocity.y);
        DialogText.enabled=true;
        player.GetComponent<Animator>().SetFloat("Speed",0);
    }
    void GiveBackControl(){
        player.GetComponent<Move>().enabled=true;
        DialogText.enabled=false;
    }
    void DiaLogWithNPC(){
        // 与奶爸对话
        if (Mathf.Abs(playerPos.x-FatherPos.x)<=speakRange&&(Input.GetKeyDown(KeyCode.W))){
            DialogText.text=father[0];
            DialogText.enabled=true;
            StartCoroutine(waits(DialogText,1));
        }
        //没送到物品前与boss对话
        if (Mathf.Abs(playerPos.x-BOSSPos.x)<=speakRange&&(Input.GetKeyDown(KeyCode.W))&&!haveDeliverd){
            DialogText.text=boss[0];
            DialogText.enabled=true;
            StartCoroutine(waits(DialogText,1));   
        }
        //送到物品后与boss对话
        if (Mathf.Abs(playerPos.x-BOSSPos.x)<=speakRange&&(Input.GetKeyDown(KeyCode.W))&&haveDeliverd){
            DialogText.text=boss[1];
            DialogText.enabled=true;
            StartCoroutine(waits(DialogText,1));   
        }
        //与女工对话
        if (Mathf.Abs(playerPos.x-GirlPos.x)<=speakRange&&(Input.GetKeyDown(KeyCode.W))){
            DialogText.text=girl[0];
            DialogText.enabled=true;
            StartCoroutine(waits(DialogText,1));   
        }
        
    }
}
