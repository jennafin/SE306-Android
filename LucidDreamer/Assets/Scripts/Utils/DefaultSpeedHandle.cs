using System;

public class DefaultSpeedHandle : SpeedHandle {
	
	double currentSpeed;

	double initialSpeed;
	double minSpeed;
	double maxSpeed;

	bool paused;

	public DefaultSpeedHandle(double initialSpeed, double minSpeed, double maxSpeed) {
		this.currentSpeed = initialSpeed;
		this.initialSpeed = initialSpeed;
		this.maxSpeed = maxSpeed;
		this.minSpeed = minSpeed;
	}

	public void incrementSpeed(double increment) {
		if (! this.paused) {
			currentSpeed += increment;
			if (currentSpeed > maxSpeed) {
				currentSpeed = maxSpeed;
			}
		}
	}

	public void decrementSpeed(double increment) {
		if (! this.paused) {
			currentSpeed -= increment;
			if (currentSpeed < minSpeed) {
				currentSpeed = minSpeed;
			}
		}
	}

	public void reset() {
		currentSpeed = initialSpeed;
	}

	public double getCurrentSpeed() {
		if (this.paused) {
			return 0;
		} else {
			return currentSpeed;
		}
	}

	public void pause() {
		this.paused = true;
	}

	public void unpause() {
		this.paused = false;
	}
}

