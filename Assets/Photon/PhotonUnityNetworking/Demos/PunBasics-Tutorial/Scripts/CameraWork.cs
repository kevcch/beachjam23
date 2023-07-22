// --------------------------------------------------------------------------------------------------------------------

// <copyright file="CameraWork.cs" company="Exit Games GmbH">

//   Part of: Photon Unity Networking Demos

// </copyright>

// <summary>

//  Used in PUN Basics Tutorial to deal with the Camera work to follow the player

// </summary>

// <author>developer@exitgames.com</author>

// --------------------------------------------------------------------------------------------------------------------



using UnityEngine;



namespace Photon.Pun.Demo.PunBasics

{

	/// <summary>

	/// Camera work. Follow a target

	/// </summary>

	public class CameraWork : MonoBehaviour

	{

		public Transform targetTransform;

		public bool lookAt = true;

        #region Private Fields



	    [Tooltip("The distance in the local x-z plane to the target")]

	    [SerializeField]

	    private float distance = 7.0f;

	    

	    [Tooltip("The height we want the camera to be above the target")]

	    [SerializeField]

	    private float height = 3.0f;

	    

	    [Tooltip("Allow the camera to be offseted vertically from the target, for example giving more view of the sceneray and less ground.")]

	    [SerializeField]

	    private Vector3 centerOffset = Vector3.zero;



	    [Tooltip("Set this as false if a component of a prefab being instanciated by Photon Network, and manually call OnStartFollowing() when and if needed.")]

	    [SerializeField]

	    private bool followOnStart = false;



	    [Tooltip("The Smoothing for the camera to follow the target")]

	    [SerializeField]

	    private float smoothSpeed = 100f;



        // cached transform of the target

        Transform cameraTransform;

		Quaternion cameraRotationOnStart;

		// maintain a flag internally to reconnect if target is lost or camera is switched

		bool isFollowing;

		

		// Cache for camera offset

		Vector3 cameraOffset = Vector3.zero;


		#endregion


		#region MonoBehaviour Callbacks


		public void UpdateTargetTransform( Transform _newTransform) {

			targetTransform = _newTransform;

			Cut();

		}

        /// <summary>

        /// MonoBehaviour method called on GameObject by Unity during initialization phase

        /// </summary>

        void Start()

		{

			targetTransform = transform;

			// Start following the target if wanted.

			if (followOnStart)

			{

				OnStartFollowing();

			}

		}





		void Update()

		{

			// The transform target may not destroy on level load, 

			// so we need to cover corner cases where the Main Camera is different everytime we load a new scene, and reconnect when that happens

			if (cameraTransform == null && isFollowing)

			{

				OnStartFollowing();

			}



			// only follow is explicitly declared

			if (isFollowing) {

				Follow();

			}

		}



		#endregion



		#region Public Methods



		/// <summary>

		/// Raises the start following event. 

		/// Use this when you don't know at the time of editing what to follow, typically instances managed by the photon network.

		/// </summary>

		public void OnStartFollowing()

		{	      

			cameraTransform = GameObject.Find("PlayerCamera").gameObject.transform;
			cameraRotationOnStart = cameraTransform.rotation;

			isFollowing = true;

			// we don't smooth anything, we go straight to the right camera shot

			Cut();

		}

		

		#endregion



		#region Private Methods



		/// <summary>

		/// Follow the target smoothly

		/// </summary>

		void Follow()

		{

			if(lookAt)
			{
				cameraOffset = -distance * targetTransform.GetChild(0).forward.normalized;
				cameraOffset.y = height;
				cameraTransform.position = targetTransform.position + cameraOffset;
				cameraTransform.LookAt(targetTransform.position);
			}
			else
			{
				cameraOffset.z = -distance;
				cameraOffset.y = height;
				cameraTransform.rotation = cameraRotationOnStart;
				cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetTransform.position + cameraOffset, smoothSpeed*Time.deltaTime);
			}

		    

	    }



	   

		public void Cut()

		{

			cameraOffset.z = -distance;

			cameraOffset.y = height;



			cameraTransform.position = targetTransform.position + cameraOffset;



			//cameraTransform.LookAt(targetTransform.position + centerOffset);

		}

		#endregion

	}

}
