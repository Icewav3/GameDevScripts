using UnityEngine;

public class LinearMover : MonoBehaviour
{
	[Tooltip(
			"Controls how the object will move." + "\n" +
			"Fixed: Moves the object the same way regardless of rotation." + "\n" +
			"Relative: moves the object relative to how it's rotated"
			)]
	public MoverType moverType = MoverType.Fixed;
	[Tooltip(
			"The direction the object should move." + "\n" +
			"A positive number in the X input means it will go to the right." + "\n" + 
			"A positive number in the Y input means it will go up."
			)]
	public Vector2 direction = new Vector2(0.0f, 0.0f);
	
	//stores the rigidbody2D component attached to this object
	private Rigidbody2D _rb;
	//keeps track of whether this object actually has a Rigidbody2D attached or not
	private bool _hasRb = true;

	//this method is called when the object is loaded
	void Awake()
	{
		//grabs any Rigidbody2D component attached to the object
		_rb = GetComponent<Rigidbody2D>();
		//makes sure there was a RigidbodyD to grab, and puts out a warning if there wasn't
		//also makes sure nothing else will run without the Rigidbody2D to prevent errors
		if (_rb == null)
		{
			_hasRb = false;
			Debug.LogWarning($"Please Attach a Rigidbody2D to {gameObject.name}!");
		}
	}
	//this method is called at a fixed interval separate from framerate
	void FixedUpdate()
	{
		//don't even try to run the rest of the code if there's no Rigidbody2D
		if (!_hasRb) return;

		//runs differen blocks of code depending on what moverType is set to
		switch (moverType)
		{
			//if moverType is set to fixed, add the direction variable as a force to the Rigidbody
			case MoverType.Fixed:
				_rb.AddForce(direction);
				break;
			//if moverType is set to relative, add the y component of direction as a force towards the object's 'up' direction
			//do the same for the x component but with the object's 'right' direction
			case MoverType.Relative:
				_rb.AddForce((Vector2)transform.up * direction.y);
				_rb.AddForce((Vector2)transform.right * direction.x);
				break;
		}
	}
}

//don't worry about it this is basically just a fancy number
//it's only here to make the UI more readable in the editor
public enum MoverType
{
	Fixed,
	Relative
}
