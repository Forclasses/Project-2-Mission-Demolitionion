using UnityEngine;
using UnityEngine.UI;

public enum GameMode {
    idle,
    playing,
    levelEnd
}
public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S;

    [Header("Inscribed")]
    public Text uitLevel;
    public Text uitShots;

    public Text Gameover;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Dynamic")]
    public int level;
    public int levelMax;

    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;

    public string showing = "Show Slingshot";


    void Start()
    {
        S = this;

        level = 0;
        levelMax = castles.Length;
        
        StartLevel();
    }

    void StartLevel(){
        if (castle != null){
            Destroy( castle );
        }

        Projectile.DESTORY_PROJECTILES();

        castle = Instantiate<GameObject>( castles[level]);
        castle.transform.position = castlePos;

        Goal.goalMet = false;
        

        UpdateGUI();

        mode = GameMode.playing;
    }

    void UpdateGUI(){
        uitLevel.text = "Level: "+(level+1)+" of "+levelMax;
        uitShots.text = "Shots taken: "+shotsTaken;

    }
    // going to be honst no idea how to make the button go inviable untill time so am gonna miss that

    void Update()
    {
        UpdateGUI();

        if ( (mode == GameMode.playing) && Goal.goalMet ){
            mode = GameMode.levelEnd;
            Invoke("NextLevel", 2f);
        }
         //else if {
         //   Gameover.text = "GAME OVER YOU SHOT: "+shotsTaken +" Over " + levelMax + " levels";
          //  Application.Quit();
        //}

    }

    void NextLevel(){
        level++;
        if (level == levelMax){
            level = 0;
            shotsTaken = 0;

        }
        StartLevel();
    }

    static public void SHOT_FIRED(){
        S.shotsTaken++;
    }

    static public GameObject GET_CASTLE(){
        return S.castle;
    }
}
