using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    private AudioSource audio;

    [SerializeField] private AudioClip coin;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip hit;

    public AudioClip Coin
    {
        get { return coin; }
        set { coin = value; }
    }

    public AudioClip Jump
    {
        get { return jump; }
        set { jump = value; }
    }

    public AudioClip Hit
    {
        get { return hit; }
        set { hit = value; }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }
}
