using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Reservoom.Models
{
    public class RoomId
    {
        public int FloorNumber { get; }
        public int RoomNumber { get; }

        public RoomId(int floorNum, int roomNum)
        {
            FloorNumber = floorNum;
            RoomNumber = roomNum;
        }

        public override string ToString()
        {
            return $"Floor {FloorNumber}, Room {RoomNumber}";
        }

        public override bool Equals(object? obj)
        {
               return obj is RoomId other && 
                FloorNumber == other.FloorNumber && 
                RoomNumber == other.RoomNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FloorNumber, RoomNumber);
        }

        public static bool operator ==(RoomId roomID1, RoomId roomID2)
        {
            if (roomID1 is null && roomID2 is null)
            {
                return true;
            }

            return roomID1?.Equals(roomID2) ?? false;
        }

        public static bool operator !=(RoomId roomID1, RoomId roomID2)
        {
            return !(roomID1 == roomID2);
        }
    }
}
