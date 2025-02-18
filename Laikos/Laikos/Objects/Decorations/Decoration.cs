﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Animation;
using MyDataTypes;

namespace Laikos
{
    public class Decoration : GameObject
    {
        public DecorationType type;
        public List<Message> messages;
        public List<BoundingBox> meshBoundingBoxes;

        public Vector2 Size
        {
            get
            {
                BoundingBox box = XNAUtils.TransformBoundingBox(Collisions.GetBoundingBox(currentModel.Model), GetWorldMatrix());

                return new Vector2(Math.Abs(box.Max.X - box.Min.X), Math.Abs(box.Max.Z - box.Min.Z));
            }
        }

        public Decoration()
            : base()
        {
        }

        public Decoration(Game game, DecorationType type, Vector3 position, float scale = 1.0f, Vector3 rotation = default(Vector3))
            : base(game,null, type.model)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
            this.messages = new List<Message>();
            this.meshBoundingBoxes = new List<BoundingBox>();
            this.type = (DecorationType)type.Clone();
            Matrix[] modelTransforms = new Matrix[currentModel.Model.Bones.Count];
            currentModel.Model.CopyAbsoluteBoneTransformsTo(modelTransforms);
            foreach (ModelMesh mesh in currentModel.Model.Meshes)
            {
                List<Vector3> meshVertices = new List<Vector3>();
                Matrix transformations = modelTransforms[mesh.ParentBone.Index] * GetWorldMatrix();
                VertexHelper.ExtractModelMeshData(mesh, ref transformations, meshVertices);
                meshBoundingBoxes.Add(BoundingBox.CreateFromPoints(meshVertices));
            }
        }

        public void Update(GameTime gameTime)
        {
            //HandleEvent(gameTime);
            base.Update(gameTime);
        }

        public bool checkIfPossible(Vector3 startPosition)
        {
            BoundingBox box = XNAUtils.TransformBoundingBox(Collisions.GetBoundingBox(currentModel.Model), GetWorldMatrix());
            Vector3 size = box.Max - box.Min;
            float lowestPoint = float.MaxValue;
            float highestPoint = float.MinValue;

            for (int i = (int)startPosition.X; i < size.X + startPosition.X; i++)
            {
                for (int j = (int)startPosition.Z; j < size.Z + startPosition.Z; j++)
                {
                    if (Terrain.GetClippedHeightAt(i, j) < lowestPoint) lowestPoint = Terrain.GetClippedHeightAt(i, j);
                    if (Terrain.GetClippedHeightAt(i, j) > lowestPoint) highestPoint = Terrain.GetClippedHeightAt(i, j);
                }
            }

           // Console.WriteLine(lowestPoint + " " + highestPoint);
            if (highestPoint - lowestPoint < 1)
                return true;
            else
                return false;
        }

        public override void HandleEvent(GameTime gameTime)
        {
            EventManager.FindMessagesByDestination(this, messages);
            for (int i = 0; i < messages.Count; i++)
            {
                switch (messages[i].Type)
                {
                    case (int)EventManager.Events.Interaction:
                        Console.WriteLine("Dekoracja - obsluga interakcji");
                        break;
                }
            }
        }
    }
}