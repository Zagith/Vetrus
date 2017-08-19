using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MultyMaskShader
{
    public class DemoMask : MonoBehaviour
    {

        [System.Serializable]
        public class MaterialVar
        {
            public Material MainMat;
            public bool isFastEnable;
        }

        private bool isVisible = true;
        public enum Status { CAMERA, NONE }
        public enum PassStatus { QUADRO, CROSS, RADIAL_QUADRO, RADIAL_CROSS }
        private Status status = Status.CAMERA;

        private PostEffect post;
        public MaterialVar[] PostMats;
        private bool _isCameraFastEnable;

        public Texture2D[] MainMasks;
        public Texture2D[] BigMasks;

        public Material TextMat;
        public Texture2D[] TestTex;
        public GUIStyle ScrollStyle;

        //property for setting
        public float Offset
        {
            get
            {
                return post.Offset;
            }
            set
            {
                post.Offset = value;
            }
        }

        //property for setting
        public int Quality
        {
            get
            {
                return post.Quality;
            }
            set
            {
                post.Quality = value;
            }
        }

        public float X_YKoef
        {
            get
            {
                return post.X_YKoef;
            }
            set
            {
                post.X_YKoef = value;
            }
        }

        public float IterationMin
        {
            get
            {
                return post.IterationMin;
            }
            set
            {
                post.IterationMin = value;
            }
        }

        public float IterationMax
        {
            get
            {
                return post.IterationMax;
            }
            set
            {
                post.IterationMax = value;
            }
        }

        public float Max
        {
            get
            {
                return post.Max;
            }
            set
            {
                post.Max = value;
            }
        }

        public float Min
        {
            get
            {
                return post.Min;
            }
            set
            {
                post.Min = value;
            }
        }

        public Color MulColor
        {
            get
            {
                return post.MulColor;
            }
            set
            {
                post.MulColor = value;
            }
        }

        public Texture2D MainMask
        {
            get
            {
                return post.MainMask;
            }
            set
            {
                post.MainMask = value;
            }
        }

        public Texture2D BigMask
        {
            get
            {
                return post.BigMask;
            }
            set
            {
                post.BigMask = value;
            }
        }

        // Use this for initialization
        void Start()
        {
            post = GetComponent<PostEffect>();
            post.material = Instantiate(PostMats[0].MainMat);
            post.material.name = PostMats[0].MainMat.name;
            _isCameraFastEnable = PostMats[0].isFastEnable;
            post.material = PostMats[0].MainMat;
            _isCameraFastEnable = PostMats[0].isFastEnable;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private int _fontSize;
        private int _size;
        private int _sizeY;

        private string formatGUI(string str)
        {
            return "<size=" + _fontSize + ">" + str + "</size>";
        }

        void OnGUI()
        {
            _size = Screen.height / 5;
            _sizeY = _size / 3;
            _fontSize = _size / 9;
            if (isVisible)
            {
                if (GUI.Button(new Rect(0, 0, _size, _sizeY), formatGUI("Hide")))
                {
                    isVisible = false;
                }

                Rect curr = new Rect(0, _sizeY, _size, _sizeY);
                if (status == Status.CAMERA)
                {
                    GUI.Box(curr, formatGUI("Camera"));
                    CameraGUI();
                }
                else if (GUI.Button(curr, formatGUI("Camera")))
                {
                    status = Status.CAMERA;
                }

                /*curr = new Rect(0, _sizeY * 2, _size, _sizeY);
                if (status == Status.UI)
                {
                    GUI.Box(curr, formatGUI("UI"));
                    UIGUI();
                }
                else if (GUI.Button(curr, formatGUI("UI")))
                {
                    status = Status.UI;
                }

                curr = new Rect(0, _sizeY * 3, _size, _sizeY);
                if (status == Status.MESH_TRANSPARENT)
                {
                    GUI.Box(curr, formatGUI("3DTransparency"));
                    MESHGUI();
                }
                else if (GUI.Button(curr, formatGUI("3DTransparency")))
                {
                    status = Status.MESH_TRANSPARENT;
                }*/

                curr = new Rect(0, _sizeY * 2, _size, _sizeY);
                if (status == Status.NONE)
                    GUI.Box(curr, formatGUI("None"));
                else if (GUI.Button(new Rect(0, _sizeY * 2, _size, _sizeY), formatGUI("None")))
                    status = Status.NONE;

                for (int i = 0; i < TestTex.Length; i++)
                {
                    curr = new Rect(Screen.width - _size, Screen.height - _sizeY * (i + 1), _size, _sizeY);
                    if (TextMat.mainTexture == TestTex[i])
                        GUI.Box(curr, formatGUI("Texture №" + (i + 1)));
                    else if (GUI.Button(curr, formatGUI("Texture №" + (i + 1))))
                        TextMat.mainTexture = TestTex[i];
                }

            }
            else
            {
                if (GUI.Button(new Rect(0, 0, _size, _sizeY), formatGUI("Show")))
                {
                    isVisible = true;
                }
            }

            post.enabled = status == Status.CAMERA;
        }

        void CameraGUI()
        {
            if (post.statusMask == PostEffect.StatusMask.QUADRO)
                GUI.Box(new Rect(2 * _size, 0, _size, _sizeY), formatGUI("Full pass"));
            else if (GUI.Button(new Rect(2 * _size, 0, _size, _sizeY), formatGUI("Full pass")))
            {
                post.statusMask = PostEffect.StatusMask.QUADRO;
            }

            if (post.statusMask == PostEffect.StatusMask.RADIALQUADRO)
                GUI.Box(new Rect(2 * _size, 2 * _sizeY, _size, _sizeY), formatGUI("Radial full pass"));
            else if (GUI.Button(new Rect(2 * _size, 2 * _sizeY, _size, _sizeY), formatGUI("Radial full pass")))
            {
                post.statusMask = PostEffect.StatusMask.RADIALQUADRO;
            }

            if (_isCameraFastEnable)
            {
                if (post.statusMask == PostEffect.StatusMask.CROSS)
                    GUI.Box(new Rect(2 * _size, _sizeY, _size, _sizeY), formatGUI("Fast pass"));
                else if (GUI.Button(new Rect(2 * _size, _sizeY, _size, _sizeY), formatGUI("Fast pass")))
                {
                    post.statusMask = PostEffect.StatusMask.CROSS;
                }

                if (post.statusMask == PostEffect.StatusMask.RADIALCROSS)
                    GUI.Box(new Rect(2 * _size, 3 * _sizeY, _size, _sizeY), formatGUI("Radial fast pass"));
                else if (GUI.Button(new Rect(2 * _size, 3 * _sizeY, _size, _sizeY), formatGUI("Radial fast pass")))
                {
                    post.statusMask = PostEffect.StatusMask.RADIALCROSS;
                }
            }
            else
            {
                if (post.statusMask == PostEffect.StatusMask.CROSS)
                    post.statusMask = PostEffect.StatusMask.QUADRO;
                if (post.statusMask == PostEffect.StatusMask.RADIALCROSS)
                    post.statusMask = PostEffect.StatusMask.RADIALQUADRO;
            }

            for (int i = 0; i < PostMats.Length; i++)
            {
                if (post.material == PostMats[i].MainMat)
                    GUI.Box(new Rect(_size, (i + 1) * _sizeY, _size, _sizeY), formatGUI(PostMats[i].MainMat.name));
                else if (GUI.Button(new Rect(_size, (i + 1) * _sizeY, _size, _sizeY), formatGUI(PostMats[i].MainMat.name)))
                {
                    post.material = PostMats[i].MainMat;
                    _isCameraFastEnable = PostMats[i].isFastEnable;
                }
            }
            SettingsGUI();
        }

        private void SettingsGUI()
        {

            //ScrollStyle = new GUIStyle(GUI.skin.button);
            GUI.skin.horizontalScrollbar = ScrollStyle;
            //GUI.skin.horizontalSlider = ScrollStyle;

            float _sizeY = this._sizeY * 0.7f;
            GUI.skin.textField.fontSize = (int)(_sizeY * 0.44f);
            float width = _size * 3;
            float widthScroll = _size * 1.9f;
            float x = Screen.width - width * 1.025f;
            float y = 0;
            GUI.Box(new Rect(x, y, width, _sizeY), "");
            float offset = Offset;
            offset = float.Parse(GUI.TextField(new Rect(x, y + _sizeY * 0.1f, _size * 0.23f, _sizeY * 0.8f), offset + ""));
            GUI.Label(new Rect(x + 0.2f * _size, y + _sizeY * 0.2f, _size, _sizeY), formatGUI("  Size: "));
            Offset = GUI.HorizontalScrollbar(new Rect(x + 1f * _size, y + _sizeY * 0.35f, widthScroll, _sizeY), offset, 0.001f, 0, 0.09f);

#if !UNITY_WEBGL
            y += _sizeY;
            GUI.Box(new Rect(x, y, width, _sizeY), "");
            int quality = Quality;
            quality = int.Parse(GUI.TextField(new Rect(x, y + _sizeY * 0.1f, _size * 0.23f, _sizeY * 0.8f), quality + ""));
            GUI.Label(new Rect(x + 0.2f * _size, y + _sizeY * 0.2f, _size, _sizeY), formatGUI("  Quality: "));
            Quality = Mathf.RoundToInt(GUI.HorizontalScrollbar(new Rect(x + 1f * _size, y + _sizeY * 0.35f, widthScroll, _sizeY), quality, 2, 1, 23));

            y += _sizeY;
            GUI.Box(new Rect(x, y, width, _sizeY), "");
            float _X_YKoef = X_YKoef;
            _X_YKoef = float.Parse(GUI.TextField(new Rect(x, y + _sizeY * 0.1f, _size * 0.23f, _sizeY * 0.8f), _X_YKoef + ""));
            GUI.Label(new Rect(x + 0.2f * _size, y + _sizeY * 0.2f, _size, _sizeY), formatGUI("  X_Y koef: "));
            X_YKoef = GUI.HorizontalScrollbar(new Rect(x + 1f * _size, y + _sizeY * 0.35f, widthScroll, _sizeY), _X_YKoef, 0.01f, 0, 2);
#endif

            y += _sizeY;
            GUI.Box(new Rect(x, y, width, _sizeY), "");
            float iterationMin = IterationMin;
            iterationMin = float.Parse(GUI.TextField(new Rect(x, y + _sizeY * 0.1f, _size * 0.23f, _sizeY * 0.8f), iterationMin + ""));
            GUI.Label(new Rect(x + 0.2f * _size, y + _sizeY * 0.2f, _size, _sizeY), formatGUI("   Iter min: "));
            IterationMin = GUI.HorizontalScrollbar(new Rect(x + 1f * _size, y + _sizeY * 0.35f, widthScroll, _sizeY), iterationMin, 0.01f, -15, 15);

            y += _sizeY;
            GUI.Box(new Rect(x, y, width, _sizeY), "");
            float iterationMax = IterationMax;
            iterationMax = float.Parse(GUI.TextField(new Rect(x, y + _sizeY * 0.1f, _size * 0.23f, _sizeY * 0.8f), iterationMax + ""));
            GUI.Label(new Rect(x + 0.2f * _size, y + _sizeY * 0.2f, _size, _sizeY), formatGUI("   Iter max: "));
            IterationMax = GUI.HorizontalScrollbar(new Rect(x + 1f * _size, y + _sizeY * 0.35f, widthScroll, _sizeY), iterationMax, 0.1f, -50, 50);

            y += _sizeY;
            GUI.Box(new Rect(x, y, width, _sizeY), "");
            float min = Min;
            min = float.Parse(GUI.TextField(new Rect(x, y + _sizeY * 0.1f, _size * 0.23f, _sizeY * 0.8f), min + ""));
            GUI.Label(new Rect(x + 0.2f * _size, y + _sizeY * 0.2f, _size, _sizeY), formatGUI("   Min: "));
            Min = GUI.HorizontalScrollbar(new Rect(x + 1f * _size, y + _sizeY * 0.35f, widthScroll, _sizeY), min, 0.01f, -7, 7);

            y += _sizeY;
            GUI.Box(new Rect(x, y, width, _sizeY), "");
            float max = Max;
            max = float.Parse(GUI.TextField(new Rect(x, y + _sizeY * 0.1f, _size * 0.23f, _sizeY * 0.8f), max + ""));
            GUI.Label(new Rect(x + 0.2f * _size, y + _sizeY * 0.2f, _size, _sizeY), formatGUI("   Max: "));
            Max = GUI.HorizontalScrollbar(new Rect(x + 1f * _size, y + _sizeY * 0.35f, widthScroll, _sizeY), max, 0.01f, -7, 7);

            y += _sizeY;
            GUI.Box(new Rect(x, y, width, _sizeY), "");
            GUI.Label(new Rect(x + 0.05f * _size, y + _sizeY * 0.2f, _size, _sizeY), formatGUI("   Mask: "));
            if (MainMask == null)
                GUI.Box(new Rect(x + 1f * _size, y, _size * 0.25f, _sizeY), "");
            else if (GUI.Button(new Rect(x + 1f * _size, y, _size * 0.25f, _sizeY), ""))
            {
                MainMask = null;
            }
            for (int i = 0; i < MainMasks.Length; i++)
            {
                if (MainMask == MainMasks[i])
                    GUI.Box(new Rect(x + (1.25f + 0.25f * i) * _size, y, _size * 0.25f, _sizeY), MainMasks[i]);
                else if (GUI.Button(new Rect(x + (1.25f + 0.25f * i) * _size, y, _size * 0.25f, _sizeY), MainMasks[i]))
                {
                    MainMask = MainMasks[i];
                }
            }
            //post.Max = GUI.HorizontalScrollbar(new Rect(x + 1f * _size, y + _sizeY * 0.35f, widthScroll, _sizeY), post.Max, 0.0025f, -7, 7);

            y += _sizeY;
            GUI.Box(new Rect(x, y, width, _sizeY), "");
            GUI.Label(new Rect(x + 0.05f * _size, y + _sizeY * 0.2f, _size, _sizeY), formatGUI("   Intensive mask: "));
            if (BigMask == null)
                GUI.Box(new Rect(x + 1f * _size, y, _size * 0.25f, _sizeY), "");
            else if (GUI.Button(new Rect(x + 1f * _size, y, _size * 0.25f, _sizeY), ""))
            {
                BigMask = null;
            }
            for (int i = 0; i < BigMasks.Length; i++)
            {
                if (BigMask == BigMasks[i])
                    GUI.Box(new Rect(x + (1.25f + 0.25f * i) * _size, y, _size * 0.25f, _sizeY), BigMasks[i]);
                else if (GUI.Button(new Rect(x + (1.25f + 0.25f * i) * _size, y, _size * 0.25f, _sizeY), BigMasks[i]))
                {
                    BigMask = BigMasks[i];
                }
            }
            //post.Max = GUI.HorizontalScrollbar(new Rect(x + 1f * _size, y + _sizeY * 0.35f, widthScroll, _sizeY), post.Max, 0.0025f, -7, 7);

            y += _sizeY;
            GUI.Box(new Rect(x, y, width, _sizeY * 2), "");
            GUI.Label(new Rect(x + 0.2f * _size, y + _sizeY * 0.2f, _size, _sizeY), formatGUI("   Gamma: "));
            Color col = MulColor;
            float _max = 2;
            GUI.skin.textField.fontSize = (int)(_sizeY * 0.3f);
            col.r = float.Parse(GUI.TextField(new Rect(x + 0.77f * _size, y + _sizeY * 0.1f, _size * 0.2f, _sizeY * 0.45f), col.r + ""));
            col.g = float.Parse(GUI.TextField(new Rect(x + 0.77f * _size, y + _sizeY * 0.75f, _size * 0.2f, _sizeY * 0.45f), col.g + ""));
            col.b = float.Parse(GUI.TextField(new Rect(x + 0.77f * _size, y + _sizeY * 1.3f, _size * 0.2f, _sizeY * 0.45f), col.b + ""));
            col.r = GUI.HorizontalScrollbar(new Rect(x + 1f * _size, y + _sizeY * 0.15f, widthScroll, _sizeY * 0.5f), col.r, 0.01f, 0, _max);
            col.g = GUI.HorizontalScrollbar(new Rect(x + 1f * _size, y + _sizeY * 0.8f, widthScroll, _sizeY * 0.5f), col.g, 0.01f, 0, _max);
            col.b = GUI.HorizontalScrollbar(new Rect(x + 1f * _size, y + _sizeY * 1.4f, widthScroll, _sizeY * 0.5f), col.b, 0.01f, 0, _max);
            MulColor = col;
        }
    }
}