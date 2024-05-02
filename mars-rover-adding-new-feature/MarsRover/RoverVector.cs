namespace MarsRover;

internal class RoverVector
{
    private readonly Direction _direction;
    private readonly Coordinates _coordinates;

    public RoverVector(
        Direction direction,
        Coordinates coordinates)
    {
        _direction = direction;
        _coordinates = coordinates;
    }

}