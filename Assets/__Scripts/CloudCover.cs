
using UnityEngine;

public class CloudCover : MonoBehaviour
{
    [Header("Inscribed")]

    public Sprite[] cloudSprites;
    public int numClouds = 45;
    public Vector3 minPos = new Vector3( -20, -5, -5 );
    public Vector3 maxPos = new Vector3( 300, 40, 5);
    public Vector2 scaleRang = new Vector2( 1, 4);

    void Start()
    {
        Transform parentTrans = this.transform;
        GameObject cloudGO;
        Transform cloudTrans;
        SpriteRenderer sRend;
        float scaleMult;
        for ( int i = 0; i < numClouds; i++){
            cloudGO = new GameObject();
            cloudTrans = cloudGO.transform;
            sRend = cloudGO.AddComponent<SpriteRenderer>();

            int spriteNum = Random.Range( 0,cloudSprites.Length );
            sRend.sprite = cloudSprites[spriteNum];

            cloudTrans.position = RandomPos();
            cloudTrans.SetParent( parentTrans, true);

            scaleMult = Random.Range( scaleRang.x , scaleRang.y );
            cloudTrans.localScale = Vector3.one * scaleMult;
        }    
    }

    Vector3 RandomPos() {
        Vector3 pos = new Vector3();
        pos.x = Random.Range( minPos.x, maxPos.x );
        pos.y = Random.Range( minPos.y, maxPos.y );
        pos.z = Random.Range( minPos.z, maxPos.z );

        return pos;
    }

}
