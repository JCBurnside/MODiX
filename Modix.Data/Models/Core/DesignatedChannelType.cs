﻿namespace Modix.Data.Models.Core
{
    /// <summary>
    /// Defines the possible types designations that may be assigned to roles.
    /// </summary>
    public enum DesignatedChannelType
    {
        /// <summary>
        /// Defines a channel that logs actions performed by the moderation feature.
        /// </summary>
        ModerationLog,
        /// <summary>
        /// Defines a channel that logs modified and deleted messages.
        /// </summary>
        MessageLog,
        /// <summary>
        /// Defines a channel that logs actions performed by the promotions feature.
        /// </summary>
        PromotionLog,
        /// <summary>
        /// Defines a channel to send promotion campaign creation/closing notifications.
        /// </summary>
        PromotionNotifications,
        /// <summary>
        /// Defines a channel that is not subject to auto-moderation behaviors of the moderation feature.
        /// </summary>
        Unmoderated
    }
}
