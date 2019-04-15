// -------------------------------------
// PhotonInkPainterView.cs
// Copyright (c) 2019 sotan.
// Licensed under the MIT License.
// -------------------------------------

using UnityEngine;
using Photon.Pun;

namespace InkPainterExtension
{
    [AddComponentMenu("Photon Networking/Photon Ink Painter")]
    [RequireComponent(typeof(PhotonView))]
    [RequireComponent(typeof(PhotonTransformView))]
    public class PhotonInkPainterView : MonoBehaviour, IPunObservable
    {
        private PhotonView m_PhotonView;
        private InkPainter m_InkPainter;

        private bool m_PaintEnabled = false;
        private Vector3 m_RayOrigin;
        private Vector3 m_RayDirection;

        private float m_BrushColorR;
        private float m_BrushColorG;
        private float m_BrushColorB;
        private float m_BrushColorA;
    
        private bool m_Erase = false;

        void Awake()
        {
            m_PhotonView = GetComponent<PhotonView>();
            m_InkPainter = GetComponent<InkPainterExtension.InkPainter>();
        }

        void Update()
        {
            if (!m_PhotonView.IsMine)
            {
                m_InkPainter.paintEnabled = m_PaintEnabled;
                m_InkPainter.ray.origin = m_RayOrigin;
                m_InkPainter.ray.direction = m_RayDirection;
                m_InkPainter.brushColor.r = m_BrushColorR;
                m_InkPainter.brushColor.g = m_BrushColorG;
                m_InkPainter.brushColor.b = m_BrushColorB;
                m_InkPainter.brushColor.a = m_BrushColorA;
                m_InkPainter.erase = m_Erase;
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(m_InkPainter.paintEnabled);
                stream.SendNext(m_InkPainter.ray.origin);
                stream.SendNext(m_InkPainter.ray.direction);
                stream.SendNext(m_InkPainter.brushColor.r);
                stream.SendNext(m_InkPainter.brushColor.g);
                stream.SendNext(m_InkPainter.brushColor.b);
                stream.SendNext(m_InkPainter.brushColor.a);
                stream.SendNext(m_InkPainter.erase);
            }
            else
            {
                m_PaintEnabled = (bool)stream.ReceiveNext();
                m_RayOrigin = (Vector3)stream.ReceiveNext();
                m_RayDirection = (Vector3)stream.ReceiveNext();
                m_BrushColorR = (float)stream.ReceiveNext();
                m_BrushColorG = (float)stream.ReceiveNext();
                m_BrushColorB = (float)stream.ReceiveNext();
                m_BrushColorA = (float)stream.ReceiveNext();
                m_Erase = (bool)stream.ReceiveNext();
            }
        }
    }
}