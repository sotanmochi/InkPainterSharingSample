using UnityEngine;
using Photon.Pun;

namespace InkPainterSharingSample
{
    public class PhotonInkPainterInitializer : MonoBehaviourPunCallbacks
    {
        public int SerializationRate = 30;
        public GameObject InkPainterPrefab;

        void Awake()
        {
            // Defines how many times per second OnPhotonSerialize should be called on PhotonViews.
            PhotonNetwork.SendRate = 2 * SerializationRate;
            PhotonNetwork.SerializationRate = SerializationRate;

            Debug.LogFormat("PhotonNetwork.SendRate: {0}", PhotonNetwork.SendRate);
            Debug.LogFormat("PhotonNetwork.SerializationRate: {0}", PhotonNetwork.SerializationRate);
        }

        public override void OnJoinedRoom()
        {
            if (InkPainterPrefab != null)
            {
                Debug.LogFormat("Instantiate: {0}", this.InkPainterPrefab.name);
                GameObject painter = PhotonNetwork.Instantiate(this.InkPainterPrefab.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
                painter.transform.SetParent(Camera.main.transform, false);
            }
        }
    }
}