using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    public string NextSceneName = "";
    public void ChangeNextScene(string s = "")  
    {
        // 다음 씬 이름 s를 입력하지 않으면 인스펙터창에 입력된 씬으로 이동
        // UI Button이나 다른 스크립트에서 ChangeNextScene을 호출할 때 다음 씬 이름을 입력해주길 권장
        if(s == "")
        {
            SceneManager.LoadScene(NextSceneName);
            Debug.Log("Success Change Scene : " + NextSceneName);
        }
        else
        {
            SceneManager.LoadScene(s);
            Debug.Log("Success Change Scene : " + s);
        }
    }

    private void Start() {
        // 다른 씬에서 사용될 수 있게 DontDestroy
        var objs = FindObjectsOfType<SceneChanger>();
        if(objs.Length == 1)
        // 다른 씬에서 사용될 수 있게 DontDestroy
            DontDestroyOnLoad(this.gameObject);
        else
            Destroy(gameObject);
    }
}

/*
// 아래코드는 에디터 인스펙터창에 버튼을 만들어 주기 위한 코드 이므로 신경쓰지 않아도 됨
[CustomEditor(typeof(SceneChanger))]
public class SceneChangerCusstomButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
 
        //SceneChanger.cs 의 객체를 받아옵니다 => 이래야 버튼시 명령을 내릴수 잇습니다
        SceneChanger my_sceneChanger = (SceneChanger)target; 

        EditorGUILayout.BeginHorizontal();  //BeginHorizontal() 이후 부터는 GUI 들이 가로로 생성됩니다.
        GUILayout.FlexibleSpace(); // 고정된 여백을 넣습니다. ( 버튼이 가운데 오기 위함)

        //버튼을 만듭니다 . GUILayout.Button("버튼이름" , 가로크기, 세로크기)
        if (GUILayout.Button("이펙트 실행", GUILayout.Width(120), GUILayout.Height(20))) 
        {
            //SceneChanger 클래스에서 버튼 누를시 해당 명령을 구현해줍니다.
            my_sceneChanger.ChangeNextScene(); 
        } 
        GUILayout.FlexibleSpace();  // 고정된 여백을 넣습니다.
        EditorGUILayout.EndHorizontal();  // 가로 생성 끝
    }

}

*/