using UnityEngine;


    public enum Axis { X, Y, Z }

    public class RotateObject : MonoBehaviour
    {
        [SerializeField] Axis axis;
        [SerializeField] private float speed = 3f;

        private static float _rotate;


        private void Update()
        {

            doRotate();
        }


        private void doRotate()
        {
            _rotate += speed * Time.deltaTime;

            switch (axis)
            {
                case Axis.X:

                    transform.eulerAngles = new Vector3(_rotate, 0, 0);

                    break;

                case Axis.Y:

                    transform.eulerAngles = new Vector3(0, _rotate, 0);

                    break;

                case Axis.Z:

                    transform.eulerAngles = new Vector3(0, 0, _rotate);

                    break;
            }

        }

    }

