using System.IO;
using UnityEngine;

namespace Utilities
{
    public static class ImgToSprite
    {
        public static Sprite LoadNewSprite(string filePath, float pixelsPerUnit = 100.0f) {
            // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference
            Texture2D spriteTexture = LoadTexture(filePath);
            Sprite newSprite = Sprite.Create(spriteTexture, 
                new Rect(0, 0, spriteTexture.width, spriteTexture.height),
                new Vector2(0,0), pixelsPerUnit);
            return newSprite;
        }
 
        public static Texture2D LoadTexture(string filePath) {
            // Load a PNG or JPG file from disk to a Texture2D
            // Returns null if load fails
            if (File.Exists(filePath)){
                byte[] fileData = File.ReadAllBytes(filePath);
                Texture2D texture2D = new Texture2D(2, 2); // Create new "empty" texture
                if (texture2D.LoadImage(fileData)) // Load the image data into the texture (size is set automatically)
                    return texture2D; // If data = readable -> return texture
            }  
            return null; // Return null if load failed
        }
    }
}