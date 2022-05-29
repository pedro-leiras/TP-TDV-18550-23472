using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trabalho_Pratico
{
    public class Sounds
    {
        public Sounds(ContentManager content)
        {
            Song backgroundSound = content.Load<Song>("theme-song");
            MediaPlayer.Play(backgroundSound);
            MediaPlayer.IsRepeating = true;
        }

        public void SoundState(bool isMuted)
        {
            if (isMuted) { 
                MediaPlayer.Resume();
            }
            else
            {
                MediaPlayer.Pause();
            }
            
        }
    }
}
