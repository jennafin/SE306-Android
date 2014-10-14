// Interface for classes to manage speed
public interface SpeedHandle {

	void incrementSpeed(double increment);

	void decrementSpeed(double increment);

	void reset();

	double getCurrentSpeed();

	void pause();

	void unpause();
}