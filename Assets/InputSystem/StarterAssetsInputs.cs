using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class CustomInputs : MonoBehaviour
{
	[Tooltip("The scene director")]
	[SerializeField]
	private SceneDirector sceneDirector;

	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;
	public float fly;
	public bool slowdown;

	[Header("Movement Settings")]
	public bool analogMovement;

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}

	public void OnLook(InputValue value)
	{
		if(cursorInputForLook)
		{
			LookInput(value.Get<Vector2>());
		}
	}

	public void OnFly(InputValue value)
	{
		FlyInput(value.Get<float>());
	}

	public void OnSlowdown(InputValue value)
	{
		SlowDownInput(value.isPressed);
	}

	public void OnAdvanceScenario(InputValue value)
	{
		AdvanceScenarioInput(value.isPressed);
	}

	public void OnBacktrackScenario(InputValue value)
	{
		BacktrackScenarioInput(value.isPressed);
	}
#endif


	public void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;
	} 

	public void LookInput(Vector2 newLookDirection)
	{
		look = newLookDirection;
	}

	public void FlyInput(float newFlyState)
	{
		fly = newFlyState;
	}

	public void SlowDownInput(bool newSlowdownState)
	{
		slowdown = !slowdown;
	}

	public void AdvanceScenarioInput(bool newAdvanceScenarioState)
	{
		sceneDirector.Next();
	}

	public void BacktrackScenarioInput(bool newBacktrackScenarioState)
	{
		sceneDirector.Previous();
	}
	
	private void OnApplicationFocus(bool hasFocus)
	{
		SetCursorState(cursorLocked);
	}

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}
}