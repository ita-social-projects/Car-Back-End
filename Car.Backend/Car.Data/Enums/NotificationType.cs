using System;
using System.Collections.Generic;
using System.Text;

namespace Car.Data.Entities
{
    public enum NotificationType
    {
        PassengerApply = 1,
        ApplicationApproval = 2,
        JourneyCancellation = 3,
        JouneyDetailsUpdate = 4,
        JourneyInvitation = 5,
        AcceptedInviation = 6,
        RejectedInviation = 7,
        PassengerWithdrawal = 8,
        HRMarketingMessage = 9,
        HRMarketingSurvey = 10,
    }
}
