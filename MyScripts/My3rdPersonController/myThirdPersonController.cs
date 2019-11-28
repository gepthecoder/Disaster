using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class myThirdPersonController : MonoBehaviour
{
    public static bool isReloading;

    public DynamicJoystick LeftJoystick;
    public FixedButton button;
    public FixedTouchField Touchfield;

    public GameObject fireParticle;
    public Transform shootFrom;

    private Animator playerAnimator;

    protected Rigidbody Rigidbody;

    protected float CameraAngleY;
    protected float CameraAngleSpeed = 0.2f;
    protected float CameraPosY;
    protected float CameraPosSpeed = 0.1f;

    protected float coolDown;
    protected float speedMultiplier = 5f;
    protected float speedMultiplier_Aim = 2f;


    public GameObject enemyHitEffect;
    public GameObject otherHitEffect;
    public GameObject explosionHitEffect;

    public ParticleSystem MuzzleFlash;

    public AudioClip fireSound;
    public AudioClip noAmmoClip;
    private AudioSource aSource;

    public int damageToTake = 10;

    private float jumpForce = 7f;

    public FixedButton jumpBtn;

    public bool isAiming;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
        aSource = GetComponent<AudioSource>();

        isReloading = false;
        isAiming = false;

    }

    void Update()
    {
        
        if (!EnterVehicle.inVehicle)
            //  Normal Player & Camera Features
        {
            if (!isAiming)
            {
                #region Movement Player/Camera

                var input = new Vector3(LeftJoystick.Horizontal, 0, LeftJoystick.Vertical);
                var vel = Quaternion.AngleAxis((CameraAngleY + 180), Vector3.up) * input * speedMultiplier;

                Rigidbody.velocity = new Vector3(vel.x, Rigidbody.velocity.y, vel.z);
                transform.rotation = Quaternion.AngleAxis(CameraAngleY + 180 + Vector3.SignedAngle(Vector3.forward, input.normalized + Vector3.forward * 0.001f, Vector3.up), Vector3.up);

                CameraAngleY += Touchfield.TouchDist.x * CameraAngleSpeed;
                CameraPosY = Mathf.Clamp(CameraPosY - Touchfield.TouchDist.y * CameraPosSpeed, 0, 5f);

                Camera.main.transform.position = transform.position + Quaternion.AngleAxis(CameraAngleY, Vector3.up) * new Vector3(0, CameraPosY, 5);

                Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2.8f - Camera.main.transform.position, Vector3.up);

                shootFrom.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2.8f - Camera.main.transform.position, Vector3.up);
                #endregion

                #region Shoot & Animations
                var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

                coolDown -= Time.deltaTime;
                if (button.Pressed && !isReloading)
                {
                    //shoot anime
                    playerAnimator.SetTrigger("shoot");
                    //shake camera
                    Debug.Log("Shoot!");

                    if (coolDown <= 0f)
                    {
                        if (AmmoManager.bulletToggelBool)
                        {
                            //we are using normal bullets
                            if (AmmoManager.HasBulletsInMagazin)
                            {
                                //play sound
                                aSource.PlayOneShot(fireSound);

                                coolDown = 0.4f;
                                MuzzleFlash.Play();

                                GameObject firePart = Instantiate(fireParticle, shootFrom.position, shootFrom.rotation);
                                Destroy(firePart, 6f);

                                FireNormalGun(ray);

                                AmmoManager.CurrentBulletsInMag--;
                            }
                            else
                            {
                                //we have no normal bullets in magazin --> no Ammo
                                coolDown = 0.4f;
                                aSource.PlayOneShot(noAmmoClip);
                            }
                        }
                        else
                        {
                            //we are using explosive bullets
                            if (AmmoManager.HasExplosiveBulletsInMagazin)
                            {
                                //play sound
                                aSource.PlayOneShot(fireSound);

                                coolDown = 0.6f;
                                MuzzleFlash.Play();

                                GameObject firePart = Instantiate(fireParticle, shootFrom.position, shootFrom.rotation);
                                Destroy(firePart, 6f);

                                FireExplosiveGun(ray);

                                AmmoManager.CurrentExplosiveBulletsInMag--;
                            }
                            else
                            {
                                coolDown = 0.6f;
                                aSource.PlayOneShot(noAmmoClip);
                            }
                        }

                    }
                }
                else
                {
                    if (Rigidbody.velocity.magnitude > 5)
                    {
                        //sprint
                        playerAnimator.SetBool("sprint", true);
                        speedMultiplier = 9f;

                    }
                    else if (Rigidbody.velocity.magnitude > 1.8f)
                    {
                        playerAnimator.SetBool("run", true);
                        playerAnimator.SetBool("idle", false);
                        playerAnimator.SetBool("sprint", false);

                        speedMultiplier = 5f;

                    }
                    else
                    {
                        playerAnimator.SetBool("idle", true);
                        playerAnimator.SetBool("run", false);
                        playerAnimator.SetBool("sprint", false);
                    }
                }
                #endregion

                #region Jump
                var grounded = Physics.Raycast(transform.position + Vector3.up * .1f, Vector3.down, 0.1f);
                Debug.DrawRay(transform.position + Vector3.up * .1f, Vector3.down, Color.red, 0.1f);
                Debug.Log(grounded + " <-- grounded");

                if (jumpBtn.Pressed && grounded)
                {
                    Debug.Log("jumpeed!");
                    playerAnimator.SetBool("sprint", false);
                    playerAnimator.SetTrigger("jump");
                    Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, jumpForce, Rigidbody.velocity.z);
                }


                #endregion
            }
            else
            {
                #region Camera Movement While Aiming
                //we are aiming --> TO:DO : Move Camera Position Closer
                var input = new Vector3(LeftJoystick.Horizontal, 0, LeftJoystick.Vertical);
                var vel = Quaternion.AngleAxis((CameraAngleY + 180), Vector3.up) * input * speedMultiplier_Aim;

                Rigidbody.velocity = new Vector3(vel.x, Rigidbody.velocity.y, vel.z);
                transform.rotation = Quaternion.AngleAxis(CameraAngleY + 180 + Vector3.SignedAngle(Vector3.forward, input.normalized + Vector3.forward * 0.001f, Vector3.up), Vector3.up);

                CameraAngleY += Touchfield.TouchDist.x * CameraAngleSpeed;
                CameraPosY = Mathf.Clamp(CameraPosY - Touchfield.TouchDist.y * CameraPosSpeed, 0, 5f);

                Camera.main.transform.position = transform.position + Quaternion.AngleAxis(CameraAngleY, Vector3.up) * new Vector3(0f, CameraPosY - 0.5f, 2f);

                Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2.6f - Camera.main.transform.position, Vector3.up);

                shootFrom.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2.8f - Camera.main.transform.position, Vector3.up);
                #endregion

                #region Shoot & Animations
                var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

                coolDown -= Time.deltaTime;
                if (button.Pressed && !isReloading)
                {
                    //shoot anime
                    playerAnimator.SetTrigger("shoot");
                    //shake camera
                    Debug.Log("Shoot!");

                    if (coolDown <= 0f)
                    {
                        if (AmmoManager.bulletToggelBool)
                        {
                            //we are using normal bullets
                            if (AmmoManager.HasBulletsInMagazin)
                            {
                                //play sound
                                aSource.PlayOneShot(fireSound);

                                coolDown = 0.4f;
                                MuzzleFlash.Play();

                                GameObject firePart = Instantiate(fireParticle, shootFrom.position, shootFrom.rotation);
                                Destroy(firePart, 6f);

                                FireNormalGun(ray);

                                AmmoManager.CurrentBulletsInMag--;
                            }
                            else
                            {
                                //we have no normal bullets in magazin --> no Ammo
                                coolDown = 0.4f;
                                aSource.PlayOneShot(noAmmoClip);
                            }
                        }
                        else
                        {
                            //we are using explosive bullets
                            if (AmmoManager.HasExplosiveBulletsInMagazin)
                            {
                                //play sound
                                aSource.PlayOneShot(fireSound);

                                coolDown = 0.6f;
                                MuzzleFlash.Play();

                                GameObject firePart = Instantiate(fireParticle, shootFrom.position, shootFrom.rotation);
                                Destroy(firePart, 6f);

                                FireExplosiveGun(ray);

                                AmmoManager.CurrentExplosiveBulletsInMag--;
                            }
                            else
                            {
                                coolDown = 0.6f;
                                aSource.PlayOneShot(noAmmoClip);
                            }
                        }

                    }
                }
                else
                {
                    if (Rigidbody.velocity.magnitude > 5)
                    {
                        //sprint
                        playerAnimator.SetBool("sprint", true);
                        speedMultiplier = 9f;

                    }
                    else if (Rigidbody.velocity.magnitude > 1.8f)
                    {
                        playerAnimator.SetBool("run", true);
                        playerAnimator.SetBool("idle", false);
                        playerAnimator.SetBool("sprint", false);

                        speedMultiplier = 5f;

                    }
                    else
                    {
                        playerAnimator.SetBool("idle", true);
                        playerAnimator.SetBool("run", false);
                        playerAnimator.SetBool("sprint", false);
                    }
                }
                #endregion

                #region Jump
                var grounded = Physics.Raycast(transform.position + Vector3.up * .1f, Vector3.down, 0.1f);
                Debug.DrawRay(transform.position + Vector3.up * .1f, Vector3.down, Color.red, 0.1f);
                Debug.Log(grounded + " <-- grounded");

                if (jumpBtn.Pressed && grounded)
                {
                    Debug.Log("jumpeed!");
                    playerAnimator.SetBool("sprint", false);
                    playerAnimator.SetTrigger("jump");
                    Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, jumpForce, Rigidbody.velocity.z);
                }


                #endregion
            }


        }
        else
            //  Car Camera Movement & Control
        {
            #region Movement Camera
            GameObject activeCar = GameObject.FindGameObjectWithTag("Vehicle");
            if(activeCar != null)
            {
                Camera.main.transform.position = activeCar.transform.position + activeCar.transform.rotation * new Vector3(0, 7, -10);
                Camera.main.transform.rotation = Quaternion.LookRotation(activeCar.transform.position + Vector3.up * 2f - Camera.main.transform.position, Vector3.up);
            }
            
                

            #endregion
        }


    }

    private void FireExplosiveGun(Ray ray)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            var other = hitInfo.collider.GetComponent<EnemyHealth>();
            if (other != null)
            {
                //instanciate immpact effect
                //enemyHitEffect.transform.localScale = new Vector3(enemyHitEffect.transform.localScale.x + 1, enemyHitEffect.transform.localScale.y + 1, enemyHitEffect.transform.localScale.z + 1);
                GameObject effect = Instantiate(explosionHitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Debug.Log(effect.transform.position + "  hit effect pos --> enemy");
                Destroy(effect, 3f);
                if (other.currentHealth > 0)
                {
                    other.TakeDamage(damageToTake);
                }
            }
            else
            {
                //otherHitEffect.transform.localScale = new Vector3(otherHitEffect.transform.localScale.x + 1, otherHitEffect.transform.localScale.y + 1, otherHitEffect.transform.localScale.z + 1);
                GameObject effectOther = Instantiate(explosionHitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Debug.Log(effectOther.transform.position + "  hit effect pos --> other");
                Destroy(effectOther, 3f);
            }
        }
    }

    private void FireNormalGun(Ray ray)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            Debug.Log(hitInfo.collider.name + " I hitted");

            var other = hitInfo.collider.GetComponent<EnemyHealth>();
            if (other != null)
            {
                //instanciate immpact effect
                //enemyHitEffect.transform.localScale = new Vector3(enemyHitEffect.transform.localScale.x + 1, enemyHitEffect.transform.localScale.y + 1, enemyHitEffect.transform.localScale.z + 1);
                GameObject effect = Instantiate(enemyHitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Debug.Log(effect.transform.position + "  hit effect pos --> enemy");
                Destroy(effect, 3f);
                if (other.currentHealth > 0)
                {
                    other.TakeDamage(damageToTake);
                }
            }
            else
            {
                //otherHitEffect.transform.localScale = new Vector3(otherHitEffect.transform.localScale.x + 1, otherHitEffect.transform.localScale.y + 1, otherHitEffect.transform.localScale.z + 1);
                GameObject effectOther = Instantiate(otherHitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Debug.Log(effectOther.transform.position + "  hit effect pos --> other");
                Destroy(effectOther, 3f);
            }
        }
    }

    public void AimDownSights()
    {
        if (isAiming)
        {
            //back to normal
            isAiming = false;
        }
        else { isAiming = true; }
    }




}
