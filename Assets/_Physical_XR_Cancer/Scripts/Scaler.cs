using UnityEngine;

public class Scaler : MonoBehaviour
{
    private float START_SCALE = 0.2f;
    private float MAX_SCALE = 2f;  
    private float SCALING_SPEED = 0.2f;

    private float scaleUpdater;
    private float scalePiece;

    public ExerciseMenu exerciseMenu;

    // Start is called before the first frame update
    void Start()
    {
        scalePiece = (MAX_SCALE - START_SCALE) / MenuManager.TOTAL_REPETITIONS; //Initialize scalePiece by deviding TOTAL_REPETITIONS.
        scaleUpdater = scalePiece; //Initialize scaleUpdater with scalePiece.
        transform.localScale = new Vector3(transform.localScale.x, scaleUpdater, transform.localScale.z); //Initialize scaleY of the flower;
        //transform.localScale = new Vector3(scaleUpdater, scaleUpdater, scaleUpdater); //Initialize scaleY of the flower;
    }

    // Update is called once per frame
    void Update()
    {
        if(scaleUpdater < MAX_SCALE) // if flower scaleY is not MAX then
        {
            if (scaleUpdater < (START_SCALE + exerciseMenu.repetitions * scalePiece))
            {
                scaleUpdater += Time.deltaTime * SCALING_SPEED;
            }
            else
            {
                scaleUpdater = (START_SCALE + exerciseMenu.repetitions * scalePiece) + 0.005f;
            }

            transform.localScale = new Vector3(transform.localScale.x, scaleUpdater, transform.localScale.z);
            //transform.localScale = new Vector3(scaleUpdater, scaleUpdater, scaleUpdater);
        }
    }
}
