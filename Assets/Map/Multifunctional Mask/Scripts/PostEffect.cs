using UnityEngine;
namespace MultyMaskShader
{
    [ExecuteInEditMode]
    public class PostEffect : MonoBehaviour
    {
        public Material _material;
        public Material material
        {
            get
            {
                return _material;
            }
            set
            {
                _material = value;

                //_material = Instantiate(value);
                //_material.name = value.name;
                if (_firstTempMat != null)
                    Destroy(_firstTempMat);
                if (_secondTempMat != null)
                    Destroy(_secondTempMat);

                if (_material != null)
                {
#if !UNITY_WEBGL
                    material.shader = decartShader;
#else
                material.shader = countDecartShader;
#endif
                    _firstTempMat = Instantiate(_material);
                    _secondTempMat = Instantiate(_material);
                    Offset = _material.GetFloat("_offset");
                    int nX = _material.GetInt("_nX");
                    int nY = _material.GetInt("_nY");
                    if (nX >= nY)
                        X_YKoef = nY / nX;
                    else
                        X_YKoef = 2 - nX / nY;

                    Quality = Mathf.Max(nX, nY);

                    IterationMin = _material.GetInt("_iterMin");
                    IterationMax = _material.GetInt("_iterMax");
                    Min = _material.GetInt("_min");
                    Max = _material.GetInt("_max");
                    MainMask = (Texture2D)_material.GetTexture("_MaskTex");
                    BigMask = (Texture2D)_material.GetTexture("_BigMaskTex");
                    MulColor = material.color;
                }
            }
        }

        //public Material materialMid;
        public int numPass = 1;
        public enum StatusMask { QUADRO, CROSS, RADIALQUADRO, RADIALCROSS }
        public StatusMask statusMask;

        public Shader decartShader;
        public Shader polarShader;
        public Shader countDecartShader;
        public Shader countPolarShader;
        public float Offset = 0.01f;
        public float X_YKoef = 1;
        public int Quality;
        private int _nX, _nY;

        public float IterationMin;
        public float IterationMax;
        public float Min;
        public float Max;
        public Color MulColor;

        public Texture2D MainMask;
        public Texture2D BigMask;

        private Material _firstTempMat;
        private Material _secondTempMat;

        void Start()
        {

            material = material;

            //_firstTempMat = Instantiate(material);
            //_secondTempMat = Instantiate(material);
            //GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
        }

        RenderTexture last;

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (_firstTempMat == null)
                return;

            _firstTempMat.SetFloat("_offset", Offset);
            if (X_YKoef <= 0)
            {
                _nX = Quality;
                _nY = Mathf.Max(1, Mathf.RoundToInt(Quality * X_YKoef));
            }
            else
            {
                _nX = Mathf.Max(1, Mathf.RoundToInt(Quality * (2 - X_YKoef)));
                _nY = Quality;
            }
            _firstTempMat.SetFloat("_iterMin", IterationMin);
            _firstTempMat.SetFloat("_iterMax", IterationMax);
            _firstTempMat.SetFloat("_min", Min);
            _firstTempMat.SetFloat("_max", Max);
            _firstTempMat.SetTexture("_MaskTex", MainMask);
            _firstTempMat.SetTexture("_BigMaskTex", BigMask);
            _firstTempMat.color = MulColor;

            if (statusMask == StatusMask.QUADRO || statusMask == StatusMask.RADIALQUADRO)
            {
                _firstTempMat.SetInt("_nX", _nX);
                _firstTempMat.SetInt("_nY", _nY);
            }
            else
            {
                _firstTempMat.SetInt("_nX", 1);
                _firstTempMat.SetInt("_nY", _nY);
                _secondTempMat.SetInt("_nX", _nX);
                _secondTempMat.SetInt("_nY", 1);
                _secondTempMat.SetFloat("_max", 1);
                _secondTempMat.SetFloat("_min", 0);
                _secondTempMat.SetFloat("_offset", Offset);
                _secondTempMat.SetTexture("_MaskTex", MainMask);
                _secondTempMat.SetTexture("_BigMaskTex", BigMask);
                //_secondTempMat.color = MulColor;
                /*if (_secondTempMat.GetFloat("_iterMin") > _secondTempMat.GetFloat("_iterMax"))
                {
                    float max = _secondTempMat.GetFloat("_iterMax");
                    _secondTempMat.SetFloat("_iterMax", _secondTempMat.GetInt("_iterMin"));
                    _secondTempMat.SetFloat("_iterMin", max);
                }*/

                _secondTempMat.SetFloat("_iterMax", 1);
                _secondTempMat.SetFloat("_iterMin", 0);
            }

            if (statusMask == StatusMask.QUADRO || statusMask == StatusMask.CROSS)
            {
#if !UNITY_WEBGL
                material.shader = decartShader;
                _firstTempMat.shader = decartShader;
                _secondTempMat.shader = decartShader;
#else
            material.shader = countDecartShader;
            _firstTempMat.shader = countDecartShader;
            _secondTempMat.shader = countDecartShader;
#endif
            }
            else
            {
#if !UNITY_WEBGL
                material.shader = polarShader;
                _firstTempMat.shader = polarShader;
                _secondTempMat.shader = polarShader;
#else
            material.shader = countPolarShader;
            _firstTempMat.shader = countPolarShader;
            _secondTempMat.shader = countPolarShader;
#endif
            }


            if (statusMask == StatusMask.QUADRO || statusMask == StatusMask.RADIALQUADRO)
                PassesMat(source, destination, _firstTempMat);
            else
            {
                RenderTexture temp = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
                PassesMat(source, temp, _firstTempMat);

                PassesMat(temp, destination, _secondTempMat);
                RenderTexture.ReleaseTemporary(temp);
            }
        }

        private void PassesMat(RenderTexture source, RenderTexture destination, Material mat)
        {
            if (numPass == 1)
                Graphics.Blit(source, destination, mat);
            else
            {
                float startOffset = mat.GetFloat("_offset");
                float currentOffset = startOffset;
                RenderTexture temp = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
                Graphics.Blit(source, temp, mat);
                //currentOffset *= 0.75f;
                //mat.SetFloat("_offset", currentOffset);
                for (int i = 1; i < numPass - 1; i++)
                {
                    RenderTexture temp2 = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
                    Graphics.Blit(temp, temp2, mat);
                    //currentOffset *= 0.75f;
                    //mat.SetFloat("_offset", currentOffset);
                    RenderTexture.ReleaseTemporary(temp);
                    temp = temp2;
                }
                Graphics.Blit(temp, destination, mat);
                //mat.SetFloat("_offset", startOffset);
                RenderTexture.ReleaseTemporary(temp);
            }
        }

        void Update()
        {

        }
    }
}
