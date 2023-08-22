using UnityEngine;
using Cinemachine;

namespace Game.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private AxisState xAxis;
        [SerializeField] private AxisState yAxis;
        
        [SerializeField] private Transform lookAt;

        // Update is called once per frame
        void Update()
        {
            xAxis.Update(Time.deltaTime);
            yAxis.Update(Time.deltaTime);

            lookAt.eulerAngles = new Vector3 (yAxis.Value, xAxis.Value, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, xAxis.Value, 0), 5 * Time.deltaTime);
        }
    }
}