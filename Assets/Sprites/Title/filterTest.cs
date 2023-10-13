using UnityEngine;

public class filterTest : MonoBehaviour
{
    //Remember to assign a Texture in the Inspector window to ensure this works
    public Texture m_MyTexture;

    void Update()
    {
        //Press the space key to switch between Filter Modes
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Switch the Texture's Filter Mode
            m_MyTexture.filterMode = SwitchFilterModes();
            //Output the current Filter Mode to the console
            Debug.Log("Filter mode : " + m_MyTexture.filterMode);
        }
    }

    //Switch between Filter Modes when the user clicks the Button
    FilterMode SwitchFilterModes()
    {
        //Switch the Filter Mode of the Texture when user clicks the Button
        switch (m_MyTexture.filterMode)
        {
            //If the FilterMode is currently Bilinear, switch it to Point on the Button click
            case FilterMode.Bilinear:
                m_MyTexture.filterMode = FilterMode.Point;
                break;

            //If the FilterMode is currently Point, switch it to Trilinear on the Button click
            case FilterMode.Point:
                m_MyTexture.filterMode = FilterMode.Trilinear;
                break;

            //If the FilterMode is currently Trilinear, switch it to Bilinear on the Button click
            case FilterMode.Trilinear:
                m_MyTexture.filterMode = FilterMode.Bilinear;
                break;
        }
        //Return the new Texture FilterMode
        return m_MyTexture.filterMode;
    }
}