using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem MainEngineParticles;
    [SerializeField] ParticleSystem RightThrustParticles;
    [SerializeField] ParticleSystem LeftThrustParticles;

    Rigidbody rb;
    AudioSource audioSource;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = rb.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();

    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        if (!MainEngineParticles.isPlaying)
        {
            MainEngineParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        MainEngineParticles.Stop();
    }

    private void ProcessRotation()
    {
       float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            RotateRight();
        }
        else if (rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();

        }
    }
    private void RotateRight()
    {
        ApplyRotation(rotationStrength);
        if (!RightThrustParticles.isPlaying)
        {
            LeftThrustParticles.Stop();
            RightThrustParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(-rotationStrength);
        if (!LeftThrustParticles.isPlaying)
        {
            RightThrustParticles.Stop();
            LeftThrustParticles.Play();
        }
    }
    private void StopRotating()
    {
        RightThrustParticles.Stop();
        LeftThrustParticles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
