﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Animation;

using MyDataTypes;

namespace Laikos
{
   public class DecorationManager : DrawableGameComponent
    {
        public Dictionary<String,DecorationType> DecorationTypes;
        public List<Decoration> DecorationList;
        
        public GraphicsDevice device;
        GraphicsDeviceManager graphics;
        public Game game;
        
        //List<Message> messages;

        public DecorationManager(Game game, GraphicsDevice device, GraphicsDeviceManager graphics)
            : base(game)
        {
            this.game = game;
            this.graphics = graphics;
            this.device = device;
        }

        public override void Initialize()
        {
            DecorationTypes = new Dictionary<String, DecorationType>();
            DecorationList = new List<Decoration>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            DecorationTypes = Game.Content.Load<DecorationType[]>("ObjectTypes/DecorationTypes").ToDictionary(t => t.name);
             
            //DecorationList.Add(new Decoration(game, DecorationTypes["Drzewo4"], new Vector3(650, 25, 750), 0.05f));
            //DecorationList.Add(new Decoration(game, DecorationTypes["ruiny2"], new Vector3(30, 25, 150), 1.5f));


        }

        public override void Update(GameTime gameTime)
        {
            foreach (Decoration unit in DecorationList)
            {
                
                unit.Update(gameTime);
            }
        }
    }
}
