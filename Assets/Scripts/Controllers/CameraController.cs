using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public Transform target;
    [SerializeField] float moveSpeed;
    [SerializeField] CameraSetting[] cameraSettings;


    Dictionary<string, CameraSetting> settingDic;
    Transform cam;
    Vector3 pos;
    Vector3 rot;
    float yMouse;
    float speedRotX, SpeedRotY;
    float minYRot, maxYRot;
    private void Awake()
    {
        Instance = this;

        settingDic = new Dictionary<string, CameraSetting>();
        for (int i = 0; i < cameraSettings.Length; i++)
        {
            settingDic.Add(cameraSettings[i].settingName, cameraSettings[i]);
        }
        cam = Camera.main.transform;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraHolderControl();
    }
    private void FixedUpdate()
    {
    }
    void CameraHolderControl()
    {
        if (target != null)
            transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed);
    }

    public void ChangeCameraPos(string settingName)
    {
        pos = settingDic[settingName].Pos;
        rot = settingDic[settingName].Rot;

        cam.DOLocalMove(pos, 1f);
        Quaternion targetRotate = Quaternion.Euler(rot.x, rot.y, rot.z);
        cam.DOLocalRotateQuaternion(targetRotate, 0.5f);
    }
}
[System.Serializable]
public class CameraSetting
{
    public string settingName;
    public Vector3 Pos;
    public Vector3 Rot;
}
