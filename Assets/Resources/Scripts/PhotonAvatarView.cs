
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;
using UnityEngine.UI;

namespace Com
{
    public class PhotonAvatarView : MonoBehaviour, IPunObservable
    {

        private PhotonView photonView;
        private OvrAvatar ovrAvatar;
        private OvrAvatarRemoteDriver remoteDriver;
        private List<byte[]> packetData;

        Vector3 trueLoc;
        Quaternion trueRot;


        public float Health;

        public void Awake()
        {
            Health = 120.0f;

        }

        public void Start()
        {

            photonView = GetComponent<PhotonView>();

            transform.position = Vector3.Lerp(transform.position, trueLoc, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, trueRot, Time.deltaTime * 5);

            if (photonView.IsMine)
            {
                ovrAvatar = GetComponent<OvrAvatar>();
                ovrAvatar.RecordPackets = true;
                ovrAvatar.PacketRecorded += OnLocalAvatarPacketRecorded;

                packetData = new List<byte[]>();
            }
            else
            {
                remoteDriver = GetComponent<OvrAvatarRemoteDriver>();
            }
        }



        public void OnDisable()
        {
            if (photonView.IsMine)
            {
                ovrAvatar.RecordPackets = false;
                ovrAvatar.PacketRecorded -= OnLocalAvatarPacketRecorded;
            }
        }

        void Update()
        {

            if (!photonView.IsMine)
            {
                transform.position = Vector3.Lerp(transform.position, trueLoc, Time.deltaTime * 5);
                transform.rotation = Quaternion.Lerp(transform.rotation, trueRot, Time.deltaTime * 5);

            }
            if (Health <= 0f)
            {
                GameManager.Instance.LeaveRoom();
            }
        }

        private int localSequence;

        public void OnLocalAvatarPacketRecorded(object sender, OvrAvatar.PacketEventArgs args)
        {
            if (!PhotonNetwork.InRoom || (PhotonNetwork.CurrentRoom.PlayerCount < 2))
            {
                return;
            }

            using (MemoryStream outputStream = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(outputStream);

                var size = Oculus.Avatar.CAPI.ovrAvatarPacket_GetSize(args.Packet.ovrNativePacket);
                byte[] data = new byte[size];
                Oculus.Avatar.CAPI.ovrAvatarPacket_Write(args.Packet.ovrNativePacket, size, data);

                writer.Write(localSequence++);
                writer.Write(size);
                writer.Write(data);

                packetData.Add(outputStream.ToArray());
            }
        }

        private void DeserializeAndQueuePacketData(byte[] data)
        {
            using (MemoryStream inputStream = new MemoryStream(data))
            {
                BinaryReader reader = new BinaryReader(inputStream);
                int remoteSequence = reader.ReadInt32();

                int size = reader.ReadInt32();
                byte[] sdkData = reader.ReadBytes(size);

                System.IntPtr packet = Oculus.Avatar.CAPI.ovrAvatarPacket_Read((System.UInt32)data.Length, sdkData);
                remoteDriver.QueuePacket(remoteSequence, new OvrAvatarPacket { ovrNativePacket = packet });
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                if (photonView.IsMine)
                {

                    if (packetData.Count == 0)
                    {
                        return;
                    }

                    stream.SendNext(packetData.Count);

                    foreach (byte[] b in packetData)
                    {
                        stream.SendNext(b);
                    }

                    stream.SendNext(transform.position);
                    stream.SendNext(transform.rotation);
                    stream.SendNext(Health);
                    packetData.Clear();

                }
            }

            if (stream.IsReading)
            {
                if (!photonView.IsMine)
                {

                    int num = (int)stream.ReceiveNext();

                    for (int counter = 0; counter < num; ++counter)
                    {
                        byte[] data = (byte[])stream.ReceiveNext();

                        DeserializeAndQueuePacketData(data);
                    }

                    this.trueLoc = (Vector3)stream.ReceiveNext() + new Vector3(0f, 1f, 0f);
                    this.trueRot = (Quaternion)stream.ReceiveNext();
                    this.Health = (float)stream.ReceiveNext();

                }
            }
        }


        // private void OnTriggerEnter(Collider other)
        //{
        // if (!other.gameObject.CompareTag("Sharp"))

        //  {
        //     return;
        //  }

        // Health -= 10.0f;
        // }

        public void CollisionDetected(Armor childScript)
        {
            Health -= 10.0f;
        }

    }
}