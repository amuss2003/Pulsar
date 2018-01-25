using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using Proshot.CommandClient;
//using Proshot.UtilityLib.CommonDialogs;

namespace Pulsar.Classes
{
    internal static class ShareUtils
    {
        internal enum SoundType
        {
            NewClientEntered,
            NewMessageReceived,
            NewMessageWithPow,
            ClientExit
        }

        internal static void PlaySound(ShareUtils.SoundType soundType)
        {            
            //Proshot.ResourceManager.Resourcer rcMngr = new Proshot.ResourceManager.Resourcer(Proshot.ResourceManager.LoadMethod.FromCallingCode);
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            switch (soundType)
            {
                case (ShareUtils.SoundType.NewClientEntered):
                    player.Stream = global::Pulsar.Properties.Resources.Knock;   // rcMngr.GetResourceStream("Knock.wav");
                    player.Play();
                    break;
                case (ShareUtils.SoundType.ClientExit):
                    player.Stream = global::Pulsar.Properties.Resources.Door;    //rcMngr.GetResourceStream("Door.wav");
                    player.Play();
                    break;
                case (ShareUtils.SoundType.NewMessageReceived):
                    player.Stream = global::Pulsar.Properties.Resources.Message; //rcMngr.GetResourceStream("Message.wav");
                    player.Play();
                    break;
                case (ShareUtils.SoundType.NewMessageWithPow):
                    player.Stream = global::Pulsar.Properties.Resources.Pow;     //rcMngr.GetResourceStream("Pow.wav");
                    player.Play();
                    break;
            }
        }
    }
}
