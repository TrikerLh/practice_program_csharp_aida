using System;

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

    public RoverVector RotateToRight()
    {
        var direction = _direction.RotateRight();
        return new RoverVector(direction, _coordinates);
    }

    public RoverVector RotateToLeft()
    {
        var direction = _direction.RotateLeft();
        return new RoverVector(direction, _coordinates);
    }

    public RoverVector Move(int displacement)
    {
        var newCoordinates = _direction.Move(_coordinates, displacement);
        return new RoverVector(_direction, newCoordinates);
    }

    protected bool Equals(RoverVector other)
    {
        return Equals(_direction, other._direction) && Equals(_coordinates, other._coordinates);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((RoverVector)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_direction, _coordinates);
    }

    public override string ToString()
    {
        return $"{nameof(_direction)}: {_direction}, {nameof(_coordinates)}: {_coordinates}";
    }
}