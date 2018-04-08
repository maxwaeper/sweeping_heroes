﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour {


	public float INITIAL_MASS = 1.0f;
	public float ACCELERATION_FROM_INPUT = 1.0f;
	public float VELOCITY_DECAY_SPEED = 1.0f;
	public float VECLOCITY_MINIMUM = 0.0001f;
	public float VELOCITY_MAXIMUM = 100.0f;
	public float ROTATION_SPEED = .5f;

	private float current_mass;
	private Vector2 current_velocity;
	private Vector2 current_acceleration;

	// Use this for initialization
	void Start () {
		current_velocity = Vector2.zero;
		current_acceleration = Vector2.zero;
		current_mass = INITIAL_MASS;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		Vector2 dir = new Vector2 (mousePos.x - transform.position.x, mousePos.y - transform.position.y);
		transform.up = Vector2.Lerp(transform.up, -dir, ROTATION_SPEED);
	}

	void FixedUpdate() {
		// get initial acceleration from input
		if (Input.GetAxisRaw ("Horizontal") != 0) {
			current_acceleration.x += ACCELERATION_FROM_INPUT * Input.GetAxisRaw ("Horizontal");
		}
		if (Input.GetAxisRaw ("Vertical") != 0) {
			current_acceleration.y += ACCELERATION_FROM_INPUT * Input.GetAxisRaw ("Vertical");
		}

		// buff acceleration from mass 
		current_mass = this.ModifyMassFromInventory();
		current_acceleration = this.ModifyFromMass(current_acceleration);
		current_acceleration = this.ModifyAccelerationFromInventory ();

		current_velocity += current_acceleration;
		current_velocity = this.ModifyVelocityFromInventory ();

		transform.position += new Vector3(current_velocity.x, current_velocity.y);
		current_acceleration = Vector2.Lerp (current_acceleration, Vector2.zero, 1.0f);
		current_velocity = Vector2.Lerp (current_velocity, Vector2.zero, 1 / current_mass);
		if (Mathf.Abs(current_velocity.x) <= VECLOCITY_MINIMUM)
			current_velocity.x = 0;
		if (Mathf.Abs(current_velocity.y) <= VECLOCITY_MINIMUM)
			current_velocity.y = 0;
	}

	Vector2 ModifyFromMass(Vector2 initial) {
		Vector2 val = initial * this.current_mass;
		return val;
	}

	float ModifyMassFromInventory() {
		return current_mass;
	}

	Vector2 ModifyAccelerationFromInventory() {
		return current_acceleration;
	}

	Vector2  ModifyVelocityFromInventory() {
		return current_velocity;
	}

	void AddToMass(float value) {
		this.current_mass += value;
	}


}
