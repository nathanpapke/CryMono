using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    internal class NativeRendererMethods : NativeMethods<INativeRendererMethods>, INativeRendererMethods
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern int GetWidth();
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern int GetHeight();

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern Vec3 ScreenToWorld(int x, int y);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void DrawTextToScreen(float x, float y, float fontSize, Color color, bool center, string text);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern int LoadTexture(string path);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void DrawTextureToScreen(float xpos, float ypos, float width, float height, int textureId, float s0 = 0, float t0 = 0, float s1 = 1, float t1 = 1, float angle = 0, float r = 1, float g = 1, float b = 1, float a = 1, float z = 1);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern int CreateRenderTarget(int width, int height, int flags);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void DestroyRenderTarget(int id);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void SetRenderTarget(int id);
    }
}
