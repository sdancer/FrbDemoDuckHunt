using System;
using System.Collections.Generic;

using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.Utilities;
using FrbUi;
using Microsoft.Xna.Framework;
#if !FRB_MDX
using System.Linq;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endif
using FlatRedBall.Screens;

namespace FrbDemoDuckHunt
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
			
#if WINDOWS_PHONE || ANDROID || IOS

			// Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            graphics.IsFullScreen = true;
#else
            graphics.PreferredBackBufferHeight = 600;
#endif
        }

        protected override void Initialize()
        {
            Renderer.UseRenderTargets = false;
            FlatRedBallServices.InitializeFlatRedBall(this, graphics);
			GlobalContent.Initialize();
			CameraSetup.SetupCamera(SpriteManager.Camera, graphics);
            FlatRedBallServices.GraphicsOptions.TextureFilter = TextureFilter.Point;
            this.IsMouseVisible = true;
			FlatRedBall.Screens.ScreenManager.Start(typeof(FrbDemoDuckHunt.Screens.GameMenu));



            base.Initialize();
        }

        /// <summary>
        /// Some gfx cards render improperly when a texel
        /// falls right inbetween pixels.  This addresses the
        /// problem.
        /// </summary>
        public static void OffsetCameraForPixelPerfectRenering()
        {
            Camera.Main.X = .2f;
            Camera.Main.Y = .2f;
        }


        protected override void Update(GameTime gameTime)
        {
            UiControlManager.Instance.RunActivities();
            FlatRedBallServices.Update(gameTime);

            FlatRedBall.Screens.ScreenManager.Activity();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            UiControlManager.Instance.UpdateDependencies();
            FlatRedBallServices.Draw();

            base.Draw(gameTime);
        }
    }
}
