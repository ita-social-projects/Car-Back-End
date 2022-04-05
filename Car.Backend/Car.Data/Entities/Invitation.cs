using System;
using Car.Data.Enums;

namespace Car.Data.Entities
{
    public class Invitation : IEntity, IEquatable<Invitation>
    {
        public int Id { get; set; }

        public int InvitedUserId { get; set; }

        public int JourneyId { get; set; }

        public InvitationType Type { get; set; }

        public User? InvitedUser { get; set; }

        public Journey? Journey { get; set; }

        public bool Equals(Invitation other)
        {
            if (other is null)
            {
                return false;
            }

            return this.InvitedUserId == other.InvitedUserId;
        }

        public override bool Equals(object obj) => Equals(obj as Invitation);

        public override int GetHashCode() => InvitedUserId.GetHashCode();
    }
}
