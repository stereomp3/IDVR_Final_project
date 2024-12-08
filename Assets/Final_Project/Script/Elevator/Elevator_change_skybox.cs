using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class meta_skybox_setting
{
    public Color top_color;
    public Color middle_color;
    public Color button_color;
}
public class Elevator_change_skybox : MonoBehaviour
{
    public Material material;
    Renderer rend;
    public float transition_time = 3;
    private float max_time;
    private Color tmp_top_color;
    private Color tmp_middle_color;
    private Color tmp_button_color;
    private bool is_smooth = false;

    public meta_skybox_setting b1;
    public meta_skybox_setting f1;
    public meta_skybox_setting f2;
    private meta_skybox_setting now_floor;

    // old
    // Sun(_SunDisk¡FSize)¡B Sun Size(_SunSize¡FRange)¡B Sun Size Covergence(_SunSizeCovergence¡FRange)¡BAtmosphere Thickness(_AtmosphereThickness¡FRange)Sky Tink(_SkyTink¡FColor)¡BGround(_GroundColor¡FColor)¡BExposure(_Exposure¡FRange)

    // current use
    // Top Color(_TopColor¡FColor)¡BMiddleColor(_MiddleColor¡FColor)¡BBottom Color(_BottomColor¡FColor)¡BDirection(_Direction¡FVector)¡BDither Strength(_DitherStrength¡Fint)

    // Start is called before the first frame update
    void Start()
    {
        now_floor = b1; // set init
        RenderSettings.skybox = material;
        RenderSettings.skybox.SetColor("_TopColor", b1.top_color);
        RenderSettings.skybox.SetColor("_MiddleColor", b1.middle_color);
        RenderSettings.skybox.SetColor("_BottomColor", b1.button_color);
        max_time = transition_time;
        //RenderSettings.skybox.SetFloat("_SunSize", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (is_smooth)
        {
            tmp_top_color = Color.Lerp(tmp_top_color, now_floor.top_color, Time.deltaTime / transition_time);
            tmp_middle_color = Color.Lerp(tmp_middle_color, now_floor.middle_color, Time.deltaTime / transition_time);
            tmp_button_color = Color.Lerp(tmp_button_color, now_floor.button_color, Time.deltaTime / transition_time);
            transition_time -= Time.deltaTime;
            RenderSettings.skybox.SetColor("_TopColor", tmp_top_color);
            RenderSettings.skybox.SetColor("_MiddleColor", tmp_middle_color);
            RenderSettings.skybox.SetColor("_BottomColor", tmp_button_color);
        }
        if(transition_time < 0)
        {
            transition_time = max_time;
            is_smooth = false;
        }
    }

    public void switch_to_f1()
    {
        Debug.Log("############### switch to f1");
        tmp_top_color = now_floor.top_color;
        tmp_middle_color = now_floor.middle_color;
        tmp_button_color = now_floor.button_color;

        now_floor = f1;
        is_smooth = true;
    }

    public void switch_to_b1()
    {
        Debug.Log("############### switch to b1");
        tmp_top_color = now_floor.top_color;
        tmp_middle_color = now_floor.middle_color;
        tmp_button_color = now_floor.button_color;

        now_floor = b1;
        is_smooth = true;
    }

    public void switch_to_f2()
    {
        Debug.Log("############### switch to f2");
        // pre color
        tmp_top_color = now_floor.top_color;
        tmp_middle_color = now_floor.middle_color;
        tmp_button_color = now_floor.button_color;

        now_floor = f2;
        is_smooth = true;
    }
    
}
