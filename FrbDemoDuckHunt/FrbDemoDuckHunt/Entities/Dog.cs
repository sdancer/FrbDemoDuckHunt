using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;


#endif

namespace FrbDemoDuckHunt.Entities
{
	public partial class Dog
	{
        public void WalkingSniffingThenDiving(Action finishedCallback)
        {
            var currentTime = 0.0;

            //Initial
            Y = WalkingStartY;
            X = WalkingStartX;
            Z = 0;
            VisibleInstance.Visible = true;
            XVelocity = WalkingSpeed;
            this.CurrentState = VariableState.Walking;
            currentTime += 1.8;

            //First Sniff
            this.Set("XVelocity").To(0f).After(currentTime);
            this.Set("CurrentState").To(VariableState.Sniffing).After(currentTime);
            currentTime += .9;

            //2nd Walk
            this.Set("XVelocity").To(WalkingSpeed).After(currentTime);
            this.Set("CurrentState").To(VariableState.Walking).After(currentTime);
            currentTime += 1.8;

            //2nd Sniff
            this.Set("XVelocity").To(0f).After(currentTime);
            this.Set("CurrentState").To(VariableState.Sniffing).After(currentTime);
            currentTime += .9;

            //Happy
            this.Set("CurrentState").To(VariableState.Happy).After(currentTime);
            currentTime += .4;

            //Jump
            this.Set("CurrentState").To(VariableState.Jumping).After(currentTime);
            this.Set("XVelocity").To(JumpingXSpeed).After(currentTime);
            this.Set("YVelocity").To(JumpingYSpeed).After(currentTime);
            this.Call(() => GlobalContent.DogBarkSoundEffect.Play()).After(currentTime);
            currentTime += .5;
            this.Set("YAcceleration").To(JumpingYDeceleration).After(currentTime);
            currentTime += .5;
            this.Set("Z").To(-2f).After(currentTime);
            currentTime += 1.8;

            //Stop
            this.Set("XVelocity").To(0f).After(currentTime);
            this.Set("YVelocity").To(0f).After(currentTime);
            this.Set("YAcceleration").To(0f).After(currentTime);
            this.VisibleInstance.Set("Visible").To(false).After(currentTime);

            //Callback
            this.Call(finishedCallback).After(currentTime);
        }

        public void ShortWalkingSniffingThenDiving(Action finishedCallback)
        {
            var currentTime = 0.0;

            //Initial
            Y = WalkingStartY;
            X = ShortWalkingLeft;
            Z = 0;
            VisibleInstance.Visible = true;
            XVelocity = WalkingSpeed;
            this.CurrentState = VariableState.Walking;
            currentTime += .6;

            //First Sniff
            this.Set("XVelocity").To(0f).After(currentTime);
            this.Set("CurrentState").To(VariableState.Sniffing).After(currentTime);
            currentTime += .9;

            //Happy
            this.Set("CurrentState").To(VariableState.Happy).After(currentTime);
            currentTime += .4;

            //Jump
            this.Set("CurrentState").To(VariableState.Jumping).After(currentTime);
            this.Set("XVelocity").To(JumpingXSpeed).After(currentTime);
            this.Set("YVelocity").To(JumpingYSpeed).After(currentTime);
            this.Call(() => GlobalContent.DogBarkSoundEffect.Play()).After(currentTime);
            currentTime += .5;
            this.Set("YAcceleration").To(JumpingYDeceleration).After(currentTime);
            currentTime += .5;
            this.Set("Z").To(-2f).After(currentTime);
            currentTime += 1.8;

            //Stop
            this.Set("XVelocity").To(0f).After(currentTime);
            this.Set("YVelocity").To(0f).After(currentTime);
            this.Set("YAcceleration").To(0f).After(currentTime);
            this.VisibleInstance.Set("Visible").To(false).After(currentTime);

            //Callback
            this.Call(finishedCallback).After(currentTime);
        }

        public void OneDuck(float position, Action finishedCallback)
        {
            var currentTime = 0.0;

            //Initial
            Y = DuckStartY;
            X = position > DuckMaxStartX 
                    ? DuckMaxStartX 
                    : position < DuckMinStartX 
                        ? DuckMinStartX 
                        : position;
            Z = -2;
            XVelocity = 0;
            YVelocity = DogDuckMoveSpeed;
            VisibleInstance.Visible = true;
            CurrentState = VariableState.OneDuck;

            //Stop At Top
            currentTime += .5;
            this.Set("YVelocity").To(0f).After(currentTime);

            //Start Down
            currentTime += .5;
            this.Set("YVelocity").To(-DogDuckMoveSpeed).After(currentTime);

            //Stop
            currentTime += .5;
            this.Set("YVelocity").To(0f).After(currentTime);
            this.VisibleInstance.Set("Visible").To(false).After(currentTime);

            //Callback
            this.Call(finishedCallback).After(currentTime);

            GlobalContent.DuckRelease.Play();
        }

        public void TwoDucks(float position, Action finishedCallback)
        {
            var currentTime = 0.0;

            //Initial
            Y = DuckStartY;
            X = position > DuckMaxStartX
                    ? DuckMaxStartX
                    : position < DuckMinStartX
                        ? DuckMinStartX
                        : position;
            Z = -2;
            XVelocity = 0;
            YVelocity = DogDuckMoveSpeed;
            VisibleInstance.Visible = true;
            CurrentState = VariableState.TwoDucks;

            //Stop At Top
            currentTime += .5;
            this.Set("YVelocity").To(0f).After(currentTime);

            //Start Down
            currentTime += .5;
            this.Set("YVelocity").To(-DogDuckMoveSpeed).After(currentTime);

            //Stop
            currentTime += .5;
            this.Set("YVelocity").To(0f).After(currentTime);
            this.VisibleInstance.Set("Visible").To(false).After(currentTime);

            //Callback
            this.Call(finishedCallback).After(currentTime);

            GlobalContent.DuckRelease.Play();
        }

        public void Laugh(Action finishedCallback)
        {
            var currentTime = 0.0;

            //Initial
            Y = DuckStartY;
            X = 0;
            Z = -2;
            XVelocity = 0;
            YVelocity = DogDuckMoveSpeed;
            VisibleInstance.Visible = true;
            CurrentState = VariableState.Laughing;

            //Stop At Top
            currentTime += .5;
            this.Set("YVelocity").To(0f).After(currentTime);

            //Start Down
            currentTime += .5;
            this.Set("YVelocity").To(-DogDuckMoveSpeed).After(currentTime);

            //Stop
            currentTime += .5;
            this.Set("YVelocity").To(0f).After(currentTime);
            this.VisibleInstance.Set("Visible").To(false).After(currentTime);

            //Callback
            this.Call(finishedCallback).After(currentTime);

            GlobalContent.DogLaughingSoundEffect.Play();
        }

        public void EndGame(Action finishedCallback)
        {
            var currentTime = 0.0;

            //Initial
            Y = DuckStartY;
            X = 0;
            Z = -2;
            XVelocity = 0;
            YVelocity = DogDuckMoveSpeed;
            VisibleInstance.Visible = true;
            CurrentState = VariableState.Laughing;

            //Stop At Top
            currentTime += .5;
            this.Set("YVelocity").To(0f).After(currentTime);

            //Callback
            currentTime += 4;
            this.Call(finishedCallback).After(currentTime);
        }

		private void CustomInitialize()
		{


		}

		private void CustomActivity()
		{


		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
